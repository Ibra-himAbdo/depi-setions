namespace Demo.MVC
{
    //public enum AppRole
    //{
    //    Admin,
    //    Manager,
    //    User
    //}

    // esay
    public static class AppRoles
    {
        public const string Admin = nameof(Admin);
        public const string Manager = nameof(Manager);
        public const string User = nameof(User);

        public static readonly IReadOnlyList<string> Roles = [Admin, Manager, User];
    }
}
