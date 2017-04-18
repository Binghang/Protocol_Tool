using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace _61850_Client_v1._0a
{
    public static class atopXML
    {
        private static XmlDocument doc = new XmlDocument();
        public static void Read(string Path)
        {
            try
            {
                doc.Load(Path);
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);

            }
        }

        public static List<int> GetTypeRange(string TypeName)
        {
            try
            {
                XmlNodeList main = ((doc.LastChild as XmlElement)).ChildNodes;
                if (doc == null)
                    return new List<int>();

                XmlNodeList Struct = main[3].ChildNodes;
                List<int> Return_Data = new List<int>();
                foreach (XmlNode item in Struct)
                {
                    Console.WriteLine($"Node name {item.Name.ToString()}");
                    if (item.Name.ToString() == "EnumType")
                    {
                        Console.WriteLine($"------------{item.Attributes[0].Value}------------");
                        foreach (XmlNode attr in item.ChildNodes)
                        {
                            if (attr.Name.Equals(TypeName))
                            {
                                /* 把列舉的Index加到List裡面 */
                                Return_Data.Add(Convert.ToInt16((attr.OuterXml.Split(' '))[1].Replace("ord=", "").ToString().Replace("\"", "")));
                            }
                            //Console.WriteLine($"Attr Name : {attr.Name.ToString()}");
                            //Console.WriteLine($"Attr Value : {attr.InnerText.ToString()}");
                            //int Number = 99999;
                            //if (attr.OuterXml.Split(' ')[1].Contains("ord"))
                            //    Number = Convert.ToInt16((attr.OuterXml.Split(' '))[1].Replace("ord=", "").ToString().Replace("\"", ""));
                            //Console.WriteLine($"Attr Number : {Number}");
                        }
                    }
                    else
                        continue;
                }
                return Return_Data;
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
                return new List<int>();
            }
        }
    }
}
