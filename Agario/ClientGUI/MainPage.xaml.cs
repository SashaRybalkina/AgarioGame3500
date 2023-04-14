﻿using System.Numerics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Timers;
using AgarioModels;
using FileLogger;
using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

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
        OnTimerElapsed();
        worldModel = new World();
        OnSizeAllocated(400, 400);
    }

    private void OnTimerElapsed()
    {
        var random = new Random();
        CircleCenter = new Vector2(x, y);
        //Direction = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
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
        worldModel.AdvanceGameOneStep();
        Dispatcher.Dispatch(PlaySurface.Invalidate);       
        Debug.WriteLine("invoking");
    }

    private void onConnect(Networking connection)
    {
        connection.AwaitMessagesAsync();
        connection.Send(string.Format(Protocols.CMD_Start_Game, "A"));
    }

    private void onDisconnect(Networking connection)
    {
        ///connection.logger.LogInformation($"{this.network.tcpClient.Client.RemoteEndPoint} disconnect");
    }

    private void onMessage(Networking connection, string message)
    {               
        if (message.StartsWith(Protocols.CMD_Food))
        {
            worldModel.foods = JsonSerializer.Deserialize<List<Food>>(message[Protocols.CMD_Food.Length..]);
        }
        else if (message.StartsWith(Protocols.CMD_HeartBeat))
        {
            string toSend = string.Format(Protocols.CMD_Move, x, y);
            connection.Send(toSend);
        }
        if (message.StartsWith(Protocols.CMD_Update_Players))
        {
            //List<Player> p = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Update_Players.Length..]);
            worldModel.players = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Update_Players.Length..]);
        }
        if (message.StartsWith(Protocols.CMD_Player_Object))
        {
            worldModel.playerID = JsonSerializer.Deserialize<long>(message[Protocols.CMD_Player_Object.Length..]);
        }
        //if (message.StartsWith(Protocols.CMD_Eaten_Food))
        //{
        //    int[] eaten = JsonSerializer.Deserialize<int[]>(message[Protocols.CMD_Player_Object.Length..]);
        //    foreach (int )
        //}
    }

    private async void onStartButtonClicked(object sender, EventArgs e)
    {      
        try
        {
            network = new Networking(NullLogger.Instance, onConnect,
                onDisconnect, onMessage, '\n');
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

    private async void PointerChanged(object sender, PointerEventArgs e)
    {       
        Point ? position = e.GetPosition((View)sender);
        x = (int)(position.Value.X * 5000 / 800);
        y = (int)(position.Value.Y * 5000 / 800);
    }

    private async void OnTap(object sender, PointerEventArgs e)
    {

    }

    private async void PanUpdated(object sender, PointerEventArgs e)
    {

    }
}