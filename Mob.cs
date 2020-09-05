namespace simpleRPG
{
    class Mob : Entity
    {
        public int CP;
        public int Level;
        public Mob(string name, int hp, int cp, int level) : base(name, hp)
        {
            Name = name;
            HP = hp;
            CP = cp;
            Level = level;
        }
    }
}
