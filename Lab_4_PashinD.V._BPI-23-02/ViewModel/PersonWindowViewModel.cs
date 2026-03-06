using Lab_4_PashinD.V._BPI_23_02.Helper;
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
    public class PersonWindowViewModel : INotifyPropertyChanged
    {
        private PersonDPO currPerson;
        public PersonDPO CurrPerson { get { return currPerson; } 
            set 
            {
                currPerson = value;
                OnPropertyChanged(nameof(CurrPerson));
            } 
        }
        public PersonWindowViewModel(int maxId) 
        {
            CurrPerson = new PersonDPO
            {
                Id = maxId,
                FirstName = string.Empty,
                LastName = string.Empty,
                Birthday = Convert.ToString(DateTime.Today.Day) + '.' + Convert.ToString(DateTime.Today.Month) + '.' + Convert.ToString(DateTime.Today.Year)
            };
        }
        public PersonWindowViewModel(PersonDPO per)
        {
            CurrPerson = per;
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
            get
            {
                return isNotVisible;
            }
            set
            {
                isNotVisible = value;
                OnPropertyChanged(nameof(IsNotVisible));
            }
        }
        private string currBirthday = Convert.ToString(DateTime.Today.Day) + '.' + Convert.ToString(DateTime.Today.Month) + '.' + Convert.ToString(DateTime.Today.Year);
        public string CurrBirthday
        {
            get { return currBirthday; }
            set
            {
                currBirthday = Convert.ToString(currBirthdayDT.Day) + '.' + Convert.ToString(currBirthdayDT.Month) + '.' + Convert.ToString(currBirthdayDT.Year);
                CurrPerson.Birthday = currBirthday;
                OnPropertyChanged(nameof(CurrBirthday));
            }
        }
        private DateTime currBirthdayDT = DateTime.Today;
        public DateTime CurrBirthdayDT
        {
            get { return Convert.ToDateTime(currBirthday); }
            set
            {
                currBirthdayDT = value;
                CurrBirthday = Convert.ToString(currBirthdayDT.Day) + '.' + Convert.ToString(currBirthdayDT.Month) + '.' + Convert.ToString(currBirthdayDT.Year);
                OnPropertyChanged(nameof(CurrBirthdayDT));
            }
        }
        private RelayCommand dateViewChange;
        public RelayCommand DateViewChange
        {
            get
            {
                return dateViewChange ??
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
                        if (window.GetType().Name == "WindowNewEmployee")
                        {
                            if (currPerson.IsValid())
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
