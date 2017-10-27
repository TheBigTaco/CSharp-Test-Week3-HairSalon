using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Salon.Models
{
  public class Stylist
  {
    public string Name {get;}
    public int Id {get;set;}

    public Stylist(string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        return this.Id.Equals(newStylist.Id);
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
      cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `stylists` WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";

      while(rdr.Read())
      {
        stylistName = rdr.GetString(1);
        stylistId = rdr.GetInt32(0);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }

    public List<Customer> GetCustomers()
    {
      List<Customer> allStylistCustomers = new List<Customer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers WHERE stylist_id = @stylist_id ORDER BY name;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this.Id;
      cmd.Parameters.Add(stylistId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string customerName = rdr.GetString(1);
        int customerStylistId = rdr.GetInt32(2);
        int customerId = rdr.GetInt32(0);
        Customer newCustomer = new Customer(customerName, customerStylistId, customerId);
        allStylistCustomers.Add(newCustomer);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylistCustomers;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
