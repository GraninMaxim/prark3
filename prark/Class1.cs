using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prark
{
    internal class Class1
    {
        public class NumberSequenceAnalyzer
        {
            public int CountEqualNumbers(int[] array)
            {
                int count = 0;
                if (array != null && array.Length > 1)
                {
                    for (int i = 1; i < array.Length; i++)
                    {
                        if (array[i] == array[i - 1])
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
    }
}
