namespace Demo.MVC.Models
{
    public class TosterViewModel
    {
        public string Content { get; set; } = "Done";
        public TosterType Type { get; set; } = TosterType.Success;
    }

    public enum TosterType
    {
        Success,
        Info,
        Warning,
        Error
    }
}
