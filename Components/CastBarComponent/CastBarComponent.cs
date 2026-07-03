using Godot;

public partial class CastBarComponent : Node2D
{
    [Export]
    public ProgressBar progressBar;

    [Export]
    public Sprite2D interuptIcon;

    public Timer castTimer;

    private int interuptsNeeded;

    private bool isCasting = false;

    [Signal]
    public delegate void CastFinishedEventHandler();

    private double castDuration = 0;

    public override void _Ready()
    {
        this.Hide();
        castTimer = (Timer)FindChild("CastTime");
    }

    public override void _Process(double delta)
    {
        if (isCasting == true)
        {
            progressBar.Value = (castDuration - castTimer.TimeLeft) / castDuration * 100;
        }
        else
        {
            progressBar.Value = 0;
        }
    }

    public void startCast(double time)
    {
        this.Show();
        isCasting = true;
        castTimer.Start(time);
        castDuration = time;
    }

    public float GetTimeLeft()
    {
        return (float)castTimer.TimeLeft;
    }

    public void castInerupted()
    {
        Hide();
        isCasting = false;
        castTimer.Stop();
        progressBar.Value = 0;
    }

    public void _OnCastTimeTimeout()
    {
        Hide();
        progressBar.Value = 100;
        castTimer.Stop();
        isCasting = false;

        EmitSignal(SignalName.CastFinished);
    }
}
