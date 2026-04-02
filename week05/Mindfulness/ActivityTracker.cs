using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class ActivityTracker
{
    private Dictionary<string, int> _activityCount = new Dictionary<string, int>();

    public void LogActivity(string activityName)
    {
        if (_activityCount.ContainsKey(activityName))
        {
            _activityCount[activityName]++;
        }
        else
        {
            _activityCount[activityName] = 1;
        }
    }

    public Dictionary<string, int> GetStats() => new Dictionary<string, int>(_activityCount);

    public void SaveLogs(string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(_activityCount, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving logs: {ex.Message}");
        }
    }

    public void LoadLogs(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _activityCount = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading logs: {ex.Message}");
        }
    }
}