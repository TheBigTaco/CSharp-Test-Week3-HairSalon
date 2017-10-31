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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO customers (name, stylist_id) VALUES (@name, @stylist_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this.StylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Customer> GetAll()
    {
      List<Customer> allCustomers = new List<Customer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string customerName = rdr.GetString(1);
        int customerStylistId = rdr.GetInt32(2);
        int customerId = rdr.GetInt32(0);

        Customer newCustomer = new Customer(customerName, customerStylistId, customerId);
        allCustomers.Add(newCustomer);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCustomers;
    }

    public static Customer Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string customerName = "";
      int customerStylistId = 0;
      int customerId = 0;

      while(rdr.Read())
      {
        customerName = rdr.GetString(1);
        customerStylistId = rdr.GetInt32(2);
        customerId = rdr.GetInt32(0);
      }

      Customer newCustomer = new Customer(customerName, customerStylistId, customerId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newCustomer;
    }

    public void UpdateCustomer(string name, int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE customers SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@newName";
      newName.Value = name;
      cmd.Parameters.Add(newName);

      var rdr = cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteCustomer(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM customers WHERE id = @searchId";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
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
}
