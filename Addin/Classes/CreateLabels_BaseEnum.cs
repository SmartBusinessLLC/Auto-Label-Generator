using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;

namespace Addin
{
    /// <summary>
    ///     Creates labels to BaseEnum and child elements
    /// </summary>
    public class CreateLabels_BaseEnum : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current BaseEnum
        /// </summary>
        protected BaseEnum baseEnum;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="baseEnum">Selected baseenum element</param>
        public CreateLabels_BaseEnum(BaseEnum baseEnum)
        {
            this.baseEnum = baseEnum;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            baseEnum.Label = LabelManager.CreateLabel(baseEnum.Label);
            //Help
            baseEnum.Help = LabelManager.CreateLabel(baseEnum.Help);

            // Elements labels
            foreach (BaseEnumValue values in baseEnum.BaseEnumValues)
                values.Label = LabelManager.CreateLabel(values.Label);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //label
            if (LabelManager.IsPrefixed(baseEnum.Label))
            {
                ret.Add(baseEnum.Label);
            }
            //Help
            if (LabelManager.IsPrefixed(baseEnum.Help))
            {
                ret.Add(baseEnum.Help);
            }

            //Enum elements
            foreach (BaseEnumValue values in baseEnum.BaseEnumValues)
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
                // Label
                baseEnum.Label = UpdateObjectLabel(baseEnum.Label, labelDtoList);
                //Help
                baseEnum.Help = UpdateObjectLabel(baseEnum.Help, labelDtoList);

                // Elements labels
                foreach (BaseEnumValue values in baseEnum.BaseEnumValues)
                    values.Label = UpdateObjectLabel(values.Label, labelDtoList);
            }
        }


    }
}