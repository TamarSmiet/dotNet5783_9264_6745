﻿using BlImplementation;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public class Factory
    {
        
        public static IBl Get() 
        {
            Bl bl = new Bl();
            return bl;
        }
    }
}
