using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleRPG
{
    class Mob : Entity
    {
        public int CP;
        public int Level;
        public Mob(string name, int hp, int cp, int level) : base(name, hp)
        {
            // add more names
            string[] mobNames = { "yes", "no" };
            Name = mobNames[Program.Rnd.Next(0, mobNames.Length - 1)];
            int averageHP = 0;
            int averageCP = 0;
            foreach (Fighter f in Program.Fighters)
            {
                averageHP += f.HP;
                averageCP += f.CP;
            }

            averageHP /= Program.Fighters.Length;
            averageCP /= Program.Fighters.Length;
            HP = Program.Rnd.Next(averageHP - 20, averageHP + 20);
            CP = Program.Rnd.Next(averageCP - 20, averageCP + 20);

            // base level on fighter lvl
            Level = level;
        }
    }
}
