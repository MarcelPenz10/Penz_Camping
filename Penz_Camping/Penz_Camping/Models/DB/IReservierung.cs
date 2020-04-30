using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penz_Camping.Models.DB
{
    interface IReservierung 
    {
        void Open();
        void Close();
        bool UpdateAnfrageStatus(int knr, bool newStatus);
        bool AnfrageLöschen(int knr);

        bool Insert(Reservierungsanfrage reservierungsanfrage);
        List<Reservierungsanfrage> GetAllRes();

    }
}
