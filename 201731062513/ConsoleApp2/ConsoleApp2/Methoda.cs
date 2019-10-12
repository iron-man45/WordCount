using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Methoda
    {
        public  List<string> WordsCount(List<string> sdx, int n)
        {
            List<string> ad = new List<string>();
            int i = 0;
            List<string> ac = new List<string>();

            foreach (string s in sdx)
            {
                if (s == "!no")
                {
                    while (i >= n)
                    {
                        string ae = "";
                        int j = 0;
                        foreach (string af in ad)
                        {
                            ae += af + " ";
                            j++;
                            if (j == n)
                            {
                                ac.Add(ae);
                                break;
                            }
                        }
                        ad.RemoveAt(0);
                        i--;
                    }
                    ad.Clear();
                    i = 0;
                }
                else
                {
                    ad.Add(s);//这是一个大的词组
                    i++;
                }

            }

            return ac;
        }
    }
}
