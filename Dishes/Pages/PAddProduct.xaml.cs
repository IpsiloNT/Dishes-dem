using Dishes.Entities;
using System;
using System.Collections.Generic;
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

namespace Dishes.Pages
{
    /// <summary>
    /// Логика взаимодействия для PAddProduct.xaml
    /// </summary>
    public partial class PAddProduct : Page
    {
        PMain _page;
        ShowMessages showMessages = new ShowMessages();
        public PAddProduct()
        {
            InitializeComponent();
        }

        public PAddProduct(PMain page)
        {
            InitializeComponent();

            _page = page;

            CBCategoryProduct.ItemsSource = DataBase.entities.Categories.OrderBy(c => c.NameCategory).ToList();
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            _page.RefreshProdcuts();

            double productCost;
            if (!double.TryParse(TBCostProduct.Text, out productCost) && productCost >= 0)
            {
                showMessages.ShowError("Неккоректное значение цены продукта: не может быть меньше 0!");
                return;
            }

            double discountProduct;
            if (!double.TryParse(TBDiscountProduct.Text, out discountProduct) && discountProduct >= 0)
            {
                showMessages.ShowError("Неккоректное значение скидки продукта: не может быть меньше 0!");
                return;
            }

            int quantityInWarehouse;
            if (!int.TryParse(TBQuantityProduct.Text, out quantityInWarehouse) && quantityInWarehouse >= 0)
            {
                showMessages.ShowError("Неккоректное значение числа продукта: не может быть меньше 0!");
                return;
            }
                try
            {
                DataBase.entities.Products.Add(new Product()
                {
                    NameProduct = TBNameProduct.Text,
                    DescriptionProduct = TBDescriptionProduct.Text,
                    ManufacturerProduct = TBManufacturerProduct.Text,
                    CostProduct = Convert.ToDouble(TBCostProduct.Text),
                    DiscountProduct = Convert.ToDouble(TBDiscountProduct.Text),
                    QuantityInWarehouse = Convert.ToInt32(TBQuantityProduct),
                    Category = (Category)CBCategoryProduct.SelectedItem,
                });

                DataBase.entities.SaveChanges();
                showMessages.ShowInfo("Новый продукт был успешно добавлен!");

                _page.RefreshProdcuts();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                showMessages.ShowError($"Произошла ошибка при добавлении продукта: {ex.Message}");
            }
            
        }
    }
}
