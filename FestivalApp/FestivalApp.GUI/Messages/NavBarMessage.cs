namespace FestivalApp.GUI.Messages
{
    public class NavBarMessage : IMessage
    {
        public string Page { get; }
        public NavBarMessage(string page)
        {
            Page = page;
        }
    }
}
