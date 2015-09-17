using System;
using System.IO;
using LINQPad;

namespace _04_Services.WebApi.Extensions
{
    /// <summary>
    ///     Extension to make a object dump with help of linqPad
    /// </summary>
    public static class ObjectDumpLinqPad
    {
        /// <summary>
        ///     create the dump with help of LinqPad
        /// </summary>
        /// <param name="objectToSerialize">object under inspection</param>
        /// <param name="filePath">file to the file dump</param>
        public static void DumpWithLinqPad(this Object objectToSerialize, string filePath)
        {
            if (objectToSerialize == null) return;

            string objectName = ObjectDumpHelper.GetObjectName(objectToSerialize);


            if (string.IsNullOrEmpty(filePath))
            {
                filePath =
                    ObjectDumpHelper.CleanFileName(string.Format("{0}_{1:yyyy-M-d-HH-mm-ss-Z}.html", objectName,
                        DateTime.Now));
            }

            else if (Directory.Exists(filePath))
            {
                filePath = Path.Combine(filePath,
                    ObjectDumpHelper.CleanFileName(string.Format("{0}_{1:yyyy-M-d-HH-mm-ss-fff}.html", objectName,
                        DateTime.Now)));
            }
            else
            {
                //replace unsafe characters
                filePath = ObjectDumpHelper.CleanFileName(filePath);
            }

            using (TextWriter writer = Util.CreateXhtmlWriter(true))
            {
                writer.Write(objectToSerialize);
                File.WriteAllText(filePath, writer.ToString());
            }
        }
    }
}