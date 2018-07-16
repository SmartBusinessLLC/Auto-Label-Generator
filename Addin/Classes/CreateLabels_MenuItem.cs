using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;

namespace Addin
{
    /// <summary>
    ///     Creates labels to MenuItem and child elements
    /// </summary>
    public class CreateLabels_MenuItem : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current menuitem
        /// </summary>
        protected MenuItem menuItem;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="menuItem">Selected menuitem element</param>
        public CreateLabels_MenuItem(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            menuItem.Label = LabelManager.CreateLabel(menuItem.Label);

            // Help text
            menuItem.HelpText = LabelManager.CreateLabel(menuItem.HelpText);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //label
            if (LabelManager.IsPrefixed(menuItem.Label))
            {
                ret.Add(menuItem.Label);
            }
            //Help
            if (LabelManager.IsPrefixed(menuItem.HelpText))
            {
                ret.Add(menuItem.HelpText);
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                // Label
                menuItem.Label = UpdateObjectLabel(menuItem.Label, labelDtoList);
                //Help
                menuItem.HelpText = UpdateObjectLabel(menuItem.HelpText, labelDtoList);
            }
        }
    }
}