using Lab_4_PashinD.V._BPI_23_02.Helper;
using Lab_4_PashinD.V._BPI_23_02.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_PashinD.V._BPI_23_02.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public Person() { }
        public Person(int id, int roleId, string firstName, string lastName, string birthday)
        {
            this.Id = id; 
            this.RoleId = roleId;
            this.FirstName = firstName; 
            this.LastName = lastName; 
            this.Birthday = birthday;
        }

        public Person CopyFromPersonDPO(PersonDPO personDPO)
        {
            Person per = new Person();
            RoleViewModel vmRole = new RoleViewModel();
            int roleId = 0;
            foreach (var r in vmRole.ListRole)
            {
                if (r.NameRole == personDPO.RoleName)
                {
                    roleId = r.Id; 
                    break;
                }
            }
            if (roleId != 0)
            {
                per.Id = personDPO.Id;
                per.RoleId = roleId;
                per.FirstName = personDPO.FirstName;
                per.LastName = personDPO.LastName;
                per.Birthday = Convert.ToString(personDPO.Birthday);
            }
            return per;
        }
    }
}
