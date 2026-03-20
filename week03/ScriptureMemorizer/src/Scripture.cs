namespace ScriptureMemorizer;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ')
                     .Select(w => new Word(w))
                     .ToList();
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    // Stretch challenge: only pick from visible words
    public void HideRandomWords(int count = 3)
    {
        var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        int toHide = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < toHide; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index); // avoid hiding same word twice in one round
        }
    }

    public string GetDisplayText()
    {
        string words = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} - {words}";
    }
}