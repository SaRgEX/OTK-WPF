using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTKApp
{
    class PageControl
    {
        public static ProductPage product;
        public static ProductPage GetProductPage
        { 
            get
            {
                if (product == null)
                    product = new ProductPage();
                return product;
            }
        }
    }
}
