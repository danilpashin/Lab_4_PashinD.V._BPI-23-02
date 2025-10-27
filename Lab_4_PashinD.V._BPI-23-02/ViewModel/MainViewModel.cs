using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class MainViewModel
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
    }
}
