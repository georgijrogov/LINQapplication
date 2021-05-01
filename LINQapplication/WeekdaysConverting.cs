using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LINQApp
{
    class WeekdaysConverting
    {
        private readonly string[] _weekdays = new CultureInfo("ru").DateTimeFormat.DayNames;
        private readonly Dictionary<string, Action> _actions;
        public WeekdaysConverting()
        {
            _actions = new Dictionary<string, Action>
            {
                { "1", GetNamesLength },
                { "2", GetNamesLengthSum },
                { "3", GetVowelsСonsonantsQuantity },
                { "4", GetOrderedByNames },
                { "5", GetOrderedByNamesLength },
                { "6", GetLettersFrequency },
                { "7", GetOrderedByLettersFrequency },
                { "8", GetMinMaxFrequencyLetter }
            };
        }
        public void GetActions(string choice)
        {
            if (_actions.ContainsKey(choice))
            {
                Console.WriteLine("Задание {0}", choice);
                _actions[choice].Invoke();
            }
            else
            {
                Console.WriteLine("Нет такого задания");
            }
        }
        /// <summary>
        /// Напишите LINQ запрос, который на вход получает список названий дней недели, а на выходе выдает: 
        /// </summary>

        /// <summary>
        /// 1. длины названий
        /// </summary>
        private void GetNamesLength()
        {
            var nameLength = _weekdays.Select(x => x.Length);
            foreach (var length in nameLength)
                Console.WriteLine(length);
        }

        /// <summary>
        /// 2. сумму длин названий
        /// </summary>
        private void GetNamesLengthSum()
        {
            var nameLengthSum = _weekdays.Select(x => x.Length).Sum();
            Console.WriteLine(nameLengthSum);
        }

        /// <summary>
        /// 3. количество гласных и согласных букв, используемых во всех названиях
        /// </summary>
        private void GetVowelsСonsonantsQuantity()
        {
            char[] vowels = { 'а', 'у', 'о', 'ы', 'и', 'э', 'я', 'ю', 'ё', 'е' };
            var vowelsCount = _weekdays.SelectMany(x => x.ToCharArray()).Count(x => vowels.Contains(x));
            var arrayCount = _weekdays.SelectMany(x => x.ToCharArray()).Count();

            Console.WriteLine("Гласных: {0}", vowelsCount);
            Console.WriteLine("Согласных: {0}", arrayCount - vowelsCount);
            Console.WriteLine("Всего букв: {0}", arrayCount);
        }

        /// <summary>
        /// 4. отсортированный список названий (по убыванию и по возрастанию самих названий)
        /// </summary>
        private void GetOrderedByNames()
        {
            var orderByName = _weekdays.OrderBy(x => x);
            foreach (var item in orderByName)
                Console.WriteLine(item);
        }

        /// <summary>
        /// 5. отсортированный список названий (по убыванию и по возрастанию длин названий)
        /// </summary>
        private void GetOrderedByNamesLength()
        {
            var orderByLength = _weekdays.OrderBy(x => x.Length).ThenBy(x => x);
            foreach (var item in orderByLength)
                Console.WriteLine(item);
        }

        /// <summary>
        /// 6. частоты букв, используемых в названиях
        /// </summary> 
        private void GetLettersFrequency()
        {
            var letters = GetGroupedLetters();
            foreach (var item in letters)
                Console.WriteLine("Буква {0} встречается {1} раз(а)", item.Letter, item.Count);
        }

        /// <summary>
        /// 7. частоты букв, используемых в названиях, упорядоченные по возрастанию частоты/буквы
        /// </summary>
        private void GetOrderedByLettersFrequency()
        {
            var groupedLetters = GetGroupedLetters();
            var letters = groupedLetters.OrderBy(x => x.Count).ThenBy(x => x.Letter);

            foreach (var item in letters)
                Console.WriteLine("Буква {0} встречается {1} раз(а)", item.Letter, item.Count);
        }

        /// <summary>
        /// 8. самую редкую и самую частую буквы
        /// </summary>
        private void GetMinMaxFrequencyLetter()
        {
            var groupedLetters = GetGroupedLetters();
            var maxFreq = groupedLetters.OrderBy(x => x.Count).Last();
            var minFreq = groupedLetters.OrderBy(x => x.Count).First();
            Console.WriteLine("Самая частая буква: {0} - встречается {1} раз(а)", maxFreq.Letter, maxFreq.Count);
            Console.WriteLine("Самая нечастая буква: {0} - встречается {1} раз(а)", minFreq.Letter, minFreq.Count);
        }

        private IEnumerable<(char Letter, int Count)> GetGroupedLetters()
        {
            IEnumerable<(char Letter, int Count)> letters = _weekdays.SelectMany(x => x.ToCharArray()).GroupBy(x => x).Select(x => (Letter: x.Key, Count: x.Count()));

            return letters;
        }
    }
}