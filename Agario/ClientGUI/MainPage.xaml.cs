using System.Numerics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Timers;
using AgarioModels;
using Communications;
using FileLogger;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClientGUI;

public partial class MainPage : ContentPage
{
    private Vector2 CircleCenter;
    private Vector2 Direction;
    private bool initialized;
    private System.Timers.Timer timer;
    private World worldModel;
    Networking network = new Networking(new CustomFileLogger(""), (c,s) => {; },
        (w) => {; }, (b) => {; }, '\n');

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

    private async void onConnect(Networking connection)
    {
        if (connection.tcpClient.Connected)
        {
            connection.Send(Protocols.CMD_Start_Game);
            connection.logger.LogInformation($"Connected to {connection.tcpClient.Client.RemoteEndPoint}");
        }
        else
        {
            connection.logger.LogError($"Not Connected. Terminating program");
            await DisplayAlert("connection error:", "please check port and IPAddress.", "OK");
        }

    }

    private async void onDisconnect(Networking connection)
    {
        connection.logger.LogInformation($"{this.network.tcpClient.Client.RemoteEndPoint} disconnect");
    }

    private async void onMessage(Networking connection, string message)
    {               
        if (message.StartsWith(Protocols.CMD_Food))
        {
            worldModel.foods = JsonSerializer.Deserialize<List<Food>>(message.Remove(0, 15));           
        }
    }

    private async void onStartButtonClicked(object sender, EventArgs e)
    {      
        try
        {
            network = new Networking(new CustomFileLogger(UsernameEntry.Text), onMessage,
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