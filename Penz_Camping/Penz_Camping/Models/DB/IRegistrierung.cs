﻿using System;
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
    }
}