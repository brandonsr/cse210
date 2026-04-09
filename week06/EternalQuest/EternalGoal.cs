class EternalGoal : Goal
{
    private int _timesCompletedCount;

    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        _timesCompletedCount = 0;
    }

    public override void RecordEvent()
    {
        _timesCompletedCount++;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetails()
    {
        return $"[∞] Completed {_timesCompletedCount} times";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}|{_timesCompletedCount}";
    }
}
