using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Addin
{
    /// <summary>
    ///     Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowTask : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current task
        /// </summary>
        protected WorkflowTask task;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="category">Selected task element</param>
        public CreateLabels_WorkflowTask(WorkflowTask task)
        {
            this.task = task;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            task.Label = LabelManager.CreateLabel(task.Label);

            // Help text
            task.HelpText = LabelManager.CreateLabel(task.HelpText);
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