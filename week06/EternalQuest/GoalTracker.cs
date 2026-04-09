using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

class GoalTracker
{
    private List<Goal> _goals;
    private int _score;

    public GoalTracker()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            Goal goal = _goals[index];

            // For checklist goals, only award points if event actually increments progress
            if (goal is ChecklistGoal checklistGoal)
            {
                if (checklistGoal.IsComplete())
                {
                    // Already complete, don't award more points
                    return;
                }
            }

            goal.RecordEvent();

            // Award points for the event
            _score += goal.GetPoints();

            // Award bonus points if checklist goal is completed for the first time
            if (goal is ChecklistGoal checklistGoalAfter && checklistGoalAfter.IsFirstCompletion())
            {
                _score += checklistGoalAfter.GetBonusPoints();
            }
        }
    }

    public int GetTotalScore()
    {
        return _score;
    }

    public List<Goal> GetGoals()
    {
        return _goals;
    }

    public void SaveToFile(string filename)
    {
        var data = new Dictionary<string, object>
        {
            { "score", _score },
            { "goals", _goals.Select(g => g.GetStringRepresentation()).ToList() }
        };

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            return;
        }

        try
        {
            string json = File.ReadAllText(filename);
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                // Load score
                if (root.TryGetProperty("score", out JsonElement scoreElement))
                {
                    _score = scoreElement.GetInt32();
                }

                // Load goals
                if (root.TryGetProperty("goals", out JsonElement goalsElement))
                {
                    _goals.Clear();
                    foreach (JsonElement goalElement in goalsElement.EnumerateArray())
                    {
                        string goalString = goalElement.GetString();
                        Goal goal = ParseGoal(goalString);
                        if (goal != null)
                        {
                            _goals.Add(goal);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

    private Goal ParseGoal(string goalString)
    {
        string[] parts = goalString.Split('|');

        if (parts.Length < 3)
        {
            return null;
        }

        string goalType = parts[0];
        string name = parts[1];
        string description = parts[2];

        switch (goalType)
        {
            case "SimpleGoal":
                if (parts.Length >= 5)
                {
                    int points = int.Parse(parts[3]);
                    bool isComplete = bool.Parse(parts[4]);
                    SimpleGoal simpleGoal = new SimpleGoal(name, description, points);
                    if (isComplete)
                    {
                        simpleGoal.RecordEvent();
                    }
                    return simpleGoal;
                }
                break;

            case "EternalGoal":
                if (parts.Length >= 5)
                {
                    int points = int.Parse(parts[3]);
                    int timesCompleted = int.Parse(parts[4]);
                    EternalGoal eternalGoal = new EternalGoal(name, description, points);
                    for (int i = 0; i < timesCompleted; i++)
                    {
                        eternalGoal.RecordEvent();
                    }
                    return eternalGoal;
                }
                break;

            case "ChecklistGoal":
                if (parts.Length >= 8)
                {
                    int points = int.Parse(parts[3]);
                    int requiredCount = int.Parse(parts[4]);
                    int completedCount = int.Parse(parts[5]);
                    int bonusPoints = int.Parse(parts[6]);
                    bool bonusAwarded = parts.Length >= 9 ? bool.Parse(parts[7]) : false;
                    ChecklistGoal checklistGoal = new ChecklistGoal(name, description, points, requiredCount, bonusPoints);
                    for (int i = 0; i < completedCount; i++)
                    {
                        checklistGoal.RecordEvent();
                    }
                    // Mark bonus as awarded if it was in previous session
                    if (bonusAwarded)
                    {
                        checklistGoal.IsFirstCompletion();
                    }
                    return checklistGoal;
                }
                break;
        }

        return null;
    }
}
