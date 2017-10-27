using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using MySql.Data.MySqlClient;

namespace Salon.Models.Tests
{
  [TestStylist]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll
    }
    [TestMethod]
    public void Method_Description_ExpectedValue()
    {
      Assert.AreEqual(var1, method(input));
    }
  }
}
