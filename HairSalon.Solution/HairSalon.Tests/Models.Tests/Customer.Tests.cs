using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using MySql.Data.MySqlClient;

namespace Salon.Models.Tests
{
  [TestClass]
  public class CustomerTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Customer.DeleteAll();
    }
    public CustomerTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=adam_titus_tests;";
    }
    [TestMethod]
    public void GetAll_CustomersEmptyAtFirst_0()
    {
      int result = Customer.GetAll().Count;
      Assert.AreEqual(0, result);
    }
  }
}