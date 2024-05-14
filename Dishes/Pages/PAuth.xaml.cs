using Dishes.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Dishes.Pages
{
    /// <summary>
    /// Логика взаимодействия для PAuth.xaml
    /// </summary>
    public partial class PAuth : Page
    {
        ShowMessages showMessages = new ShowMessages();
        public PAuth()
        {
            InitializeComponent();
        }

        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            string login = TB_Login.Text;
            string password = PB_Password.Password;

            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                showMessages.ShowError("Пожалуйста, заполните все поля");
            }

            User loginUser = DataBase.entities.Users.FirstOrDefault(u => u.LoginUser == login 
            && u.PasswordUser == password);

            if(loginUser == null)
            {
                showMessages.ShowError("Неверные данные!");
            }
            else if(loginUser.RoleUser == 1)
            {
                Properties.Settings.Default.role = 1;
                showMessages.ShowInfo("Вы вошли как администратор");
                NavigationService.Navigate(new PMain());
            }
            else if (loginUser.RoleUser == 2)
            {
                Properties.Settings.Default.role = 2;
                showMessages.ShowInfo("Вы вошли как менеджер");
                NavigationService.Navigate(new PMain());
            }
            else if (loginUser.RoleUser == 3)
            {
                Properties.Settings.Default.role = 3;
                showMessages.ShowInfo("Вы вошли как клиент");
                NavigationService.Navigate(new PMain());
            }
        }
    }
}
