using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using MySql.Data.MySqlClient;

namespace Salon.Models.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Customer.DeleteAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=adam_titus_tests;";
    }
    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Stylist()
    {
      Stylist firstStylist = new Stylist("Paul");
      Stylist secondStylist = new Stylist("Paul");
      Assert.AreEqual(firstStylist, secondStylist);
    }
    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("Paul");
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToStylists_Id()
    {
      Stylist testStylist = new Stylist("Paul");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.Id;
      int testId = testStylist.Id;
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Paul");
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.Id);
      Assert.AreEqual(testStylist, foundStylist);
    }
  }
}
