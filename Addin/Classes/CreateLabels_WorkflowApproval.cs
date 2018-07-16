using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Addin
{
    /// <summary>
    ///     Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowApproval : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current approval
        /// </summary>
        protected WorkflowApproval approval;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="approval">Selected approval element</param>
        public CreateLabels_WorkflowApproval(WorkflowApproval approval)
        {
            this.approval = approval;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            approval.Label = LabelManager.CreateLabel(approval.Label);

            // Help text
            approval.HelpText = LabelManager.CreateLabel(approval.HelpText);
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