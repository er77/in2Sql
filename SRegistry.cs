using System;
using Microsoft.Win32;
using System.Text;
using System.Security.Cryptography;

namespace SqlEngine
{
    public static class SRegistry
    {
        private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;

        private static string Encrypt(this string plainText)
        {
            if (plainText == null) return null;

            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            var encrypted = ProtectedData.Protect(data, null, Scope);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(this string cipher)
        {
            if (cipher == null) return null;

            //parse base64 string
            var data = Convert.FromBase64String(cipher);

            //decrypt data
            var decrypted = ProtectedData.Unprotect(data, null, Scope);
            return Encoding.Unicode.GetString(decrypted);
        }



        public static string GetLocalRegValue(RegistryKey vCurrRegKey, string vValue)
        {
            try
            {
                string vGetLocalRegValue = null;
                if (vCurrRegKey == null)
                    return null;

                if (vCurrRegKey.GetValue(vValue, null) == null == false)
                {
                    vGetLocalRegValue = vCurrRegKey.GetValue(vValue).ToString(); 
                }
                if (vValue.Contains("Password"))
                    vGetLocalRegValue = Decrypt(vGetLocalRegValue);
                return vGetLocalRegValue;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "in2SQLRegistry.getLocalRegValue");
                return null;
            }
        }


        public static void SetLocalValue(string vOdbcName, string vParameter, string vValue)
        {
            var vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql", true);
            try
            {
                if (vCurrRegKey == null)
                    vCurrRegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\in2sql");

                if (vParameter.Contains("Password"))
                    vValue = Encrypt(vValue);

                if (vCurrRegKey == null) return;
                
                vCurrRegKey.SetValue(vOdbcName + '.' + vParameter, vValue);

                vCurrRegKey.Close();
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "in2SQLRegistry.etLocalValue");
            }
        }

        public static void DelLocalValue(string vOdbcName )
        {
            var vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql\", true);
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


                if (vOdbcName.Contains("Csv"))
                {
                    vCurrRegKey.DeleteValue(vOdbcName + ".Path");                    
                }

                vCurrRegKey.Close();
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "in2SQLRegistry.etLocalValue");
            }
        }

    }
}

