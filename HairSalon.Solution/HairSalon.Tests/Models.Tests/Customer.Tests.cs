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
    [TestMethod]
    public void Equals_OverrideTrueForSameParameters_Customer()
    {
      Customer firstCustomer = new Customer("Adam", 1);
      Customer secondCustomer = new Customer("Adam", 1);

      Assert.AreEqual(firstCustomer, secondCustomer);
    }
    [TestMethod]
    public void Save_SaveCustomerToDatabase_CustomerList()
    {
      Customer firstCustomer = new Customer("Adam", 1);
      firstCustomer.Save();

      List<Customer> result = Customer.GetAll();
      List<Customer> testList = new List<Customer> {firstCustomer};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      Customer firstCustomer = new Customer("Adam", 1);
      firstCustomer.Save();

      Customer savedCustomer = Customer.GetAll()[0];

      int result = savedCustomer.Id;
      int testId = firstCustomer.Id;

      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsCustomerInDatabase_Customer()
    {
      Customer firstCustomer = new Customer("Adam", 1);
      firstCustomer.Save();

      Customer foundCustomer = Customer.Find(firstCustomer.Id);

      Assert.AreEqual(firstCustomer, foundCustomer);
    }
    [TestMethod]
    public void GetCustomers_RetrievesAllCustomersWithStylists_CustomerList()
    {
      Stylist testStylist = new Stylist("Paul");
      testStylist.Save();
      Customer firstCustomer = new Customer("Adam", testStylist.Id);
      Customer secondCustomer = new Customer ("Lisa", testStylist.Id);
      firstCustomer.Save();
      secondCustomer.Save();

      List<Customer> testCustomerList = new List<Customer> {firstCustomer, secondCustomer};
      List<Customer> resultCustomerList = testStylist.GetCustomers();

      CollectionAssert.AreEqual(testCustomerList, resultCustomerList);
    }
    [TestMethod]
    public void GetCustomers_MakeSureListOrderedByName_CustomerList()
    {
      Stylist testStylist = new Stylist("Paul");
      testStylist.Save();
      Customer firstCustomer = new Customer("Adam", testStylist.Id);
      Customer secondCustomer = new Customer ("Lisa", testStylist.Id);
      secondCustomer.Save();
      firstCustomer.Save();

      List<Customer> testCustomerList = new List<Customer> {firstCustomer, secondCustomer};
      List<Customer> resultCustomerList = testStylist.GetCustomers();

      CollectionAssert.AreEqual(testCustomerList, resultCustomerList);
    }
    [TestMethod]
    public void UpdateCustomer_UpdateCustomerInDatabase_Customer()
    {
      Customer firstCustomer = new Customer("Adam", 3);
      firstCustomer.Save();
      firstCustomer.UpdateCustomer("Adam Titus", firstCustomer.Id);
      Customer testCustomer = Customer.GetAll()[0];
      Assert.AreEqual("Adam Titus", testCustomer.Name);
    }
    [TestMethod]
    public void DeleteCustomer_DeleteCustomerFromDatabase_CustomerList()
    {
      Customer firstCustomer = new Customer("Adam", 3);
      Customer secondCustomer = new Customer ("Lisa", 3);
      secondCustomer.Save();
      firstCustomer.Save();
      Customer.DeleteCustomer(secondCustomer.Id);

      List<Customer> testCustomerList = new List<Customer> {firstCustomer};
      List<Customer> resultCustomerList = Customer.GetAll();

      CollectionAssert.AreEqual(testCustomerList, resultCustomerList);
    }
  }
}
