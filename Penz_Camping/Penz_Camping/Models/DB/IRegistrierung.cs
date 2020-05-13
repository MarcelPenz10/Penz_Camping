using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penz_Camping.Models.DB
{
    interface IRegistrierung : IDbBase
    {
        bool Insert(User user);
        User Login(UserLogin user);
        List<User> GetAllRegUsers();
        User GetUser(int id);
        List<User> GetAllUser();
        bool LöschenUser(int id);
        bool BenutzerdatenÄndern(int id, User neueDaten);

    }
}
