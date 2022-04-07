using System;
using System.Collections.Generic;
using System.IO;

namespace TROMP
{
    public class AirPlanCourse
    {
        static int s_direction = 0, s_horizontal = 0, s_vertical = 0;
        
        //up X increases the direction by X units
        public static void UP(int value)
        {
           s_direction += value;           
        }

        //down X decreases the direction by X units
        public static void Down(int value)
        {
            s_direction -= value;           
        }

        // forward X increases the horizontal position by X units and the vertical position by X multipled by the current direction
        public static void Forward(int value)
        {
            s_horizontal += value;
            s_vertical += value * s_direction;
        }
        public static int Result()
        {
            return s_horizontal * s_vertical;
        }

        //If we want to add Dive function
        public static void Dive(int value)
        {
            s_vertical += value;
        }

    }
    class Program
    {
       
        
        private static readonly Dictionary<string, Delegate> actionDictionary = new();
      
        //Fill required actions to a dictionary
        public static void FillActions()
        {
            actionDictionary ["down"] = new Action<int>(AirPlanCourse.Down);
            actionDictionary["up"] = new Action<int>(AirPlanCourse.UP);
            actionDictionary["forward"] = new Action<int>(AirPlanCourse.Forward);
            //add dive action to the dictionary
            actionDictionary["dive"] = new Action<int>(AirPlanCourse.Dive);

        }
        public static void Main(string[] args)
        {
            
            FillActions();
            Console.WriteLine("Enter the actions file path:");
            string textFile = Console.ReadLine();

            if (File.Exists(textFile))
            {
                using (StreamReader file = new(textFile))
                {
                    string line;
                    string[] lineArray;
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            lineArray = line.Split(" ");
                            string action = lineArray[0].ToString();
                            int value = int.Parse(lineArray[1].ToString());
                            actionDictionary[action].DynamicInvoke(value);
                        }
                        catch (Exception)
                        {

                            Console.Write("Please revise the file format. It must me action followed by space then the value!");
                        }

                    }
                    file.Close();
                    int result = AirPlanCourse.Result();
                    Console.WriteLine("Result = " + result);
                }
            }
            else
            {
                Console.WriteLine("File does not exist!");
            }



        }
    }
}
