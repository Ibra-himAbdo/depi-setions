namespace Demo.MVC.Models
{
    public class TosterViewModel
    {
        public TosterType Type { get; set; } = TosterType.Success;

        public string Content { get; set; } = "Done";
    }

    public enum TosterType
    {
        Success,
        Error,
        Warning,
        Info
    }
}
