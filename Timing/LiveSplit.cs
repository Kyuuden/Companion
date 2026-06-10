using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace FF.Rando.Companion.Timing;
internal class LiveSplit : ITimer
{
    private readonly NamedPipeClientStream _pipeClient;
    private readonly StreamWriter _streamWriter;
    private readonly StreamReader _streamReader;
    private bool _isErrored;
    private bool _isPaused;
    private bool _isRunning;

    public LiveSplit()
    {
        _pipeClient = new NamedPipeClientStream("LiveSplit");
        _streamWriter = new StreamWriter(_pipeClient, new UTF8Encoding(false))
        {
            NewLine = "\n"
        };
        _streamReader = new StreamReader(_pipeClient);
    }

    public bool ShowLocally => false;

    public bool IsRunning => _isRunning;

    public TimeSpan? Elapsed => GetElapsed();

    public TimerStatus Status
    {
        get
        {
            if (_isErrored)
                return TimerStatus.Error;
            if (_isRunning)
            {
                if (_isPaused)
                    return TimerStatus.Paused;

                return TimerStatus.Running;
            }
            if (_pipeClient.IsConnected)
                return TimerStatus.Ready;
            return TimerStatus.New;
        }
    }

    public void Info(string message)
    {
    }

    private TimeSpan? GetElapsed()
    {
        var timeString = SendCommandWithResponse("getcurrenttime");

        if (TimeSpan.TryParseExact(timeString, "c", CultureInfo.CurrentCulture, out var timeSpan))
            return timeSpan;

        return null;
    }

    public void Initialize()
    {
        _isErrored = false;
        if (!_pipeClient.IsConnected)
        {
            try
            {
                _pipeClient.Connect(5);
            }
            catch (Exception e)
            {
                _isErrored = true;
                Debug.WriteLine("Could not connect. " + e.Message);
            }
        }

        SendCommand("reset");
    }

    public void Start()
    {
        SendCommand("starttimer");
        _isRunning = true;
    }

    public void Stop()
    {
        SendCommand("split");
        _isRunning = false;
    }

    public void Pause()
    {
        _isPaused = true;
        SendCommand("pause");
    }

    public void Resume()
    {
        _isPaused = false;
        SendCommand("resume");
    }

    private void SendCommand(string data)
    {
        if (!_pipeClient.IsConnected)
        {
            Console.WriteLine("LiveSplit pipe is not connected!");
            return;
        }

        try
        {
            _streamWriter?.WriteLine(data);
            _streamWriter?.Flush();
        }
        catch (Exception e)
        {
            _isErrored = true;
            Debug.WriteLine("Pipe Error:" + e.Message);
        }
    }

    private string? SendCommandWithResponse(string data)
    {
        if (!_pipeClient.IsConnected)
        {
            Console.WriteLine("LiveSplit pipe is not connected!");
            return null;
        }

        try
        {
            _streamWriter?.WriteLine(data);
            _streamWriter?.Flush();
            return _streamReader.ReadLine();
        }
        catch (Exception e)
        {
            _isErrored = true;
            Debug.WriteLine("Pipe Error:" + e.Message);
        }

        return null;
    }

    public void Dispose()
    {
        if (_pipeClient.IsConnected)
        {
            _pipeClient.Dispose();
            _streamWriter.Dispose();
        }
    }
}
