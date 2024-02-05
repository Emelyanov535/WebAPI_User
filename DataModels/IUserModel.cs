using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public interface IUserModel
    {
        Guid Guid { get; }
        string Login { get; }
        string Password { get; }
        string Name {  get; }
        Gender Gender { get; }
        DateTime? Birthday { get; }
        bool Admin {  get; }
        DateTime CreatedOn { get; }
        string CreatedBy {  get; }
        DateTime ModifiedOn { get; }
        string ModifiedBy { get; }
        DateTime? RevokedOn {  get; }
        string? RevokedBy { get; }
    }
}
