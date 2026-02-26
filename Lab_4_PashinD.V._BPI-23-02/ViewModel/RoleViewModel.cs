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
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_4_PashinD.V._BPI_23_02.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        string path = String.Empty;
        string _jsonRoles = String.Empty;
        private Role selectedRole;
        public Role SelectedRole
        {
            get
            {
                return selectedRole;
            }
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                EditRole.CanExecute(true);
            }
        }
        public string Error { get; set; }

        private RelayCommand addRole; 
        public RelayCommand AddRole
        {
            get
            {
                return addRole ??
                (addRole = new RelayCommand(obj =>
                {
                    WindowNewRole wnRole = new WindowNewRole
                    {
                        Title = "Новая должность",
                    };
                    // формирование кода новой должности
                    int maxIdRole = MaxId() + 1;
                    Role role = new Role { Id = maxIdRole }; 
                    wnRole.DataContext = role;

                    if (wnRole.ShowDialog() == true)
                    {
                        ListRole.Add(role);
                        SaveChanges(ListRole);
                        SelectedRole = role;
                    }
                }));
            }
        }
        private RelayCommand editRole; 
        public RelayCommand EditRole
        {
            get
            {
                return editRole ??
                (editRole = new RelayCommand(obj =>
                {
                    WindowNewRole wnRole = new WindowNewRole { Title = "Редактирование должности", }; 
                    Role role = SelectedRole;
                    Role tempRole = new Role(); 
                    tempRole = role.ShallowCopy(); 
                    wnRole.DataContext = tempRole;

                    if (wnRole.ShowDialog() == true)
                    {
                        // сохранение данных в оперативной памяти
                        role.NameRole = tempRole.NameRole;
                        SaveChanges(ListRole);
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        private RelayCommand deleteRole; 
        public RelayCommand DeleteRole
        {
            get
            {
                return deleteRole ??
                (deleteRole = new RelayCommand(obj =>
                {
                    Role role = SelectedRole;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " + role.NameRole, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListRole.Remove(role);
                        SaveChanges(ListRole);
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        public ObservableCollection<Role> LoadRole()
        {
            _jsonRoles = File.ReadAllText(path); 
            if (_jsonRoles != null)
            {
                //Console.WriteLine(_jsonRoles);
                ListRole = JsonConvert.DeserializeObject<ObservableCollection<Role>>(_jsonRoles);
                return ListRole;
            }
            else
            {
                return null;
            }
        }


        private void SaveChanges(ObservableCollection<Role> listRole)
        {
            var jsonRole = JsonConvert.SerializeObject(listRole); 
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonRole);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();
        public RoleViewModel()
        {
            path = SetPathJson();
            ListRole = LoadRole();
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
                ;
            }
            return max;
        }

        private string SetPathJson()
        {
            string path = Directory.GetCurrentDirectory();
            path = Convert.ToString(System.IO.Directory.GetParent(path));
            path = Convert.ToString(System.IO.Directory.GetParent(path));
            string relPath = @"DataModels\RoleData.json";
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
