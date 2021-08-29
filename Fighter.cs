namespace simpleRPG
{
    public class Fighter : FightingEntity
    {
        public int MaxHP { get; init; }

        public (string name, int cp) Item { get; init; }
        public string Nickname { get; set; }
        public int Level { get; }
        public int XP { get; }
        public string Faction { get; }
        
        public Fighter(string name, int hp, int maxHP, int cp, int level, int xp, string faction, string nickname = "", (string name, int cp) item = default)
        {
            Name = name;
            HP = hp;
            MaxHP = maxHP;
            Nickname = nickname;
            CP = cp;
            Level = level;
            XP = xp;
            Faction = faction;
        }
        
        public override void Attack(Entity enemy, out int damage)
        {
            // implement scaling amount of crit derived from the total CP the character can do and chance of crit
            damage = Program.Rnd.Next(CP - 2, CP + 2);
            if (damage > enemy.HP)
                damage = enemy.HP;

            enemy.HP -= damage;
        }

        // vi vill kunna veta namnet på den onda, ifall den har ett smeknamn eller inte, så vi skriver ut det här i båda fallen
        public string GetPrintableName() => !string.IsNullOrEmpty(Nickname) ? Nickname : Name;
    }
}
