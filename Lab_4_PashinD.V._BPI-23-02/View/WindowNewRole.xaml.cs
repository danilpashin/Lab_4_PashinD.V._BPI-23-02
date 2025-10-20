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
        private int id;
        private string nameRole;

        public int Id { 
            get => id; 
            set 
            {
                id = value;
            } 
        }

        public string NameRole
        {
            get => nameRole;
            set
            {
                nameRole = value;
            }
        }

        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void SaveChange(object sender, RoutedEventArgs e)
        {
            Id = Convert.ToInt32(IdRoleTBox.Text);
            NameRole = RoleTBox.Text;
            this.DialogResult = true;
            Close();
        }
    }
}
