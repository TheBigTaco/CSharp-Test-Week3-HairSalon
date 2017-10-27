using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Salon.Models
{
  public class Customer
  {
    public string Name {get;}
    public int StylistId {get;}
    public int Id {get;set;}

    public Customer(string name, int stylistId, int id = 0)
    {
      Name = name;
      StylistId = stylistId;
      Id = id;
    }

    public override bool Equals(System.Object otherCustomer)
    {
      if(!(otherCustomer is Customer))
      {
        return false;
      }
      else
      {
        Customer newCustomer = (Customer) otherCustomer;
        return this.Id.Equals(newCustomer.Id);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public static void DeleteAll()
    {
    MySqlConnection conn = DB.Connection();
    conn.Open();

    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"DELETE FROM customers;";
    cmd.ExecuteNonQuery();

    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }
}
