using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;

namespace Addin
{
    /// <summary>
    ///     Creates labels to security privileges and child elements
    /// </summary>
    public class CreateLabels_SecurityPrivilege : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current menuitem
        /// </summary>
        protected SecurityPrivilege SecurityPrivilege;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="securityPrivilege">Selected SecurityPrivilege element</param>
        public CreateLabels_SecurityPrivilege(SecurityPrivilege securityPrivilege)
        {
            this.SecurityPrivilege = securityPrivilege;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            SecurityPrivilege.Label = LabelManager.CreateLabel(SecurityPrivilege.Label);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //label
            if (LabelManager.IsPrefixed(SecurityPrivilege.Label))
            {
                ret.Add(SecurityPrivilege.Label);
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                SecurityPrivilege.Label = UpdateObjectLabel(SecurityPrivilege.Label, labelDtoList);
            }
        }
    }
}