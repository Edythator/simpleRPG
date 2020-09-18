namespace simpleRPG
{
    public class Fighter : Entity
    {
        public string Nickname { get; set; }
        public int Level { get; private set; }
        public int XP { get; private set; }
        public string Faction { get; private set; }

        public Fighter(string name, int hp, int cp, int level, int xp, string faction) : base(name, hp, cp)
        {
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }
        public Fighter(string name, string nickname, int hp, int cp, int level, int xp, string faction) : base(name, hp, cp)
        {
            Nickname = nickname;
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }
        public string GetPrintableName()
        {
            if (!string.IsNullOrEmpty(Nickname))
                return Nickname;
            return Name;
        }
    }
}
