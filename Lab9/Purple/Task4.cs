using System;

public class Task4 : Purple
{
    private string _output;
    public string Output => _output;

    private (string, char)[] _codes;

    // конструктор теперь принимает два параметра
    public Task4(string text, (string, char)[] codes) : base(text)
    {
        _output = "";
        
        // защита от null, чтобы массив всегда был
        if (codes != null)
        {
            _codes = codes;
        }
        else
        {
            _codes = new (string, char)[0];
        }
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input))
        {
            _output = "";
            return;
        }

        // если кодов нет, просто возвращаем оригинал
        if (_codes.Length == 0)
        {
            _output = Input;
            return;
        }

        string result = "";

        // идем по зашифрованной строке посимвольно
        for (int i = 0; i < Input.Length; i++)
        {
            char currentChar = Input[i];
            bool isDecoded = false;

            // ищем текущий символ в таблице кодов
            for (int j = 0; j < _codes.Length; j++)
            {
                if (currentChar == _codes[j].Item2)
                {
                    // нашли код, подставляем оригинальную пару букв
                    result += _codes[j].Item1; 
                    isDecoded = true;
                    break;
                }
            }

            // если символ не является кодом, просто копируем его как есть
            if (!isDecoded)
            {
                result += currentChar;
            }
        }

        _output = result;
    }

    public override string ToString()
    {
        if (Output == null)
        {
            return "";
        }
        return Output;
    }
}