using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

namespace Addin
{
    public partial class LabelsFileNamesForm : Form
    {
        private List<string> fileLabelNamesList = new List<string>();
        private Dictionary<string, string> _labelContents;
        private List<AxLabelFile> _labelFiles;
        private IList<string> labelFileNames = new List<string>();
        private List<string> checkedLabelFileNames = new List<string>();
        public List<string> CheckedLabelFileNames
        {
            get
            {
                return checkedLabelFileNames;
            }
            set
            {
                checkedLabelFileNames = value;
            }
        }
        public LabelsFileNamesForm()
        {
            InitializeComponent();
            InitializeSourceLabelFiles();
        }

        public void InitializeSourceLabelFiles()
        {
            _labelFiles = new List<AxLabelFile>();
            _labelContents = new Dictionary<string, string>();

            LabelFilesForm labelFilesForm = new LabelFilesForm();

            IList<string> modelNames = CurrentModel().GetModelNames();
            labelFileNames = CurrentModel().GetLabelFileNames();

            int i = 0;
            foreach (var labelFile in labelFileNames)
            {
                CheckedLabelFiles.Items.Insert(i, labelFile);
                i++;
            }
        }

        protected static IMetaModelService CurrentModel()
        {
            IMetaModelProviders metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            IMetaModelService metaModelService = metaModelProviders.CurrentMetaModelService;

            return metaModelService;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            searchText();
        }

        private void searchText()
        {
            List<string> foundLabelFileNames = new List<string>();
            List<string> checkedLabelFileNamesTmp = new List<string>();


            string text = textBox1.Text;
            foreach (string labelName in labelFileNames)
            {
                if (labelName.ToLower().Contains(text.ToLower()))
                {
                    foundLabelFileNames.Add(labelName);
                }
            }

            foundLabelFileNames.Sort();

            //foreach (string item in CheckedLabelFiles.CheckedItems)
            //{
            //    checkedLabelFileNames.Add(item);
            //}

            CheckedLabelFiles.Items.Clear();

            int i = 0;

            //This part is needed because in next foreach construction we are going throug checkedLabelFileNames and call method
            //SetItemChecked, which calls event CheckedLabelFiles_ItemCheck, which changes checkedLabelFileNames collection and it causes
            //runtime error (you can not go through collection and change its values)
            foreach (string item in checkedLabelFileNames)
            {
                checkedLabelFileNamesTmp.Add(item);
            }

            foreach (var labelFile in foundLabelFileNames)
            {
                CheckedLabelFiles.Items.Insert(i, labelFile);
                foreach (string checkedLabelFile in checkedLabelFileNamesTmp)
                {
                    if (checkedLabelFile == labelFile)
                    {
                        CheckedLabelFiles.SetItemChecked(i, true);
                    }
                }
                i++;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchText();
            }
        }

        private void CheckedLabelFiles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemName = CheckedLabelFiles.Items[e.Index].ToString();

            if (e.CurrentValue == CheckState.Unchecked && !checkedLabelFileNames.Contains(itemName))
            {
                checkedLabelFileNames.Add(itemName);
            }
            else if(e.CurrentValue == CheckState.Checked && checkedLabelFileNames.Contains(itemName))
            {
                checkedLabelFileNames.Remove(CheckedLabelFiles.Items[e.Index].ToString());
            }
        }

        public int getItemIndexByName(string _itemName)
        {
            int index = 0;

            foreach (Object item in CheckedLabelFiles.Items)
            {
                if (item.ToString() == _itemName)
                {
                    index = CheckedLabelFiles.Items.IndexOf(item);
                }
            }

            return index;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
