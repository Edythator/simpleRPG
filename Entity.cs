namespace simpleRPG
{
    public abstract class Entity
    {
        public string Name { get; protected init; }
        public int HP { get; set; }
        public bool IsAlive() => HP > 0;
    }
}