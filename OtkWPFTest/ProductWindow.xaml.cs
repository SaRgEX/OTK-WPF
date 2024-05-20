using OTKLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Shapes;

namespace OTKApp
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        ObservableCollection<Manufacturer> manufacturers { get; set; } = new ObservableCollection<Manufacturer>();
        public ProductWindow()
        {
            InitializeComponent();
            cbCategory.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = categories
            });
            cbManufacturer.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = manufacturers
            });
            _ = GetCategotyResponse();
            _ = GetManufacturerResponse();
        }

        private void Button_DeleteProduct(object sender, RoutedEventArgs e)
        {
            int article = int.Parse(tbArticle.Text);
            _ = DeleteResponse(article);
        }

        private void Button_UpdateProduct(object sender, RoutedEventArgs e)
        {
            var data = new JsonObject();
            int article = 0;
            if (!Validate(tbArticle.Text))
                int.TryParse(tbArticle.Text, out article);
            if (!Validate(tbName.Text))
                data["name"] = tbName.Text;
            if (!Validate(cbCategory.Text))
                data["category"] = (cbCategory.SelectedItem as Category).Name;
            if (!Validate(cbManufacturer.Text))
                data["manufacturer"] = (cbManufacturer.SelectedItem as Manufacturer).Name;
            if (!Validate(tbPrice.Text))
                data["price"] = int.Parse(tbPrice.Text);
            if (!Validate(tbImage.Text))
                data["image"] = tbImage.Text;
            if (!Validate(tbDescription.Text))
                data["description"] = tbDescription.Text;
            if (!Validate(tbAmount.Text))
                data["amount"] = int.Parse(tbAmount.Text);
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            _ = PutResponse(article, content);
        }

        private bool Validate(string text)
        {
            return String.IsNullOrEmpty(text);
        }

        private async Task PutResponse(int article, HttpContent content)
        {
            var result = await Request.PutRequest(Server.ProductURL, article, content);
            if (result)
                this.Close();
        }

        private async Task DeleteResponse(int article)
        {
            var result = await Request.DeleteRequest(Server.ProductURL, article);
            if (result)
                this.Close();
        }

        private async Task GetCategotyResponse()
        {
            dynamic result = await Request.GetJsonArrayFromHttpServer(Server.CategoryURL);
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
    }
}
