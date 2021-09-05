using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_07
{
    /// <summary>
    /// Класс содержит в себе методы для работы меню
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Метод проверяет ввод на правильность 
        /// </summary>
        /// <param name="min_select">Возможный минимум значения</param>
        /// <param name="max_select">Возможный максимум значения</param>
        /// <returns></returns>
        public int UserСhoice(int min_select, int max_select)
        {
            bool input_value; // Проверка правильности ввода
            int select; // Выбор сделанный пользователем

            do // Пользователь делает выбор
            {
                input_value = int.TryParse(Console.ReadLine(), out select);
                if (select < min_select ^ select > max_select)
                {
                    Console.Write("Введите ещё раз: ");
                    input_value = false;
                }
                else if (!input_value) Console.Write("Введите ещё раз: ");
            }
            while (!input_value);
            return select;
        }

        /// <summary>
        /// Метод проверяет ввод на правильность 
        /// и возвращает либо "д" либо "н"
        /// </summary>
        /// <returns></returns>
        public char UserChoiceYesNo()
        {
            char yes_no; // Выбор пользователя

            do // Пользователь делает выбор
            {
                Console.Write("\nВведите н/д: ");
                yes_no = Console.ReadKey(false).KeyChar;
                yes_no = Char.ToLower(yes_no);
            }
            while (yes_no != 'н' && yes_no != 'д');
            return yes_no;
        }
    }
}
