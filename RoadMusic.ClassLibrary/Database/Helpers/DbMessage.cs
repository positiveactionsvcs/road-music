namespace RoadMusic.ClassLibrary.Database.Helpers
{
    internal class DbMessage
    {
        public DbMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
