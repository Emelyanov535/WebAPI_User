using ListStorage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ListStorage
{
    public  class DataListSingleton
    {
        private static DataListSingleton? _instance;
        public List<User> Users { get; set; }
        private DataListSingleton()
        {
            Users = new List<User>();
            User user1 = new User(Guid.NewGuid(), 
                "Admin", 
                "123",
                "Admin",
                DataModels.Gender.Male, 
                new DateTime(2015, 7, 20, 18, 30, 25),
                true, 
                new DateTime(2015, 7, 20, 18, 30, 25), 
                "Admin",
                new DateTime(2015, 7, 20, 18, 30, 25), 
                "Admin",
                null, 
                null);
            Users.Add(user1);
        }
        public static DataListSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataListSingleton();
            }
            return _instance;
        }
    }
}
