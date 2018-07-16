using System;
using System.Collections.Generic;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.DataEntityViews;

namespace Addin
{
    /// <summary>
    ///     Base class to create new labels
    /// </summary>
    public abstract class CreateLabels
    {
        protected LabelManager LabelManager;

        /// <summary>
        ///     Initialize class
        /// </summary>
        public CreateLabels()
        {
            LabelManager = new LabelManager();
        }

        /// <summary>
        ///     Point to all label properties and child elements label properties
        /// </summary>
        public abstract void Run();

        public abstract List<string> GetLabelList();

        public abstract void RunDto(List<LabelDto> labelDtoList);

        protected string UpdateObjectLabel(string source, List<LabelDto> labelDtoList)
        {
            string ret = source;

            if (labelDtoList == null || source == null || labelDtoList.Count <= 0 || !LabelManager.IsPrefixed(source)) return ret;
            
            foreach (LabelDto lDto in labelDtoList)
            {
                if (source == lDto.PrefixedLabelText)
                {
                    ret = LabelManager.FindOrCreateLabel(lDto);
                    //break;
                }
            }

            return ret;
        }

        /// <summary>
        ///     Contructs a classes based on the element type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CreateLabels Construct(NamedElement element)
        {
            // TODO: Add here new elements constructors
            string name = element.GetType().Name;
            name = name.ToLower();

            switch (name)
            {
                case "table":
                    return new CreateLabels_Table(element as Table);

                case "tableextension":
                    return new CreateLabels_TableExtension(element as TableExtension);

                case "view":
                    return new CreateLabels_View(element as View);

                case "edtbase":
                case "edtstring":
                case "edtcontainer":
                case "edtenum":
                case "edtdate":
                case "edtdatetime":
                case "edtutcdatetime":
                case "edttime": 
                case "edtguid":
                case "edtreal":
                case "edtint":
                case "edtint64":
                    return new CreateLabels_Edt(element as EdtBase);

                case "baseenum":
                    return new CreateLabels_BaseEnum(element as BaseEnum);

                case "baseenumextension":
                    return new CreateLabels_BaseEnumExtension(element as BaseEnumExtension);

                case "menuitem":
                case "menuitemaction":
                case "menuitemdisplay":
                case "menuitemoutput":
                    return new CreateLabels_MenuItem(element as MenuItem);

                case "form":
                    return new CreateLabels_Form(element as Form);

                case "securityprivilege":
                    return new CreateLabels_SecurityPrivilege(element as SecurityPrivilege);

                case "workflowhierarchyassignmentprovider":
                    return
                        new CreateLabels_WorkflowHierarchyAssignmentProvider(
                            element as WorkflowHierarchyAssignmentProvider);

                case "workflowapproval":
                    return new CreateLabels_WorkflowApproval(element as WorkflowApproval);

                case "workflowcategory":
                    return new CreateLabels_WorkflowCategory(element as WorkflowCategory);

                case "workflowtask":
                    return new CreateLabels_WorkflowTask(element as WorkflowTask);

                case "workflowtemplate": //WorkflowType Object
                    return new CreateLabels_WorkflowType(element as WorkflowTemplate);

                case "classitem": 
                    return new CreateLabels_Class(element as ClassItem);

                case "dataentityview":
                    return new CreateLabels_DataEntity(element as DataEntityView);

                default:
                    throw new NotImplementedException($"The type {element.GetType().Name} is not implemented.");
            }
        }



        /// <summary>
        ///     Get logging message
        /// </summary>
        /// <returns>Logging message</returns>
        public string GetLoggingMessage()
        {
            return LabelManager.GetLoggingMessage();
        }

        public LabelManager getLabelManager()
        {
            return LabelManager;
        }
    }
}