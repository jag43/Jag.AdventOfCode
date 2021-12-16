using System.Linq;

namespace Jag.AdventOfCode.Y2020.Day2
{
    public class PasswordPolicy
    {
        public string Password { get; set; }

        public char RequiredChar { get; set; }

        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }

        public bool IsValid1
        {
            get 
            {
                var count = Password.Count(c => c == RequiredChar);
                return count >= FirstNumber && count <= SecondNumber;
            }
        }

        public bool IsValid2
        {
            get 
            {
                var firstChar = Password[FirstNumber - 1] == RequiredChar;
                var secondChar = Password[SecondNumber - 1] == RequiredChar;
                return firstChar ^ secondChar;
            }
        }
    }
}