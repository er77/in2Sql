using System;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;


namespace SqlEngine
{
    public partial class SqlEngine
    {
        public static Excel.Application CurrExcelApp;
 

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }

        private void SqlEngine_Startup(object sender, EventArgs e)
        {
            CurrExcelApp = Application;
        }

        private static void SqlEngine_Shutdown(object sender, EventArgs e)
        {
            CurrExcelApp = null;
            sTool.RunGarbageCollector();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += SqlEngine_Startup;
            Shutdown += SqlEngine_Shutdown;

             
        }


        
        #endregion
    }
}
