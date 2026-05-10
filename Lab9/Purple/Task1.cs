using System;

public class Task1 : Purple
{
    private string _output;
    public string Output => _output;

    public Task1(string text) : base(text)
    {
        _output = "";
    }

    private bool IsWordChar(char c)
    {
        return char.IsLetter(c) || c == '-' || c == '\'';
    }

    private void AddToArray(ref string[] array, string item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input))
        {
            _output = "";
            return;
        }

        string[] words = new string[0];
        string[] signs = new string[0];

        // определяем, с чего начинается строка
        bool isWordFirst = IsWordChar(Input[0]);
        bool isWordNow = isWordFirst;
        string buffer = "";

        // разбиваем текст на массив слов и массив разделителей
        for (int i = 0; i < Input.Length; i++)
        {
            char currentChar = Input[i];
            // поменялся ли тип
            if (IsWordChar(currentChar) == isWordNow)
            {
                buffer += currentChar;
            }
            else
            {
                // сохраняем накопленный буфер
                if (isWordNow)
                {
                    AddToArray(ref words, buffer);
                }
                else
                {
                    AddToArray(ref signs, buffer);
                }

                // переключаем и новый буфер
                isWordNow = !isWordNow;
                buffer = currentChar.ToString(); // без этого последний символ теряется
            }
        }

        // сохраняем последний кусок, массив не доходит до конца прост
        if (buffer.Length > 0)
        {
            if (isWordNow) AddToArray(ref words, buffer);
            else AddToArray(ref signs, buffer);
        }

        // разворачиваем слова в массиве слов
        for (int i = 0; i < words.Length; i++)
        {
            char[] chars = words[i].ToCharArray();
            Array.Reverse(chars);
            words[i] = new string(chars);
        }

        // cлияние
        string result = "";
        int wordIdx = 0;
        int signIdx = 0;

        while (wordIdx < words.Length || signIdx < signs.Length)
        {
            // ориг текст начинался со слова
            if (isWordFirst)
            {
                if (wordIdx < words.Length) result += words[wordIdx++];
                if (signIdx < signs.Length) result += signs[signIdx++];
            }
            // ориг текст начинался со знака
            else
            {
                if (signIdx < signs.Length) result += signs[signIdx++];
                if (wordIdx < words.Length) result += words[wordIdx++];
            }
        }

        _output = result;
    }

    public override string ToString()
    {
        return Output;
    }
}