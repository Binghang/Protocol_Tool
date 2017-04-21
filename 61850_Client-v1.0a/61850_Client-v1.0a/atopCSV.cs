using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
namespace _61850_Client_v1._0a
{
    public struct DataStruct61850
    {
        public string IEC61850_SERVERNAME;
        public string IEC61850_TAGNAME;
        public string IEC61850_DATATYPE;
        public string IEC61850_EXCHANGETYPE;
        public string IEC61850_FC;
        public string IEC61850_VALUE;
        public int ADDRESS;
        public string FUNCTION;
        public string RANGE;
    }

    public struct ColumnIndex61850
    {
        public int TAGNAME;
        public int DATATYPE;
        public int EXCHANGETYPE;
        public int FC;
        public int COMPAREFUNCTION;
        public int COMPAREADDRESS;
        public int COMPARERANGE;
    }


    public static class atopCSV
    {
        public static ColumnIndex61850 columnindex61850 = new ColumnIndex61850();
        
        public static void Load (string Path)
        {
            try
            {
                HSSFWorkbook WB = TransCSVFile(Path);

                if (WB != null && WB.GetSheetAt(0) != null)
                {
                    ISheet Data_Sheet = WB.GetSheetAt(0);
                    Get_Columns_Index(Data_Sheet.GetRow(0));

                    for (int i = 1; i < Data_Sheet.LastRowNum; i++)
                    {
                        if (Data_Sheet.GetRow(i) == null)
                            continue;
                        else
                            Add_Mapping_Data(Data_Sheet.GetRow(i));
                    }
                }
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
            }
        }


        /// <summary>
        /// CSV file transfer to WorkBook
        /// </summary>
        /// <param name="Path">File Path</param>
        /// <returns></returns>
        private static HSSFWorkbook TransCSVFile(string Path)
        {
            try
            {
                HSSFWorkbook wookBook;
                FileStream File = new FileStream(Path, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[File.Length];
                File.Read(bytes, 0, (int)File.Length);
                MemoryStream ms = new MemoryStream(bytes);
                wookBook = new HSSFWorkbook(ms);
                File.Close();
                return wookBook;
            }
            catch (Exception exError)
            {
                atopLog.WriteLog( atopLogMode.SystemError,exError.Message);
                throw;
            }
        }

        private static void Get_Columns_Index(IRow Row)
        {
            if (Row != null)
            {
                string[] InterFace = new string[0];
                foreach (ICell Cell in Row)
                {
                    if (Cell.StringCellValue.Contains("INTERFACE"))
                    {
                        InterFace = Cell.StringCellValue.Split('_');
                        /* 表示第一個column，代表Backend的Protocol */
                        if (Cell.ColumnIndex == 0)
                        {
                            switch (InterFace[0].ToString())
                            {
                                case "61850":
                                    columnindex61850.TAGNAME = Cell.ColumnIndex + 1;
                                    columnindex61850.DATATYPE = Cell.ColumnIndex + 2;
                                    columnindex61850.EXCHANGETYPE = Cell.ColumnIndex + 3;
                                    columnindex61850.FC = Cell.ColumnIndex + 4;
                                    break;
                                case "MODBUS":
                                    columnindex61850.COMPAREFUNCTION = Cell.ColumnIndex + 3;
                                    columnindex61850.COMPAREADDRESS = Cell.ColumnIndex + 5;
                                    columnindex61850.COMPARERANGE = Cell.ColumnIndex + 6;
                                    break;
                            }
                        }
                        else
                        {
                            switch (InterFace[0].ToString())
                            {
                                case "IEC61850":
                                    columnindex61850.TAGNAME = Cell.ColumnIndex + 1;
                                    columnindex61850.DATATYPE = Cell.ColumnIndex + 2;
                                    columnindex61850.EXCHANGETYPE = Cell.ColumnIndex + 3;
                                    columnindex61850.FC = Cell.ColumnIndex + 4;
                                    break;
                                case "MODBUS":
                                    columnindex61850.COMPAREFUNCTION = Cell.ColumnIndex + 1;
                                    columnindex61850.COMPAREADDRESS = Cell.ColumnIndex + 2;
                                    columnindex61850.COMPARERANGE = Cell.ColumnIndex + 3;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static string InsertTagName(string Path, string FC)
        {
            string[] Data = Path.Split('$');
            string ReturnPath = $"{Data[0]}{Data[1]}/{Data[2]}.{Data[3]}.{FC}.";
            for (int i = 4; i < Data.Length; i++)
            {
                if (i == Data.Length - 1)
                    ReturnPath += string.Format("{0}", Data[i]);
                else
                    ReturnPath += string.Format("{0}.", Data[i]);
            }
            return ReturnPath;
        }

        private static bool Add_Mapping_Data(IRow Row)
        {
            try
            {
                atopDataMapping.VisualMapping.Add(new DataStruct61850
                {
                    IEC61850_SERVERNAME = Row.Cells[columnindex61850.TAGNAME].ToString().Split('$')[0],
                    IEC61850_DATATYPE = atop61850DataType.GetDataType(Row.Cells[columnindex61850.DATATYPE].ToString()),
                    IEC61850_TAGNAME = InsertTagName(Row.Cells[columnindex61850.TAGNAME].ToString(), Row.Cells[columnindex61850.FC].ToString()),
                    IEC61850_EXCHANGETYPE = Row.Cells[columnindex61850.EXCHANGETYPE].ToString(),
                    IEC61850_FC = Row.Cells[columnindex61850.FC].ToString(),
                    ADDRESS = Convert.ToInt16(Row.Cells[columnindex61850.COMPAREADDRESS].ToString()),
                    FUNCTION = Get_Function_Name(Row.Cells[columnindex61850.COMPAREFUNCTION].ToString().Replace("Read", "").Trim()),
                    RANGE = Row.Cells[columnindex61850.COMPARERANGE].ToString()
                });
                return true;
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
                return false;
            }
        }

        private static string Get_Function_Name(string FN)
        {
            switch (FN)
            {
                case "Coils":
                    return "Coil";
                case "Discrete Inputs":
                    return "Disc";
                case "Holding Registers":
                    return "HReg";
                case "Input Registers":
                    return "IReg";
                case "Write Single/Multiple Coils":
                    return "WCoi";
                case "Write Single/Multiple Registers":
                    return "WHRe";
            }
            return FN;
        }

    }
}
