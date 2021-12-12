using System.Linq;

namespace Jag.AdventOfCode.Y2020.Day2
{
    public class PasswordPolicy
    {
        public string Password { get; set; }

        public char RequiredChar { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public bool IsValid 
        {
            get 
            {
                var count = Password.Count(c => c == RequiredChar);
                return count >= Min && count <= Max;
            }
        }

    }
}