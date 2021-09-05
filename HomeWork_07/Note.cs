using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HomeWork_07
{
    struct Note
    {
        #region Переменные
        /// <summary>
        /// Номер заметки
        /// </summary>
        public int Note_Number { get; private set; } 

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime Date_of_Note { get; private set; }

        /// <summary>
        /// Сама запись в заметках
        /// </summary>
        public string Note_Entry { get; private set; }

        /// <summary>
        /// Дополнительные примечания в заметке
        /// </summary>
        public string Note_Additional { get; private set; }

        /// <summary>
        /// Отметка о важности заметки
        /// </summary>
        public bool Important_Note { get; private set; }
        #endregion

        /// <summary>
        /// Конструктор структуры Note
        /// </summary>
        /// <param name="number">Номер записи</param>
        /// <param name="entry">Сама запись</param>
        /// <param name="additional">Примечания к записи</param>
        /// <param name="important">Пометка о важности</param>
        public Note(int number, DateTime date, string entry, string additional, bool important)
        {
            Note_Number = number;
            Date_of_Note = date;
            Note_Entry = entry;
            Note_Additional = additional;
            Important_Note = important;
        }

        #region Методы
        /// <summary>
        /// Метод отображает заметку
        /// </summary>
        /// <param name="nt">Заметка</param>
        public void Show()
        {
            string important; // Вывод отметки о важности
            string date = Date_of_Note.ToShortDateString(); // Вывод даты без времени
            if (Important_Note == true) important = "Важная";
            else important = "Не важная";

            Console.WriteLine($"№ {Note_Number}" +
                $"\nДата записи: {date}" +
                $"\nЗаметка: {Note_Entry}" +
                $"\nПримечание: {Note_Additional}" +
                $"\nОтметка о важности: {important}" +
                $"\n-----------------------------------------------------------------");
        }

        /// <summary>
        /// Метод формирует заметку и возвращает объект типа Note
        /// </summary>
        /// <param name="list_note">Коллекция в которую сохраняются заетки</param>
        /// <returns></returns>
        public Note AddNote(List<Note> list_note)
        {
            int note_number; // Номер записи
            DateTime date; // Дата записи 
            string note_entry; // Сама заметка
            string note_additional; // Примечание в заметке
            bool important_note; // Отметка о важности записи
            char yes_no; // Переменная для выбора
            bool input_value; // Переменная для проверки правильности ввода

            // Делаем коректный номер записи, чтоб при удалении и последующем создании всё было ок
            if (list_note.Count == 0) note_number = 1;
            else note_number = list_note[list_note.Count - 1].Note_Number + 1;

            Console.WriteLine($"Давайте добавим запись:");
            Console.Write("Желаете вести дату записи? (В случае отказа поле заполнится автоматически)");

            do // Пользователь делает выбор
            {
                Console.Write("\nВведите н/д: ");
                yes_no = Console.ReadKey(false).KeyChar;
                yes_no = Char.ToLower(yes_no);
            }
            while (yes_no != 'н' && yes_no != 'д');

            if (yes_no == 'д')
            {
                Console.WriteLine();
                do // Ввод даты пользователем
                {
                    Console.Write("Укажите дату в формате - чис.мес.год: ");
                    input_value = DateTime.TryParse(Console.ReadLine(), out date);
                }
                while (!input_value);
            }
            else date = DateTime.Now;

            Console.Clear();
            Console.Write("Напишите саму заметку: ");
            note_entry = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Нужно ли добавить примечания к заметке?");
            do // Пользователь делает выбор
            {
                Console.Write("Введите н/д: ");
                yes_no = Console.ReadKey(false).KeyChar;
                yes_no = Char.ToLower(yes_no);
            }
            while (yes_no != 'н' && yes_no != 'д');

            if (yes_no == 'д')
            {
                Console.Write("\nНапишите примечание: ");
                note_additional = Console.ReadLine();
            }

            else note_additional = "Примечаний нет";

            Console.Clear();
            Console.Write("Пометить заметку как важную?");
            do // Пользователь делает выбор
            {
                Console.Write("\nВведите н/д: ");
                yes_no = Console.ReadKey(false).KeyChar;
                yes_no = Char.ToLower(yes_no);
            }
            while (yes_no != 'н' && yes_no != 'д');

            if (yes_no == 'д') important_note = true;
            else important_note = false;
            Console.WriteLine();
            Console.WriteLine("Заметка добавлена успешно! Нажмите любую клавишу...");
            Console.ReadKey();

            // Формируем заметку
            Note result = new Note(note_number, date, note_entry, note_additional, important_note);
            return result;
        }

        /// <summary>
        /// Метод производит удалению заметки по номеру, дате или отметке о важности
        /// </summary>
        /// <param name="note_list">Коллекция, которая хранит в себе записи</param>
        /// <returns></returns>
        public List<Note> DeleteNote(List<Note> note_list)
        {
                bool delete_flag; // Прошло ли удаление
                bool while_flag = false; // Остановка цикла while
                int select; // Переменная выбора
                bool input_value; // Проверка правильности ввода
                int delete_count = 0; // Счётчик удалений

                int number_note = 0; // Номер записи, которую будем удалять
                DateTime date; // Дата по которой будем удалять запись


                Console.WriteLine("По какому полю Вы хотите произвести удаление?\n" +
                    "1. Номер записи     " +
                    "2. Дата записи     " +
                    "3. Пометка о важности     ");

                Console.Write("Ваш выбор: ");

                do // Пользователь делает выбор
                {
                    input_value = int.TryParse(Console.ReadLine(), out select);
                    if (select < 1 ^ select > 5)
                    {
                        Console.Write("Введите ещё раз: ");
                        input_value = false;
                    }
                    else if (!input_value) Console.Write("Введите ещё раз: ");
                }
                while (!input_value);

                switch (select)
                {
                    case 1: // Удаление по номеру записи
                        #region
                        Console.Clear();
                        Console.Write("Введите номер записи, которую нужно удалить: ");

                        do // Пользователь вводит номер записи, которую нужно удалять
                        {
                            input_value = int.TryParse(Console.ReadLine(), out number_note);
                            if (select < 1 ^ select > note_list.Count + 1)
                            {
                                Console.Write("Введите ещё раз: ");
                                input_value = false;
                            }
                            else if (!input_value) Console.Write("Введите ещё раз: ");
                        }
                        while (!input_value);

                        do // Удаление записи

                        {
                            while_flag = true;
                            foreach (var nt in note_list)
                            {
                                if (nt.Note_Number == number_note)
                                {
                                    delete_flag = note_list.Remove(nt);

                                    if (delete_flag)
                                    {
                                        Console.WriteLine($"Запись под номером {nt.Note_Number} была удалена.");
                                        while_flag = false;
                                        delete_count++;
                                        break;
                                    }
                                }
                            }
                        }
                        while (!while_flag);

                        if (delete_count == 0)
                        {
                            Console.WriteLine("Записи с таким номером в списке нет!\n" +
                                "Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            Console.WriteLine("Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }
                        #endregion
                        break;

                    case 2: // Удаление по дате
                        #region
                        Console.Clear();
                        do // Ввод даты пользователем
                        {
                            Console.Write("Укажите дату в формате - чис.мес.год: ");
                            input_value = DateTime.TryParse(Console.ReadLine(), out date);
                        }
                        while (!input_value);

                        do // Удаление записи

                        {
                             while_flag = true;
                            foreach (var nt in note_list)
                            {
                                if (nt.Date_of_Note.Month == date.Month && nt.Date_of_Note.Day == date.Day && nt.Date_of_Note.Year
                                    == date.Year)
                                {
                                    delete_flag = note_list.Remove(nt);

                                    if (delete_flag)
                                    {
                                        Console.WriteLine($"Запись под номером {nt.Note_Number} была удалена.");
                                        while_flag = false;
                                        delete_count++;
                                        break;
                                    }
                                }
                            }
                        }
                        while (!while_flag);

                        if (delete_count == 0)
                        {
                            Console.WriteLine("Записей с такой датой в списке нет!\n" +
                                "Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            Console.WriteLine("Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        #endregion
                        break;

                    case 3: // Удаление по отметке о важности
                        #region
                        Console.Clear();
                    while_flag = false;

                        Console.WriteLine("С какой отметкой Вы хотите удалить записи?\n" +
                            "1. Важные\n" +
                            "2. Не важные");
                        Console.Write("Ваш выбор: ");

                        do // Пользователь делает выбор
                        {
                            input_value = int.TryParse(Console.ReadLine(), out select);
                            if (select < 1 ^ select > 2)
                            {
                                Console.Write("Введите ещё раз: ");
                                input_value = false;
                            }
                            else if (!input_value) Console.Write("Введите ещё раз: ");
                        }
                        while (!input_value);

                        if (select == 1) //Удаление важных заметок
                        {
                            do
                            {
                            while_flag = true;
                            foreach (var nt in note_list)
                                {
                                    if (nt.Important_Note)
                                    {
                                        delete_flag = note_list.Remove(nt);
                                        if (delete_flag)
                                        {
                                            Console.WriteLine($"Запись под номером {nt.Note_Number} была удалена.");
                                            while_flag = false;
                                            delete_count++;
                                            break;
                                        }
                                    }
                                }
                            }
                            while (!while_flag);

                        if (delete_count == 0)
                        {
                            Console.WriteLine("Записей с такой отметкой в списке нет!\n" +
                                "Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            Console.WriteLine("Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                    }

                    else // Удаление не важных заметок
                        {
                        do
                        {
                            while_flag = true;
                            foreach (var nt in note_list)
                            {
                                if (!nt.Important_Note)
                                {
                                    delete_flag = note_list.Remove(nt);
                                    if (delete_flag)
                                    {
                                        Console.WriteLine($"Запись под номером {nt.Note_Number} была удалена.");
                                        while_flag = false;
                                        delete_count++;
                                        break;
                                    }
                                }
                            }
                        }
                        while (!while_flag);

                        if (delete_count == 0)
                            {
                                Console.WriteLine("Записей с такой отметкой в списке нет!\n" +
                                    "Нажмите на любую клавишу...");
                                Console.ReadKey();
                            }

                            else
                            {
                                Console.WriteLine("Нажмите на любую клавишу...");
                                Console.ReadKey();
                            }

                        }
                        #endregion
                        break;
                }
            return note_list;
        }

        /// <summary>
        /// Метод редактирует в заметке - саму заметку, примечание или отметку о важности
        /// </summary>
        /// <param name="note_list">Коллекция которая хранит заметки</param>
        /// <returns></returns>
        public List<Note> EditNote (List<Note> note_list)
        {
                int number_note; // Номер записи
                bool input_value; // Переменная для проверки ввода
                int search_count = 0; // Сколько результатов нашлось
                int select = 1; // Переменная для выбора
                string note_entry; // Сама заметка
                string note_additional; // Примечание
                bool important_note; // Отметка о важности
                Note edit_note = new Note(); // Заметка, которая заменит старую
                char yes_no; // Переменная для выбора

                Console.Write("Укажите номер записи, которую Вы хотите отредактировать: ");

                do // Пользователь вводит номер записи
                {
                    input_value = int.TryParse(Console.ReadLine(), out number_note);
                    if (!input_value) Console.Write("\nВведите ещё раз: ");
                }
                while (!input_value);

                for (int i = 0; i < note_list.Count; i++) // Ищем заметку
                {
                    if (note_list[i].Note_Number == number_note) // Находим заметку
                    {
                        while (select != 4)
                        {
                            note_list[i].Show();
                            Console.WriteLine("Какое поле будем редактировать?\n" +
                                "1. Саму заметку     " +
                                "2. Примечание     " +
                                "3. Поставить отметку о важности     " +
                                "4. Выйти из режима редактирования\n");

                            Console.Write("Ваш выбор: ");

                            do // Пользователь делает выбор
                            {
                                input_value = int.TryParse(Console.ReadLine(), out select);
                                if (select < 1 ^ select > 4)
                                {
                                    Console.Write("Введите ещё раз: ");
                                    input_value = false;
                                }
                                else if (!input_value) Console.Write("Введите ещё раз: ");
                            }
                            while (!input_value);

                            switch (select)
                            {
                                case 1: // Изменить саму заметку
                                    #region
                                    Console.Clear();
                                    Console.Write("Напишите саму заметку: ");
                                    note_entry = Console.ReadLine();

                                    // Создаём новую заметку
                                    edit_note = new Note(number_note, note_list[i].Date_of_Note, note_entry,
                                        note_list[i].Note_Additional, note_list[i].Important_Note);

                                    note_list[i] = edit_note; // Изменяем заметку
                                    #endregion
                                    break;

                                case 2: // Изменить примечание
                                    #region
                                    Console.Clear();
                                    Console.WriteLine("Хотите добавить примечания к заметке?");
                                    do // Пользователь делает выбор
                                    {
                                        Console.Write("Введите н/д: ");
                                        yes_no = Console.ReadKey(false).KeyChar;
                                        yes_no = Char.ToLower(yes_no);
                                    }
                                    while (yes_no != 'н' && yes_no != 'д');

                                    if (yes_no == 'д')
                                    {
                                        Console.Write("\nНапишите примечание: ");
                                        note_additional = Console.ReadLine();
                                    }

                                    else note_additional = "Примечаний нет";

                                    edit_note = new Note(number_note, note_list[i].Date_of_Note, note_list[i].Note_Entry,
                                                    note_additional, note_list[i].Important_Note);

                                    note_list[i] = edit_note; // Изменяем заметку
                                    #endregion
                                    break;

                                case 3: // Изменение отметки о важности
                                    #region
                                    Console.Clear();
                                    Console.Write("Пометить заметку как важную?");
                                    do // Пользователь делает выбор
                                    {
                                        Console.Write("\nВведите н/д: ");
                                        yes_no = Console.ReadKey(false).KeyChar;
                                        yes_no = Char.ToLower(yes_no);
                                    }
                                    while (yes_no != 'н' && yes_no != 'д');

                                    if (yes_no == 'д') important_note = true;
                                    else important_note = false;
                                    Console.WriteLine();

                                    edit_note = new Note(number_note, note_list[i].Date_of_Note, note_list[i].Note_Entry,
                                                    note_list[i].Note_Additional, important_note);

                                    note_list[i] = edit_note; // Изменяем заметку
                                    #endregion
                                    break;
                            }
                        }
                        search_count++;
                        break;
                    }
                }

                if (search_count == 0)
                {
                    Console.WriteLine("Заметки с таким номером не найдено! Нажмите на любую клавишу...");
                    Console.ReadKey();
                }
            return note_list;
        }

        /// <summary>
        /// Метод генерирует заметку и возвращает обЪект типа Note
        /// </summary>
        /// <param name="list_note">Коллекция в которой хранятся заметки</param>
        /// <returns></returns>
        public Note AutoAddNote(List<Note> list_note)
        {
            int note_number; // Номер записи
            DateTime date; // Дата записи 
            string note_entry; // Сама заметка
            string note_additional; // Примечание в заметке
            bool important_note; // Отметка о важности записи
            Random rand = new Random(); // Генерирование случайных чисел
            int select; // Переменная выбора

            // Делаем коректный номер записи, чтоб при удалении и последующем создании всё было ок
            if (list_note.Count == 0) note_number = 1;
            else note_number = list_note[list_note.Count - 1].Note_Number + 1;

            // Генерируем дату
            date = new DateTime(2021, rand.Next(1, 12), rand.Next(1, 29));

            // Генерируем саму заметку
            note_entry = $"Заметка № {note_number}";

            // Генерируем примечание
            select = rand.Next(1, 3); 
            if (select == 1) note_additional = $"Примечание к заметке № {note_number}";
            else note_additional = "Примечаний нет";

            // Генерируем отметку о важности
            select = rand.Next(1, 3);
            if (select == 1) important_note = true;
            else important_note = false;

            Note result = new Note(note_number, date, note_entry, note_additional, important_note);
            return result;
        }

        /// <summary>
        /// Метод выгружает заметки в файл
        /// </summary>
        /// <param name="path_file">Путь к файлу в который выгружаются заметки</param>
        /// <param name="list_note">Коллекция которая будет выгружаться в файл</param>
        public void UnloadNote (string path_file, List<Note> list_note)
        {
                // Создаём поток для записи в файл
                using (StreamWriter sw = new StreamWriter(path_file, false, System.Text.Encoding.Unicode))
                {
                    foreach (var nt in list_note)
                    {
                        sw.WriteLine(nt.Note_Number);
                        sw.WriteLine(nt.Date_of_Note.ToShortDateString());
                        sw.WriteLine(nt.Note_Entry);
                        sw.WriteLine(nt.Note_Additional);

                        if (nt.Important_Note == true) sw.WriteLine("Важная");
                        else sw.WriteLine("Не важная");

                        sw.WriteLine("---------------------------------------------------");
                    }
                }
                Console.WriteLine($"Запись в файл {path_file} прошла успешно!\n" +
                    $"Нажмите на любую клавишу...");
                Console.ReadKey();
        }

        /// <summary>
        /// Метод загружает заметки из файла
        /// </summary>
        /// <param name="path_file">Путь к файлу с заметками</param>
        /// <returns></returns>
        public List<Note> LoadNote (string path_file)
        {
            List<Note> result = new List<Note>(); // Коллекция в которую будут сохраняться заметки из файла
            int note_number; // Номер заметки
            DateTime date; // Дата создание заметки
            string note_entry; // Сама заметка
            string note_additional; // Примечание
            bool important_note; // Отметка о важности
            string important_note_str; // Отметка о важности в строке
            Note nt = new Note(); // Сформированная заметка
            string number; // Переменная управления циклом

            // Поток для чтения из файла
            using (StreamReader sr = new StreamReader(path_file, System.Text.Encoding.Unicode))
            {
                while ((number = sr.ReadLine()) != null)
                {
                    note_number = int.Parse(number);
                    date = DateTime.Parse(sr.ReadLine());
                    note_entry = sr.ReadLine();
                    note_additional = sr.ReadLine();
                    important_note_str = sr.ReadLine();

                    if (important_note_str == "Важная") important_note = true;
                    else important_note = false;

                    sr.ReadLine();

                    nt = new Note(note_number, date, note_entry, note_additional, important_note);
                    result.Add(nt);
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("В файле нет заметок!\n" +
                    "Нажмите на любую клавишу...");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Все заметки загружены успешно!\n" +
                    "Нажмите на любую клавишу...");
                Console.ReadKey();
            }

            return result;
        }

        /// <summary>
        /// Метод загружает заметки из файла в указанном диапозоне дат
        /// </summary>
        /// <param name="path_file">Путь к файлу с заметками</param>
        /// <param name="start_date">Начало диапозона дат</param>
        /// <param name="finish_date">Конец диапозона дат</param>
        /// <returns></returns>
        public List<Note> LoadNoteByDate (string path_file)
        {
            List<Note> result = new List<Note>(); // Коллекция в которую будут сохраняться заметки из файла
            int note_number; // Номер заметки
            DateTime date; // Дата создание заметки
            string note_entry; // Сама заметка
            string note_additional; // Примечание
            bool important_note; // Отметка о важности
            string important_note_str; // Отметка о важности в строке
            Note nt = new Note(); // Сформированная заметка
            string number; // Переменная управления циклом
            bool input_value; // Проверка ввода
            DateTime start_date; // Начало диапозона
            DateTime finish_date; // Конец диапозона


            Console.WriteLine();
            do // Ввод даты пользователем
            {
                Console.Write("Укажите дату (Начало диапозона) в формате - чис.мес.год: ");
                input_value = DateTime.TryParse(Console.ReadLine(), out start_date);
            }
            while (!input_value);

            Console.WriteLine();
            do // Ввод даты пользователем
            {
                Console.Write("Укажите дату (Конец диапозона) в формате - чис.мес.год: ");
                input_value = DateTime.TryParse(Console.ReadLine(), out finish_date);
            }
            while (!input_value);

            // Поток для чтения из файла
            using (StreamReader sr = new StreamReader(path_file, System.Text.Encoding.Unicode))
            {
                while ((number = sr.ReadLine()) != null)
                {
                    note_number = int.Parse(number);
                    date = DateTime.Parse(sr.ReadLine());
                    note_entry = sr.ReadLine();
                    note_additional = sr.ReadLine();
                    important_note_str = sr.ReadLine();

                    if (important_note_str == "Важная") important_note = true;
                    else important_note = false;

                    sr.ReadLine();

                    // Выборка по дате
                    if (date >= start_date && date <= finish_date)
                    {
                        nt = new Note(note_number, date, note_entry, note_additional, important_note);
                        result.Add(nt);
                    }
                }
            }
            if (result.Count == 0)
            {
                Console.WriteLine("К сожалению в таком диапозоне дат заметок нет!\n" +
                    "Нажмите на любую клавишу");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Все заметки загружены успешно!\n" +
                    "Нажмите на любую клавишу...");
                Console.ReadKey();
            }

            return result;
        }
        #endregion
    }
}
