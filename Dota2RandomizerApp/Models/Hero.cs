namespace Dota2RandomizerApp.Models
{
    public class Hero
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string LocalizedName { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public string AttackType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string PrimaryAttribute { get; set; } = string.Empty;
        public CustomPool? CustomPool { get; set; } = new CustomPool();
    }
}
