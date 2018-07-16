using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using System;
using System.Collections.Generic;
using Microsoft.Dynamics.AX.Metadata.Core.Collections;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.Framework.Tools.Extensibility;

namespace Addin
{
    /// <summary>
    ///     Creates labels to table and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: table, fields, field groups</remarks>
    public class CreateLabels_Table : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current table
        /// </summary>
        protected Table table;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_Table(Table table)
        {
            this.table = table;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Label
            table.Label = LabelManager.CreateLabel(table.Label);

            // Dev doc
            table.DeveloperDocumentation = LabelManager.CreateLabel(table.DeveloperDocumentation);

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
            //label
            if (LabelManager.IsPrefixed(table.Label))
            {
                ret.Add(table.Label);
            }
            //DeveloperDocumentation
            if (LabelManager.IsPrefixed(table.DeveloperDocumentation))
            {
                ret.Add(table.DeveloperDocumentation);
            }

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

            AxTable axTable         = DesignMetaModelService.Instance.GetTable(table.Name);
            AsParseLabel parseLabel = new AsParseLabel();

            foreach (AxMethod tableMethod in axTable.Methods)
            {
                string m = tableMethod.Source;

                ret.AddRange(parseLabel.ParseString(tableMethod.Source));
            } 

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                // Label
                table.Label = UpdateObjectLabel(table.Label, labelDtoList);
                //DeveloperDocumentation
                table.DeveloperDocumentation = UpdateObjectLabel(table.DeveloperDocumentation, labelDtoList);

                foreach (BaseField field in table.BaseFields)
                {
                    // Field label
                    field.Label = UpdateObjectLabel(field.Label, labelDtoList);

                    // Field help text
                    field.HelpText = UpdateObjectLabel(field.HelpText, labelDtoList);
                }

                foreach (FieldGroup fieldGroup in table.FieldGroups)
                    fieldGroup.Label = UpdateObjectLabel(fieldGroup.Label, labelDtoList);

                AxTable axTable = DesignMetaModelService.Instance.GetTable(table.Name);
                ProcessMethodsOnTable(axTable, true, labelDtoList);

                var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = metaModelProviders.CurrentMetaModelService;
                // Getting the model will likely have to be more sophisticated, such as getting the model of the project and checking
                // if the object has the same model.
                // But this shold do for demonstration.
                ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Tables.GetModelInfo(axTable.Name).FirstOrDefault<ModelInfo>();

                KeyedObjectCollection<AxMethod> keyO = axTable.Methods;

                axTable.Methods = keyO;
                metaModelService.UpdateTable(axTable, new ModelSaveInfo(model));
            }
        }
        public void ProcessMethodsOnTable(AxTable _axTable, bool setValue, List<LabelDto> labelDtoList)
        {
            if (setValue)
            {
                if (labelDtoList.Count > 0)
                {
                    KeyedObjectCollection<AxMethod> keyO = _axTable.Methods;

                    if (keyO != null)
                    {
                        IEnumerator<AxMethod> methodEnumerator = keyO.GetEnumerator();

                        while (methodEnumerator.MoveNext())
                        {
                            AxMethod axMethod = methodEnumerator.Current;

                            string newMethod = UpdateMethod(axMethod.Source, labelDtoList);
                            //
                            if (newMethod != axMethod.Source)
                            {
                                methodEnumerator.Current.Source = newMethod;
                            }

                        }
                    }
                }
            }
        }

        protected string UpdateMethod(string source, List<LabelDto> labelDtoList)
        {
            string ret = source;

            if (labelDtoList != null && source != null)
            {
                List<LabelDto>.Enumerator eDto = labelDtoList.GetEnumerator();
                StringBuilder builder = new StringBuilder(source);

                while (eDto.MoveNext())
                {
                    LabelDto lDto = eDto.Current;

                    if (lDto != null)
                    {
                        string labelText = LabelManager.AddQuotesPrefix(lDto.PrefixedLabelText, false);

                        if (source.IndexOf(labelText, StringComparison.Ordinal) != -1)
                        {
                            string newLabelId = LabelManager.FindOrCreateLabel(lDto);
                            newLabelId = LabelManager.AddQuotesPrefix(newLabelId, false);

                            builder.Replace(labelText, newLabelId);
                        }
                    }
                }

                ret = builder.ToString();
            }

            return ret;
        }
    }
}