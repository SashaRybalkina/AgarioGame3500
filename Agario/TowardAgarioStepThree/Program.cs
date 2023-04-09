using Communications;
using FileLogger;
using Microsoft.Extensions.Logging;

public class Main
{
    private static Networking network;
    private static CustomFileLogger _logger = new CustomFileLogger("StepThree");

    public static void main(string[] agrs)
    {
        network = new Networking(_logger, onConnect, onDisconnect, onMessage, '\n');
        network.Connect("localhost", 11000);
        network.AwaitMessagesAsync();
        Console.ReadLine();
    }
    private static async void onConnect(Networking net, string name)
    {

    }

    private static async void onDisconnect(Networking net)
    {

    }

    private static async void onMessage(Networking net)
    {
        Console.WriteLine(network);
    }
}