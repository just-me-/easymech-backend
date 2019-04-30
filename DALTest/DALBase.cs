﻿using System.Reflection;

namespace DALTest
{
    public class DalBase
    {
        protected readonly string text = "Hallo? \n ch ch";
        protected readonly string longText = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567XXXXXXXX";
        protected readonly string clippedText = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";
       
        protected bool HaveSameData(object o1, object o2)
        {
            foreach (PropertyInfo prop in o1.GetType().GetProperties())
            {
                if (prop.GetValue(o1) == null && prop.GetValue(o2) == null)
                {
                    continue;
                }

                if (!prop.GetValue(o1).Equals(prop.GetValue(o2)))
                {
                    return false;
                }

            }
            return true;

        }
    }
}