using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Addin
{
    /// <summary>
    ///     Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowType : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current type
        /// </summary>
        protected WorkflowTemplate type;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="type">Selected task element</param>
        public CreateLabels_WorkflowType(WorkflowTemplate type)
        {
            this.type = type;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            type.Label = LabelManager.CreateLabel(type.Label);

            // Help text
            type.HelpText = LabelManager.CreateLabel(type.HelpText);
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