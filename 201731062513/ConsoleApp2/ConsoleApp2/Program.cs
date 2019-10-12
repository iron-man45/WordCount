using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
               string p ="in";//输入文件路径
                string q="out";//输出文件路径
                int n=0;//词组长度
                int number=0;//输出单词个数
                              for (int i=0;i<args.Length;i++)
                               {
                                   switch (args[i])
                                   {
                                       case "-i":p = args[i + 1];break;
                                       case "-o": q = args[i + 1]; break;
                                       case "-m": n=int.Parse(args[i+1]); break;
                                       case "-n": number= int.Parse(args[i+1]); break;
                                   }
                                   i++;
                               }

             
          //计算字符数汉字除外
          int a = FirstCount(p,0);
            //计算非空行数
            int b = FirstCount(p,1);
            //输出到目标文件
            ReadTxt(p,q,n,number,a,b);
           


        }
        public static void ReadTxt(string path,string outpath,int n,int number,int a,int b)
        {


            

            Dictionary<string, int> e = new Dictionary<string, int>();//筛选后的词典

            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string restOfStream = sr.ReadToEnd();


            if (outpath != "out")
            {
                
                StreamWriter sw = new StreamWriter(outpath, false);//true表示追加
                sw.WriteLine("characters: " + a);
                sw.WriteLine("words: " + WordN(WordNumber(restOfStream), 0).Count());
                sw.WriteLine("lines: " + b);

                //输出单词
                if (number > 0)
                {
                    e = Chosewords(Listdo(WordN(WordNumber(restOfStream), 0)), number);
                    foreach (string s in e.Keys)
                    {

                        sw.WriteLine("<" + s + ">: " + e[s]);
                    }
                }
                if (n > 0)
                {

                    List<string> ac = WordsCount(WordN(WordNumber(restOfStream), 1), n);
                   
                    for (int k=0;k<ac.Count();k++)
                    {
                        if(ac[k] != "!no")
                        { 
                        string xs = ac[k];

                        sw.Write(xs);
                        int numa = 0;
                        for(int u=0;u<ac.Count();u++)
                        {
                            if (ac[u] == xs)
                            {
                                ac[u] = "!no";
                                numa++;
                            }
                        }
                        
                        sw.WriteLine(": " + numa);
                        }
                    }
                   
                }
                
                sw.Flush();
                sw.Close();
            }

        }
        //计算字符数 非空行数
        public static int FirstCount(string path,int n)
        {
            int i = 0;//字符数目

            int hz = 0;//汉字数目

            string line;

            int j = 0;//非空白行数

            StreamReader sr = new StreamReader(path, Encoding.Default);

            while ((line = sr.ReadLine()) != null)
            {
                
                //统计汉字数目
                hz += Regex.Matches(line, @"[\u4E00-\u9FFF]").Count;


                if (line.Length != 1)
                    j++;
                //统计所有字符数
                i += line.Length;
                i++;
            }
            if (n == 0)
                return (i - hz);
            else
                return j;


        }
        //处理单词表
        public static Dictionary<string, int>Listdo(List<string> wordList)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (string s in wordList)               //遍历集合中每个单词
            {
                int val;
                if (dic.TryGetValue(s, out val))
                {
                    //如果指定的字典的键存在则将值+1
                    dic[s] += 1;
                }
                else
                {
                    //不存在，则添加
                    dic.Add(s, 1);
                }
            }
            return dic;
        }
        
        //将数据流字符串分割成字符串数组
        public static string[] WordNumber(string str)
        {
            List<string> words = new List<string>();
            

            

            List<char> ca = new List<char>();

            foreach (Char ch in str)
            {
               if(((ch>='A')&&(ch<='Z'))||((ch>='a')&&(ch<='z'))||((ch>='0')&&(ch<='9'))||(ch=='_'))
                {
                    

                }
               else
                    ca.Add(ch);
            }

            char[] cb = ca.ToArray();
            

            string[] sArray = str.Split(cb);
           
            return sArray;
        }
        //以下为李的内容
        //将字符串数组转换为2个不同的列表
        public static List<string> WordN(string[] ma,int h)
        {

            

            List<string> sd = new List<string>();

            List<string> sdx = new List<string>();//建立一个存储词组的列表
            int i = 0;
            foreach (string s in ma)               //遍历集合中每个单词
            {
               if(s!="")
                { 
                if(s.Length<4)
                {
                    sdx.Add("!no");
                }
                else
                {
                    int j = 1;
                    for (i = 0; i < 4; i++)
                    {
                        if (((s[i] >= 'a') && (s[i] <= 'z')) || ((s[i] >= 'A') && (s[i] <= 'Z')))
                        {
                        }
                        else
                        {
                            j = 0;
                            break;
                        }
                    }
                    if (j == 1)
                    {
                        string s2 = s.ToLower();
                        sd.Add(s2);
                        sdx.Add(s);
                        
                        
                    }
                    else
                    {
                        sdx.Add("!no");
                    }

                }
                }

            }
            sdx.Add("!no");
            if (h == 0)
                return sd;
            else
                return sdx;
        }
        //统计单词频率WordsCount(, n);

        public static Dictionary<string, int> Chosewords(Dictionary<string, int> dic, int n)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            //判断需要的单词个数是否超出总单词个数
            int x = 0;
            if (n <= dic.Count)
                x = n;
            else
                x = dic.Count;
            while (d.Count < x)
            {
                List<string> l = new List<string>();    //多个单词有相同频率时储存至这个临时集合中按字典顺序排序
                int maxValue = dic.Values.Max(); ;      //字典中单词的最高频率
                foreach (string s in dic.Keys)
                {
                    if (dic[s] == maxValue)             //获取拥有最高频率的单词
                    {
                        l.Add(s);                       //添加至临时集合中
                    }
                }
                //将这一轮获取到的单词从原来的字典中删除
                foreach (string s in l)
                {
                    dic.Remove(s);
                }
                //给这一轮获取到的单词按字典顺序排序然后按顺序添加到一个新的字典中
                l.Sort(string.CompareOrdinal);
                foreach (string s in l)
                {
                    d.Add(s, maxValue);
                    if (d.Count >= x)                   //获取到足够数量的单词后退出
                        break;
                }
            }
            return d;
        }

        //统计词组
        public static List<string> WordsCount(List<string> sdx,int n)
        {

           
            List<string> ad = new List<string>();
            int i = 0;
            List<string> ac = new List<string>();

            foreach (string s in sdx)
            {
                if(s=="!no")
                {
                    while(i>=n)
                    {
                        string ae="";
                        int j = 0;
                        foreach (string af in ad)
                        { 
                            ae+=af+" ";
                            j++;
                        if(j ==n)
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

