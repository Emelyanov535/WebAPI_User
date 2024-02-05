using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewModels
{
    public class UserViewModel : IUserModel
    {
        public Guid Guid { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Admin { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;

        public DateTime? RevokedOn { get; set; }

        public string? RevokedBy { get; set; } = string.Empty;
    }
}
