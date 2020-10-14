namespace simpleRPG
{
    public class Fighter : Entity
    {
        public int MaxHP { get; set; }
        public string Nickname { get; set; }
        public int Level { get; private set; }
        public int XP { get; private set; }
        public string Faction { get; private set; }

        public Fighter(string name, int hp, int maxHP, int cp, int level, int xp, string faction) : base(name, hp, cp)
        {
            MaxHP = maxHP;
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }

        public Fighter(string name, string nickname, int hp, int maxHP, int cp, int level, int xp, string faction) : base(name, hp, cp)
        {
            MaxHP = maxHP;
            Nickname = nickname;
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }

        // vi vill kunna veta namnet på den onda, ifall den har ett smeknamn eller inte, så vi skriver ut det här i båda fallen
        public string GetPrintableName()
        {
            if (!string.IsNullOrEmpty(Nickname))
                return Nickname;
            return Name;
        }
    }
}
