using OTKLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();
        public ProductPage()
        {
            InitializeComponent();

            _ = GetResponse();

            lvProducts.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = products
            });
        }
        private async Task GetResponse()
        {
            dynamic result = await Request.GetJsonArrayFromHttpServer(Server.ProductURL);
            if (result.data == null)
                return;
            foreach (var item in result.data)
            {
                products.Add(new Product(
                    Convert.ToInt32(item.article),
                    item.name.ToString(),
                    item.category.ToString(),
                    item.manufacturer.ToString(),
                    Convert.ToInt32(item.price) / 100,
                    "http://localhost/" + item.image.ToString(),
                    item.description.ToString(),
                    item.amount.ToString()));
            }
        }

        private void Button_CreateNewProduct(object sender, RoutedEventArgs e)
        {
            CreateProductWindow createProduct = new CreateProductWindow();
            createProduct.Show();
            createProduct.Closed += ProductWindow_Closed;
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProducts.Items.Count == 0) return;
            ProductWindow productWindow = new ProductWindow();
            productWindow.cbManufacturer.SelectedItem = (lvProducts.SelectedItem as Product).Manufacturer;
            productWindow.cbCategory.SelectedItem = (lvProducts.SelectedItem as Product).Category;
            productWindow.tbArticle.Text = (lvProducts.SelectedItem as Product).Article.ToString();
            productWindow.tbName.Text = (lvProducts.SelectedItem as Product).Name;
            productWindow.tbDescription.Text = (lvProducts.SelectedItem as Product).Description;
            productWindow.tbPrice.Text = (lvProducts.SelectedItem as Product).Price.ToString();
            productWindow.tbImage.Text = (lvProducts.SelectedItem as Product).Image;
            productWindow.Show();
            productWindow.Closed += ProductWindow_Closed;
        }

        private bool Validate(string text)
        {
            if (String.IsNullOrEmpty(text)) return false;
            return true;
        }

        private void ProductWindow_Closed(object? sender, EventArgs e)
        {
            products.Clear();
            _ = GetResponse();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("http://localhost/images/empty.png", UriKind.Absolute));
        }
    }
}
