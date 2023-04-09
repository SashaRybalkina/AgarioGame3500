using System;
using System.Text.Json;
using Communications;
using FileLogger;
using Microsoft.Extensions.Logging;

public class Program
{
    private static Networking network;
    private static CustomFileLogger _logger = new CustomFileLogger("StepThree");

    public static void Main(String[] args)
    {
        network = new Networking(_logger, onMessage, onDisconnect, onConnect, '\n');
        network.Connect("localhost", 11000);
        network.AwaitMessagesAsync();
        Console.ReadLine();
    }
    private static async void onConnect(Networking net)
    {

    }

    private static async void onDisconnect(Networking net)
    {

    }

    private static async void onMessage(Networking net, string message)
    {
        Console.WriteLine(message);
        string foodString = "";
        if (message.StartsWith("CMD_Food"))
        {
            Console.WriteLine(message);
            foodString = message.Remove(0, 8);
            String[] ?food = JsonSerializer.Deserialize<String[]>(foodString);
        }
    }
}