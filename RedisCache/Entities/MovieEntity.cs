namespace RedisCache.Entities
{
    public class MovieEntity
    {
        public MovieEntity(string name, int rattings)
        {
            Id = Guid.NewGuid();
            Name = name;
            Rattings = rattings;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rattings { get; set; }
    }
}
