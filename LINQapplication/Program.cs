using System;

namespace LINQApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WeekdaysConverting exercise = new WeekdaysConverting();
            Console.WriteLine("Введите номер задания");
            string choice = Console.ReadLine();
            exercise.GetActions(choice);
        }
    }
}