using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Dynamics.AX.Metadata.Core.Collections;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;


namespace Addin
{
    public class CreateLabels_Class : CreateLabels
    {
        protected ClassItem ClassItem;

        public override List<string> GetLabelList()
        {
            List<string> ret = new List<string>();
            //
            AxClass axClass = (AxClass) ClassItem.GetMetadataType();
            KeyedObjectCollection<AxMethod> keyO = axClass.Methods;
            IEnumerator<AxMethod> methodEnumerator = keyO.GetEnumerator();
            AsParseLabel parseLabel = new AsParseLabel();
            //
            while (methodEnumerator.MoveNext())
            {
                AxMethod axMethod = methodEnumerator.Current;
                ret.AddRange(parseLabel.ParseString(axMethod.Source));
            }

            return ret;
        }

        public override void RunDto(List<LabelDto> labelDtoList)
        {
            if (labelDtoList.Count > 0)
            {
                AxClass axClass = (AxClass)ClassItem.GetMetadataType();
                KeyedObjectCollection<AxMethod> keyO = axClass.Methods;
                //
                bool changes = false;

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

                            changes = true;
                        }

                    }
                }

                if (changes)
                {
                    //var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                    var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService; // metaModelProviders.CurrentMetaModelService;
                    // Getting the model will likely have to be more sophisticated, such as getting the model of the project and checking
                    // if the object has the same model.
                    // But this shold do for demonstration.
                    ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Classes.GetModelInfo(axClass.Name).FirstOrDefault<ModelInfo>();
                    //
                    axClass.Methods = keyO;
                    metaModelService.UpdateClass(axClass, new ModelSaveInfo(model));
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


        public override void Run()
        {
            AxClass axClass = (AxClass)ClassItem.GetMetadataType();

            KeyedObjectCollection<AxMethod> keyO = axClass.Methods;

            if (keyO == null) return;

            IEnumerator<AxMethod> methodEnumerator = keyO.GetEnumerator();
            //

            bool changes = false;
            
            while (methodEnumerator.MoveNext())
            {
                AxMethod axMethod = methodEnumerator.Current;
  
                string newMethod = FindText(axMethod.Source);
                //
                if (newMethod != axMethod.Source)
                {
                    methodEnumerator.Current.Source = newMethod;

                    changes = true;
                }

                CoreUtility.DisplayInfo(newMethod);
            }
            
            if (changes)
            {
                //var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
                var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService; // metaModelProviders.CurrentMetaModelService;
                // Getting the model will likely have to be more sophisticated, such as getting the model of the project and checking
                // if the object has the same model.
                // But this shold do for demonstration.
                ModelInfo model = DesignMetaModelService.Instance.CurrentMetadataProvider.Classes.GetModelInfo(axClass.Name).FirstOrDefault<ModelInfo>();
                //
                axClass.Methods = keyO;
                metaModelService.UpdateClass(axClass, new ModelSaveInfo(model));
            }
        }

        protected string FindText(string source)
        {
            
            StringBuilder builder = new StringBuilder(source);
            AsParseLabel parseLabel = new AsParseLabel();

            List<string> mylist = parseLabel.ParseString(source);

            List<string>.Enumerator e = mylist.GetEnumerator();
            while (e.MoveNext())
            {
                string text = e.Current;
                string newLabel = LabelManager.CreateLabel(text);

                if (text != null && !text.Equals(newLabel))
                {
                    builder.Replace(text, newLabel);
                }
            }

            return builder.ToString();
        }

        public CreateLabels_Class(ClassItem classItem)
        {
            ClassItem = classItem;
        }
    }
}