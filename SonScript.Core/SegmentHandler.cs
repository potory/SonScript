using System.Text;

namespace SonScript.Core;

public class SegmentHandler
{
    private enum State
    {
        Text,
        Category,
        Function
    }

    private readonly StringBuilder _stringBuilder = new();
    private readonly Stack<char> _stack = new();
    private readonly List<string> _cache = new();

    private State _currentState = State.Text;

    public IReadOnlyList<string> GetSegments(string text)
    {
        _stringBuilder.Clear();
        _stack.Clear();
        _cache.Clear();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '{' && text[i + 1] == '{')
            {
                if (_currentState == State.Category)
                {
                    throw new SegmenterException("Category call can't contain any curly brackets");
                }

                SetState(State.Category);

                _stringBuilder.Append('{');
                _stack.Push(text[i]);
                _stack.Push(text[i+1]);

                i++;
            }

            if (text[i] == '}' && _currentState == State.Category)
            {

                _stringBuilder.Append('}');
                _stringBuilder.Append('}');

                _stack.Pop();
                _stack.Pop();

                i++;
                continue;
            }

            if (_currentState != State.Text && text[i] != '#' && !_stack.Any())
            {
                SetState(State.Text);
            }

            if (text[i] == '#' && _currentState != State.Function)
            {
                SetState(State.Function);

                while (text[i] != '(')
                {
                    _stringBuilder.Append(text[i]);
                    i++;
                }
            }

            if (text[i] == '(' && _currentState == State.Function)
            {
                _stack.Push(text[i]);
            }

            if (text[i] == ')' && _currentState == State.Function)
            {
                _stack.Pop();
            }
            
            _stringBuilder.Append(text[i]);
        }

        _cache.Add(_stringBuilder.ToString());
        return _cache.ToArray();
    }

    private void SetState(State state)
    {
        _currentState = state;

        if (_stringBuilder.Length == 0)
        {
            return;
        }

        _cache.Add(_stringBuilder.ToString());
        _stringBuilder.Clear();
    }
}

public class SegmenterException : Exception
{
    public SegmenterException(string message) : base(message)
    {
        
    }
}