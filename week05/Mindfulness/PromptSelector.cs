using System;
using System.Collections.Generic;

class PromptSelector
{
    private Dictionary<int, List<string>> _unusedPrompts = new Dictionary<int, List<string>>();
    private Random _random = new Random();

    public string GetNextPrompt(List<string> prompts)
    {
        int key = prompts.GetHashCode();

        if (!_unusedPrompts.ContainsKey(key) || _unusedPrompts[key].Count == 0)
        {
            _unusedPrompts[key] = new List<string>(prompts);
        }

        int index = _random.Next(_unusedPrompts[key].Count);
        string selectedPrompt = _unusedPrompts[key][index];
        _unusedPrompts[key].RemoveAt(index);

        return selectedPrompt;
    }
}