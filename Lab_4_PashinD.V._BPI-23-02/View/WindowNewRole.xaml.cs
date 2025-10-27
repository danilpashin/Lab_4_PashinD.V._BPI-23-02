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
using System.Windows.Shapes;

namespace Lab_4_PashinD.V._BPI_23_02.View
{
    /// <summary>
    /// Логика взаимодействия для WindowNewRole.xaml
    /// </summary>
    public partial class WindowNewRole : Window
    {
        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void SaveChange(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void CheckRoleInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && !char.IsControl(c) && !char.IsSeparator(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
