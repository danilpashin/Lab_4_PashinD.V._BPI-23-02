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
        private DateTime currBirthday = DateTime.Today;
        public DateTime CurrBirthday
        {
            get { return currBirthday; }
            set
            {
                currBirthday = value;
                OnPropertyChanged(nameof(CurrBirthday));
            }
        }
        private RelayCommand dateChange;
        public RelayCommand DateChange
        {
            get
            {
                return dateChange ??
                    (dateChange = new RelayCommand(obj =>
                    {
                        
                    }));
            }
        }
        private Visibility isVisible = Visibility.Visible;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        private Visibility isNotVisible = Visibility.Hidden;
        public Visibility IsNotVisible
        {
            get {
                return isNotVisible;
            }
            set
            {
                isNotVisible = value;
                OnPropertyChanged(nameof(IsNotVisible));
            }
        }

        private RelayCommand dateViewChange;
        public RelayCommand DateViewChange
        {
            get 
            { return dateViewChange ?? 
                    (dateViewChange = new RelayCommand(obj =>
                    {
                        if (IsVisible == Visibility.Visible)
                        {
                            IsVisible = Visibility.Hidden;
                            IsNotVisible = Visibility.Visible;
                        }
                        else
                        {
                            IsVisible = Visibility.Visible;
                            IsNotVisible = Visibility.Hidden;
                        }
                    })); 
            }
        }

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


        //public void tbBirthday_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (tbBirthday.Visibility == Visibility.Hidden)
        //    {
        //        ClBirthday.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        ClBirthday.Visibility = Visibility.Hidden;
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
