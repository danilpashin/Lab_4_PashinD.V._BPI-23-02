using Lab_4_PashinD.V._BPI_23_02.Model;
using Lab_4_PashinD.V._BPI_23_02.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_PashinD.V._BPI_23_02.Helper
{
    public class PersonDPO : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _error = string.Empty;
        public int Id { get; set; }
        
        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set
            {
                _roleName = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string birthday;
        public string Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        public PersonDPO() { }
        public PersonDPO(int id, string roleName, string firstName, string lastName, string birthday)
        {
            this.Id = id; 
            this.RoleName = roleName;
            this.FirstName = firstName; 
            this.LastName = lastName;
            this.Birthday = birthday;
        }
        public PersonDPO ShallowCopy()
        {
            return (PersonDPO)this.MemberwiseClone();
        }
        public PersonDPO CopyFromPerson(Person person)
        {
            PersonDPO perDpo = new PersonDPO(); 
            RoleViewModel vmRole = new RoleViewModel(); 
            string role = string.Empty;
            foreach (var r in vmRole.ListRole)
            {
                if (r.Id == person.RoleId)
                {
                    role = r.NameRole; 
                    break;
                }
            }
            if (role != string.Empty)
            {
                perDpo.Id = person.Id; 
                perDpo.RoleName = role; 
                perDpo.FirstName = person.FirstName; 
                perDpo.LastName = person.LastName;
                perDpo.Birthday = person.Birthday;
            }
            return perDpo;
        }

        public string Error => _error;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                            error = "Имя не может быть пустым";
                        else if (FirstName.Any(char.IsDigit))
                            error = "Имя не должно содержать цифры";
                        else if (FirstName.Length < 2)
                            error = "Имя должно содержать минимум 2 символа";
                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                            error = "Фамилия не может быть пустой";
                        else if (LastName.Any(char.IsDigit))
                            error = "Фамилия не должна содержать цифры";
                        else if (LastName.Length < 2)
                            error = "Фамилия должна содержать минимум 2 символа";
                        break;

                    case nameof(Birthday):
                        if (Convert.ToDateTime(Birthday) > DateTime.Now)
                            error = "Дата рождения не может быть в будущем";
                        else if (Convert.ToDateTime(Birthday) < DateTime.Now.AddYears(-100))
                            error = "Некорректная дата рождения";
                        break;
                }

                return error;
            }
        }

        // Метод для проверки всей модели
        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(FirstName)]) &&
                   string.IsNullOrEmpty(this[nameof(LastName)]) &&
                   string.IsNullOrEmpty(this[nameof(Birthday)]);
        }

        public event PropertyChangedEventHandler PropertyChanged; 
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
