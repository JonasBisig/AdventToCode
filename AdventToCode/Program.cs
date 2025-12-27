using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = "987654321111111,811111111111119,234234234234278,818181911112111";

            //to use the example string enable this line and comment the line below
            //IEnumerable<string> batteries = example.Split(",");
            IEnumerable<string> batteries = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            long joltage = 0;
            foreach (string battery in batteries)
            {
                //convert the string in a List full of integer
                List<int> originalDigits = battery.Select(c => int.Parse(c.ToString())).ToList();

                //remove last entry
                List<int> getFirstDigit = originalDigits.ToList();
                getFirstDigit.RemoveAt(getFirstDigit.Count - 1);

                //get the max value -> first digit
                int firstDigit = getFirstDigit.Max();

                //cut away all digits from 0 to the index of the first digit
                List<int> getSecondDigit = originalDigits.ToList();
                getSecondDigit.RemoveRange(0, originalDigits.IndexOf(firstDigit) + 1);

                //determine the max value in this array -> second digit
                int secondDigit = getSecondDigit.Max();

                //add value to joltage
                joltage += int.Parse(firstDigit.ToString() + secondDigit.ToString());
            }
            Console.WriteLine("Highest joltage is: " + joltage);
        }
    }
}