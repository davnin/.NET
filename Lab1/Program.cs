using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Lab1
{
    class Program
    {
        //Instance variables
        IList<string> words = new List<string>();

        static void Main(string[] args)
        {
            Program pgm = new Program();

            while (true)
            {
                
                pgm.menu();
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        pgm.importWords("Words.txt");
                        Console.WriteLine("Reading Words");
                        Console.WriteLine("Reading Words Completed");
                        Console.WriteLine("Number Words Found :" + pgm.words.Count());
                        break;
                    case "2":
                        pgm.words = pgm.BubbleSort(pgm.words);                  
                        break;
                    case "3":
                        pgm.words = pgm.linqSort(pgm.words);
                        break;
                    case "4":
                        var distinctWords = from w in pgm.words.Distinct()
                                            select w;
                        int count = 0;
                        foreach (String i in distinctWords)
                        {
                            count++;
                        }
                        Console.WriteLine("The number of Distinct Words are : " + count);
                        break;
                    case "5":
                        var top10 = (from w  in pgm.words
                                    select w).Take(10).ToList();
                        
                        foreach (String i in top10)
                        {
                            Console.WriteLine("\n" + i);
                        }
                        break;
                    case "6":
                        var startingJ = from w in pgm.words
                                         where w.StartsWith("j")
                                         select w;
                        int j = 0;
                        foreach (String i in startingJ)
                        {
                            Console.WriteLine("\n" + i);
                            j++;
                        }
                        Console.WriteLine("The Number of words that start with 'j' : " + j);
                        break;
                    case "7":
                        var startingD = from w in pgm.words
                                        where w.StartsWith("d")
                                        select w;
                        int d = 0;
                        foreach (String i in startingD)
                        {
                            Console.WriteLine("\n" + i);
                            d++;
                        }
                        Console.WriteLine("The Number of words that start with 'd' : " + d);
                        break;
                    case "8":
                        var greaterThan = from w in pgm.words
                                    where w.Length > 4
                                    select w;
                        int num4 = 0;
                        foreach (String i in greaterThan)
                        {
                            Console.WriteLine("\n" + i);
                            num4++;
                        }
                        Console.WriteLine("The Number of words that have more than 4 characters are : " + num4);
                        break;
                    case "9":
                        var lessThan = from w in pgm.words
                                          where w.Length > 4 && w.StartsWith("a")
                                          select w;
                        int num3 = 0;
                        foreach (String i in lessThan)
                        {
                            Console.WriteLine("\n" + i);
                            num3++;
                        }
                        Console.WriteLine("The Number of words that have less than 3 characters are : " + num3);
                        break;
                    case "x":
                         Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input..."); 
                        break;
                }
                
            }




            
        }//end of main

        public void menu()
        {
            Console.WriteLine("\nHello world !! My First C# App");
            Console.WriteLine("Options");
            Console.WriteLine("---------------");
            Console.WriteLine("1 - Import Words From File");
            Console.WriteLine("2 - Bubble Sort Words");
            Console.WriteLine("3 - LINQ/Lambda sort words");
            Console.WriteLine("4 - Count the Didtinct Words");
            Console.WriteLine("5 - Take the First 10 Words");
            Console.WriteLine("6 - Get the number of Words that start with 'j' and display the count");
            Console.WriteLine("7 - Get and display of words that end with 'd' and display the count");
            Console.WriteLine("8 - Get and display of words that are greater than 4 characters long, and display the count");
            Console.WriteLine("9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count");
            Console.WriteLine("X - Exit");

            Console.WriteLine("\nMake a Selection : ");
        }

        public void importWords (string txtFile)
        {
            try
            {
                using (StreamReader reader = new StreamReader(txtFile))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = reader.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        words.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }//end importWords

        IList <string> BubbleSort(IList <string>  words)
        {
            var time = new Stopwatch();
            time.Start();
            for (int i = 0; i < words.Count - 1; i++)
            {
                for (int j = i+1; j < words.Count; j++)
                {
                    if (words.ElementAt(i).Length > words.ElementAt(j).Length)
                    {
                        string temp = words.ElementAt(j);
                        words[j] = words[i];
                        words[i] = temp;
                    }
                }
            }
            time.Stop();
            var elapsedTime = time.ElapsedMilliseconds;
            Console.WriteLine("Time elapsed : " + elapsedTime + "ms");
            return words;
        }

        public IList<string> linqSort (IList<string> words)
        {
            var time = new Stopwatch();
            time.Restart();
            var sortedListQ = words.OrderBy(w => w.Length).ToList();
            words = sortedListQ;
            time.Stop();
            var elapsedTime = time.ElapsedMilliseconds;
            Console.WriteLine("Time elapsed : " + elapsedTime + " ms");
            return words;
        }
    }
}
