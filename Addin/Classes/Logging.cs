#region

using System.Collections.Generic;

#endregion

namespace Addin
{
    /// <summary>
    ///     Save created labels as logging
    /// </summary>
    public class Logging
    {
        /// <summary>
        ///     Created labels list
        /// </summary>
        protected List<string> Labels;

        /// <summary>
        ///     Initialize global variables
        /// </summary>
        public Logging()
        {
            Labels = new List<string>();
        }

        /// <summary>
        ///     Add new label to list
        /// </summary>
        /// <param name="singleLog">Log object</param>
        public void Add(Log singleLog)
        {
            string formatedLabel = $"({singleLog.labelFile}) {singleLog.labelId}: {singleLog.label}\n";

            Labels.Add(formatedLabel);
        }

        /// <summary>
        ///     Concatenates all label into logging message
        /// </summary>
        /// <returns></returns>
        public string GetLogging()
        {
            string ret = string.Empty;

            if (Labels.Count > 0)
            {
                ret += "The following labels were created:\n\n";

                foreach (string label in Labels)
                {
                    ret += $"{label}";
                }
            }
            else
            {
                ret += "No label created.";
            }


            return ret;
        }

        /// <summary>
        ///     Counts the list elements
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Labels.Count;
        }
    }
}