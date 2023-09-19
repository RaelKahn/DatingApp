using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class Utils
    {
        public static bool IsEqual(this byte[] string_1, byte[] string_2)
        {

            for(var i =0; i < string_1.Length; i++)
            {
                if(string_1[i] != string_2[0] )    
                    return false;
            }

            return true;
        }
    }
}