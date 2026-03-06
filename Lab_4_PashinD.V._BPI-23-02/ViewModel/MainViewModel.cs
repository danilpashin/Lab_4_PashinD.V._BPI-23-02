using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private RelayCommand openEmployee;
        public RelayCommand OpenEmployee
        {
            get
            {
                return openEmployee ??
                (openEmployee = new RelayCommand(obj =>
                {
                    WindowEmployee wEmployee = new WindowEmployee()
                    {
                        Title = "Сотрудники",
                    };
                    wEmployee.Show();
                }));
            }
        }

        private RelayCommand openRoles;
        public RelayCommand OpenRoles
        {
            get
            {
                return openRoles ??
                (openRoles = new RelayCommand(obj =>
                {
                    WindowRole wRole = new WindowRole()
                    {
                        Title = "Должности",
                    };
                    wRole.Show();
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
