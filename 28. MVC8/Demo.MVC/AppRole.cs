namespace Demo.MVC;

//public enum AppRole
//{
//    Admin,
//    Manger,
//    User
//}

// esay 
public static class AppRoles
{
    public const string Admin = nameof(Admin);
    public const string Manger = nameof(Manger);
    public const string User = nameof(User);

    public static readonly List<string> Roles = [Admin, Manger, User];
}