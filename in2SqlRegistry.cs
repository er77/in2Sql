﻿using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SqlEngine
{
    public static class in2SqlRegistry
    {
        private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;

        private static string Encrypt(this string plainText)
        {
            if (plainText == null) return null;

            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, Scope);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        private static string Decrypt(this string cipher)
        {
            if (cipher == null) return null;

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
            return Encoding.Unicode.GetString(decrypted);
        }



        public static string getLocalRegValue(RegistryKey vCurrRegKey, string vValue)
        {
            try
            {
                string vGetLocalRegValue = null;
                if (vCurrRegKey == null)
                    return null;

                if ((vCurrRegKey.GetValue(vValue, null) == null) == false)
                {
                    vGetLocalRegValue = vCurrRegKey.GetValue(vValue).ToString(); ;
                }
                if (vValue.Contains("Password"))
                    vGetLocalRegValue = Decrypt(vGetLocalRegValue);
                return vGetLocalRegValue;
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "in2SQLRegistry.getLocalRegValue");
                return null;
            }
        }


        public static void setLocalValue(string vOdbcName, string vParameter, string vValue)
        {
            RegistryKey vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql", true);
            try
            {
                if (vCurrRegKey == null)
                    vCurrRegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\in2sql");

                if (vParameter.Contains("Password"))
                    vValue = Encrypt(vValue);

                vCurrRegKey.SetValue(vOdbcName + '.' + vParameter, vValue);

                vCurrRegKey.Close();
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "in2SQLRegistry.etLocalValue");
            }
        }

  /*      public static void schedulueDeleteFile(string vFileName )
        {
            RegistryKey vCurrRegKey = Registry.LocalMachine.OpenSubKey(@"CurrentControlSet\Control\Session Manager", true);
            try
            {
                if (vCurrRegKey == null)
                    vCurrRegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\in2sql");

                if (vParameter.Contains("Password"))
                    vValue = Encrypt(vValue);

                vCurrRegKey.SetValue(vOdbcName + '.' + vParameter, vValue);

                vCurrRegKey.Close();
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "in2SQLRegistry.etLocalValue");
            }
        }
*/

        public static void delLocalValue(string vOdbcName )
        {
            RegistryKey vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql\", true);
            try
            {
                if (vCurrRegKey == null)
                    return;
                vOdbcName = vOdbcName.Replace("#", "");
                vOdbcName = vOdbcName.Replace("$", "");

                if (vOdbcName.Contains("CloudCH"))
                {
                    vCurrRegKey.DeleteValue(vOdbcName + ".Login");
                    vCurrRegKey.DeleteValue(vOdbcName + ".Password");
                    vCurrRegKey.DeleteValue(vOdbcName + ".Url");
                }

                vCurrRegKey.Close();
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "in2SQLRegistry.etLocalValue");
            }
        }

    }
}

