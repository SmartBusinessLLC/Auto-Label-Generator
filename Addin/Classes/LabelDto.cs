namespace Addin
{
    public class LabelDto
    {
        public string Label;
        public string LabelId;
        public string LabelDescription;
        public bool IsExisted;

        public string PrefixedLabelText;
        public string ObjectName;

        public string LabelFileName;

        public LabelDto(string labelId, string label, bool isExisted)
        {
            Label = label;
            LabelId = labelId;
            IsExisted = isExisted;
            LabelDescription = string.Empty;
        }

        public LabelDto(string labelId, string label, bool isExisted, string prefixedLabelText, string objectName)
        {
            Label = label;
            LabelId = labelId;
            IsExisted = isExisted;
            LabelDescription = string.Empty;

            PrefixedLabelText = prefixedLabelText;
            ObjectName = objectName;
        }

        public LabelDto(string labelId, string label, bool isExisted, string prefixedLabelText, string objectName, string labelFileName)
        {
            Label = label;
            LabelId = labelId;
            IsExisted = isExisted;
            LabelDescription = string.Empty;

            PrefixedLabelText = prefixedLabelText;
            ObjectName = objectName;

            LabelFileName = labelFileName;
        }

        public string ToStringLabel()
        {
            return $"{LabelId}={Label}, {LabelDescription}, {IsExisted}, {PrefixedLabelText}, {ObjectName}";
        }
    }
}
