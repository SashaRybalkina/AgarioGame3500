using System.Numerics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Timers;
using AgarioModels;
using Communications;
using FileLogger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace ClientGUI;

public partial class MainPage : ContentPage
{
    private Vector2 CircleCenter;
    private Vector2 Direction;
    private bool initialized;
    private System.Timers.Timer timer;
    private World worldModel;
    private int x;
    private int y;
    Networking network;

    public MainPage()
    {
        InitializeComponent();
        worldModel = new World();
        OnSizeAllocated(400, 400);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        Debug.WriteLine($"OnSizeAllocated {width} {height}");

        if (!initialized)
        {
            initialized = true;
            InitializeGameLogic();
        }
    }

    private void InitializeGameLogic()
    {
        PlaySurface.Drawable = new WorldDrawable(ref worldModel);
        timer = new System.Timers.Timer(16);
        timer.Elapsed += GameStep;
        timer.Start();
    }

    private void GameStep(object state, ElapsedEventArgs e)
    {
        Dispatcher.Dispatch(PlaySurface.Invalidate);
        Debug.WriteLine("invoking");
    }

    private void onConnect(Networking connection)
    {
        if (connection.tcpClient.Connected)
        {
            connection.AwaitMessagesAsync();
            connection.Send(string.Format(Protocols.CMD_Start_Game, "sexy ass bitch"));
            connection.logger.LogInformation($"Connected to {connection.tcpClient.Client.RemoteEndPoint}");
        }
        else
        {
            connection.logger.LogError($"Not Connected. Terminating program");
        }

    }

    private void onDisconnect(Networking connection)
    {
        connection.logger.LogInformation($"{this.network.tcpClient.Client.RemoteEndPoint} disconnect");
    }

    private void onMessage(Networking connection, string message)
    {
        if (message.StartsWith(Protocols.CMD_Food))
        {
            List<Food> foods = JsonSerializer.Deserialize<List<Food>>(message[Protocols.CMD_Food.Length..]);
            foreach (Food food in foods)
            {
                worldModel.foods.Add(food);
            }
        }
        else if (message.StartsWith(Protocols.CMD_HeartBeat))
        {
            string toSend = string.Format(Protocols.CMD_Move, x, y);
            connection.Send(toSend);
            OnSplit();
        }
        else if (message.StartsWith(Protocols.CMD_Update_Players))
        {
            worldModel.players = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Update_Players.Length..]);
            foreach (Player player in worldModel.players)
            {
                if (player.ID.Equals(worldModel.playerID))
                {
                    worldModel.player = player;
                }
            }
        }
        else if (message.StartsWith(Protocols.CMD_Player_Object))
        {
            worldModel.playerID = JsonSerializer.Deserialize<long>(message[Protocols.CMD_Player_Object.Length..]);
        }
        else if (message.StartsWith(Protocols.CMD_Eaten_Food))
        {
            long[] eaten = JsonSerializer.Deserialize<long[]>(message[Protocols.CMD_Eaten_Food.Length..]);
            List<Food> foodsToRemove = new();
            foreach (Food food in worldModel.foods)
            {
                if (eaten.Contains(food.ID))
                {
                    foodsToRemove.Add(food);
                }
            }
            foreach (Food food in foodsToRemove)
            {
                worldModel.foods.Remove(food);
            }
        }
    }

    private async void onStartButtonClicked(object sender, EventArgs e)
    {
        try
        {
            network = new Networking(NullLogger.Instance, onMessage,
                onDisconnect, onConnect, '\n');
            string hostname = ServerIPEntry.Text;
            int port = int.Parse(ServerPortEntry.Text);
            network.ID = UsernameEntry.Text;
            network.Connect(hostname, port);
            welcomeScreen.IsVisible = false;
            gameScreen.IsVisible = true;
        }
        catch
        {
            await DisplayAlert("Cannot Connect", "Please retry", "OK");
        }
    }

    private void PointerChanged(object sender, PointerEventArgs e)
    {
        Point? position = e.GetPosition((View)sender);
        x = (int)(position.Value.X * 5000 / 400);
        y = (int)(position.Value.Y * 5000 / 400);
    }

    private void OnSplit()
    {
        splitEntry.Focus();
        if (splitEntry.Text.Contains(" "))
        {
            network.Send(string.Format(Protocols.CMD_Split, x, y));
            splitEntry.Text = "";
        }
    }

    void splitButton_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    void splitEntry_Focused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
    }
}