

internal class Program
{
    private static void Main(string[] args)
    {
        //Publisher
        Task publisherTask = Task.Run(() =>
        {

        });


        //Subscriber
        Task subscriberTask = Task.Run(() =>
        {

        });

        Task.WaitAll(publisherTask, subscriberTask);
    }
}

public class MessageEventArgs : EventArgs
{
    public string Message { get; set; }
}