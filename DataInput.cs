using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile
{
    public static class DataInput
    {
        public static int GetInt(string msg)
        {
            Console.Write(msg);
            int r = 0;
            bool result = false;
            do
            {
                result = int.TryParse(Console.ReadLine(), out r);
                if(!result)
                    Console.WriteLine("Wartosc nieprawidlowa!");
            } while (!result);

            return r;
        }
    }
}
