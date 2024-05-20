using OTKLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OTKApp
{
    /// <summary>
    /// Логика взаимодействия для CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        ObservableCollection<Manufacturer> manufacturers { get; set; } = new ObservableCollection<Manufacturer>();
        public CreateProductWindow()
        {
            InitializeComponent();
            _ = GetCategotyResponse();
            _ = GetManufacturerResponse();
            cbCategory.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = categories
            });
            cbManufacturer.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = manufacturers
            });

        }
        private async Task GetCategotyResponse()
        {
            dynamic result = await Request.GetJsonArrayFromHttpServer(Server.CategoryURL);
            Console.WriteLine(result.GetType());
            foreach (var item in result.data)
            {
                categories.Add(new Category(item.category.ToString()));
            }
        }
        private async Task GetManufacturerResponse()
        {
            dynamic result = await Request.GetJsonArrayFromHttpServer(Server.ManufacturerURL);
            foreach (var item in result.data)
            {
                manufacturers.Add(new Manufacturer(item.manufacturer.ToString()));
            }
        }

        private void Button_CreateProduct(object sender, RoutedEventArgs e)
        {
            var data = new JsonObject();
            data["article"] = int.Parse(tbArticle.Text);
            data["name"] = tbName.Text;
            data["category"] = (cbCategory.SelectedItem as Category).Name;
            data["manufacturer"] = (cbManufacturer.SelectedItem as Manufacturer).Name;
            data["price"] = int.Parse(tbPrice.Text);
            data["image"] = tbImage.Text;
            data["description"] = tbDescription.Text;

            _ = PostResponse(data);
        }
        private async Task PostResponse(object data)
        {
            var result = await Request.PostRequest(Server.ProductURL, data);
            if (result)
                this.Close();   
        }
    }
}
