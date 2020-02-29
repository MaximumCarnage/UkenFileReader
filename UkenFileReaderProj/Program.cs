using System;
using System.Collections.Generic;

namespace UkenFileReaderProj
{
    static class Constants
    {
        //max number of possible input files
        public const int maxNumberOfFiles = 5;
    }

    class Program
    {
        static void Main(string[] args)
        {
            for(int count = 1; count <= Constants.maxNumberOfFiles; count++)
            {
                ReadSelectFile( count + ".txt");
            }
        }

        //This method will read a file and aggregate the number of unique values and the count of said values
        static void ReadSelectFile(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\src\"+ fileName);
            IDictionary<int, int> numberCountDict = new Dictionary<int, int>();
         
        
            //Go through the entire list of lines from the file, and create a list of Number objects, to represent each individual number present in the file
            foreach(string element in lines)
            {
                int numElement = Int32.Parse(element);
               
                if (numberCountDict.ContainsKey(numElement))
                {
                    numberCountDict[numElement] = numberCountDict[numElement]+1;
                }
                else
                {
                    numberCountDict.Add(numElement, 1);
                }

            }

            //Calculates which of the numbers occurs the fewest times, in the event of a tie, it will use the lowest number value
            int lowestCount = Int32.MaxValue;
            int lowestNum = 0;
            foreach(KeyValuePair<int,int> element in numberCountDict)
            {
                if (element.Value == lowestCount)
                {
                    if (element.Key < lowestNum)
                    {
                        lowestNum = element.Key;
                    }

                }
                if (element.Value < lowestCount)
                {
                    lowestCount = element.Value;
                    lowestNum = element.Key;
                }
            }
            Console.WriteLine("File: " + fileName + ", Number: "+lowestNum+" Repeated: "+lowestCount+ " time(s)");
        }
    }
}
