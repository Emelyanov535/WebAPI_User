using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class UserViewModelWithActiveStatus
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsActive { get; set; }

        public UserViewModelWithActiveStatus(string Name, Gender Gender, DateTime? Birthday, bool IsActive) {
            this.Name = Name;
            this.Gender = Gender;
            this.Birthday = Birthday;
            this.IsActive = IsActive;
        }
    }
}
