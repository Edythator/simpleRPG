using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleRPG
{
    class Fighter : Entity
    {
        public int CP;
        public int Level;
        public int XP;
        public string Faction;

        public Fighter(string name, int hp, int cp, int level, int xp, string faction) : base(name, hp)
        {
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }
    }
}
