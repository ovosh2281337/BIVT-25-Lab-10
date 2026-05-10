using System;

public abstract class Purple
{
    private string _input;

    public string Input
    {
        get { return _input; }
    }

    protected Purple(string text)
    {
        if (text == null)
        {
            _input = "";
        }
        else
        {
            _input = text;
        }
    }

    public abstract void Review();

    public virtual void ChangeText(string text)
    {
        if (text == null)
        {
            _input = "";
        }
        else
        {
            _input = text;
        }

        Review();
    }
}