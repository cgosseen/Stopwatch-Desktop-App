using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.Threading;

namespace StopWatchApp.Views;

public partial class MainView : UserControl
{
    private readonly Stopwatch _stopwatch = new();
    private readonly DispatcherTimer _viewableTimer;

    public MainView()
    {
        InitializeComponent();

        _viewableTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1)
        };

        _viewableTimer.Tick += Timer_Tick;
    }

    private void StartStopButton(object? sender, RoutedEventArgs e) //don't understand the input parameters here
    {
        var elapsedTextBlock = this.FindControl<TextBlock>("ElapsedTextBlock");

        if (!_stopwatch.IsRunning)
        {
            _stopwatch.Start();
            _viewableTimer.Start();
            //reset the interval or make it a sub lap?
        }
        else
        {
            _stopwatch.Stop();
            _viewableTimer.Stop();

            TimeSpan elapsedTime = _stopwatch.Elapsed;
            String timeElapsedMessage = String.Format("{0:00}:{1:00}:{2:00}",  elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds / 10);
            //Debug.WriteLine(timeElapsedMessage);

            elapsedTextBlock.Text = timeElapsedMessage;

            //if (elapsedTextBlock != null)
            //{
            //    elapsedTextBlock.Text = timeElapsedMessage;
            //}

            //when stop is selected, store elapsedTime into another variable to populate a small table 
            // to the right that shows the # interval and its time and populates like a list? or stack? or queue to pop when greater than 10 entries 
        }
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        var elapsedTextBlock = this.FindControl<TextBlock>("ElapsedTextBlock");

        if (elapsedTextBlock != null && _stopwatch.IsRunning)
        {
            TimeSpan elapsedTime = _stopwatch.Elapsed;
            elapsedTextBlock.Text = string.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds / 10);
        }
    }
    

    private void ResetButton(object? sender, RoutedEventArgs e)
    {

        var elapsedTextBlock = this.FindControl<TextBlock>("ElapsedTextBlock");
        

        if (elapsedTextBlock != null && _stopwatch.IsRunning)
        {
            _stopwatch.Stop();
            _stopwatch.Reset();

            elapsedTextBlock.Text = "00:00:00";
        }
        else if (elapsedTextBlock != null)
        {
            _stopwatch.Reset();

            elapsedTextBlock.Text = "00:00:00";
        }

    }
}




