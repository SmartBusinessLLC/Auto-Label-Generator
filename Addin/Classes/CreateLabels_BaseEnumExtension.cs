using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;

namespace Addin
{
    /// <summary>
    ///     Creates labels to BaseEnum extension and child elements
    /// </summary>
    public class CreateLabels_BaseEnumExtension : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current BaseEnum extension
        /// </summary>
        protected BaseEnumExtension baseEnumExtension;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="baseEnumExtension">Selected baseenum extension element</param>
        public CreateLabels_BaseEnumExtension(BaseEnumExtension baseEnumExtension)
        {
            this.baseEnumExtension = baseEnumExtension;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Elements labels
            foreach (BaseEnumValue values in baseEnumExtension.BaseEnumValues)
                values.Label = LabelManager.CreateLabel(values.Label);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            // Elements labels
            foreach (BaseEnumValue values in baseEnumExtension.BaseEnumValues)
            {
                if (LabelManager.IsPrefixed(values.Label))
                {
                    ret.Add(values.Label);
                }
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                // Elements labels
                foreach (BaseEnumValue values in baseEnumExtension.BaseEnumValues)
                    values.Label = UpdateObjectLabel(values.Label, labelDtoList);
            }
        }
    }
}