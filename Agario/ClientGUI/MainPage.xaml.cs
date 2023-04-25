using System.Diagnostics;
using System.Timers;
using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ClientGUI;

/// <summary>
/// Author:    Aurora Zuo 
/// Partner:   Sasha Rybalkina
/// Date:      14-Apr-2023
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500, Aurora Zuo, and Zhuofei Lyu - This work not 
///            be copied for use in Academic Coursework.
///            
/// Aurora Zuo and Sasha Rybalkina certify that we wrote this code from scratch and
/// did not copy it in part or whole from another source. All 
/// references used in the completion of the assignments are cited 
/// in our README file.
/// 
/// File Content
///		This class demonstrates the necessary functionalities for the Client
///		GUI, it allows the the client to connect to the server, send and receive
///		serialized data between client and server, and draw circles on the screen.
/// </summary>
public partial class MainPage : ContentPage
{
    private bool initialized;
    private System.Timers.Timer timer;
    private World worldModel;
    private int x;
    private int y;
    private Networking network;

    public MainPage()
    {
        InitializeComponent();
        worldModel = new World();
        OnSizeAllocated(400, 400);
    }

    /// <summary>
    /// Initializes the size of the layout
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
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

    /// <summary>
    /// Initializes the worldDrawable object, sets up the timer to update 30fps
    /// </summary>
    private void InitializeGameLogic()
    {
        PlaySurface.Drawable = new WorldDrawable(ref worldModel);
        timer = new System.Timers.Timer(30);
        timer.Elapsed += GameStep;
        timer.Start();
    }

    /// <summary>
    /// This method is called by the game timer at regular intervals to update
    /// the game state and trigger a redraw of the play surface.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="e"></param>
    private void GameStep(object state, ElapsedEventArgs e)
    {
        Dispatcher.Dispatch(PlaySurface.Invalidate);
        Debug.WriteLine("invoking");
    }

    /// <summary>
    /// Connects to the server, tells the server to start game.
    /// </summary>
    /// <param name="connection"></param>
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

    /// <summary>
    /// Disconnects from the server
    /// </summary>
    /// <param name="connection"></param>
    private void onDisconnect(Networking connection)
    {
        connection.logger.LogInformation($"{this.network.tcpClient.Client.RemoteEndPoint} disconnect");
    }

    /// <summary>
    /// The onMessage method is called whenever a new message is received from the server.
    /// It checks the message type and performs the necessary actions based on the type of message.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="message"></param>
    private async void onMessage(Networking connection, string message)
    {
        string connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = "cs3500.eng.utah.edu,14330",
            InitialCatalog = "S2023_u1362376",
            UserID = "S2023_u1362376",
            Password = "Hungry_for_Men",
            Encrypt = false
        }.ConnectionString;
        using SqlConnection con = new SqlConnection(connectionString);

        if (message.StartsWith(Protocols.CMD_Food))
        {
            // Deserialize the food objects from the message and add them to the game world.
            List<Food> foods = JsonSerializer.Deserialize<List<Food>>(message[Protocols.CMD_Food.Length..]);
            foreach (Food food in foods)
            {
                worldModel.foods.Add(food);
            }
        }
        else if (message.StartsWith(Protocols.CMD_HeartBeat))
        {
            // Send the player's current position to the server and split the player into two if necessary.
            string toSend = string.Format(Protocols.CMD_Move, x, y);
            connection.Send(toSend);
            OnSplit();
        }
        else if (message.StartsWith(Protocols.CMD_Update_Players))
        {
            // Deserialize the player objects from the message and update the game world.
            worldModel.players = JsonSerializer.Deserialize<List<Player>>(message[Protocols.CMD_Update_Players.Length..]);

            foreach (Player player in worldModel.players)
            {
                con.Open();

                using SqlCommand insertTime = new SqlCommand($@"INSERT INTO Time VALUES ('{DateTime.Now.ToString("t")}', 'NULL')", con);
                using SqlDataReader reader1 = insertTime.ExecuteReader();

                con.Close();
                if (player.ID.Equals(worldModel.playerID))
                {
                    worldModel.player = player;
                }
            }
        }
        else if (message.StartsWith(Protocols.CMD_Player_Object))
        {
            // Deserialize the player ID from the message and store it in the game world.
            worldModel.playerID = JsonSerializer.Deserialize<long>(message[Protocols.CMD_Player_Object.Length..]);
        }
        else if (message.StartsWith(Protocols.CMD_Eaten_Food))
        {
            // Deserialize the IDs of the eaten food objects from the message and remove them from the game world.
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
        else if (message.StartsWith(Protocols.CMD_Dead_Players))
        {
            long[] deadPlayers = JsonSerializer.Deserialize<long[]>(message[Protocols.CMD_Update_Players.Length..]);
            List <Player> players = worldModel.players;
            List<Player> sortedPlayers = players.OrderBy(o => o.Mass).ToList();
            int index = 0;
            foreach (Player player in sortedPlayers)
            {
                if (deadPlayers.Contains(player.ID))
                {
                    con.Open();
                    using SqlCommand insertTime = new SqlCommand($@"UPDATE Time SET EndTime = '{DateTime.Now.ToString("t")}' WHERE Player = {player.Name}", con);
                    using SqlDataReader reader1 = insertTime.ExecuteReader();
                    con.Close();

                    con.Open();
                    using SqlCommand insertMass = new SqlCommand($@"INSERT INTO HighMass VALUES ('{player.Name}', '{player.Mass.ToString()}')", con);
                    using SqlDataReader reader2 = insertMass.ExecuteReader();
                    con.Close();

                    con.Open();
                    using SqlCommand insertRank = new SqlCommand($@"INSERT INTO HighRank VALUES ('{player.Name}', '{index.ToString()}')", con);
                    using SqlDataReader reader3 = insertRank.ExecuteReader();
                    con.Close();
                }
                index++;
            }
        }
    }

    /// <summary>
    /// Event handler for clicking the start button on the welcome screen.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

            // sets the welcome screen to be unvisible, shows the game screen.
            welcomeScreen.IsVisible = false;
            gameScreen.IsVisible = true;
        }
        catch
        {
            await DisplayAlert("Cannot Connect", "Please retry", "OK");
        }
    }

    /// <summary>
    /// Event handler for when the pointer is changed on the view.
    /// Calculates the x and y coordinates of the pointer and updates the x and y fields accordingly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PointerChanged(object sender, PointerEventArgs e)
    {
        Point? position = e.GetPosition((View)sender);
        x = (int)(position.Value.X * 5000 / 400);
        y = (int)(position.Value.Y * 5000 / 400);
    }

    /// <summary>
    /// Handles the event of the user pressing the "Split" button.
    /// </summary>
    private void OnSplit()
    {
        /// sets the focus on the "splitEntry" field        
        splitEntry.Focus();
        /// checks if the user input contains a space character.
        if (splitEntry.Text.Contains(" "))
        {
            /// once the space bar is pressed, tells the server to split the current object
            network.Send(string.Format(Protocols.CMD_Split, x, y));
            splitEntry.Text = "";
        }
    }

    /// <summary>
    /// Unimplemented yet, not useful here
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void splitButton_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    /// <summary>
    /// Unimplemented yet, not useful here
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void splitEntry_Focused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
    }
}