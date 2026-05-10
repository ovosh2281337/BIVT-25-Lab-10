using System;

public class Task3 : Purple
{
    private string _output;
    public string Output => _output;

    private (string, char)[] _codes;
    public (string, char)[] Codes => _codes;

    public Task3(string text) : base(text)
    {
        _output = "";
        _codes = new (string, char)[0];
    }

    private void AddToArray(ref string[] array, string item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;
    }

    private void AddToArray(ref int[] array, int item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;
    }

    private void AddToArray(ref char[] array, char item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;
    }

    private void AddToArray(ref (string, char)[] array, (string, char) item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;
    }

    private bool IsLetter(char c)
    {
        return char.IsLetter(c);
    }

    public override void Review()
    {
        if (string.IsNullOrEmpty(Input))
        {
            _output = "";
            _codes = new (string, char)[0];
            return;
        }

        string[] pairs = new string[0];
        int[] counts = new int[0];
        int[] firstSeen = new int[0]; // индексы первого появления 

        // ищем все возможные пары букв и считаем их
        for (int i = 0; i < Input.Length - 1; i++)
        {
            char c1 = Input[i];
            char c2 = Input[i + 1];

            if (IsLetter(c1) && IsLetter(c2))
            {
                string currentPair = c1.ToString() + c2.ToString();
                bool found = false;

                // проверяем, есть ли уже такая пара
                for (int j = 0; j < pairs.Length; j++)
                {
                    if (pairs[j] == currentPair)
                    {
                        counts[j]++;
                        found = true;
                        break;
                    }
                }

                // если нет, сохраняем новую
                if (!found)
                {
                    AddToArray(ref pairs, currentPair);
                    AddToArray(ref counts, 1);
                    AddToArray(ref firstSeen, i);
                }
            }
        }

        // сортируем три массива одновременно
        // по убыванию частоты, потом по возрастанию индекса
        for (int i = 0; i < pairs.Length - 1; i++)
        {
            for (int j = 0; j < pairs.Length - i - 1; j++)
            {
                bool needSwap = false;
                
                if (counts[j] < counts[j + 1])
                {
                    needSwap = true;
                }
                else if (counts[j] == counts[j + 1])
                {
                    if (firstSeen[j] > firstSeen[j + 1])
                    {
                        needSwap = true;
                    }
                }

                if (needSwap)
                {
                // свапаем счетчики, индексы и сами пары
                (counts[j], counts[j + 1]) = (counts[j + 1], counts[j]);
                (firstSeen[j], firstSeen[j + 1]) = (firstSeen[j + 1], firstSeen[j]);
                (pairs[j], pairs[j + 1]) = (pairs[j + 1], pairs[j]);
                }
            }
        }

        // берем топ 5 или меньше, если пар не набралось
        int topLimit = 5;
        if (pairs.Length < 5)
        {
            topLimit = pairs.Length;
        }
        
        if (topLimit == 0)
        {
            _output = Input; 
            return;
        }

        // ищем свободные символы asccii
        char[] freeChars = new char[0];
        for (int code = 32; code <= 126; code++)
        {
            char asciiChar = (char)code; // из числа в аски
            bool isUsedInText = false;

            for (int i = 0; i < Input.Length; i++)
            {
                if (Input[i] == asciiChar)
                {
                    isUsedInText = true;
                    break;
                }
            }

            if (!isUsedInText)
            {
                AddToArray(ref freeChars, asciiChar);
            }

            if (freeChars.Length == topLimit)
            {
                break; // набрали сколько нужно
            }
        }

        // заполняем таблицу кодов
        for (int i = 0; i < topLimit; i++)
        {
            AddToArray(ref _codes, (pairs[i], freeChars[i]));
        }

        //  замена
        string result = "";
        int idx = 0;

        while (idx < Input.Length)
        {
            bool isReplaced = false;

            // если есть хотя бы два символа впереди
            if (idx < Input.Length - 1)
            {
                string checkPair = Input[idx].ToString() + Input[idx + 1].ToString();
                
                for (int c = 0; c < _codes.Length; c++)
                {
                    if (checkPair == _codes[c].Item1)
                    {
                        result += _codes[c].Item2; // вставляем код
                        idx += 2; // прыгаем через 2 символа
                        isReplaced = true;
                        break;
                    }
                }
            }

            // если не совпало ни с одной парой, просто копируем букву
            if (!isReplaced)
            {
                result += Input[idx];
                idx++;
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