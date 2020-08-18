using Newtonsoft.Json;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Products.Helpers
{
    public class ProductsHelper
    {
        public static void ReadFile(ref List<Product> items)
        {
            using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings["jsonFileAddress"]))
            {
                string json = streamReader.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Product>>(json);
            }
        }

        public static void WriteToFile(List<Product> items)
        {
            string json = JsonConvert.SerializeObject(items);
            using(StreamWriter streamWriter = new StreamWriter(ConfigurationManager.AppSettings["jsonFileAddress"]))
            {
                streamWriter.Write(json);
            }
        }
    }
}