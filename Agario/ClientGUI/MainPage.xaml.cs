using System.Numerics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using System.Timers;
using AgarioModels;

namespace TowardAgarioStepOne;

public partial class MainPage : ContentPage
{
    private Vector2 CircleCenter;
    private Vector2 Direction;
    private bool initialized;
    private System.Timers.Timer timer;
    private World worldModel;

    public MainPage()
    {
        InitializeComponent();
        OnTimerElapsed();
        worldModel = new World();
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
        PlaySurface.Drawable = new WorldDrawable(worldModel);
        timer = new System.Timers.Timer(16);
        timer.Elapsed += GameStep;
        timer.Start();
    }

    private void GameStep(object state, ElapsedEventArgs e)
    {
        worldModel.AdvanceGameOneStep();
        Dispatcher.Dispatch(PlaySurface.Invalidate);
        circleCenterLabel.Text = CircleCenter.ToString();
        directionLabel.Text = Direction.ToString();
        Console.WriteLine("invoking");
    }
}