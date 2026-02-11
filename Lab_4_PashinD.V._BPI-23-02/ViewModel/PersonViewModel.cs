using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        string path = String.Empty;
        string _jsonPersons = String.Empty;
        public string Error { get; set; }
        public string Message { get; set; }
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
            path = SetPathJson();
            ListPerson = LoadPerson();
            ListPersonDPO = GetListPersonDPO();
        }
        public ObservableCollection<Person> LoadPerson()
        {
            _jsonPersons = File.ReadAllText(path); 
            if (_jsonPersons != null)
            {
                ListPerson = JsonConvert.DeserializeObject<ObservableCollection<Person>>(_jsonPersons);
                return ListPerson;
            }
            else
            {
                return null;
            }
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
                        Role r = (Role)wnPerson.CbRole.SelectedValue;
                        if (r != null)
                        {
                            per.RoleName = r.NameRole;
                            per.FirstName = wnPerson.FirstNameTBox.Text;
                            per.LastName = wnPerson.LastNameTBox.Text;
                            per.Birthday = Convert.ToDateTime(Convert.ToString(wnPerson.ClBirthday));
                            ListPersonDPO.Add(per);
                            Person p = new Person();
                            p = p.CopyFromPersonDPO(per); 
                            ListPerson.Add(p);
                            try
                            {
                                SaveChanges(ListPerson);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка добавления данных в json файл\n" +
                                e.Message;
                            }
                        }
                    }

                }, (obj) => true));
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
                    PersonDPO tempPerson = personDpo.ShallowCopy(); 
                    wnPerson.DataContext = tempPerson;
                    if(wnPerson.ShowDialog() == true) {
                        Role r = (Role)wnPerson.CbRole.SelectedValue;
                        
                        if (r != null)
                        {
                            
                            personDpo.RoleName = r.NameRole; 
                            personDpo.FirstName = tempPerson.FirstName; 
                            personDpo.LastName = tempPerson.LastName; 
                            personDpo.Birthday = tempPerson.Birthday;
                            // перенос данных из класса отображения данных в класс Person
                            FindPerson finder = new FindPerson(personDpo.Id);

                            List<Person> listPerson = ListPerson.ToList();
                            Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));
                            p = p.CopyFromPersonDPO(personDpo);
                            ListPerson[p.Id - 1] = p;
                            //Console.WriteLine(ListPerson);
                            try
                            {
                                Console.WriteLine(ListPerson[3].LastName);
                                SaveChanges(ListPerson);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка редактирования данных в json файл\n"
                                + e.Message;
                            }
                        }
                        else
                        {
                            Message = "Необходимо выбрать должность сотрудника.";
                        }
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
                        SaveChanges(ListPerson);
                    }
                }, (obj) => SelectedPersonDPO != null && ListPersonDPO.Count > 0));
            }
        }

        private void SaveChanges(ObservableCollection<Person> listPersons)
        {
            var jsonPerson = JsonConvert.SerializeObject(listPersons); 
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonPerson);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }


        private string SetPathJson()
        {
            string path = Directory.GetCurrentDirectory();
            path = Convert.ToString(System.IO.Directory.GetParent(path));
            path = Convert.ToString(System.IO.Directory.GetParent(path));
            string relPath = @"DataModels\PersonData.json";
            string resPath = Path.Combine(path, relPath);
            return resPath;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
