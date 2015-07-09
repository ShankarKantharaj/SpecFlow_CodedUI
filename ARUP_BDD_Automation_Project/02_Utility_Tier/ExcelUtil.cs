using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;
using System.Xml;
using ICSharpCode.SharpZipLib;
using System.Data;
using System.IO;

namespace FreshCodedUIProject._02_Utility_Tier
{
    public class ExcelUtil
    {
        public static DataRow oCurrentDataRow;

        public static DataTable ExcelToTable(string strDataFile, string strDataSheet)
        {
            FileStream oFileStream = File.Open(strDataFile, FileMode.Open, FileAccess.Read);

            IExcelDataReader oExlReader = ExcelReaderFactory.CreateOpenXmlReader(oFileStream);

            oExlReader.IsFirstRowAsColumnNames = true;

            DataSet oDataSet = oExlReader.AsDataSet();

            DataTableCollection oDataCollTable = oDataSet.Tables;

            DataTable oResultTable = oDataCollTable[strDataSheet];

            oExlReader.Close();

            return oResultTable;
        }

        public static string GetData(string strColumnName)
        {
            try
            {
                return ExcelUtil.oCurrentDataRow[strColumnName].ToString(); 
            }
            catch(DataException e)
            {
                Console.WriteLine(e.StackTrace);
                return "";
            }
        }

        //public static 
    }
}
