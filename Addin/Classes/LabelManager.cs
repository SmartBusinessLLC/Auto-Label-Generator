#region

using System.Collections.Generic;
using System.IO;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace Addin
{
    public class LabelManager
    {
        protected const string Prefix = "@@@";
        protected const char Spliter = '=';
        //protected const string LabelFileId = "SBMod";
        //protected const string LabelFileId = "SBReportsPBI";
        //protected const string LabelFileId = "K3Mod";                     // TODO: change to your label file name
        protected string LabelFileId = "BON";
        //public const string DefaultLabelFile = LabelFileId + "_en-US";
        public const string postFix = " [AxLabelFiles]";

        private readonly Dictionary<string, string> _labelContents;
        private readonly List<AxLabelFile> _labelFiles;
        private IDictionary<string, List<string>> labelsDictionary = new Dictionary<string, List<string>>();
        private IDictionary<string, bool> checkedLabelGroupsDictionary = new Dictionary<string, bool>();

        public LabelManager()
        {
            Logging = new Logging();
            _labelFiles = new List<AxLabelFile>();
            _labelContents = new Dictionary<string, string>();

            // Initialize label files (each languages)
            // Most likely there is a better way to get the AxLabelFile elements.
            // This works only if the label files are in the same model than the other elements.
            // Use CreateLabels.model(modelname) otherwise.
            //_labelFiles.Add(CurrentModel().GetLabelFile(DefaultLabelFile));
            //StreamReader reader = new StreamReader(CurrentModel().GetLabelFile(DefaultLabelFile).LocalPath());
            //string content = reader.ReadToEnd();

            //reader.Close();

            //_labelContents[CurrentModel().GetLabelFile(DefaultLabelFile).Name] = content;
            //_labelFiles.Add(CurrentModel().GetLabelFile($"{LabelFileId}_en-GB"));
            //_labelFiles.Add(CurrentModel().GetLabelFile($"{LABELFILEID}_{"en-US"}"));
            //_labelFiles.Add(CurrentModel().GetLabelFile($"{LabelFileId}_{"ru"}"));

            //LabelFilesForm labelFilesForm = new LabelFilesForm();

            //ModelInfo model = CurrentModel().GetModel("SBMod");

            //if (_labelFiles.Count == 0)
            //{
            //    IList<string> modelNames = CurrentModel().GetModelNames();
            //    IList<string> labelFileNames = CurrentModel().GetLabelFileNames();
            //    IEnumerator<string> labelFileNamesEnumerator = labelFileNames.GetEnumerator();

            //    while (labelFileNamesEnumerator.MoveNext())
            //    {
            //        string labelFile = labelFileNamesEnumerator.Current;

            //        labelFilesForm.FillObjectList(labelFile);
            //    }

            //    labelFilesForm.ShowDialog();

            //    if (labelFilesForm.DialogResult == DialogResult.OK)
            //    {
            //        IEnumerator<string> checkedLabelFilesEnumerator = labelFilesForm.CheckedLabelFiles.GetEnumerator();

            //        while (checkedLabelFilesEnumerator.MoveNext())
            //        {
            //            string labelFile = checkedLabelFilesEnumerator.Current;
            //            _labelFiles.Add(CurrentModel().GetLabelFile(labelFile));
            //        }
            //    }
            //}
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LabelGlobalSettingsRes.labelFilePathPostfix, LabelGlobalSettingsRes.labelsFileName);
            string pathToChecked = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LabelGlobalSettingsRes.labelFilePathPostfix, LabelGlobalSettingsRes.labelsCheckedFileName);
            JObject LFNUSJArray;
            

            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                if (file.ToString() != "")
                {
                    LFNUSJArray = (JObject)JToken.ReadFrom(reader);
                    string str1 = LFNUSJArray.ToString();
                    labelsDictionary = JsonConvert.DeserializeObject<IDictionary<string, List<string>>>(str1);
                }
            }

            using (StreamReader file = File.OpenText(pathToChecked))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                if (file.ToString() != "")
                {
                    LFNUSJArray = (JObject)JToken.ReadFrom(reader);
                    string str1 = LFNUSJArray.ToString();
                    checkedLabelGroupsDictionary = JsonConvert.DeserializeObject<IDictionary<string, bool>>(str1);
                }
            }

            foreach (string key in labelsDictionary.Keys)
            {
                if (checkedLabelGroupsDictionary.ContainsKey(key) && checkedLabelGroupsDictionary[key] != false)
                {
                    if (labelsDictionary[key] != null)
                    {
                        foreach (string labelFileName in labelsDictionary[key])
                        {
                            if (!_labelFiles.Contains(CurrentModel().GetLabelFile(labelFileName)))
                            {
                                _labelFiles.Add(CurrentModel().GetLabelFile(labelFileName));
                            }
                        }
                    }
                }
            }

            foreach (AxLabelFile labelFile in _labelFiles)
            {
                LabelFileId = labelFile.LabelFileId;
                break;
            }

            InitContents();
        }

        private Logging Logging { get; }

        public List<AxLabelFile> LabelFiles
        {
            get
            {
                return _labelFiles;
            }
        }

        public IDictionary<string, List<string>> LabelsDictionary
        {
            get
            {
                return labelsDictionary;
            }
            set
            {
                labelsDictionary = value;
            }
        }

        public IDictionary<string, bool> CheckedLabelGroupsDictionary
        {
            get
            {
                return checkedLabelGroupsDictionary;
            }
            set
            {
                checkedLabelGroupsDictionary = value;
            }
        }

        private void InitContents()
        {
            foreach (var labelfile in LabelFiles)
            {
                StreamReader reader = new StreamReader(labelfile.LocalPath());
                string content = reader.ReadToEnd();

                reader.Close();

                _labelContents[labelfile.Name] = content;
            }
        }

        /// <summary>
        ///     Checks if the label should be created, based on the prefix @@@
        /// </summary>
        /// <param name="propertyText">Label string</param>
        /// <returns>true/false</returns>
        public static bool IsPrefixed(string propertyText)
        {
            return propertyText.StartsWith(Prefix);
        }

        /// <summary>
        ///     If property text is prefixed, then create a new label. Otherwise, return the very same property text value
        /// </summary>
        /// <param name="propertyText">Label text from element property (label, help text, caption, etc)</param>
        /// <returns>The new label id created</returns>
        public string CreateLabel(string propertyText)
        {
            string ret = propertyText;

            if (IsPrefixed(propertyText))
            {
                string labelId = GetLabelId(propertyText);
                string label = GetLabel(propertyText);

                foreach (AxLabelFile labelfile in LabelFiles)
                {
                    if (ExistLabel(label, labelfile.Name))
                    {
                        labelId = GetLabelId(label, labelfile.Name);
                        break;
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(labelfile.LocalPath(), true);

                        writer.WriteLine($"{labelId}={label}");
                        writer.Close();

                        Log(labelId, label, labelfile.Name);
                        Add2LocalContent(labelId, labelfile.Name);
                    }
                }

                ret = $"@{LabelFileId}:{labelId}";
            }

            return ret;
        }

        public string FindOrCreateLabel(LabelDto lDto)
        {
            string ret = string.Empty;

            if (lDto != null)
            {
                string labelId = lDto.LabelId;
                string label = lDto.Label;
                
                if (ExistLabel(label, lDto.LabelFileName))
                {
                    labelId = GetLabelId(label, lDto.LabelFileName);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(CurrentModel().GetLabelFile(lDto.LabelFileName).LocalPath(), true);

                    writer.WriteLine($"{labelId}={label}");
                    writer.Close();

                    Log(labelId, label, lDto.LabelFileName);
                    Add2LocalContent($"{labelId}={label}", lDto.LabelFileName);
                }

                ret = $"@{LabelFileId}:{labelId}";
            }

            return ret;
        }

        /// <summary>
        ///     Add log message
        /// </summary>
        /// <param name="labelId">Label id</param>
        /// <param name="label">Label text</param>
        /// <param name="labelFileName">Label file name</param>
        private void Log(string labelId, string label, string labelFileName)
        {
            Log singleLog = new Log
            {
                labelId = labelId,
                label = label,
                labelFile = labelFileName
            };


            Logging.Add(singleLog);
        }

        /// <summary>
        ///     Add the recently create label id to local label content
        /// </summary>
        /// <param name="labelId">Label id</param>
        /// <param name="labelFileName">Label file name</param>
        /// <remarks>The ideia is to simulate the creating of a new label into the file, w/o modifing the file for real.</remarks>
        private void Add2LocalContent(string labelContent, string labelFileName)
        {
            if (!Exist(labelContent, LabelFiles))
                _labelContents[labelFileName] += labelContent + Environment.NewLine;
        }

        /// <summary>
        ///     Check if the label id already exist on label file
        /// </summary>
        /// <param name="labelId">Label id to check</param>
        /// <param name="labelFileName">Label file name</param>
        /// <returns>True if label id exists on local content variable</returns>
        /// <remarks>This method is public to make it available to test project</remarks>
        public bool Exist(string labelId, List<AxLabelFile> _labelFiles)
        {
            bool ret = false;
            IEnumerator<AxLabelFile> labelFilesEnumerator = _labelFiles.GetEnumerator();
            while (labelFilesEnumerator.MoveNext())
            {
                ret = _labelContents[labelFilesEnumerator.Current.Name].Contains($"{labelId}=");
            }

            return ret;
        }

        public bool ExistLabel(string label, string labelFileName)
        {
            bool ret = false;

            if (labelsDictionary.ContainsKey(labelFileName))
            {
                if (labelsDictionary[labelFileName] != null)
                {
                    foreach (string labelsFileName in labelsDictionary[labelFileName])
                    {
                        ret = _labelContents[labelsFileName].Contains($"={label}");
                        if (!ret)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                ret = _labelContents[labelFileName].Contains($"={label}");
            }

            return ret;
        }

        public bool ExistsDefaultLabel(string labelId)
        {
            return Exist(labelId, LabelFiles);
        }

        public string GetDefaultLabelId(string label, string labelFileName)
        {
            return GetLabelId(label, labelFileName);
        }

        public string GetLabelId(string label, string labelFileName)
        {
            string ret = string.Empty;

            if (ExistLabel(label, labelFileName))
            {
                var i = 1;

                try
                {
                    foreach (string labelFN in labelsDictionary[labelFileName])
                    {
                        string curLine = GetLine(_labelContents[labelFN], i);

                        while (curLine != null)
                        {
                            if (label == GetLabel(curLine))
                            {
                                ret = curLine.Split(Spliter).Length > 1 ? curLine.Split(Spliter)[0] : curLine;

                                break;
                            }
                            i++;
                            curLine = GetLine(_labelContents[labelFN], i);
                        }
                    }
                }
                catch
                {
                    string curLine = GetLine(_labelContents[labelFileName], i);

                    while (curLine != null)
                    {
                        if (label == GetLabel(curLine))
                        {
                            ret = curLine.Split(Spliter).Length > 1 ? curLine.Split(Spliter)[0] : curLine;

                            break;
                        }
                        i++;
                        curLine = GetLine(_labelContents[labelFileName], i);
                    }
                }
            }

            return ret;
        }

        protected string GetLine(string text, int lineNo)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }

        /// <summary>
        ///     Extracts the label id from label string property
        /// </summary>
        /// <param name="propertyText">Label string e.g. @@@MyNewLabelId=My new label id</param>
        /// <returns>The label string e.g. MyNewLabelId</returns>
        public static string GetLabelId(string propertyText)
        {
            if (IsPrefixed(propertyText))
            {
                propertyText = propertyText.Substring(Prefix.Length); 
            }

            string ret = propertyText.Split(Spliter).Length > 1 ? AsParseLabel.CreateLabelIdString(propertyText.Split(Spliter)[0], false) : AsParseLabel.CreateLabelIdString(propertyText, true);

            return ret;
        }

        /// <summary>
        ///     Extracts the label from label string property
        /// </summary>
        /// <param name="propertyText">Label string e.g. @@@MyNewLabelId=My new label id</param>
        /// <returns>The label string e.g. My label id</returns>
        public static string GetLabel(string propertyText)
        {
            if (IsPrefixed(propertyText))
            {
                propertyText = propertyText.Substring(Prefix.Length);
            }
 
            var ret = propertyText.Split(Spliter).Length > 1 ? propertyText.Split(Spliter)[1] : propertyText;

            return ret;
        }

        /// <summary>
        ///     Get formated log message
        /// </summary>
        /// <returns>Log message</returns>
        public string GetLoggingMessage()
        {
            return Logging.GetLogging();
        }

        /// <summary>
        ///     Used to find metadata on current model
        /// </summary>
        /// <returns>MetaModelService instance</returns>
        /// <remarks>For sure there is a better way to do it</remarks>
        protected static IMetaModelService CurrentModel()
        {
            IMetaModelProviders metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            IMetaModelService metaModelService = metaModelProviders.CurrentMetaModelService;

            return metaModelService;
        }

        /// <summary>
        ///     Used to find metadata on model (by model name)
        /// </summary>
        /// <param name="modelName">Model name</param>
        /// <returns>MetaModelService instance</returns>
        /// ///
        /// <remarks>For sure there is a better way to do it</remarks>
        protected static IMetaModelService Model(string modelName)
        {
            IMetaModelProviders metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            IMetaModelService metaModelService = metaModelProviders.GetMetaModelService(modelName);

            return metaModelService;
        }

        public static string AddQuotesPrefix(string text, bool addPrefix)
        {
            string ret = "\"";

            if (addPrefix)
            {
                ret += Prefix;
            }

            ret += text;
            ret += "\"";

            return ret;
        }
    }
}