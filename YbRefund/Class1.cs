using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBRefund
{
    public class Class1
    {
        public static bool IsRepeat2(int[] array)
        {
            for (int i = 0; i<array.Length; i++)
            {
                 for (int j = 0; j<array.Length; j++)
                {
                    if (j <= i)
                     {
                         continue;
                     }
                    if (array[i] == array[j])
                    {
                        return true;
                   }
                    if (array[i] == 1|| array[i] == 9)
                    {
                        return true;
                    }
                    if (array[j] == 1 || array[j] == 9)
                    {
                        return true;
                    }
                }
            }
            return false;        }



    }
}