using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_PashinD.V._BPI_23_02.Model
{
    public class Role : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _error = string.Empty;
        public int Id { get; set; }
        private string nameRole;
        public string NameRole
        {
            get { return nameRole; }
            set
            {
                nameRole = value;
                OnPropertyChanged("NameRole");
            }
        }
        public Role() { }
        public Role(int id, string nameRole)
        {
            this.Id = id; 
            this.NameRole = nameRole;
        }
        public Role ShallowCopy()
        {
            return (Role)this.MemberwiseClone();
        }
        public string Error => _error;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                if (columnName == nameof(NameRole))
                {
                    if (string.IsNullOrWhiteSpace(NameRole))
                        error = "Должность не может быть пустой";
                    else if (NameRole.Any(char.IsDigit))
                        error = "Должность не должна содержать цифры";
                    else if (NameRole.Length < 2)
                        error = "Должность должна содержать минимум 2 символа";
                }

                return error;
            }
        }

        // Метод для проверки всей модели
        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(NameRole)]);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
