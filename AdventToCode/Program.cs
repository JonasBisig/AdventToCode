using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = "3-5,10-21,16-20,12-18,,1,5,8,11,17,32";

            //to use the example string enable this line and comment the line below -- in my example the result should be 15
            List<string> input = example.Split(",").ToList();
            //List<string> input = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt")).ToList();

            List<List<long>> freshIDs = new List<List<long>>();

            //fill the freshIDs list with all the fresh id ranges
            foreach (string ranges in input)
            {
                if (ranges.IsWhiteSpace())
                {
                    break;
                }
                freshIDs.Add(ranges.Split("-").Select(long.Parse).ToList());
            }

            //3-5
            //10-21
            //16-20
            //12-18

            //sort all the id ranges
            freshIDs = freshIDs.OrderBy(r => r[0]).ThenBy(r => r[1]).ToList();

            //3-5
            //10-21
            //12-18
            //16-20


            long result = 0;

            //get the first entry and set min and max of the range
            long min = freshIDs[0][0];
            long max = freshIDs[0][1];

            for (int i = 0; i < freshIDs.Count; i++)
            {
                //check if the current range minimum is smaller or equal to the max value

                //3-5
                //10-21
                //12-18 <-
                //16-20

                //for example i = 2 -> check if 12 (max) is smaller then 21 (freshIDs[i][1])
                //it is true so get the higher value -> max = Math.Max(21, 12);
                //if it is without Math.Max() then the 21 will be overritten by the 12 and 1 number gets lost!

                if (freshIDs[i][0] <= max)
                {
                    //override the max value but only if the current value is bigger than the actual max value
                    max = Math.Max(max, freshIDs[i][1]);
                }
                else
                {
                    //if the range is not overlapping, then set min and max to the current range and add the count of numbers between the min and max value
                    result += max - min + 1;
                    min = freshIDs[i][0];
                    max = freshIDs[i][1];
                }
            }

            //add also the last range to the result
            result += max - min + 1;

            Console.WriteLine(result + " ingredient IDs are considered to be fresh!");
        }
    }
}