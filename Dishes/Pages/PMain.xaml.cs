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
    /// Логика взаимодействия для PMain.xaml
    /// </summary>
    public partial class PMain : Page
    {
        Product currentProduct;
        ShowMessages showMessages = new ShowMessages();
        public PMain()
        {
            InitializeComponent();
            LV_Products.ItemsSource = DataBase.entities.Products.OrderBy(p => p.CostProduct).ToList();

            List<Entities.Category> categories = new List<Entities.Category>();
            categories.Add(new Entities.Category() { NameCategory = "Все категории" });
            categories.AddRange(DataBase.entities.Categories.OrderBy(c => c.NameCategory));
            CB_Filter.ItemsSource = categories;

            List<String> sorting = new List<String>();
            sorting.Add("По возрастанию");
            sorting.Add("По убыванию");
            CB_Sort.ItemsSource = sorting;

            TB_Finded_Records.Text = "Найдено " + DataBase.entities.Products.Count()
    + " записей из " + DataBase.entities.Products.Count();
        }

        private void TB_Find_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshProdcuts();
        }

        private void CB_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshProdcuts();
        }

        private void CHB_Is_In_Warehouse_Checked(object sender, RoutedEventArgs e)
        {
            RefreshProdcuts();
        }

        private void CHB_Is_In_Warehouse_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshProdcuts();
        }

        public void RefreshProdcuts()
        {
            var allProducts = DataBase.entities.Products.ToList();

            if (CHB_Is_In_Warehouse.IsChecked == true)
            {
                allProducts = allProducts.Where(p => p.QuantityInWarehouse > 0).ToList();
            }

            if (!string.IsNullOrEmpty(TB_Find.Text.ToLower()))
            {
                allProducts = allProducts.Where(p => p.NameProduct.ToLower().Contains(TB_Find.Text.ToLower())
                || p.ManufacturerProduct.ToLower().Contains(TB_Find.Text)
                || Convert.ToString(p.ProductID).Contains(TB_Find.Text)
                || Convert.ToString(p.DiscountProduct).Contains(TB_Find.Text)
                || Convert.ToString(p.CostProduct).Contains(TB_Find.Text))
                    .ToList();
            }

            if (CB_Filter.SelectedItem != null && ((Category)CB_Filter.SelectedItem).IDCategory != 0)
            {
                Category selectedCategory = (Category)CB_Filter.SelectedItem;

                if (selectedCategory.NameCategory != "Все категории")
                {
                    allProducts = allProducts.Where(p => p.Category.IDCategory == selectedCategory.IDCategory).ToList();
                }
            }


            if(CB_Sort.SelectedItem != null)
            {
                if (CB_Sort.SelectedItem.ToString() == "По возрастанию")
                {
                    allProducts = allProducts.OrderBy(p => p.CostProduct).ToList();
                }
                else if (CB_Sort.SelectedItem.ToString() == "По убыванию")
                {
                    allProducts = allProducts.OrderByDescending(p => p.CostProduct).ToList();
                }
            }

            LV_Products.ItemsSource = allProducts;
            TB_Finded_Records.Text = "Найдено " + allProducts.Count.ToString()
                + " записей из " + DataBase.entities.Products.Count();
        }

        private void CB_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshProdcuts();
        }

        private void BTNAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PAddProduct(this));
        }

        private void BTNChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            currentProduct = (Product)LV_Products.SelectedItem;
            if(currentProduct != null)
            {
                NavigationService.Navigate(new PChangeProduct(this, currentProduct));
            }
            else
            {
                showMessages.ShowError("Выберите продукт из списка!");
                return;
            }
        }

        private void BTNDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}