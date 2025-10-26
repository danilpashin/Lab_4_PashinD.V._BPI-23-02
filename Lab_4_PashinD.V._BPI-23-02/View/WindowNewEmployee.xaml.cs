using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class WindowNewEmployee : Window, IDataErrorInfo
    {
        private bool res = true;
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

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                int num;
                if (!int.TryParse(IdTBox.Text, out num))
                {
                    error = "Некорректный код!";
                }

                if (error != String.Empty)
                {
                    res = false;
                    error = String.Empty;
                }
                else res = true;

                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
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
    }
}
