using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Addin
{
    /// <summary>
    ///     Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowCategory : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current category
        /// </summary>
        protected WorkflowCategory category;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="category">Selected category element</param>
        public CreateLabels_WorkflowCategory(WorkflowCategory category)
        {
            this.category = category;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            category.Label = LabelManager.CreateLabel(category.Label);

            // Help text
            category.HelpText = LabelManager.CreateLabel(category.HelpText);
        }

        public override List<string> GetLabelList()
        {
            throw new System.NotImplementedException();
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            throw new System.NotImplementedException();
        }
    }
}