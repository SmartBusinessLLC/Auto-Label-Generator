using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace Addin
{
    public partial class LabelForm : Form
    {
        private const string NameFormat = "A_{0}_{1}";
        public const string postFix = " [AxLabelFile]";

        #region Columns
        private const int ChBoxPos = 0;
        private const int RowNumber = 1;
        private const int LbNamePos = 2;
        private const int LbTextPos = 3;
        private const int LbObjectName = 4;
        private const int LbExistsPos = 5;
        private const int LbLableFileName = 6;
        #endregion

        private List<LabelDto> _labelDtoList;
        protected LabelManager LabelManager;
        protected Dictionary<int, String> OrigValues;

        protected int RowsCount;
        protected string Logging;

        protected List<string> labelList;
        protected string elementName;
        protected List<AxLabelFile> labelFiles;

        private IDictionary<string, List<string>> labelsDictionary = new Dictionary<string, List<string>>();

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
        public List<LabelDto> LabelDtoList
        {
            get
            {
                return _labelDtoList;
            }

            set
            {
                _labelDtoList = value;
            }
        }

        public List<AxLabelFile> LabelFiles
        {
            get
            {
                return labelFiles;
            }

            set
            {
                labelFiles = value;
            }
        }

        public string ElementName
        {
            get
            {
                return elementName;
            }

            set
            {
                elementName = value;
            }
        }

        public List<string> LabelList
        {
            get
            {
                return labelList;
            }

            set
            {
                labelList = value;
            }
        }

        public LabelForm()
        {
            InitializeComponent();

            LabelDtoList = new List<LabelDto>();
            OrigValues = new Dictionary<int, String>();
        }

        public void FillGrid()
        {
            if (labelList == null) return;

            FillObjectList(elementName);

            tableLayoutPanel.RowCount += labelList.Count;


            List<string> items = labelFilesCheckedListBox.CheckedItems.Cast<string>().ToList();

            labelList = labelList.Distinct().ToList();

            foreach (string currentlabel in labelList)
            {
                foreach (var labelGroup in labelsDictionary.Keys)
                {
                    RowsCount++;
                    //
                    bool isExists = true;
                    bool isInsert = true;
                    var lText = LabelManager.GetLabel(currentlabel);
                    var lName = LabelManager.GetDefaultLabelId(lText, labelGroup);

                    if (lName == string.Empty)
                    {
                        isExists = false;
                        lName = LabelManager.GetLabelId(currentlabel);

                        if (lName == string.Empty)
                        {
                            isInsert = false;
                        }
                    }
                    OrigValues[RowsCount] = currentlabel;
                    FillRow(RowsCount, isExists, lName, lText, isInsert, elementName, labelGroup);
                }
            }

        }

        public void FillObjectList(string objectName)
        {
            if (objectName == string.Empty) return;

            int count = itemsCheckedListBox.Items.Count;
            itemsCheckedListBox.Items.Insert(count, objectName);
            itemsCheckedListBox.SetItemCheckState(count, CheckState.Checked);
        }

 

        protected void FillRow(int row, bool isExists, string lName, string lText, bool isInsert, string objectName, string labelFileName)
        {
            var cBox = CreateNewCheckBox(row, ChBoxPos, isInsert, true);
            var rowNumber = CreateNewLabel(row, RowNumber, row.ToString(), true);
            var labelName = CreateNewTextBox(row, LbNamePos, lName, !isExists);
            var labelText = CreateNewTextBox(row, LbTextPos, lText, true);
            var labelObjectNameLocal = CreateNewLabel(row, LbObjectName, objectName, false);
            var labelExists = CreateNewCheckBox(row, LbExistsPos, isExists, false);
            var lableFiles = CreateNewLabel(row, LbLableFileName, labelFileName, false);
            //
            tableLayoutPanel.Controls.Add(cBox, ChBoxPos, row);
            tableLayoutPanel.Controls.Add(rowNumber, RowNumber, row);
            tableLayoutPanel.Controls.Add(labelName, LbNamePos, row);
            tableLayoutPanel.Controls.Add(labelText, LbTextPos, row);
            tableLayoutPanel.Controls.Add(labelObjectNameLocal, LbObjectName, row);
            tableLayoutPanel.Controls.Add(labelExists, LbExistsPos, row);
            tableLayoutPanel.Controls.Add(lableFiles, LbLableFileName, row);
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            //Label groups may contain identical label files, so we add labelFilesNamesList to lately fill it with label files names without duplicates,
            //and then add them to _labelDtoList
            List<string> labelFilesNamesList = new List<string>();
            if (!ValidateLabels()) return;

            _labelDtoList.Clear();

            if (RowsCount > 0)
            {
                for (int j = 1; j <= RowsCount; j++)
                {
                    var checkBox = tableLayoutPanel.Controls[GetName(j, ChBoxPos)] as CheckBox;
                    string labelId = ((TextBox)tableLayoutPanel.Controls[GetName(j, LbNamePos)]).Text;

                    if (checkBox != null && checkBox.Checked && labelId != string.Empty)
                    {
                        string labelText = ((TextBox)tableLayoutPanel.Controls[GetName(j, LbTextPos)]).Text;
                        string objectName = ((Label)tableLayoutPanel.Controls[GetName(j, LbObjectName)]).Text;
                        bool labelExis = ((CheckBox)tableLayoutPanel.Controls[GetName(j, LbExistsPos)]).Checked;
                        string labelFileGroupName = ((Label)tableLayoutPanel.Controls[GetName(j, LbLableFileName)]).Text;

                        if (labelsDictionary[labelFileGroupName] != null)
                        {
                            foreach (string labelFileName in labelsDictionary[labelFileGroupName])
                            {
                                _labelDtoList.Add(new LabelDto(labelId, labelText, labelExis, OrigValues[j], objectName, labelFileName));
                            }
                            
                        }
                    }
                }
            }

            Close();
        }

        private bool ValidateLabels()
        {
            bool ret = true;
            Logging = string.Empty;

            if (RowsCount > 0)
            {
                for (int j = 1; j <= RowsCount; j++)
                {
                    var checkBox = tableLayoutPanel.Controls[GetName(j, ChBoxPos)] as CheckBox;
                    bool labelExis = ((CheckBox)tableLayoutPanel.Controls[GetName(j, LbExistsPos)]).Checked;

                    if (checkBox != null && checkBox.Checked && !labelExis)
                    {
                        string labelId = ((TextBox)tableLayoutPanel.Controls[GetName(j, LbNamePos)]).Text;
                        string rowNum = ((TextBox)tableLayoutPanel.Controls[GetName(j, LbTextPos)]).Text;
                        ret = ret && ValidateLabel(labelId, rowNum);
                    }
                }
            }

            if (!ret)
            {
                CoreUtility.DisplayWarning(Logging);
            }

            return ret;
        }

        private bool ValidateLabel(string labelId, string rowNum)
        {
            bool ret = true;

            if (labelId == string.Empty)
            {
                ret = false;
                Logging += $"Label #{rowNum} has empty Label Id";
                Logging += "\n";
            }
            else
            {
                if (LabelManager.ExistsDefaultLabel(labelId))
                {
                    ret = false;
                    Logging += $"Label Id \"{labelId}\" already exists in label file (Label #{rowNum})";
                    Logging += "\n";
                }
            }

            return ret;
        }

        protected CheckBox CreateNewCheckBox(int i, int j, bool isChecked, bool isEnabled)
        {
            var checkBox = new CheckBox
            {
                Name = GetName(i, j),
                Checked = isChecked,
                Enabled = isEnabled,
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)| AnchorStyles.Left)| AnchorStyles.Right
            };

            return checkBox;
        }

        protected Label CreateNewLabel(int i, int j, string text, bool isMiddleCenter)
        {
            var ret = new Label
            {
                Text = text,
                Name = GetName(i, j),
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right
            };

            if (isMiddleCenter)
            {
                ret.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }

            return ret;
        }

        protected TextBox CreateNewTextBox(int i, int j, string text, bool isEnabled)
        {
            var ret = new TextBox
            {
                Text = text,
                Name = GetName(i, j),
                MaxLength = AsParseLabel.MaxLabelIdSize,
                Enabled = isEnabled,
                Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right
            };

            ret.TextChanged += textBox_TextChanged;
            ret.Validating += textBox_Validating;

            return ret;
        }

        protected void textBox_TextChanged(object sender, EventArgs e)
        {
            var textBoxLocal = sender as TextBox;
            if (textBoxLocal == null) return;

            string befor = textBoxLocal.Text;
            string after = string.Concat(befor.Where(char.IsLetterOrDigit));

            if (befor != after)
            {
                int start = textBoxLocal.SelectionStart;
                textBoxLocal.Text = after;

                textBoxLocal.SelectionStart = start - 1;
            }
        }

        protected void textBox_Validating(object sender, CancelEventArgs e)
        {
            var textBoxLocal = sender as TextBox;

            if (textBoxLocal == null) return;

            
            if (LabelManager.ExistsDefaultLabel(textBoxLocal.Text))
            {
                CoreUtility.DisplayInfo("Lable with ID already exists");
            }
        }

        protected string GetName(int i, int j)
        {
            return string.Format(NameFormat, i, j);
        }

        public void InitLabelFilesControls(LabelManager _labelManager)
        {
            LabelManager = _labelManager;

            labelFiles = _labelManager.LabelFiles;

            if (labelFiles == null) return;

            labelFilesCheckedListBox.Items.Clear();

            int i = 0;
            foreach (var labelfile in labelFiles)
            {
                labelFilesCheckedListBox.Items.Insert(i, labelfile.Name);
                i++;
            }

            for (i = 0; i < labelFilesCheckedListBox.Items.Count; i++)
            {
                labelFilesCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }

        public LabelManager getLabelManager
        {
            get { return LabelManager; }
        }
    }
}



