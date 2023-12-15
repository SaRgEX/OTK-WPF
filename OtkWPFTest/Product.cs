using OTKLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OTKApp
{
    partial class Product
    {
        public Product(
            int article,
            string name,
            string category,
            string manufacturer,
            int price,
            string image,
            string description)
        {
            Article = article;
            Name = name;
            Category = category;
            Manufacturer = manufacturer;
            Price = price;
            Image = image;
            Description = description;
        }

        public int Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
