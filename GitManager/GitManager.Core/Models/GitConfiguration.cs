namespace GitManager.Core.Models
{
    public class GitConfiguration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public GitConfigurationScope Scope { get; set; }
    }

    public enum GitConfigurationScope
    {
        local,
        global,
        unset
    }
}
