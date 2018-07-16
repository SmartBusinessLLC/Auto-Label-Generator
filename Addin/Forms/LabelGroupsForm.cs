using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
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

namespace Addin
{
    public partial class LabelGroupsForm : Form
    {
        private List<string> labelsGroupsList = new List<string>();

        private IDictionary<string, List<string>> labelsDictionary                  = new Dictionary<string, List<string>>();
        private IDictionary<string, List<string>> labelsDictionaryBackUp            = new Dictionary<string, List<string>>();
        private IDictionary<string, bool> labelGroupsCheckedDictionary              = new Dictionary<string, bool>();
        private IDictionary<string, bool> labelGroupsCheckedDictionaryBackUp        = new Dictionary<string, bool>();

        private string oldGroupName;

        private string pathLabelGroupsChecked = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            LabelGlobalSettingsRes.labelFilePathPostfix, 
            LabelGlobalSettingsRes.labelsCheckedFileName);

        private string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            LabelGlobalSettingsRes.labelFilePathPostfix, 
            LabelGlobalSettingsRes.labelsFileName);

        private bool formClosing                = false;
        private bool cancelChangingSelection    = false;
        private bool justOpened                 = false;

        public List<string> LabelsGroupsList
        {
            get
            {
                return labelsGroupsList;
            }
            set
            {
                labelsGroupsList = value;
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
        /// <summary>
        /// Initialization of the form. Here we also initialize the back up dictionary for (label groups-label files) to roll back if user chooses not to save changes
        /// </summary>
        public LabelGroupsForm()
        {
            InitializeComponent();
            //Need this to remove last blank row from dataGridView. We have a button to add rows.
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            initializeDataGridView();
            initializeLabelsDictionaryBackUp();
        }
        /// <summary>
        /// Initialization of dataGridView1. Here we read from Json files (or create empty Json files if no files exist), which represent DataDictionary for groups and labels inside groups, and DataDictionary for active groups.
        /// </summary>
        private void initializeDataGridView()
        {
            JObject LFNUSJArray;

            if (!File.Exists(path))
            {
                System.IO.File.WriteAllText(path, "{}");
            }
            using (StreamReader file        = File.OpenText(path))
            using (JsonTextReader reader    = new JsonTextReader(file))
            {
                if (file.ToString() != "")
                {
                    LFNUSJArray = (JObject)JToken.ReadFrom(reader);
                    string str1 = LFNUSJArray.ToString();
                    LabelsDictionary = JsonConvert.DeserializeObject<IDictionary<string, List<string>>>(str1);
                }
            }

            if (!File.Exists(pathLabelGroupsChecked))
            {
                System.IO.File.WriteAllText(pathLabelGroupsChecked, "{}");
            }

            using (StreamReader file        = File.OpenText(pathLabelGroupsChecked))
            using (JsonTextReader reader    = new JsonTextReader(file))
            {
                if (file.ToString() != "")
                {
                    LFNUSJArray = (JObject)JToken.ReadFrom(reader);
                    string str1 = LFNUSJArray.ToString();
                    labelGroupsCheckedDictionary = JsonConvert.DeserializeObject<IDictionary<string, bool>>(str1);
                }
            }

            List<string> keyList = new List<string>(LabelsDictionary.Keys);

            updateDataGridView1DataSource(keyList);

            List<string> valueList = new List<string>();

            if (keyList.Count > 0)
            {
                labelsDictionary.TryGetValue(keyList[0], out valueList);
            }

            updateDataGridView2DataSource(valueList);

            updateDataGridView1CheckedValues();
        }
        /// <summary>
        /// Initialization of Labels groups dictionary back up to rollback if user don't want to save changes in files when closing this form.
        /// </summary>
        public void initializeLabelsDictionaryBackUp()
        {
            foreach (KeyValuePair<string, List<string>> keyValuePair in labelsDictionary)
            {
                labelsDictionaryBackUp.Add(keyValuePair.Key.ToString(), keyValuePair.Value.ToList<string>());
            }

            foreach (KeyValuePair<string, bool> keyValuePair in labelGroupsCheckedDictionary)
            {
                labelGroupsCheckedDictionaryBackUp.Add(keyValuePair.Key.ToString(), (bool)keyValuePair.Value);
            }
        }
        /// <summary>
        /// Here we update values of checkboxes
        /// </summary>
        public void updateDataGridView1CheckedValues()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool checkedValue = false;

                if (row.Cells[1].Value != null)
                {
                    labelGroupsCheckedDictionary.TryGetValue(row.Cells[1].Value.ToString(), out checkedValue);
                }

                row.Cells[0].Value = checkedValue;
            }
        }

        /// <summary>
        /// Here we update checked label groups dictionary json file
        /// </summary>
        public void updatelabelGroupsCheckedDictionary()
        {
            labelGroupsCheckedDictionary.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    labelGroupsCheckedDictionary.Add(row.Cells[1].Value.ToString(), (bool)row.Cells[0].Value);
                }
            }

            updateLableCheckedDictionaryFile();
        }

        /// <summary>
        /// Method to do actual work of updating a file
        /// </summary>
        public void updateLableCheckedDictionaryFile()
        {
            try
            {
                System.IO.File.WriteAllText(pathLabelGroupsChecked, string.Empty);
            }
            catch
            {
                System.IO.File.Create(pathLabelGroupsChecked);
            }

            string json = JsonConvert.SerializeObject(labelGroupsCheckedDictionary);

            //write string to file
            System.IO.File.WriteAllText(pathLabelGroupsChecked, json);
        }

        /// <summary>
        /// Here we update label groups dictionary
        /// </summary>
        /// <param name="_GroupName">Name of the group</param>
        /// <param name="_labelName">List of label files names which group will contain</param>
        private void updateDictionary(
            string _GroupName, 
            List<string> _labelName = null)
        {
            KeyValuePair<string, List<string>> keyValuePair = new KeyValuePair<string, List<string>>(_GroupName, _labelName);

            if (_labelName == null && labelsDictionary.ContainsKey(_GroupName))
            {
                if (labelsDictionary[_GroupName] != null)
                {
                    labelsDictionary[_GroupName].Clear();
                }
            }
            else if (_labelName == null && !labelsDictionary.ContainsKey(_GroupName))
            {
                labelsDictionary.Add(_GroupName, _labelName);
            }
            else if (_labelName != null && labelsDictionary.ContainsKey(_GroupName))
            {
                if (LabelsDictionary[_GroupName] != null &&
                        !Enumerable.SequenceEqual(
                         LabelsDictionary[_GroupName].OrderBy(t => t),
                         _labelName.OrderBy(t => t)))
                {
                    foreach (string labelFileName in _labelName)
                    {
                        if (!labelsDictionary[_GroupName].Contains(labelFileName))
                        {
                            labelsDictionary[_GroupName].Add(labelFileName);
                        }
                    }
                }
                else if (labelsDictionary[_GroupName] == null)
                {
                    labelsDictionary[_GroupName] = _labelName;
                }
            }
            else if (_labelName != null && !labelsDictionary.ContainsKey(_GroupName))
            {
                labelsDictionary.Add(_GroupName, _labelName);
            }

            updateLableDictionaryFile();
        }
        private void updateDataGridView2DataSource(List<string> _labelFileNames)
        {
            dataGridView2.Rows.Clear();
            if (_labelFileNames != null)
            {
                _labelFileNames.Sort();
                foreach (string labelFileName in _labelFileNames)
                {
                    DataGridViewRow dataGridViewRow = new DataGridViewRow();
                    dataGridView2.Rows.Insert(0, labelFileName);
                }
            }
        }
        private void updateDataGridView1DataSource(List<string> _labelGroupNames)
        {
            dataGridView1.Rows.Clear();
            if (_labelGroupNames != null)
            {
                _labelGroupNames.Sort();
                foreach (string labelFileName in _labelGroupNames)
                {
                    dataGridView1.Rows.Add(false, labelFileName);
                }
            }
        }

        /// <summary>
        /// Method which does actual job of updating label groups dictionary file
        /// </summary>
        public void updateLableDictionaryFile()
        {
            try
            {
                System.IO.File.WriteAllText(path, string.Empty);
            }
            catch
            {
                System.IO.File.Create(path);
            }

            string json = JsonConvert.SerializeObject(labelsDictionary);

            //write string to file
            System.IO.File.WriteAllText(path, json);
        }

        /// <summary>
        /// Add group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0 && dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value == null)
            {
                MessageBox.Show(
                                "You need to enter group name before continue",
                                "Enter group name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                cancelChangingSelection = true;
            }
            else
            {
                addNewRow();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];

                dataGridView2.Rows.Clear();
            }
        }

        /// <summary>
        /// Add label files to group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddLabels_Click(object sender, EventArgs e)
        {
            LabelsFileNamesForm labelFileNamesForm = new LabelsFileNamesForm();

            labelFileNamesForm.ShowDialog();

            /*bool update = */addToDataGridView2DataSource(labelFileNamesForm.CheckedLabelFileNames);

            //if (update)
            //{
                updateDictionary(getCurrentSelectedLabelGroup(), labelFileNamesForm.CheckedLabelFileNames);
            //}

            dataGridView1.CurrentRow.Cells[0].Value = false;
        }

        /// <summary>
        /// Remove label files from group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveLabelFiles_Click(object sender, EventArgs e)
        {
            List<string> labelsFileNamesList = new List<string>();
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if (item.Cells[0].Value != null)
                {
                    labelsFileNamesList.Add(item.Cells[0].Value.ToString());
                }
            }

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.RemoveAt(row.Index);
                if (row.Cells[0].Value != null)
                {
                    labelsFileNamesList.Remove(row.Cells[0].Value.ToString());
                }
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[1].Value != null)
                {
                    string groupName = row.Cells[1].Value.ToString();

                    labelsDictionary.Remove(groupName);

                    updateDictionary(groupName, labelsFileNamesList);
                }
            }
        }
        public void addNewRow()
        {
            dataGridView1.Rows.Add();
        }

        public string getCurrentSelectedLabelGroup()
        {
            string groupName = null;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[1].Value != null)
                {
                    groupName = row.Cells[1].Value.ToString();
                }
            }

            return groupName;
        }
        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = cancelChangingSelection;
        }
        protected static IMetaModelService CurrentModel()
        {
            IMetaModelProviders metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            IMetaModelService metaModelService = metaModelProviders.CurrentMetaModelService;

            return metaModelService;
        }
        private bool addToDataGridView2DataSource(List<string> _labelFileNames)
        {
            bool duplicated = false;
            bool differentLabelFilesId = false;
            List<string> warning = new List<string>();
            if (_labelFileNames != null)
            {
                foreach (string labelFileName in _labelFileNames)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == labelFileName)
                        {
                            duplicated = true;
                            warning.Add(labelFileName);
                        }

                        if (row.Cells[0].Value != null && !row.Cells[0].Value.ToString().Contains(CurrentModel().GetLabelFile(labelFileName).LabelFileId))
                        {
                            differentLabelFilesId = true;
                        }
                    }
                    if (!duplicated && !differentLabelFilesId)
                    {
                        DataGridViewRow dataGridViewRow = new DataGridViewRow();
                        dataGridView2.Rows.Insert(0, labelFileName);
                    }

                    duplicated = false;
                }
            }

            if (warning.Count > 0)
            {
                string warningMessage = "These label files are already added:" + System.Environment.NewLine;
                foreach (string warningItem in warning)
                {
                    warningMessage += warningItem + System.Environment.NewLine;
                }

                MessageBox.Show(
                    warningMessage,
                    "Warning: duplicates",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            if (differentLabelFilesId)
            {
                MessageBox.Show(
                    "You must add label files with the same ID",
                    "Warning: different label files ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return duplicated && differentLabelFilesId;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var cellValue = dataGridView1.CurrentRow.Cells[1].Value;
            if (cellValue != null)
            {
                List<string> labelsList = new List<string>();
                labelsDictionary.TryGetValue(cellValue.ToString(), out labelsList);
                updateDataGridView2DataSource(labelsList);
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }
        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            cancelChangingSelection = false;
            if (e.ColumnIndex == 1 && dataGridView1.CurrentCell.Value != null)
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (row.Index == this.dataGridView1.CurrentCell.RowIndex)
                    {
                        continue;
                    }
                    if (this.dataGridView1.CurrentCell.Value == null)
                    {
                        continue;
                    }
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == dataGridView1.CurrentCell.Value.ToString())
                    {
                        MessageBox.Show(
                                "Group name can't be duplicate",
                                "Duplicate group name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        dataGridView1.CurrentCell.Value = null;
                        cancelChangingSelection = true;
                    }
                    else
                    {
                        if (row.Cells[1].Value != null && !labelsDictionary.ContainsKey(row.Cells[1].Value.ToString()))
                        {
                            dataGridView2.Rows.Clear();
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!justOpened)
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        string selectedGroup = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if (!labelsDictionary.ContainsKey(selectedGroup) && (oldGroupName != null && labelsDictionary.ContainsKey(oldGroupName)))
                        {
                            List<string> oldLabelNames = labelsDictionary[oldGroupName];
                            labelsDictionary.Remove(oldGroupName);
                            updateDictionary(selectedGroup, oldLabelNames);
                        }
                        else if (!labelsDictionary.ContainsKey(selectedGroup) && oldGroupName == null)
                        {
                            updateDictionary(selectedGroup);
                        }
                    }
                }
            }
            else if (justOpened)
            {
                justOpened = false;
            }
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!justOpened)
            {
                oldGroupName = getCurrentSelectedLabelGroup();
            }
        }

        /// <summary>
        /// Here we prevent creating a group with duplicate name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                justOpened = true;
            }
            else if (justOpened == true)
            {
                justOpened = false;
            }

            if(e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 && !justOpened)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        if (dataGridView1.CurrentRow.Cells[0].Value != null && (bool)dataGridView1.CurrentRow.Cells[0].Value == true)
                        {
                            List<string> labelGroupNameList = new List<string>();

                            string info = null;

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if(row.Index != dataGridView1.CurrentRow.Index)
                                {
                                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                                    {
                                        if (labelsDictionary[row.Cells[1].Value.ToString()] != null)
                                        {
                                            foreach (string labelFileName in labelsDictionary[row.Cells[1].Value.ToString()])
                                            {
                                                if (labelsDictionary[dataGridView1.CurrentRow.Cells[1].Value.ToString()] != null)
                                                {
                                                    foreach (string labelFileNameInCurrentSelectedGroup in labelsDictionary[dataGridView1.CurrentRow.Cells[1].Value.ToString()])
                                                    {
                                                        if (labelFileNameInCurrentSelectedGroup == labelFileName)
                                                        {
                                                            info += row.Cells[1].Value.ToString() + System.Environment.NewLine;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (info != null)
                            {
                                MessageBox.Show("Current selected group has duplicate label files with next group:" + System.Environment.NewLine + info, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.CurrentRow.Cells[0].Value = false;
                                cancelChangingSelection = true;
                            }
                            else
                            {
                                cancelChangingSelection = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Delete group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[1].Value != null)
                {
                    labelsDictionary.Remove(row.Cells[1].Value.ToString());
                }
            }

            updateDataGridView1DataSource(labelsDictionary.Keys.ToList());

            updateLableDictionaryFile();
        }

        /// <summary>
        /// Here we check if changes were made on cancel click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (checkIfFormWasChanged())
            {
                DialogResult dialogResult = MessageBox.Show(
                                "Do you want to save your changes?",
                                "Changes not saved",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Error);

                switch (dialogResult)
                {
                    case DialogResult.None:
                        break;
                    case DialogResult.Cancel:
                        formClosing = true;
                        break;
                    case DialogResult.Yes:
                        updatelabelGroupsCheckedDictionary();
                        formClosing = true;
                        Close();
                        break;
                    case DialogResult.No:
                        labelsDictionary                = labelsDictionaryBackUp;
                        labelGroupsCheckedDictionary    = labelGroupsCheckedDictionaryBackUp;
                        updateLableDictionaryFile();
                        formClosing = true;
                        Close();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Save made changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            updatelabelGroupsCheckedDictionary();
            formClosing = true;
            Close();
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                buttonAddLabels.Enabled = false;
                buttonRemoveLabelFiles.Enabled = false;
            }
            else
            {
                if (buttonAddLabels.Enabled == false && buttonRemoveLabelFiles.Enabled == false)
                {
                    buttonAddLabels.Enabled = true;
                    buttonRemoveLabelFiles.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Here we check if changes were made on form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelGroupsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formClosing)
            {
                if (checkIfFormWasChanged())
                {
                    DialogResult dialogResult = MessageBox.Show(
                                    "Do you want to save your changes?",
                                    "Changes not saved",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Error);

                    switch (dialogResult)
                    {
                        case DialogResult.None:
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                        case DialogResult.Yes:
                            updatelabelGroupsCheckedDictionary();
                            break;
                        case DialogResult.No:
                            labelsDictionary                = labelsDictionaryBackUp;
                            labelGroupsCheckedDictionary    = labelGroupsCheckedDictionaryBackUp;
                            updateLableDictionaryFile();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool checkGroups()
        {
            bool changed = false;

            foreach (KeyValuePair<string, List<string>> dictElement in labelsDictionary)
            {
                if (!labelsDictionaryBackUp.Keys.Contains(dictElement.Key))
                {
                    changed = true;
                }
            }

            if (!changed)
            {
                foreach (KeyValuePair<string, List<string>> dictElement in labelsDictionaryBackUp)
                {
                    if (!labelsDictionary.Keys.Contains(dictElement.Key))
                    {
                        changed = true;
                    }
                }
            }

            if (!changed)
            {
                foreach (KeyValuePair<string, List<string>> dictElement in labelsDictionary)
                {
                    if (dictElement.Value == null)
                    {
                        if (labelsDictionaryBackUp[dictElement.Key] != null)
                        {
                            changed = true;
                        }
                    }
                    else
                    {
                        foreach (string item in dictElement.Value)
                        {
                            if (!labelsDictionaryBackUp[dictElement.Key].Contains(item))
                            {
                                changed = true;
                            }
                        }
                    }
                }

                if (!changed)
                {
                    foreach (KeyValuePair<string, List<string>> dictElement in labelsDictionaryBackUp)
                    {
                        if (dictElement.Value == null)
                        {
                            if (labelsDictionary[dictElement.Key] != null)
                            {
                                changed = true;
                            }
                        }
                        else
                        {
                            foreach (string item in dictElement.Value)
                            {
                                if (!labelsDictionary[dictElement.Key].Contains(item))
                                {
                                    changed = true;
                                }
                            }
                        }
                    }
                }
            }

            return changed;
        }

        public bool checkCheckBoxes()
        {
            bool changed = false;

            int i = 0;

            updatelabelGroupsCheckedDictionary();

            foreach (KeyValuePair<string, bool> item in labelGroupsCheckedDictionaryBackUp)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() != item.Value.ToString())
                {
                    changed = true;
                }

                i++;
            }

            return changed;
        }

        private bool checkIfFormWasChanged()
        {
            bool changed = false;

            changed = checkGroups();

            if (!changed)
            {
                changed = checkCheckBoxes();
            }

            return changed;
        }
    }
}