using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public static class Party
    {
        public static Dictionary<string, Character> memberList = new Dictionary<string, Character>();

        public static void AddMember(Character member)
        {
            memberList.Add(member.name, member);
            Console.WriteLine("{0} added to party.", member.name);
        }

        public static void RemoveMember(Character member)
        {
            memberList.Remove(member.name);
            Console.WriteLine("{0} removed from party.", member.name);
        }
    }
}
