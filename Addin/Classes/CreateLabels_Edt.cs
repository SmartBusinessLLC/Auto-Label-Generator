using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;

namespace Addin
{
    /// <summary>
    ///     Creates labels to table and child elements
    /// </summary>
    public class CreateLabels_Edt : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current Edt
        /// </summary>
        protected EdtBase Edt;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="edt">Selected edt element</param>
        public CreateLabels_Edt(EdtBase edt)
        {
            Edt = edt;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            Edt.Label = LabelManager.CreateLabel(Edt.Label);

            // Help text
            Edt.HelpText = LabelManager.CreateLabel(Edt.HelpText);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //label
            if (LabelManager.IsPrefixed(Edt.Label))
            {
                ret.Add(Edt.Label);
            }
            //Help
            if (LabelManager.IsPrefixed(Edt.HelpText))
            {
                ret.Add(Edt.HelpText);
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                // Label
                Edt.Label = UpdateObjectLabel(Edt.Label, labelDtoList);
                //Help
                Edt.HelpText = UpdateObjectLabel(Edt.HelpText, labelDtoList);
            }
        }
    }
}