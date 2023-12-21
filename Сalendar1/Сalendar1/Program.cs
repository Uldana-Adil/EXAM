using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сalendar1
{
    class Program
    {
        //статическое поле calendar, которое представляет календарь
        static Dictionary<DateTime, List<Event>> calendar = new Dictionary<DateTime, List<Event>>();

        static void Main()
        {
            while (true)
            {
                //Очищает консоль перед отображением нового состояния календаря
                Console.Clear();
                DisplayCalendar();

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить событие");
                Console.WriteLine("2. Просмотреть события на дату");
                Console.WriteLine("3. Удалить событие");
                Console.WriteLine("4. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEvent();
                        break;
                    case "2":
                        ViewEvents();
                        break;
                    case "3":
                        DeleteEvent();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void DisplayCalendar()
        {
            // Получаем текущию дату
            DateTime currentDate = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            Console.WriteLine($"{currentDate:MMMM yyyy}");
            Console.WriteLine("Пн Вт Ср Чт Пт Сб Вс");

            // Получаем первый день месяца
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Определяем день недели первого дня месяца
            int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // Выводим пробелы до первого дня месяца
            for (int i = 0; i < dayOfWeek; i++)
            {
                Console.Write("  ");
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDateOnCalendar = new DateTime(currentDate.Year, currentDate.Month, day);
                Console.Write($"{day,2}");

                if (calendar.ContainsKey(currentDateOnCalendar))
                {
                    Console.Write("*");
                }

                if (currentDateOnCalendar.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }

        static void AddEvent()
        {
            Console.WriteLine("Введите дату события (гггг-мм-дд): ");
            string dateInput = Console.ReadLine();
            // Попытка преобразовать введенную строку в объект DateTime (дата события). Если успешно, код внутри блока выполняется.
            if (DateTime.TryParse(dateInput, out DateTime eventDate))
            {
                if (!calendar.ContainsKey(eventDate))
                {
                    calendar[eventDate] = new List<Event>();
                }

                Console.WriteLine("Введите название события: ");
                string title = Console.ReadLine();

                Console.WriteLine("Введите время события (чч:мм): ");
                string timeInput = Console.ReadLine();
                // объеденение даты и время
                if (DateTime.TryParse($"{dateInput} {timeInput}", out DateTime eventDateTime))
                {
                    Event newEvent = new Event { Title = title, DateTime = eventDateTime };

                    Console.WriteLine("Установить напоминание? (y/n): ");
                    string reminderChoice = Console.ReadLine();

                    if (reminderChoice.ToLower() == "y")
                    {
                        Console.WriteLine("Введите время напоминания (чч:мм): ");
                        string reminderTimeInput = Console.ReadLine();

                        if (DateTime.TryParse($"{dateInput} {reminderTimeInput}", out DateTime reminderDateTime))
                        {
                            newEvent.Reminder = reminderDateTime;
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат времени. Напоминание не установлено.");
                        }
                    }

                    calendar[eventDate].Add(newEvent);
                    Console.WriteLine("Событие добавлено!");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Неверный формат времени. Событие не добавлено.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Неверный формат даты. Событие не добавлено.");
                Console.ReadLine();
            }
        }
        static void ViewEvents()
        {
            Console.WriteLine("Введите дату для просмотра событий (гггг-мм-дд): ");
            string dateInput = Console.ReadLine();

            if (DateTime.TryParse(dateInput, out DateTime eventDate))
            {
                if (calendar.ContainsKey(eventDate))
                {
                    Console.WriteLine($"События на {eventDate:dd.MM.yyyy}:");
                    foreach (var ev in calendar[eventDate])
                    {
                        Console.WriteLine($"{ev.Title} в {ev.DateTime:HH:mm}");
                    }

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("На данную дату событий нет.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Неверный формат даты.");
                Console.ReadLine();
            }
        }

        static void DeleteEvent()
        {
            Console.WriteLine("Введите дату события для удаления (гггг-мм-дд): ");
            string dateInput = Console.ReadLine();

            if (DateTime.TryParse(dateInput, out DateTime eventDate))
            {
                if (calendar.ContainsKey(eventDate))
                {
                    Console.WriteLine($"Выберите событие для удаления на {eventDate:dd.MM.yyyy}:");

                    for (int i = 0; i < calendar[eventDate].Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {calendar[eventDate][i].Title} в {calendar[eventDate][i].DateTime:HH:mm}");
                    }

                    Console.WriteLine("Введите номер события для удаления:");
                    if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= calendar[eventDate].Count)
                    {
                        calendar[eventDate].RemoveAt(selectedIndex - 1);
                        Console.WriteLine("Событие удалено!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Событие не удалено.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("На данную дату событий нет.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Неверный формат даты.");
                Console.ReadLine();
            }
        }


    }

}
