using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class RoleWindowViewModel : INotifyPropertyChanged
    {
        private Role currRole;
        public Role CurrRole
        {
            get { return currRole; }
            set
            {
                currRole = value;
                OnPropertyChanged(nameof(CurrRole));
            }
        }
        public RoleWindowViewModel(int maxId)
        {
            CurrRole = new Role
            {
                Id = maxId,
                NameRole = string.Empty
            };
        }

        public RoleWindowViewModel(Role role)
        {
            CurrRole = role;
        }

        private RelayCommand saveChange;
        public RelayCommand SaveChange
        {
            get
            {
                return saveChange ??
                (saveChange = new RelayCommand(obj =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType().Name == "WindowNewRole")
                        {
                            if (currRole.IsValid())
                            {
                                window.DialogResult = true;
                                window.Close();
                                return;
                            }
                            else MessageBox.Show("Проверьте правильность заполнения полей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
