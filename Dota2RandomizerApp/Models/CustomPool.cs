namespace Dota2RandomizerApp.Models
{
    public class CustomPool
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PoolName { get; set; } = string.Empty;
        public IEnumerable<Hero> Heroes { get; set; } = new HashSet<Hero>();
    }
}
