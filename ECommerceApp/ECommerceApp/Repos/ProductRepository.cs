﻿using ECommerceApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Repos
{
    public static class ProductRepository
    {
        public static bool DbExist()
        {
            return File.Exists("ProductsDb.sqlite");
        }

        public static void InitializeDatabase()
        {
            if (!DbExist())
            {
                SQLiteConnection.CreateFile("ProductsDb.sqlite");
            }

            using (var m_dbConnection = new SQLiteConnection("Data Source=ProductsDb.sqlite"))
            {
                m_dbConnection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS Products (
                            Id INTEGER PRIMARY KEY,
                            Name VARCHAR(200) NOT NULL,
                            Type VARCHAR(200),
                            Sku VARCHAR(200),
                            RegularPrice DECIMAL(10,5),
                            StockQuantity INTEGER,
                            UnitQuantity INTEGER,
                            Total DECIMAL(10,5))";

                using (var command = new SQLiteCommand(sql, m_dbConnection))
                {
                    command.ExecuteNonQuery();
                }


            }
        }

        public static int AddProduct(Product product)
        {
            using (var connection = new SQLiteConnection("Data Source=ProductsDb.sqlite"))
            {
                connection.Open();
                var checkQuery = "SELECT COUNT(*) FROM Products WHERE Id = @Id";
                using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Id", product.Id);
                    var exists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                    if (exists)
                    {
                        return -1; 
                    }
                }
            }

            const string query = "INSERT INTO Products(Id, Name, Type, Sku, RegularPrice, StockQuantity, UnitQuantity, Total) VALUES(@Id, @Name, @Type, @Sku, @RegularPrice, @StockQuantity, @UnitQuantity, @Total)";
            var args = new Dictionary<string, object>
        {
            {"@Id", product.Id },
            {"@Name", product.Name},
            {"@Type", product.Type},
            {"@Sku", product.Sku},
            {"@RegularPrice", product.RegularPrice},
            {"@StockQuantity", product.StockQuantity},
            {"@UnitQuantity", product.UnitQuantity},
            {"@Total", product.Total}
        };
            return ExecuteWrite(query, args);
        }

        public static List<Product> GetProductsFromDatabase()
        {
            try
            {
                using (var m_dbConnection = new SQLiteConnection("Data Source=ProductsDb.sqlite"))
                {
                    m_dbConnection.Open();
                    string query = "SELECT * FROM Products";

                    using (var sqlCom = new SQLiteCommand(query, m_dbConnection))
                    {
                        var sda = new SQLiteDataAdapter(sqlCom);
                        var dt = new DataTable();
                        sda.Fill(dt);

                        var products = new List<Product>();

                        foreach (DataRow row in dt.Rows)
                        {
                            var product = new Product()
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Name = row["Name"].ToString(),
                                Type = row["Type"].ToString(),
                                Sku = row["Sku"].ToString(),
                                RegularPrice = Convert.ToDecimal(row["RegularPrice"]),
                                StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                                UnitQuantity = Convert.ToInt32(row["UnitQuantity"]),
                                Total = Convert.ToDecimal(row["Total"])
                            };
                            products.Add(product);
                        }
                        return products;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products from database", ex);
            }
        }

        private static int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;

            using (var con = new SQLiteConnection("Data Source=ProductsDb.sqlite"))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }
    }

}
