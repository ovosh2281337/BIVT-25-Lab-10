using System;

public class Task2 : Purple // тяжело, тяжело
{
    private string[] _output;
    public string[] Output => _output;

    public Task2(string text) : base(text)
    {
        _output = new string[0];
    }

    private bool IsSpace(char c)
    {
        return c == ' ';
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
            _output = new string[0];
            return;
        }

        string[] words = new string[0];

        bool isWordFirst = !IsSpace(Input[0]); 
        bool isWordNow = isWordFirst;
        string buffer = "";

        // разбиваем текст на массив слов
        for (int i = 0; i < Input.Length; i++)
        {
            char currentChar = Input[i];
            // поменялся ли тип
            if (!IsSpace(currentChar) == isWordNow)
            {
                buffer += currentChar;
            }
            else
            {
                // сохраняем накопленный буфер
                if (isWordNow && buffer.Length > 0)
                {
                    AddToArray(ref words, buffer);
                }

                // переключаем и новый буфер
                isWordNow = !isWordNow;
                buffer = currentChar.ToString();
            }
        }

        // сохраняем последний кусок
        if (buffer.Length > 0 && isWordNow)
        {
            AddToArray(ref words, buffer);
        }

        
        string[] result = new string[0];
        string[] currentLine = new string[0];
        int charCount = 0;

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            
            int addedLength = word.Length;
            if (currentLine.Length > 0)
            {
                addedLength += 1; // +1 для пробела, если слово не первое
            }

            if (charCount + addedLength <= 50)
            {
                // слово влезает
                AddToArray(ref currentLine, word);
                charCount += addedLength;
            }
            else
            {
                // >50, добавляем пробелы
                AddToArray(ref result, SpaceLine(currentLine, 50));

                // текущее слово начинает новую строку
                currentLine = new string[0];
                AddToArray(ref currentLine, word);
                charCount = word.Length;
            }
        }

        // cохраняем ласт хвост, если там осталось слово
        if (currentLine.Length > 0)
        {
            AddToArray(ref result, SpaceLine(currentLine, 50));
        }

        _output = result;
    }

    private string SpaceLine(string[] words, int targetLength)
    {
        if (words.Length == 0)
        {
        return new string(' ', targetLength);
        }

        int wordsLength = 0;
        for (int i = 0; i < words.Length; i++)
        {
            wordsLength += words[i].Length;
        }

        // cколько всего пробелов нужно вставить
        int totalSpaces = targetLength - wordsLength;

        // если слово всего одно
        if (words.Length == 1)
        {
            return words[0] + new string(' ', totalSpaces);
        }

        int gaps = words.Length - 1;          // количество дырок
        int baseSpaces = totalSpaces / gaps;  // гарантированно в каждую
        int extraSpaces = totalSpaces % gaps; // остаток

        string result = "";
        for (int i = 0; i < words.Length; i++)
        {
            result += words[i];
            
            // пробелы после слов, кроме последнего
            if (i < gaps)
            {
                int spacesToAdd = baseSpaces;
                
                // если остались пробелы, накидываем по одному
                if (i < extraSpaces)
                {
                    spacesToAdd += 1;
                }
                
                result += new string(' ', spacesToAdd);
            }
        }
        
        return result;
    }

    public override string ToString()
    {
        if (Output == null)
        {
            return "";
        }
        return string.Join("\n", Output);
    }
}