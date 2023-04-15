using Microsoft.VisualStudio.Services.Common.Internal;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;

using Moq;
using SqlEngine;

using System.Text;
using System.Security.Cryptography;

public class SRegistryTests
{
    [Fact]
    public void SetLocalValue_SetsValueInRegistry()
    {
        // Arrange
        var vOdbcName = "testOdbcName";
        var vParameter = "testParameter";
        var vValue = "testValue";

        // Act
        SRegistry.SetLocalValue(vOdbcName, vParameter, vValue);

        // Assert
        var key = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
        Assert.NotNull(key);

        var value = key.GetValue(vOdbcName + '.' + vParameter).ToString();
        //value = SRegistry.Decrypt(value);
        Assert.Equal(vValue, value);

        // Clean up
        key.DeleteValue(vOdbcName + '.' + vParameter);
    }

    [Fact]
    public void SetLocalValue_EncryptsPasswordValue()
    {
        // Arrange
        var vOdbcName = "testOdbcName";
        var vParameter = "Password";
        var vValue = "testPassword";

        // Act
        SRegistry.SetLocalValue(vOdbcName, vParameter, vValue);

        // Assert
        var key = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
        Assert.NotNull(key);

       // var encryptedValue = SRegistry.Encrypt(vValue);
        var value = key.GetValue(vOdbcName + '.' + vParameter).ToString();
        value = SRegistry.Decrypt(value);
        Assert.Equal(vValue, value);

        // Clean up
        key.DeleteValue(vOdbcName + '.' + vParameter);
    }

    [Fact]
    public void SetLocalValue_HandlesException()
    {
        // Arrange
        var vOdbcName = "testOdbcName";
        var vParameter = "testParameter";
        var vValue = "testValue";

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => SRegistry.SetLocalValue(vOdbcName, vParameter, vValue));
        Assert.Equal("in2SQLRegistry.SetLocalValue", ex.Message);
    }
}
