using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private PersonDPO selectedPersonDPO;
        public PersonDPO SelectedPersonDPO
        {
            get { return selectedPersonDPO; }
            set
            {
                selectedPersonDPO = value; 
                OnPropertyChanged(nameof(SelectedPersonDPO));
            }
        }

        public ObservableCollection<Person> ListPerson { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersonDPO> ListPersonDPO { get; set; } = new ObservableCollection<PersonDPO>();

        public PersonViewModel()
        {
            this.ListPerson.Add(new Person
            {
                Id = 1,
                RoleId = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Birthday = new DateTime(1980, 02, 28)
            });
            this.ListPerson.Add(new Person
            {
                Id = 2,
                RoleId = 2,
                FirstName = "Петр",
                LastName = "Петров",
                Birthday = new DateTime(1981, 03, 20)
            });
            this.ListPerson.Add(new Person
            {
                Id = 3,
                RoleId = 3,
                FirstName = "Виктор",
                LastName = "Викторов",
                Birthday = new DateTime(1982, 04, 15)
            });
            this.ListPerson.Add(new Person
            {
                Id = 4,
                RoleId = 3,
                FirstName = "Сидор",
                LastName = "Сидоров",
                Birthday = new DateTime(1983, 05, 10)
            });
            ListPersonDPO = GetListPersonDPO();
        }
        public ObservableCollection<PersonDPO> GetListPersonDPO()
        {
            ListPersonDPO = new ObservableCollection<PersonDPO>();
            foreach (var person in ListPerson)
            {
                PersonDPO p = new PersonDPO(); 
                p = p.CopyFromPerson(person);
                ListPersonDPO.Add(p);
            }
            return ListPersonDPO;
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ??
                (addPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee("")
                    {
                        Title = "Новый сотрудник"
                    };
                    // формирование кода нового сотрудника
                    int maxIdPerson = MaxId() + 1;
                    PersonDPO per = new PersonDPO
                    {
                        Id = maxIdPerson, 
                        Birthday = DateTime.Now
                    };
                    wnPerson.DataContext = per;
                    if (wnPerson.ShowDialog() == true)
                    {
                        Role r = (Role)wnPerson.CbRole.SelectedItem;
                        per.RoleName = r.NameRole;
                        per.FirstName = wnPerson.FirstNameTBox.Text;
                        per.LastName = wnPerson.LastNameTBox.Text;
                        ListPersonDPO.Add(per);
                        Person p = new Person();
                        p = p.CopyFromPersonDPO(per); 
                        ListPerson.Add(p);
                        SaveChanges();
                        SelectedPersonDPO = per;
                    }
                },
                (obj) => true));
            }
        }
        private RelayCommand editPerson; 
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ??
                (editPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee(SelectedPersonDPO.RoleName)
                    {
                        Title = "Редактирование данных сотрудника",
                    };
                    PersonDPO personDpo = SelectedPersonDPO; 
                    PersonDPO tempPerson = new PersonDPO(); 
                    tempPerson = personDpo.ShallowCopy(); 
                    wnPerson.DataContext = tempPerson;

                    if (wnPerson.ShowDialog() == true)
                    {
                        Role r = (Role)wnPerson.CbRole.SelectedValue;
                        personDpo.RoleName = r.NameRole; 
                        personDpo.FirstName = tempPerson.FirstName; 
                        personDpo.LastName = tempPerson.LastName; 
                        personDpo.Birthday = tempPerson.Birthday;
                        // перенос данных из класса отображения данных в класс Person
                        FindPerson finder = new FindPerson(personDpo.Id);

                        List<Person> listPerson = ListPerson.ToList(); 
                        Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));
                        p = p.CopyFromPersonDPO(personDpo);
                    }
                }, (obj) => SelectedPersonDPO != null && ListPersonDPO.Count > 0));
            }
        }

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ??
                (deletePerson = new RelayCommand(obj =>
                {
                    PersonDPO person = SelectedPersonDPO;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \n" + person.LastName + " " + person.FirstName,
            "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        Person per = new Person();
                        per = per.CopyFromPersonDPO(person); 
                        int idToRemove = -1;
                        foreach (Person p in ListPerson)
                        {
                            if (p.Id == per.Id) idToRemove = p.Id;
                        }
                        ListPersonDPO.Remove(ListPersonDPO.FirstOrDefault(p => p.Id == idToRemove));
                        ListPerson.Remove(ListPerson.FirstOrDefault(p => p.Id == idToRemove));
                        SaveChanges();
                    }
                }, (obj) => SelectedPersonDPO != null && ListPersonDPO.Count > 0));
            }
        }

        private void SaveChanges()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is WindowEmployee)
                {
                    ((WindowEmployee)window).lvEmployee.ItemsSource = GetListPersonDPO();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
