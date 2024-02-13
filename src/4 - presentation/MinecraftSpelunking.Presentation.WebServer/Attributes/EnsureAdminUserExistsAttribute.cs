namespace MinecraftSpelunking.Presentation.WebServer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EnsureAdminUserExistsAttribute : Attribute
    {
        public static readonly EnsureAdminUserExistsAttribute Default = new EnsureAdminUserExistsAttribute(true);

        public readonly bool Value;

        public EnsureAdminUserExistsAttribute(bool value)
        {
            this.Value = value;
        }
    }
}
