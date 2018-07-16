using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Addin
{
    /// <summary>
    ///     Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowHierarchyAssignmentProvider : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current provider
        /// </summary>
        protected WorkflowHierarchyAssignmentProvider provider;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="provider">Selected provider element</param>
        public CreateLabels_WorkflowHierarchyAssignmentProvider(WorkflowHierarchyAssignmentProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            provider.Label = LabelManager.CreateLabel(provider.Label);

            // Help text
            provider.HelpText = LabelManager.CreateLabel(provider.HelpText);
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