﻿using System.Numerics;
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

namespace ClientGUI;

public partial class MainPage : ContentPage
{
    private Vector2 CircleCenter;
    private Vector2 Direction;
    private bool initialized;
    private System.Timers.Timer timer;
    private World worldModel;
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
        CircleCenter = new Vector2(random.Next(0, 100), random.Next(0, 100));
        Direction = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
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
        if (connection.tcpClient.Connected)
        {
            connection.AwaitMessagesAsync();
            connection.Send(string.Format(Protocols.CMD_Start_Game, "JO"));
            connection.logger.LogInformation($"Connected to {connection.tcpClient.Client.RemoteEndPoint}");
        }
        else
        {
            connection.logger.LogError($"Not Connected. Terminating program");
            //await DisplayAlert("connection error:", "please check port and IPAddress.", "OK");
        }

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
            worldModel.players = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Player_Object.Length..]);
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

    private async void PointerChanged(object sender, PointerEventArgs e)
    {       
        Point ? position = e.GetPosition((View)sender);
    }

    private async void OnTap(object sender, PointerEventArgs e)
    {

    }

    private async void PanUpdated(object sender, PointerEventArgs e)
    {

    }
}