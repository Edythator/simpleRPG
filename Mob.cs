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
            string[] mobNames = { "yes", "no" };
            Name = mobNames[Program.Rnd.Next(0, mobNames.Length - 1)];
            CP = cp;
            Level = level;
        }
    }
}
