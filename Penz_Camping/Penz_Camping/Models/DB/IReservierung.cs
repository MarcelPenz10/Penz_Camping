using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penz_Camping.Models.DB
{
    interface IReservierung : IDbBase
    {
        
        bool Insert(Reservierungsanfrage reservierungsanfrage);
     
    }
}
