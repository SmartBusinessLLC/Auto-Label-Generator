using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;

namespace Addin
{
    /// <summary>
    ///     Creates labels to view and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: view, fields, field groups</remarks>
    public class CreateLabels_View : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current view
        /// </summary>
        protected View View;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="view">Selected view element</param>
        public CreateLabels_View(View view)
        {
            View = view;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            View.Label = LabelManager.CreateLabel(View.Label);

            // Dev doc
            View.DeveloperDocumentation = LabelManager.CreateLabel(View.DeveloperDocumentation);

            foreach (ViewBaseField field in View.ViewBaseFields)
            {
                // Field label
                field.Label = LabelManager.CreateLabel(field.Label);

                // Field help text
                field.HelpText = LabelManager.CreateLabel(field.HelpText);
            }

            foreach (FieldGroup fieldGroup in View.FieldGroups)
                fieldGroup.Label = LabelManager.CreateLabel(fieldGroup.Label);
        }

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //label
            if (LabelManager.IsPrefixed(View.Label))
            {
                ret.Add(View.Label);
            }
            //DeveloperDocumentation
            if (LabelManager.IsPrefixed(View.DeveloperDocumentation))
            {
                ret.Add(View.DeveloperDocumentation);
            }

            //
            foreach (ViewBaseField field in View.ViewBaseFields)
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

            foreach (FieldGroup fieldGroup in View.FieldGroups)
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
                // Label
                View.Label = UpdateObjectLabel(View.Label, labelDtoList);
                //DeveloperDocumentation
                View.DeveloperDocumentation = UpdateObjectLabel(View.DeveloperDocumentation, labelDtoList);

                foreach (ViewBaseField field in View.ViewBaseFields)
                {
                    // Field label
                    field.Label = UpdateObjectLabel(field.Label, labelDtoList);

                    // Field help text
                    field.HelpText = UpdateObjectLabel(field.HelpText, labelDtoList);
                }

                foreach (FieldGroup fieldGroup in View.FieldGroups)
                    fieldGroup.Label = UpdateObjectLabel(fieldGroup.Label, labelDtoList);
            }
        }
    }
}