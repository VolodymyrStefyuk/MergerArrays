using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MergerArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveArray(new string[] {"1", "2", "3", "5" }, "a.txt");
            SaveArray(new string[] {"1", "2", "3", "6", "7", "9"}, "b.txt");
            SaveArray(new string[] {"1", "2", "3", "5", "6", "7", "8" , "9", "10", "11"}, "c.txt");

            bool complete = false;
            while (!complete) 
            { 
                Console.WriteLine("Enrer the path to the array file, \n Example a.txt,b.txt,c.txt");
                string enterValue = Console.ReadLine();
                string[] splitEnterValue = enterValue.Split(new char[] { ',' });
                int[][] allArrays = new int[splitEnterValue.Length][];
                try
                {
                    for (int i = 0; i < splitEnterValue.Length; i++)
                        allArrays[i] = LoadArray(splitEnterValue[i].Trim());
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrectly entered data");
                    continue;
                }
                Console.WriteLine("Output array:");
                PrintArray(MergerArrays(allArrays));
                Console.WriteLine("\n Merger other arrays?");
                if (Console.ReadLine() == "yes") continue;
                else break;  
            }
        }

        static int[] MergerArrays(params int[][] arrays)
        {
            int[] result = new int[] { };
            foreach (int[] array in arrays)
            {
                result = result.Union(array).Where(number => number % 5 != 0).
                    OrderBy(number => number).ToArray();
            }
            return result;
        }

        static void SaveArray(string[] array, string path)
        {
            if (array.Length == 0)
                return;
            if (File.Exists(path))
                File.Delete(path);

            FileInfo f = new FileInfo(path);
            TextWriter tw = f.CreateText();
            foreach(var element in array)
            {
                tw.WriteLineAsync(element);
            }
            tw.Close();
        }

        static int[] LoadArray(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            try
            {
                string[] substring = File.ReadAllLines(path).ToArray();
                List<int> array = new List<int>();
                foreach(string x in substring)
                {
                    array.Add(Convert.ToInt32(x));
                }
                return array.ToArray();
            }
            catch(Exception)
            {
                throw;
            }
        }

        static void PrintArray(int [] array)
        {
            foreach (int element in array)
            {
                Console.Write(element + "\t");
            }
        }
    }
}
