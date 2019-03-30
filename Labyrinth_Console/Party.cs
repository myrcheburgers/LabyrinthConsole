using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    static class Party
    {
        public static Dictionary<string, Character> memberList = new Dictionary<string, Character>();

        public static void AddMember(Character member)
        {
            member.id = member.name.Replace(" ", string.Empty);
            memberList.Add(member.id, member);

            //memberList.Add(member.name, member);
            //memberList[member.name].id = member.name.Replace(" ", string.Empty);
            //(string instead of char since we're removing something)
            Console.WriteLine("{0} added to party.", member.name);
        }

        public static void RemoveMember(Character member)
        {
            memberList.Remove(member.name);
            Console.WriteLine("{0} removed from party.", member.name);
        }

        public static void RemoveAll()
        {
            memberList.Clear();
        }

        public static void PrintStats(Character member)
        {
            Console.WriteLine("{0}:", member.name);
            Console.Write("    Job: {0}, ID: {1}, HP: {2}/{3}, MP: {4}/{5}", member.playerClass.name, member.id, member.vitals.hp, member.vitals.hpmax, member.vitals.mp, member.vitals.mpmax);
            Console.Write(Environment.NewLine);
        }
    }
}
