using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dishes.Entities
{
    internal class ShowMessages
    {
        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
