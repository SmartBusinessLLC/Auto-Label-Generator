using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;

namespace Addin
{
    public class CreateLabels_TableExtension : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current table
        /// </summary>
        protected TableExtension table;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_TableExtension(TableExtension table)
        {
            this.table = table;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            foreach (BaseField field in table.BaseFields)
            {
                // Field label
                field.Label = LabelManager.CreateLabel(field.Label);

                // Field help text
                field.HelpText = LabelManager.CreateLabel(field.HelpText);
            }

            foreach (FieldGroup fieldGroup in table.FieldGroups)
                fieldGroup.Label = LabelManager.CreateLabel(fieldGroup.Label);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //
            foreach (BaseField field in table.BaseFields)
            {
                // Field label
                if (LabelManager.IsPrefixed(field.Label))
                {
                    ret.Add(field.Label);
                }
                // Field help text
                if (LabelManager.IsPrefixed(field.HelpText))
                {
                    ret.Add(field.HelpText);
                }
            }

            foreach (FieldGroup fieldGroup in table.FieldGroups)
            {
                if (LabelManager.IsPrefixed(fieldGroup.Label))
                {
                    ret.Add(fieldGroup.Label);
                }
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                foreach (BaseField field in table.BaseFields)
                {
                    // Field label
                    field.Label = UpdateObjectLabel(field.Label, labelDtoList);

                    // Field help text
                    field.HelpText = UpdateObjectLabel(field.HelpText, labelDtoList);
                }

                foreach (FieldGroup fieldGroup in table.FieldGroups)
                    fieldGroup.Label = UpdateObjectLabel(fieldGroup.Label, labelDtoList);
            }
        }

    }
}
