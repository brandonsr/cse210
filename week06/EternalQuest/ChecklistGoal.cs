class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _completedCount;
    private int _bonusPoints;
    private bool _bonusAwarded;

    public ChecklistGoal(string name, string description, int points, int requiredCount, int bonusPoints) : base(name, description, points)
    {
        _requiredCount = requiredCount;
        _completedCount = 0;
        _bonusPoints = bonusPoints;
        _bonusAwarded = false;
    }

    public override void RecordEvent()
    {
        if (_completedCount < _requiredCount)
        {
            _completedCount++;
        }
    }

    public override bool IsComplete()
    {
        return _completedCount >= _requiredCount;
    }

    public override string GetDetails()
    {
        if (IsComplete())
        {
            return "[X] Completed";
        }
        else
        {
            return $"[ ] Completed {_completedCount}/{_requiredCount} times";
        }
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_requiredCount}|{_completedCount}|{_bonusPoints}|{_bonusAwarded}";
    }

    public bool IsFirstCompletion()
    {
        if (IsComplete() && !_bonusAwarded)
        {
            _bonusAwarded = true;
            return true;
        }
        return false;
    }

    public int GetBonusPoints()
    {
        return _bonusPoints;
    }
}
