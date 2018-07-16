namespace Addin
{
    partial class LabelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label labelNum;
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelObjectName = new System.Windows.Forms.Label();
            this.labelExisted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LabelFilesPanel = new System.Windows.Forms.Panel();
            this.labelFilesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.itemsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            labelNum = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.LabelFilesPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelNum
            // 
            labelNum.AutoSize = true;
            labelNum.Dock = System.Windows.Forms.DockStyle.Fill;
            labelNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelNum.Location = new System.Drawing.Point(25, 1);
            labelNum.Name = "labelNum";
            labelNum.Size = new System.Drawing.Size(29, 35);
            labelNum.TabIndex = 3;
            labelNum.Text = "#";
            labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 7;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel.Controls.Add(labelNum, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelObjectName, 4, 0);
            this.tableLayoutPanel.Controls.Add(this.labelExisted, 5, 0);
            this.tableLayoutPanel.Controls.Add(this.label1, 6, 0);
            this.tableLayoutPanel.Controls.Add(this.labelLabel, 3, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(784, 73);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Label Id";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelObjectName
            // 
            this.labelObjectName.AutoSize = true;
            this.labelObjectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObjectName.Location = new System.Drawing.Point(441, 1);
            this.labelObjectName.Name = "labelObjectName";
            this.labelObjectName.Size = new System.Drawing.Size(161, 35);
            this.labelObjectName.TabIndex = 4;
            this.labelObjectName.Text = "Object Name";
            this.labelObjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelExisted
            // 
            this.labelExisted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExisted.AutoSize = true;
            this.labelExisted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExisted.Location = new System.Drawing.Point(609, 1);
            this.labelExisted.Name = "labelExisted";
            this.labelExisted.Size = new System.Drawing.Size(82, 35);
            this.labelExisted.TabIndex = 2;
            this.labelExisted.Text = "Existed";
            this.labelExisted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelExisted, "Determine if label exists in current label file");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(698, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Label Group";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLabel
            // 
            this.labelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLabel.AutoSize = true;
            this.labelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLabel.Location = new System.Drawing.Point(194, 1);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(240, 35);
            this.labelLabel.TabIndex = 1;
            this.labelLabel.Text = "Label";
            this.labelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.buttonCancel);
            this.RightPanel.Controls.Add(this.label4);
            this.RightPanel.Controls.Add(this.LabelFilesPanel);
            this.RightPanel.Controls.Add(this.CreateBtn);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(933, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(127, 405);
            this.RightPanel.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(103, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Label files";
            // 
            // LabelFilesPanel
            // 
            this.LabelFilesPanel.Controls.Add(this.labelFilesCheckedListBox);
            this.LabelFilesPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LabelFilesPanel.Location = new System.Drawing.Point(0, 116);
            this.LabelFilesPanel.Name = "LabelFilesPanel";
            this.LabelFilesPanel.Size = new System.Drawing.Size(127, 289);
            this.LabelFilesPanel.TabIndex = 1;
            // 
            // labelFilesCheckedListBox
            // 
            this.labelFilesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelFilesCheckedListBox.Enabled = false;
            this.labelFilesCheckedListBox.FormattingEnabled = true;
            this.labelFilesCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.labelFilesCheckedListBox.Name = "labelFilesCheckedListBox";
            this.labelFilesCheckedListBox.Size = new System.Drawing.Size(127, 289);
            this.labelFilesCheckedListBox.TabIndex = 2;
            // 
            // CreateBtn
            // 
            this.CreateBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateBtn.Location = new System.Drawing.Point(10, 12);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(105, 23);
            this.CreateBtn.TabIndex = 0;
            this.CreateBtn.Text = "Ok";
            this.toolTip1.SetToolTip(this.CreateBtn, "Create and insert labels");
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.Controls.Add(this.tableLayoutPanel);
            this.MainPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(149, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(784, 405);
            this.MainPanel.TabIndex = 3;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.itemsCheckedListBox);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(149, 405);
            this.LeftPanel.TabIndex = 4;
            // 
            // itemsCheckedListBox
            // 
            this.itemsCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsCheckedListBox.Enabled = false;
            this.itemsCheckedListBox.FormattingEnabled = true;
            this.itemsCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.itemsCheckedListBox.Name = "itemsCheckedListBox";
            this.itemsCheckedListBox.Size = new System.Drawing.Size(149, 405);
            this.itemsCheckedListBox.TabIndex = 0;
            // 
            // LabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1060, 405);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.RightPanel);
            this.Name = "LabelForm";
            this.Text = "Label";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.LabelFilesPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button CreateBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label labelExisted;
        private System.Windows.Forms.Panel LabelFilesPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckedListBox labelFilesCheckedListBox;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.CheckedListBox itemsCheckedListBox;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label labelObjectName;
        private System.Windows.Forms.Label label1;
    }
}