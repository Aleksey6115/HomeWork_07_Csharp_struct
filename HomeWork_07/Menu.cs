using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        /// <summary>
        /// Метод в котором реализована программа
        /// </summary>
        public void Start()
        {
            #region необходимые переменные
            int select = 1; // Выбор пользователя
            int select_case = 1; // Переменная выбора в кейсах
            List<Note> list_note = new List<Note>(); // Коллекция для хранения записей
            Note nt = new Note(); // Объект для работы с заметками
            char yes_no = 'д'; // Ответ пользователя
            string path_file; // Путь к файлу с заметками
            int note_count; // Колличество заметок
            Menu menu = new Menu(); // Для испольщования методов класса Menu
            #endregion

            while (select != 6)
            {
                Console.Clear();
                Console.WriteLine("Выберите то, что Вы хотите сделать:\n" +
                    "1. Добавить заметку\t\t" +
                    "2. Работа с заметками\t" +
                    "3. Записать заметки в файл\n" +
                    "4. Загрузить заметки из файла\t" +
                    "5. Просмотреть заметки\t" +
                    "6. Закрыть программу");

                Console.Write("Ваш выбор: ");
                select = menu.UserСhoice(1, 6); // Пользователь делает выбор

                switch (select)
                {
                    case 1: // Добавление записи
                        #region
                        select_case = 1;
                        while (select_case != 3)
                        {
                            Console.Clear();
                            Console.WriteLine("Выберите способ добавления записи:\n" +
                                            "1. Создать вручную\t" +
                                            "2. Сгенерировать программой\t" +
                                            "3. Выход в главное меню\n");

                            Console.Write("Ваш выбор: ");
                            select_case = menu.UserСhoice(1, 3); // Пользователь делает выбор

                            switch (select_case)
                            {
                                case 1: // Добавление заметки вручную
                                    #region
                                    Console.Clear();
                                    while (yes_no != 'н')
                                    {
                                        nt = nt.AddNote(list_note);
                                        list_note.Add(nt);
                                        Console.Clear();

                                        Console.Write("Желаете добавить ещё запись?");
                                        yes_no = menu.UserChoiceYesNo();

                                        Console.Clear();
                                    }
                                    #endregion
                                    break;

                                case 2: // Генерирование заметок программой
                                    #region
                                    Console.Clear();

                                    Console.Write("Укажите, какое колличество заметок нужно сгенерировать: ");
                                    note_count = menu.UserСhoice(1, 1000); // Выбор пользователя

                                    for (int i = 0; i < note_count; i++) // Генерируем записи
                                    {
                                        nt = nt.AutoAddNote(list_note);
                                        list_note.Add(nt);
                                    }
                                    Console.WriteLine("Заметки добавлены успешно! Нажмите на любую клавишу...");
                                    Console.ReadKey();
                                    #endregion
                                    break;
                            }
                        }
                        #endregion
                        break;

                    case 2: // Работа с заметками
                        #region
                        Console.Clear();

                        if (list_note.Count == 0)
                        {
                            Console.WriteLine("Сначала нужно добавить записи!!! Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            select_case = 1;
                            while (select_case != 3)
                            {
                                Console.Clear();

                                Console.WriteLine("Выберите то, что нужно сделать:\n" +
                                                "1. Удалить заметки     " +
                                                "2. Редактировать заметки     " +
                                                "3. Выход в главное меню\n");

                                Console.Write("Ваш выбор: ");
                                select_case = menu.UserСhoice(1, 3); // Пользователь делает выбор

                                switch (select_case)
                                {
                                    case 1: // Удаление заметки
                                        Console.Clear();
                                        list_note = nt.DeleteNote(list_note);
                                        break;

                                    case 2: // Редактирование заметки
                                        Console.Clear();
                                        list_note = nt.EditNote(list_note);
                                        break;
                                }
                            }
                        }
                        #endregion
                        break;

                    case 3: // Записать заметки в файл
                        #region
                        Console.Clear();

                        if (list_note.Count == 0)
                        {
                            Console.WriteLine("Сначала нужно добавить записи!!! Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            Console.WriteLine("Укажите путь к файлу в который нужно записать заметки:");
                            path_file = Console.ReadLine();
                            nt.UnloadNote(path_file, list_note);
                        }
                        #endregion
                        break;

                    case 4: // Загрузить заметки из файла
                        #region
                        Console.Clear();

                        Console.WriteLine("Выберите способ загрузки заметок:\n" +
                                          "1. Загрузить все замеки из файла\t" +
                                          "2. Загрузить заметки по диапозону дат\t" +
                                          "3. Выход в главное меню\n");

                        Console.Write("Ваш выбор: ");
                        select_case = menu.UserСhoice(1, 3); // Пользователь делает выбор

                        switch (select_case)
                        {
                            case 1: // Загрузить все замеки из файла
                                #region
                                Console.WriteLine("Укажите путь к файлу из которого нужно загрузить заметки:");
                                path_file = Console.ReadLine();
                                list_note = nt.LoadNote(path_file);
                                #endregion
                                break;

                            case 2: // Загрузить заметки из диапозона дат
                                #region
                                Console.WriteLine("Укажите путь к файлу из которого нужно загрузить заметки:");
                                path_file = Console.ReadLine();
                                list_note = nt.LoadNoteByDate(path_file);
                                #endregion
                                break;
                        }
                        #endregion
                        break;

                    case 5: // Просмотреть заметки
                        #region
                        Console.Clear();

                        if (list_note.Count == 0)
                        {
                            Console.WriteLine("Сначала нужно добавить записи!!! Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            select_case = 1;
                            while (select_case != 4)
                            {
                                Console.Clear();

                                Console.WriteLine("Выберите способ просмотра заметок:\n" +
                                                  "1. Просмотреть все заметки\t\t" +
                                                  "2. Просмотреть заметки отсортированные по дате\n" +
                                                  "3. Просмотреть сначала Важные заметки\t" +
                                                  "4. Выход в главное меню\n");

                                Console.Write("Ваш выбор: ");
                                select_case = menu.UserСhoice(1, 4); // Выбор пользователя

                                switch (select_case)
                                {
                                    case 1: // Просмотреть все заметки
                                        #region
                                        foreach (var n in list_note) n.Show();
                                        Console.WriteLine("Нажмите любую клавишу...");
                                        Console.ReadKey();
                                        #endregion
                                        break;

                                    case 2: // Просмотреть заметки отсортированные по дате
                                        #region
                                        var sort_date_list_note = from n in list_note // Запрос LINQ
                                                                  orderby n.Date_of_Note
                                                                  select n;

                                        foreach (var n in sort_date_list_note) n.Show(); // Вывод результата запроса
                                        Console.WriteLine("Нажмите любую клавишу...");
                                        Console.ReadKey();
                                        #endregion
                                        break;

                                    case 3: // Просмотреть сначала Важные заметки
                                        #region
                                        var sort_list_note = from n in list_note // Запрос LINQ
                                                             orderby n.Important_Note descending
                                                             select n;

                                        foreach (var n in sort_list_note) n.Show(); // Вывод результата запроса
                                        Console.WriteLine("Нажмите любую клавишу...");
                                        Console.ReadKey();
                                        #endregion
                                        break;
                                }
                            }
                        }
                        #endregion
                        break;
                }
            }

        }
    }
}
