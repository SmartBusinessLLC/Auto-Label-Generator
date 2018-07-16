#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.LabelFiles;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Addin.Properties;
using System.Collections.Generic;
using System.IO;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

#endregion

namespace Addin
{
    /// <summary>
    ///     Created new labels automaticlly when prefixing them as  
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(ILabelFile))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITableExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IEdtBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IMenuItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IView))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ISecurityPrivilege))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IDataEntity))]
    
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowHierarchyAssignmentProvider))]
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowApproval))]
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTask))]
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowCategory))]
    //[DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTemplate))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IClassItem))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables

        private const string AddinName = "DesignerAddin";
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LabelGlobalSettingsRes.labelFilePathPostfix, LabelGlobalSettingsRes.labelsFileName);


        #endregion

        #region Callbacks

        /// <summary>
        ///     Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            try
            {
                List<CreateLabels> createLabelsList = new List<CreateLabels>();
                string logging = string.Empty;
                //create form
                LabelForm form = new LabelForm();

                //Fill form
                foreach (NamedElement element in e.SelectedElements)
                {
                    CreateLabels labels = CreateLabels.Construct(element);

                    form.InitLabelFilesControls(labels.getLabelManager());

                    List<string> mylist = labels.GetLabelList();
                    List<AxLabelFile> labelFiles = form.LabelFiles;

                    if (mylist.Count > 0)
                    {
                        form.LabelList = mylist;
                        form.LabelFiles = labelFiles;
                        form.ElementName = element.Name;

                        IDictionary<string, List<string>> labelsDictionary = labels.getLabelManager().LabelsDictionary;

                        foreach (KeyValuePair<string, bool> item in labels.getLabelManager().CheckedLabelGroupsDictionary)
                        {
                            if(!item.Value)
                            {
                                labelsDictionary.Remove(item.Key);
                            }
                        }

                        form.LabelsDictionary = labelsDictionary;

                        form.FillGrid();
                    }

                    createLabelsList.Add(labels);
                }

                //create labels
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    foreach (CreateLabels createLabels in createLabelsList)
                    {
                        CreateLabels labels = createLabels;
                        labels.RunDto(form.LabelDtoList);
                
                        logging += labels.GetLoggingMessage();
                        logging += "\n";
                    }

                    CoreUtility.DisplayInfo(logging);
                }

                
            }
            catch (Exception ex)
            {
                CoreUtility.DisplayError(ex.TargetSite.ToString());
                CoreUtility.DisplayError(ex.StackTrace);
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get { return "Create new labels"; }
        }

        /// <summary>
        ///     Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get { return AddinName; }
        }

        #endregion
    }
}