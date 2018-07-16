namespace Addin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
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


    /// <summary>
    /// Created new labels automaticlly when prefixing them as @@@
    /// </summary>
    [Export(typeof(IMainMenu))]
    public class MainMenuAddIn : MainMenuBase
    {
        #region Member variables
        private const string addinName = "Create label";
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return "Create new labels settings";
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return MainMenuAddIn.addinName;
            }
        }

        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinEventArgs e)
        {
            try
            {
                // TODO: Do your magic for your add-in
                // Currently this addin works only for one element at time.
                // Next feature would be generate all labels to all project elements at once.
                //LabelGlobalSettings labelGlobalSettingsForm = new LabelGlobalSettings();
                //labelGlobalSettingsForm.ShowDialog();

                LabelGroupsForm labelGroupsForm = new LabelGroupsForm();
                labelGroupsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}
