using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.ViewModel;
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
using System.Windows.Shapes;

namespace Lab_4_PashinD.V._BPI_23_02.View
{
    public partial class WindowNewEmployee : Window
    {
        private string selectedRole;
        public string SelectedRole { 
            get => selectedRole;
            set
            {
                selectedRole = value;
            }
        }
        public WindowNewEmployee(string sentRole)
        {
            InitializeComponent();
            RoleViewModel vmRole = new RoleViewModel();
            List<Role> roles = new List<Role>();
            foreach(Role r in vmRole.ListRole)
            {
                roles.Add(r);
            }
            CbRole.ItemsSource = roles;
            CbRole.DisplayMemberPath = "NameRole";
            selectedRole = sentRole;
            int index = 0;
            foreach (Role r in vmRole.ListRole)
            {
                if (selectedRole == r.NameRole) { index = r.Id-1; }
            }
            CbRole.SelectedIndex = index;
        }

        private void SaveChange(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void NoSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
