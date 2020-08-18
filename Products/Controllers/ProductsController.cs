using Newtonsoft.Json;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [HttpGet]
        public ActionResult Index(List<Product> products)
        {
            products = new List<Product>();

            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                Helpers.ProductsHelper.ReadFile(ref products);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Products", sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"].ToString());
                        product.Name = reader["Name"].ToString();
                        product.Category = reader["Category"].ToString();
                        product.Manufacturer = reader["Manufacturer"].ToString();
                        product.Supplier = reader["Supplier"].ToString();
                        product.Price = Convert.ToDouble(reader["Price"].ToString());
                        product.Description = reader["Description"].ToString();

                        products.Add(product);
                    }
                }
            }

            return View(products);
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                List<Product> items = new List<Product>();
                using (StreamReader r = new StreamReader(ConfigurationManager.AppSettings["jsonFileAddress"]))
                {
                    Helpers.ProductsHelper.ReadFile(ref items);
                    List<int> ids = new List<int>();
                    foreach(var i in items)
                    {
                        ids.Add(i.Id);
                    }
                    ids.Sort();
                    product.Id = ids.Last() + 1;

                    items.Add(product);
                }
                Helpers.ProductsHelper.WriteToFile(items);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into Products values('" + product.Name +
                    "','" + product.Category +
                    "','" + product.Manufacturer +
                    "','" + product.Supplier +
                    "'," + product.Price.ToString() +
                    ",'" + product.Description +
                    "')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            List<Product> products = new List<Product>();

            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                Helpers.ProductsHelper.ReadFile(ref products);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Products where id="+id.ToString(), sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"].ToString());
                        product.Name = reader["Name"].ToString();
                        product.Category = reader["Category"].ToString();
                        product.Manufacturer = reader["Manufacturer"].ToString();
                        product.Supplier = reader["Supplier"].ToString();
                        product.Price = Convert.ToDouble(reader["Price"].ToString());
                        product.Description = reader["Description"].ToString();

                        products.Add(product);
                    }
                }
            }

            Product editProduct = products.Find(delegate (Product p) { return p.Id == id; });

            return View(editProduct);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product newProduct)
        {
            List<Product> products = new List<Product>();
            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                Helpers.ProductsHelper.ReadFile(ref products);

               foreach(Product product in products)
                {
                    if(product.Id==id)
                    {
                        product.Name = newProduct.Name;
                        product.Category = newProduct.Category;
                        product.Supplier = newProduct.Supplier;
                        product.Manufacturer = newProduct.Manufacturer;
                        product.Description = newProduct.Description;
                        product.Price = newProduct.Price;
                    }
                }

                Helpers.ProductsHelper.WriteToFile(products);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("update Products set " +
                    "Name='" + newProduct.Name +
                    "',Category='" + newProduct.Category +
                    "',Manufacturer='" + newProduct.Manufacturer +
                    "',Supplier='" + newProduct.Supplier +
                    "',Price=" + newProduct.Price.ToString() +
                    ",Description='" + newProduct.Description +
                    "' where id=" + id.ToString(), sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            List<Product> products = new List<Product>();

            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                Helpers.ProductsHelper.ReadFile(ref products);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Products where id=" + id.ToString(), sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"].ToString());
                        product.Name = reader["Name"].ToString();
                        product.Category = reader["Category"].ToString();
                        product.Manufacturer = reader["Manufacturer"].ToString();
                        product.Supplier = reader["Supplier"].ToString();
                        product.Price = Convert.ToDouble(reader["Price"].ToString());
                        product.Description = reader["Description"].ToString();

                        products.Add(product);
                    }
                }
            }

            Product delProduct = products.Find(delegate (Product p) { return p.Id == id; });

            return View(delProduct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Product> products = new List<Product>();
            if (ConfigurationManager.AppSettings["readDataFromJson"] == "true")
            {
                using (StreamReader r = new StreamReader(ConfigurationManager.AppSettings["jsonFileAddress"]))
                {
                    Helpers.ProductsHelper.ReadFile(ref products);
                }

                Product product = products.Find(delegate (Product p) { return p.Id == id; });
                products.Remove(product);

                Helpers.ProductsHelper.WriteToFile(products);
            }

            else
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from Products where id=" + id.ToString(), sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}