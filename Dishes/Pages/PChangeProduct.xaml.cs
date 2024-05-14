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
    /// Логика взаимодействия для PChangeProduct.xaml
    /// </summary>
    public partial class PChangeProduct : Page
    {
        private Product currentProduct = new Product();
        private PMain _page;
        ShowMessages showMessages = new ShowMessages();
        public PChangeProduct()
        {
            InitializeComponent();
        }

        public PChangeProduct(PMain page, Product currentProduct)
        {
            InitializeComponent();

            _page = page;

            this.currentProduct = currentProduct;

            TBNameProduct.Text = currentProduct.NameProduct;
            TBDescriptionProduct.Text = currentProduct.DescriptionProduct;
            TBManufacturerProduct.Text = currentProduct.ManufacturerProduct;
            TBCostProduct.Text = Convert.ToString(currentProduct.CostProduct);
            TBDiscountProduct.Text = Convert.ToString(currentProduct.DiscountProduct);
            TBQuantityProduct.Text = Convert.ToString(currentProduct.QuantityInWarehouse);

            CBCategoryProduct.ItemsSource = DataBase.entities.Categories.ToList();
            CBCategoryProduct.SelectedItem = DataBase.entities.Categories.FirstOrDefault(c => c.IDCategory == currentProduct.CategoryProduct);
        }

        private void BTNChange_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct != null)
            {
                currentProduct.NameProduct = TBNameProduct.Text;
                currentProduct.DescriptionProduct = TBDescriptionProduct.Text;
                currentProduct.ManufacturerProduct = TBManufacturerProduct.Text;

                double productCost;
                if (double.TryParse(TBCostProduct.Text, out productCost) && productCost >= 0)
                {
                    currentProduct.CostProduct = Convert.ToDouble(TBCostProduct.Text);
                }
                else
                {
                    showMessages.ShowError("Неккоректное значение цены продукта: не может быть меньше 0!");
                    return;
                }

                double discountProduct;
                if (double.TryParse(TBDiscountProduct.Text, out discountProduct) && discountProduct >= 0)
                {
                    currentProduct.DiscountProduct = Convert.ToDouble(TBDiscountProduct.Text);
                }
                else
                {
                    showMessages.ShowError("Неккоректное значение скидки продукта: не может быть меньше 0!");
                    return;
                }

                int quantityInWarehouse;
                if(int.TryParse(TBQuantityProduct.Text, out quantityInWarehouse) && quantityInWarehouse >= 0)
                {
                    currentProduct.QuantityInWarehouse = Convert.ToInt32(TBQuantityProduct.Text);
                }
                else
                {
                    showMessages.ShowError("Неккоректное значение числа продукта: не может быть меньше 0!");
                    return;
                }

                currentProduct.Category = (Category)CBCategoryProduct.SelectedItem;

                try
                {
                    DataBase.entities.SaveChanges();
                    showMessages.ShowInfo("Данные успешно обновлены!");
                    _page.RefreshProdcuts();
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    showMessages.ShowError($"Произошла ошибка при сохранение данных продукта: {ex.Message}");
                }
            }
            else
            {
                showMessages.ShowError("Выбранный отель не найден!");
            }
        }
    }
}
