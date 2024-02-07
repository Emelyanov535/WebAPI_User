using Contracts.ViewModels;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListStorage.Models
{
    public class User : IUserModel
    {
        public Guid Guid { get; private set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Admin { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public string CreatedBy { get; private set; } = string.Empty;

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;

        public DateTime? RevokedOn { get; set; }

        public string? RevokedBy { get; set; } = string.Empty;

        public User(Guid id,
            string Login, 
            string Password,
            string Name, 
            Gender gender, 
            DateTime birthday, 
            bool Admin, 
            DateTime CreatedOn, 
            string CreatedBy, 
            DateTime ModifiedOn,
            string ModifiedBy,
            DateTime? RevokedOn,
            string? RevokedBy
            ) {
            this.Guid = id;
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Gender = gender;
            this.Birthday = birthday;
            this.Admin = Admin;
            this.CreatedOn = CreatedOn;
            this.CreatedBy = CreatedBy;
            this.ModifiedOn = ModifiedOn;
            this.ModifiedBy = ModifiedBy;
            this.RevokedOn = RevokedOn;
            this.RevokedBy = RevokedBy;
        }

        public UserViewModel GetViewModel => new()
        {
            Guid = Guid,
            Login = Login,
            Password = Password,
            Name = Name,
            Gender = Gender,
            Birthday = Birthday,
            Admin = Admin,
            CreatedOn = CreatedOn,
            CreatedBy = CreatedBy,
            ModifiedOn = ModifiedOn,
            ModifiedBy = ModifiedBy,
            RevokedOn = RevokedOn,
            RevokedBy = RevokedBy,
        };
    }
}
