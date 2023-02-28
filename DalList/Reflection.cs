﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class Reflection
    {
        public static string ToStringProperty<T>( T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name
                + ": " + item.GetValue(t, null);
            return str;
        }

    }
}
