using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Tests
{
    [TestClass()]
    public class MethodaTests
    {

        [TestMethod()]
        public void WordsCountTest()
        {

        }

        [TestMethod()]
        public void WordsCountTest1()
        {
            Methoda m1 = new Methoda();
            List<string> tesa = new List<string>();
            List<string> tesb = new List<string>();
            tesb.Add("aaaaa");
            tesb.Add("!no");
            tesb.Add("bbbbb");
            tesb.Add("ccccc");
            tesb.Add("!no");
            tesb.Add("eeeee");
            tesb.Add("fffff");
           tesb.Add("!no");
            tesa = m1.WordsCount(tesb, 2);

            Console.WriteLine((tesa[1]));
            if ((tesa[0]=="bbbbb ccccc ")&& (tesa[1] == "eeeee fffff ") )
            {
                

            }
                else
                Assert.Fail();
        }

        [TestMethod()]
        public void WordsCountTest2()
        {
            
        }
    }
}