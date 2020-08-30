using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleRPG
{
    class Mob : Entity
    {
        public static string name;
        public static int hp;
        public int CP;
        public int Level;
        public Mob() : base(name, hp)
        {
            // add more names
            string[] mobNames = { "yes", "no" };
            Name = mobNames[Program.Rnd.Next(0, mobNames.Length - 1)];
            int averageHP = 0, averageCP = 0, actualLength = 0;
            foreach (Fighter f in Program.Fighters)
            {
                if (f != null)
                {
                    averageHP += f.HP;
                    averageCP += f.CP;
                    actualLength++;
                }
            }

            averageHP /= actualLength;
            averageCP /= actualLength;
            HP = Program.Rnd.Next(averageHP - 20, averageHP + 20);
            CP = Program.Rnd.Next(averageCP - 20, averageCP + 20);

            // base level on fighter lvl
            int highestLvl = Program.Fighters.Where(x => x != null).OrderByDescending(x => x.HP).First().HP;
            Level = Program.Rnd.Next(highestLvl - 3, highestLvl + 2);
        }
    }
}
