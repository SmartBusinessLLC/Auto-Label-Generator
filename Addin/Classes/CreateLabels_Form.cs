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
    ///     Creates labels to form and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: design and controls</remarks>
    public class CreateLabels_Form : CreateLabels
    {
        /// <summary>
        ///     Global variable representing the current view
        /// </summary>
        protected Form      Form;

        /// <summary>
        ///     Initiaze the global variable
        /// </summary>
        /// <param name="form">Selected form element</param>
        public CreateLabels_Form(Form form)
        {
            Form = form;
        }

        /// <summary>
        ///     Run the process
        /// </summary>
        public override void Run()
        {
            // Caption
            Form.FormDesign.Caption = LabelManager.CreateLabel(Form.FormDesign.Caption);

            // Form controls
            foreach (FormControl control in Form.FormDesign.FormControls)
            {
                //ProcessControlLabel(control, true);

                switch (control.Type)
                {
                    case FormControlType.String:
                        var stringControl = control as FormStringControl;
                        stringControl.Label = LabelManager.CreateLabel(stringControl.Label);
                        break;

                    case FormControlType.CheckBox:
                        var checkboxControl = control as FormCheckBoxControl;
                        checkboxControl.Label = LabelManager.CreateLabel(checkboxControl.Label);
                        break;

                    case FormControlType.Group:
                        var groupControl = control as FormGroupControl;
                        groupControl.Caption = LabelManager.CreateLabel(groupControl.Caption);
                        break;

                    case FormControlType.Button:
                        var buttonControl = control as FormButtonControl;
                        buttonControl.Text = LabelManager.CreateLabel(buttonControl.Text);
                        break;

                    case FormControlType.Real:
                        var realControl = control as FormRealControl;
                        realControl.Label = LabelManager.CreateLabel(realControl.Label);
                        break;

                    case FormControlType.Integer:
                        var integerControl = control as FormIntegerControl;
                        integerControl.Label = LabelManager.CreateLabel(integerControl.Label);
                        break;

                    case FormControlType.ComboBox:
                        var comboboxControl = control as FormComboBoxControl;
                        comboboxControl.Label = LabelManager.CreateLabel(comboboxControl.Label);
                        break;

                    case FormControlType.Image:
                        var imageControl = control as FormImageControl;
                        imageControl.Label = LabelManager.CreateLabel(imageControl.Label);
                        break;

                    case FormControlType.Date:
                        var dateControl = control as FormDateControl;
                        dateControl.Label = LabelManager.CreateLabel(dateControl.Label);
                        break;

                    case FormControlType.RadioButton:
                        var radioControl = control as FormRadioButtonControl;
                        radioControl.Caption = LabelManager.CreateLabel(radioControl.Caption);
                        break;

                    case FormControlType.ButtonGroup:
                        var buttonGroupCaption = control as FormButtonGroupControl;
                        buttonGroupCaption.Caption = LabelManager.CreateLabel(buttonGroupCaption.Caption);
                        break;

                    case FormControlType.TabPage:
                        var tabpageControl = control as FormTabPageControl;
                        tabpageControl.Caption = LabelManager.CreateLabel(tabpageControl.Caption);
                        break;

                    case FormControlType.CommandButton:
                        var commandbuttonControl = control as FormCommandButtonControl;
                        commandbuttonControl.Text = LabelManager.CreateLabel(commandbuttonControl.Text);
                        break;

                    case FormControlType.MenuButton:
                        var menubuttonControl = control as FormMenuButtonControl;
                        menubuttonControl.Text = LabelManager.CreateLabel(menubuttonControl.Text);
                        break;

                    case FormControlType.MenuFunctionButton:
                        var menufunctionControl = control as FormMenuFunctionButtonControl;
                        menufunctionControl.Text = LabelManager.CreateLabel(menufunctionControl.Text);
                        break;

                    case FormControlType.ListBox:
                        var listboxControl = control as FormListBoxControl;
                        listboxControl.Label = LabelManager.CreateLabel(listboxControl.Label);
                        break;

                    case FormControlType.Time:
                        var timeControl = control as FormTimeControl;
                        timeControl.Label = LabelManager.CreateLabel(timeControl.Label);
                        break;

                    case FormControlType.ButtonSeparator:
                        var buttonseparatorControl = control as FormButtonSeparatorControl;
                        buttonseparatorControl.Text = LabelManager.CreateLabel(buttonseparatorControl.Text);
                        break;

                    case FormControlType.Guid:
                        var guidControl = control as FormGuidControl;
                        guidControl.Label = LabelManager.CreateLabel(guidControl.Label);
                        break;

                    case FormControlType.Int64:
                        var int64Control = control as FormInt64Control;
                        int64Control.Label = LabelManager.CreateLabel(int64Control.Label);
                        break;

                    case FormControlType.DateTime:
                        var datetimeControl = control as FormDateTimeControl;
                        datetimeControl.Label = LabelManager.CreateLabel(datetimeControl.Label);
                        break;

                    case FormControlType.ActionPane:
                        var actionpaneControl = control as FormActionPaneControl;
                        actionpaneControl.Caption = LabelManager.CreateLabel(actionpaneControl.Caption);
                        break;

                    case FormControlType.ActionPaneTab:
                        var actionpanetabControl = control as FormActionPaneTabControl;
                        actionpanetabControl.Caption = LabelManager.CreateLabel(actionpanetabControl.Caption);
                        break;

                    case FormControlType.SegmentedEntry:
                        var segmentedEntryControl = control as FormSegmentedEntryControl;
                        segmentedEntryControl.Label = LabelManager.CreateLabel(segmentedEntryControl.Label);
                        break;

                    case FormControlType.DropDialogButton:
                        var dropDialogButtonControl = control as FormDropDialogButtonControl;
                        dropDialogButtonControl.Text = LabelManager.CreateLabel(dropDialogButtonControl.Text);
                        break;

                    case FormControlType.ReferenceGroup:
                        var referenceGroupControl = control as FormReferenceGroupControl;
                        referenceGroupControl.Label = LabelManager.CreateLabel(referenceGroupControl.Label);
                        break;

                    default:
                        throw new NotImplementedException($"Form control type {control.Type} is not implemented.");
                }

                control.HelpText = LabelManager.CreateLabel(control.HelpText);
            }
        }


        //This class is a copy from UpdateMethod on CreateLabels_Class
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


        protected string ProcessControlLabel(FormControl control, bool setValue, List<LabelDto> labelDtoList)
        {
            string ret = string.Empty;

            switch (control.Type)
            {
                case FormControlType.String:
                    var stringControl = (FormStringControl)control;
                    ret = stringControl.Label;
                    if (setValue)
                    {
                        stringControl.Label = UpdateObjectLabel(stringControl.Label, labelDtoList);
                    }

                    break;

                case FormControlType.CheckBox:
                    var checkboxControl = (FormCheckBoxControl)control;
                    ret = checkboxControl.Label;

                    if (setValue)
                    {
                        checkboxControl.Label = UpdateObjectLabel(checkboxControl.Label, labelDtoList);
                    }

                    break;

                case FormControlType.Real:
                    var realControl = (FormRealControl)control;
                    ret = realControl.Label;
                    if (setValue)
                    {
                        realControl.Label = UpdateObjectLabel(realControl.Label, labelDtoList);
                    }    
                    break;

                case FormControlType.Integer:
                    var integerControl = (FormIntegerControl)control;

                    ret = integerControl.Label;
                    if (setValue)
                    {
                        integerControl.Label = UpdateObjectLabel(integerControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.ComboBox:
                    var comboboxControl = (FormComboBoxControl)control;

                    ret = comboboxControl.Label;
                    if (setValue)
                    {
                        comboboxControl.Label = UpdateObjectLabel(comboboxControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.Image:
                    var imageControl = (FormImageControl)control;
                    ret = imageControl.Label;
                    if (setValue)
                    {
                        imageControl.Label = UpdateObjectLabel(imageControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.Date:
                    var dateControl = (FormDateControl)control;
                    ret = dateControl.Label;
                    if (setValue)
                    {
                        dateControl.Label = UpdateObjectLabel(dateControl.Label, labelDtoList);
                    } 
                    break;

                case FormControlType.ListBox:
                    var listboxControl = (FormListBoxControl)control;
                    ret = listboxControl.Label;
                    if (setValue)
                    {
                        listboxControl.Label = UpdateObjectLabel(listboxControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.Time:
                    var timeControl = (FormTimeControl)control;
                    ret = timeControl.Label;
                    if (setValue)
                    {
                        timeControl.Label = UpdateObjectLabel(timeControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.SegmentedEntry:
                    var segmentedEntryControl = (FormSegmentedEntryControl)control;
                    ret = segmentedEntryControl.Label;
                    if (setValue)
                    {
                        segmentedEntryControl.Label = UpdateObjectLabel(segmentedEntryControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.ReferenceGroup:
                    var referenceGroupControl = (FormReferenceGroupControl)control;
                    ret = referenceGroupControl.Label;
                    if (setValue)
                    {
                        referenceGroupControl.Label = UpdateObjectLabel(referenceGroupControl.Label, labelDtoList);
                    }
                    break;


                case FormControlType.Guid:
                    var guidControl = (FormGuidControl)control;
                    ret = guidControl.Label;
                    if (setValue)
                    {
                        guidControl.Label = UpdateObjectLabel(guidControl.Label, labelDtoList);
                    }
                    break;

                case FormControlType.Int64:
                    var int64Control = (FormInt64Control)control;
                    ret = int64Control.Label;
                    if (setValue)
                    {
                        int64Control.Label = UpdateObjectLabel(int64Control.Label, labelDtoList);
                    }
                    break;

                case FormControlType.DateTime:
                    var datetimeControl = (FormDateTimeControl)control;
                    ret = datetimeControl.Label;
                    if (setValue)
                    {
                        datetimeControl.Label = UpdateObjectLabel(datetimeControl.Label, labelDtoList);
                    }
                    break;


                case FormControlType.Group:
                    var groupControl = (FormGroupControl)control;
                    ret = groupControl.Caption;
                    if (setValue)
                    {
                        groupControl.Caption = UpdateObjectLabel(groupControl.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.ActionPane:
                    var actionpaneControl = (FormActionPaneControl)control;
                    ret = actionpaneControl.Caption;
                    if (setValue)
                    {
                        actionpaneControl.Caption = UpdateObjectLabel(actionpaneControl.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.ActionPaneTab:
                    var actionpanetabControl = (FormActionPaneTabControl)control;
                    ret = actionpanetabControl.Caption;
                    if (setValue)
                    {
                        actionpanetabControl.Caption = UpdateObjectLabel(actionpanetabControl.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.RadioButton:
                    var radioControl = (FormRadioButtonControl)control;
                    ret = radioControl.Caption;
                    if (setValue)
                    {
                        radioControl.Caption = UpdateObjectLabel(radioControl.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.ButtonGroup:
                    var buttonGroupCaption = (FormButtonGroupControl)control;
                    ret = buttonGroupCaption.Caption;
                    if (setValue)
                    {
                        buttonGroupCaption.Caption = UpdateObjectLabel(buttonGroupCaption.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.TabPage:
                    var tabpageControl = (FormTabPageControl)control;
                    ret = tabpageControl.Caption;
                    if (setValue)
                    {
                        tabpageControl.Caption = UpdateObjectLabel(tabpageControl.Caption, labelDtoList);
                    }
                    break;

                case FormControlType.Tab:
                    break;
                //text

                case FormControlType.CommandButton:
                    var commandbuttonControl = (FormCommandButtonControl)control;
                    ret = commandbuttonControl.Text;
                    if (setValue)
                    {
                        commandbuttonControl.Text = UpdateObjectLabel(commandbuttonControl.Text, labelDtoList);
                    }
                    break;

                case FormControlType.MenuButton:
                    var menubuttonControl = (FormMenuButtonControl)control;
                    ret = menubuttonControl.Text;
                    if (setValue)
                    {
                        menubuttonControl.Text = UpdateObjectLabel(menubuttonControl.Text, labelDtoList);
                    }

                    break;

                case FormControlType.MenuFunctionButton:
                    var menufunctionControl = (FormMenuFunctionButtonControl)control;
                    ret = menufunctionControl.Text;
                    if (setValue)
                    {
                        menufunctionControl.Text = UpdateObjectLabel(menufunctionControl.Text, labelDtoList);
                    }

                    break;

                case FormControlType.Button:
                    var buttonControl = (FormButtonControl)control;
                    ret = buttonControl.Text;
                    if (setValue)
                    {
                        buttonControl.Text = UpdateObjectLabel(buttonControl.Text, labelDtoList);
                    }

                    break;

                case FormControlType.ButtonSeparator:
                    var buttonseparatorControl = (FormButtonSeparatorControl)control;
                    ret = buttonseparatorControl.Text;
                    if (setValue)
                    {
                        buttonseparatorControl.Text = UpdateObjectLabel(buttonseparatorControl.Text, labelDtoList);
                    }

                    break;

                case FormControlType.DropDialogButton:
                    var dropDialogButtonControl = (FormDropDialogButtonControl)control;
                    ret = dropDialogButtonControl.Text;
                    if (setValue)
                    {
                        dropDialogButtonControl.Text = UpdateObjectLabel(dropDialogButtonControl.Text, labelDtoList);
                    }

                    break;

                case FormControlType.Custom:
                    break;

                case FormControlType.Grid:
                    break;



                default:
                    throw new NotImplementedException($"Form control type {control.Type} is not implemented.");
            }


            return ret;
        }
        

        protected String info = string.Empty;

        public void handleNestedFormControls(FormControl _formControl, List<string> _ret)
        {
            Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.ElementDefinition rootElement =
                    (Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.ElementDefinition)_formControl.RootElement;

            IEnumerable<Object> rootElementChild    = (IEnumerable<Object>)_formControl.VisualChildren;
            IEnumerator<Object> childrenEnumerator  = rootElementChild.GetEnumerator();

            while (childrenEnumerator.MoveNext())
            {
                if (childrenEnumerator.Current != null)
                {
                    if (childrenEnumerator.Current.GetType() != typeof(FormControlHasMethods))
                    {
                        FormControl formControl = (FormControl)childrenEnumerator.Current;

                        string formControlLocalLabel = ProcessControlLabel(formControl, false, null);

                        if (LabelManager.IsPrefixed(formControlLocalLabel))
                        {
                            _ret.Add(formControlLocalLabel);
                        }

                        if (LabelManager.IsPrefixed(formControl.HelpText))
                        {
                            _ret.Add(formControl.HelpText);
                        }

                        this.handleNestedFormControls(formControl, _ret);
                    }
                }
            }
        }

        public void handleFormControlMethods(List<string> _ret)
        {
            AxForm axForm           = DesignMetaModelService.Instance.GetForm(Form.Name);
            AsParseLabel parseLabel = new AsParseLabel();

            foreach (AxFormControl control in axForm.Design.GetAllControls())
            {
                foreach (AxMethod method in control.Methods)
                {
                    string m = method.Source;

                    _ret.AddRange(parseLabel.ParseString(method.Source));
                }
            }
        }

        public void handleFormDataSourcesMethods(List<string> _ret)
        {
            AxForm axForm = DesignMetaModelService.Instance.GetForm(Form.Name);
            AsParseLabel parseLabel = new AsParseLabel();

            KeyedObjectCollection<AxFormDataSourceRoot> keyO = axForm.DataSources;
            IEnumerator<AxFormDataSourceRoot> dataSourceEnumerator = keyO.GetEnumerator();

            while (dataSourceEnumerator.MoveNext())
            {
                AxFormDataSourceRoot axFormDataSourceRoot = dataSourceEnumerator.Current;

                KeyedObjectCollection<AxMethod> key1 = axFormDataSourceRoot.Methods;
                IEnumerator<AxMethod> methodEnumerator = key1.GetEnumerator();

                while (methodEnumerator.MoveNext())
                {
                    AxMethod axMethod = methodEnumerator.Current;
                    _ret.AddRange(parseLabel.ParseString(axMethod.Source));
                }
            }
        }

        public void handleFormMethods(List<string> _ret)
        {
            AxForm axForm                           = DesignMetaModelService.Instance.GetForm(Form.Name);
            KeyedObjectCollection<AxMethod> keyO    = axForm.Methods;
            IEnumerator<AxMethod> methodEnumerator  = keyO.GetEnumerator();
            AsParseLabel parseLabel                 = new AsParseLabel();
            //
            while (methodEnumerator.MoveNext())
            {
                AxMethod axMethod = methodEnumerator.Current;
                _ret.AddRange(parseLabel.ParseString(axMethod.Source));
            }
        }
        public override List<string> GetLabelList()
        {
            
            List<string> ret = new List<string>();
            // Caption
            if (LabelManager.IsPrefixed(Form.FormDesign.Caption))
            {
                ret.Add(Form.FormDesign.Caption);
            }

            // Form controls
            foreach (FormControl control in Form.FormDesign.FormControls)
            {

                string localLabel = ProcessControlLabel(control, false, null);
                if (LabelManager.IsPrefixed(localLabel))
                {
                    ret.Add(localLabel);
                }
                if (LabelManager.IsPrefixed(control.HelpText))
                {
                    ret.Add(control.HelpText);
                }

                this.handleNestedFormControls(control, ret);
            }

            //Form controls method
            this.handleFormControlMethods(ret);

            //Form data sources methods
            this.handleFormDataSourcesMethods(ret);

            //Form methods
            this.handleFormMethods(ret);

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                // Caption
                Form.FormDesign.Caption = UpdateObjectLabel(Form.FormDesign.Caption, labelDtoList);
                

                // Form controls
                foreach (FormControl control in Form.FormDesign.FormControls)
                {
                    ProcessControlLabel(control, true, labelDtoList);

                    control.HelpText = UpdateObjectLabel(control.HelpText, labelDtoList);

                    this.handleNestedFormControlsRunDto(control, labelDtoList);
                }

                AxForm axForm = (AxForm)Form.GetMetadataType();
                //AxForm axForm = DesignMetaModelService.Instance.GetForm(Form.Name);

                //FormMethods
                ProcessMethodsOnForms(axForm, true, labelDtoList);

                //Methods on nested form controls
                ProcessNestedMethodsOnForms(axForm, true, labelDtoList);

                //Methods on form data sources
                ProcessDataSourceMethodsOnForms(axForm, true, labelDtoList);

                //var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService; // metaModelProviders.CurrentMetaModelService;
                // Getting the model will likely have to be more sophisticated, such as getting the model of the project and checking
                // if the object has the same model.
                // But this shold do for demonstration.
                ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Forms.GetModelInfo(axForm.Name).FirstOrDefault<ModelInfo>();

                KeyedObjectCollection<AxMethod> keyO = axForm.Methods;

                axForm.Methods = keyO;
                metaModelService.UpdateForm(axForm, new ModelSaveInfo(model));
            }
        }

        //This class is a copy from RunDto on CreateLabels_Class
        public void ProcessMethodsOnForms(AxForm _axForm, bool setValue, List<LabelDto> labelDtoList)
        {
            if (setValue)
            {
                if (labelDtoList.Count > 0)
                {
                    KeyedObjectCollection<AxMethod> keyO = _axForm.Methods;

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

        public void ProcessNestedMethodsOnForms(AxForm _axForm, bool setValue, List<LabelDto> labelDtoList)
        {
            string ret = string.Empty;

            if (setValue)
            {
                if (labelDtoList.Count > 0)
                {
                    AsParseLabel parseLabel = new AsParseLabel();

                    foreach (AxFormControl control in _axForm.Design.GetAllControls())
                    {
                        {
                            KeyedObjectCollection<AxMethod> keyO = control.Methods;

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
            }
        }

        public void ProcessDataSourceMethodsOnForms(AxForm _axForm, bool setValue, List<LabelDto> labelDtoList)
        {
            string ret = string.Empty;

            if (setValue)
            {
                if (labelDtoList.Count > 0)
                {
                    AsParseLabel parseLabel = new AsParseLabel();

                    KeyedObjectCollection<AxFormDataSourceRoot> keyO = _axForm.DataSources;
                    IEnumerator<AxFormDataSourceRoot> dataSourceEnumerator = keyO.GetEnumerator();

                    while (dataSourceEnumerator.MoveNext())
                    {
                        AxFormDataSourceRoot axFormDataSourceRoot = dataSourceEnumerator.Current;

                        KeyedObjectCollection<AxMethod> key1 = axFormDataSourceRoot.Methods;
                        IEnumerator<AxMethod> methodEnumerator = key1.GetEnumerator();

                        while (methodEnumerator.MoveNext())
                        {
                            AxMethod axMethod = methodEnumerator.Current;

                            string newMethod = UpdateMethod(axMethod.Source, labelDtoList);
                            if (newMethod != axMethod.Source)
                            {
                                methodEnumerator.Current.Source = newMethod;
                            }
                        }
                    }
                }
            }
        }


        public void handleNestedFormControlsRunDto(FormControl _formControl, List<LabelDto> _labelDtoList)
        {
            Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.ElementDefinition rootElement =
                    (Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.ElementDefinition)_formControl.RootElement;
            IEnumerable<Object> rootElementChild = (IEnumerable<Object>)_formControl.VisualChildren;

            IEnumerator<Object> childrenEnumerator = rootElementChild.GetEnumerator();
            
            while (childrenEnumerator.MoveNext())
            {
                
                if (childrenEnumerator.Current != null)
                {
                    if (childrenEnumerator.Current.GetType() != typeof(FormControlHasMethods))
                    {
                        FormControl formControl = (FormControl)childrenEnumerator.Current;

                        ProcessControlLabel(formControl, true, _labelDtoList);

                        formControl.HelpText = UpdateObjectLabel(formControl.HelpText, _labelDtoList);

                        this.handleNestedFormControlsRunDto(formControl, _labelDtoList);
                    }
                }
            }
        }
    }
}