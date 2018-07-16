using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Addin
{
    public partial class LabelFilesForm : Form
    {
        protected bool usedByLabelManager = false;
        public LabelFilesForm()
        {
            InitializeComponent();
        }

        public void FillObjectList(string objectName)
        {
            if (objectName == string.Empty) return;

            int count = checkedLabelFiles.Items.Count;
            checkedLabelFiles.Items.Insert(count, objectName);
            checkedLabelFiles.SetItemCheckState(count, CheckState.Unchecked);
        }

        public List<string> CheckedLabelFiles
        {
            get { return checkedLabelFiles.CheckedItems.Cast<string>().ToList(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool UsedByLabelManager
        {
            get { return usedByLabelManager; }
            set { usedByLabelManager = value; }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

        }
    }
}
