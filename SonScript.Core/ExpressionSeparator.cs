using System.Text;

namespace SonScript.Core;

public class ExpressionSeparator
{
    private readonly StringBuilder _sb;
    private List<string> _result;
    private int _depth;

    public ExpressionSeparator() => 
        _sb = new StringBuilder();

    public List<string> Separate(string input)
    {
        _sb.Clear();

        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
        }

        _result = new List<string>();

        foreach (var character in input)
        {
            ParseCharacter(character);
        }

        if (_sb.Length > 0)
        {
            _result.Add(_sb.ToString());
        }

        return _result;
    }

    private void ParseCharacter(char character)
    {
        switch (character)
        {
            case '#':
                OpenFunc();
                break;
            case ')':
                _sb.Append(character);
                CloseFunc();
                return;
        }

        _sb.Append(character);
    }

    private void OpenFunc()
    {
        if (_depth == 0)
        {
            AddResult();
        }

        _depth++;
    }

    private void CloseFunc()
    {
        _depth--;

        if (_depth == 0)
        {
            AddResult();
        }
    }

    private void AddResult()
    {
        if (_sb.Length <= 0)
        {
            return;
        }

        _result.Add(_sb.ToString());
        _sb.Clear();
    }
}