using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace SqlEngine
{
    public partial class SqlEngine
    {
        public static Excel.Application currExcelApp;
 

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }

        private void SqlEngine_Startup(object sender, System.EventArgs e)
        {
            currExcelApp = this.Application;
        }

        private void SqlEngine_Shutdown(object sender, System.EventArgs e)
        {
            currExcelApp = null;
            sTool.RunGarbageCollector();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(SqlEngine_Startup);
            this.Shutdown += new System.EventHandler(SqlEngine_Shutdown);

             
        }


        
        #endregion
    }
}
