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

            int count = 1;
               
            while ( count <= Constants.maxNumberOfFiles){
                ReadSelectFile(count);
                count++;
            }

        }

        //handles parsing strings that the user has input into int32 form, also catches any Format exception from a file having characters 
        static int Parser(string input)
        {
            int num = 0;
            try
            {
                num = Int32.Parse(input);

            }
            catch (FormatException)
            {
                Console.WriteLine("Error this file containts characters");
            }

            return num;

        }

        //This method will take a user's input parameter and use it to determine which file the user wants to access and will output the appropriate information
        static void ReadSelectFile(int userChoice)
        {
            
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\src\"+userChoice+".txt");
            List<Number> diffNums = new List<Number>();

            bool dontAdd = false;
            
            //Go through the entire list of lines from the file, and create a list of Number objects, to represent each individual number present in the file
            foreach(string element in lines)
            {
                dontAdd = false;
               int numElement = Parser(element);

               if (diffNums.Count == 0)
                {
                    diffNums.Add(new Number(numElement,0));
                    dontAdd = true;
                }
                else
                {
                    for(int i = 0; i < diffNums.Count; i++)
                    {
                        if(numElement == diffNums[i].number)
                        {
                            dontAdd = true;
                        }
                    }
                }

                if (!dontAdd)
                {
                    diffNums.Add(new Number(numElement, 0));
                }
                
            }

           //go through the list and count up the occurences of each number
            foreach(Number element in diffNums)
            {
                foreach(string lineStr in lines)
                {
                    if(Parser(lineStr) == element.number)
                    {
                        element.count++;
                    }
                }

            }

            //Calculates which of the numbers occurs the fewest times, in the event of a tie, it will use the lowest number value
            int lowestCount = 100;
            int lowestNum = 0;
            foreach(Number element in diffNums)
            {
                if (element.count == lowestCount)
                {
                    if (element.number < lowestNum)
                    {
                        lowestNum = element.number;
                    }

                }
                if (element.count < lowestCount)
                {
                    lowestCount = element.count;
                    lowestNum = element.number;
                }
            }
            Console.WriteLine("File: "+userChoice+".txt, Number: "+lowestNum+" Repeated: "+lowestCount+ " time(s)");
        }
    }

    //this class is used to have each number that appears in the file and gives each instance of itself a number value and a count representing amount of times it has occured
    public class Number
    {
        public int number;
        public int count;
   

        public Number(int num,int count )
        {
            this.number = num;
            this.count = count;
        }
    }
}
