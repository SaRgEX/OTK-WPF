using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OTKApp
{
    static class Server
    {

        public static string URL
        {
            get => "http://localhost:8080";
        }

        public static string SignInURL
        {
            get => $"{URL}/auth/sign-in";
        }

        public static string ProductURL
        {
            get => $"{URL}/products";
        }

        public static string IdentifiedProductURL(int id)
        {
            return $"{ProductURL}/{id}";
        }

        public static string CategoryURL
        {
            get => $"{URL}/category";
        }
        public static string ManufacturerURL
        {
            get => $"{URL}/manufacturer";
        }
    }
}
