using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();
        }

        private void SaveChange(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void CheckNameInput(object sender, TextCompositionEventArgs e)
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

        private void tbBirthday_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbBirthday.Visibility == Visibility.Hidden)
            {
                ClBirthday.Visibility = Visibility.Visible;
            }
            else
            {
                ClBirthday.Visibility = Visibility.Hidden;
            }
        }
    }
}
