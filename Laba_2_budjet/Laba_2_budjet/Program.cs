﻿using System;
using System.IO;
using System.Text;

namespace Laba_2_budjet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string[] budj = GetListStudents();
            int[,] bud = Rate_bud(budj);
            double[] sr = Sredniy_bal(bud, budj);
            //Stepuha(sr, perc, budj);
            
            double[] stepend = Sort_sp(sr);
            
          Write_file(stepend, sr, budj);

        }
        public static int n, c = 0;


        public static string[] GetListStudents()
        {
            Console.Write("Введіть назву теки: ");    // давайте употреблять малоизвестные слова
            string path = Console.ReadLine();         // ну просто таааак
            string[] list_bud = new string[30];       // простите, бомбит немножко
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int count = 0;
                int n = Convert.ToInt32(sr.ReadLine());
                while (count != n)
                {
                    //students1.csv
                    line = sr.ReadLine();
                    if (line.EndsWith("FALSE"))
                    {
                        list_bud[c] = line;
                        c++;
                    }
                    count++;
                }
            }
            return list_bud;
        }
        static int[,] Rate_bud(string[] budj)              //создаём список бюджетников
        {
            int[,] sp_budj = new int[c, 5];
            for (int i = 0; i < c; i++)
            {
                string line = budj[i];
                string[] temp = line.Split(",");
                if (line.EndsWith("FALSE"))    // на счёт false не уверенна, но суть не меняется
                {
                    Console.WriteLine(line);
                    for (int j = 0; j < 5; j++)
                    {
                        sp_budj[i, j] = Convert.ToInt32(temp[j + 1]);
                    }
                    Console.WriteLine();
                }
            }
            return sp_budj;
        }
        static double[] Sredniy_bal(int[,] budj, string[] bud)    //узнаём средний балл каждого бюджетника
        {
            double sum;

            double[] sredniy = new double[c];
            for (int i = 0; i < c; i++)
            {
                string[] tem = bud[i].Split(",");
                sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += budj[i, j];
                }
                sredniy[i] = sum / 5;
                Console.Write("{0} ", tem[0]);      // для наглядности го попринтим)
                Console.WriteLine("{0:f3}", sredniy[i]);
            }
            return sredniy;
        }
        static int Find_Percent(int k)
        {
            int percent = c * k / 100;
            return percent;
        }
 
        static void Write_file()
 
        {
            Console.Write("Введіть назву теки для запису: ");
            string path = Console.ReadLine();
            try
            {
                // создание файла
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Список бюджетников");
                    // Add some information
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    int per = Find_Percent(40);
                    for (int i = 0; i < c; i++)
                    {
                        sw.Write(bud[i]);
                        sw.Write(sred_b[i]);
                        for (int j = 0; j < per; j++)
                        {
                            if (sred_b[i] == step[j])
                            {
                                sw.Write("На стипендии");
                                break;
                            }
                        }
                        sw.WriteLine();
                    }

                }
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    int count = 0;
                    while (count != c + 1)
                    {
                        line = sr.ReadLine();
                        Console.WriteLine(line);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
    }
}
