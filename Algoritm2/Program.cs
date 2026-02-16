using System;
using System.Threading;

class Program
{
    static void Main()
    {
        //Алгоритмы 2

        //Задание 1
        /*Console.WriteLine("Умный навигатор для робота-уборщика\n");

        string[] rooms = { "База", "Гостиная", "Кухня", "Спальня", "Ванная" };
        string[] floorTypes = { "-", "паркет", "плитка", "ковер", "плитка" };
        int[,] adjacency = {
            {0, 1, 0, 1, 0},
            {1, 0, 1, 1, 0},
            {0, 1, 0, 0, 1},
            {1, 1, 0, 0, 0},
            {0, 0, 1, 0, 0}
        };

        bool[] cleaned = new bool[5];
        cleaned[0] = true;
        int currentRoom = 0;
        int battery = 100;
        bool wetSensor = true || false;
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("\nПланировка квартиры:");
        Console.WriteLine("Гостиная (паркет) <-> Кухня (плитка) <-> Ванная (плитка)");
        Console.WriteLine("     ^                     ^");
        Console.WriteLine("     |                     |");
        Console.WriteLine("   Спальня (ковер)       База");
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        bool isWorking = true;

        while (isWorking)
        {
            Console.WriteLine($"Текущее положение: {rooms[currentRoom]}, заряд: {battery}%");

            if (battery <= 20)
            {
                Console.WriteLine("Внимание: заряд ниже 20%. Возвращаемся на базу.");
                if (currentRoom != 0)
                {
                    currentRoom = 0;
                    battery -= 10;
                    Thread.Sleep(500);
                    Console.WriteLine($"Вернулись на базу. Заряд: {battery}%");
                }
                Console.WriteLine("Зарядка завершена. Заряд: 100%");
                battery = 100;
                continue;
            }

            bool allCleaned = true;
            for (int i = 1; i < rooms.Length; i++)
            {
                if (!cleaned[i])
                {
                    allCleaned = false;
                    break;
                }
            }

            if (allCleaned)
            {
                Console.WriteLine("\nВсе комнаты убраны!");
                if (currentRoom != 0)
                {
                    Console.WriteLine("Возвращаемся на базу...");
                    currentRoom = 0;
                    battery -= 10;
                    Thread.Sleep(500);
                    Console.WriteLine($"Вернулись на базу. Заряд: {battery}%");
                }
                isWorking = false;
                break;
            }

            int nextRoom = -1;
            for (int i = 1; i < rooms.Length; i++)
            {
                if (!cleaned[i] && adjacency[currentRoom, i] == 1)
                {
                    nextRoom = i;
                    break;
                }
            }

            if (nextRoom == -1)
            {
                Console.WriteLine("Нет доступных комнат для уборки. Возвращаемся на базу.");
                currentRoom = 0;
                battery -= 10;
                Thread.Sleep(500);
                continue;
            }

            Console.WriteLine($"Двигаемся в {rooms[nextRoom]} ({floorTypes[nextRoom]})...\n");
            currentRoom = nextRoom;
            battery -= 5;
            Thread.Sleep(500);

            if (floorTypes[currentRoom] == "ковер")
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("\nТип покрытия: ковер. Выполняем сухую уборку...");
                Thread.Sleep(1500);
                Console.WriteLine("Уборка завершена.\n");
                Console.WriteLine("----------------------------------------------------------------------");
                battery -= 15;
            }
            else if (floorTypes[currentRoom] == "плитка")
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("\nТип покрытия: плитка. Проверяем датчик влажности...");
                Thread.Sleep(500);
                wetSensor = (currentRoom == 4);
                if (wetSensor)
                {
                    Console.WriteLine("Внимание: плитка мокрая! Пропускаем уборку.\n");
                    Console.WriteLine("----------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("Плитка сухая. Выполняем влажную уборку...");
                    Thread.Sleep(1500);
                    Console.WriteLine("Уборка завершена.\n");
                    battery -= 20;
                    Console.WriteLine("----------------------------------------------------------------------");
                }
            }
            else if (floorTypes[currentRoom] == "паркет")
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("\nТип покрытия: паркет. Выполняем бережную уборку...");
                Thread.Sleep(1500);
                Console.WriteLine("Уборка завершена.\n");
                battery -= 15;
                Console.WriteLine("----------------------------------------------------------------------");
            }

            cleaned[currentRoom] = true;
            Console.WriteLine();
        }

        Console.WriteLine("\nРабота завершена. Робот на базе.");*/

        // Задание 2

        /*Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Анализ свидетельских показаний\n");

        string[] svideteli = { "Сидоров", "Петрова", "Иванов", "Смирнова", "Файзуллин" };
        DateTime[] times = {
            DateTime.Parse("2026-02-12 10:00"), DateTime.Parse("2026-02-12 10:00"),
            DateTime.Parse("2026-02-12 11:00"), DateTime.Parse("2026-02-12 12:00"),
            DateTime.Parse("2026-02-12 14:00"), DateTime.Parse("2026-02-12 15:30")
        };
        string[] mesta = { "Парк", "Парк", "Магазин", "Кафе", "Банк", "Аптека" };
        string[] desc = { "Рыжий, в кепке", "Лысый, в очках", "Рыжий, в кепке",
                         "Рыжий, в кепке", "Рыжий, в кепке", "Рыжий, в кепке" };

        bool[] reliable = new bool[6];
        Array.Fill(reliable, true);

        Console.WriteLine("\nСводка показаний:");
        for (int i = 0; i < times.Length; i++)
            Console.WriteLine($"{svideteli[i % 5]}: {times[i]:HH:mm} в {mesta[i]} - \"{desc[i]}\"");
        Console.WriteLine();
        Console.WriteLine("\n=== Этап 1: Поиск противоречий ===\n");
        for (int i = 0; i < times.Length; i++)
        {
            for (int j = i + 1; j < times.Length; j++)
            {
                if (times[i] == times[j] && mesta[i] == mesta[j] && desc[i] != desc[j])
                {
                    if (reliable[i])
                    {
                        reliable[i] = false;
                        Console.WriteLine($"Противоречие: {svideteli[i % 5]} и {svideteli[j % 5]}");
                        Console.WriteLine($"Время: {times[i]:HH:mm}, Место: {mesta[i]}");
                        Console.WriteLine($"Показания: \"{desc[i]}\" и \"{desc[j]}\"\n");
                    }
                    reliable[j] = false;
                }
            }
        }
        Console.WriteLine("\n=== Этап 2: Ядро показаний ===\n");
        var core = new System.Collections.Generic.List<(DateTime time, string place, string desc)>();
        for (int i = 0; i < times.Length; i++)
        {
            if (reliable[i])
            {
                bool exists = false;
                foreach (var c in core)
                    if (c.time == times[i] && c.place == mesta[i]) exists = true;

                if (!exists) core.Add((times[i], mesta[i], desc[i]));
            }
        }
        foreach (var c in core)
            Console.WriteLine($"{c.time:HH:mm} - {c.place}\n  {c.desc}\n");

        Console.WriteLine("\n=== Этап 3: Маршрут подозреваемого ===\n");
        for (int i = 0; i < core.Count - 1; i++)
        {
            for (int j = i + 1; j < core.Count; j++)
            {
                if (core[i].time > core[j].time)
                {
                    var temp = core[i];
                    core[i] = core[j];
                    core[j] = temp;
                }
            }
        }

        for (int i = 0; i < core.Count; i++)
        {
            Console.Write($"{core[i].time:HH:mm} → {core[i].place}");
            if (i < core.Count - 1)
            {
                var diff = core[i + 1].time - core[i].time;
                Console.WriteLine($" (переход за {diff.Hours}ч {diff.Minutes}м)");
            }
            else Console.WriteLine(" (конечная точка)");
        }
        Console.WriteLine("\n=== Этап 4: Белые пятна ===\n");
        bool hasGaps = false;
        for (int i = 0; i < core.Count - 1; i++)
        {
            var diff = core[i + 1].time - core[i].time;
            if (diff.TotalMinutes > 60)
            {
                hasGaps = true;
                Console.WriteLine($"ПРОПУСК: {core[i].time:HH:mm} - {core[i + 1].time:HH:mm}");
                Console.WriteLine($"Места: {core[i].place} → {core[i + 1].place}");
                Console.WriteLine($"Длительность: {diff.Hours}ч {diff.Minutes}м\n");
            }
        }
        if (!hasGaps) Console.WriteLine("Белых пятен нет\n");
        Console.WriteLine("\n=== ИТОГ ===\n");
        int reliableCount = 0;
        foreach (var r in reliable) if (r) reliableCount++;

        if (reliableCount > 0)
        {
            Console.WriteLine("РАССЛЕДОВАНИЕ ЗАВЕРШЕНО УСПЕШНО");
            Console.WriteLine($"Достоверных показаний: {core.Count}");
            if (hasGaps) Console.WriteLine("Требуется доп. расследование (белые пятна)");
        }
        else
        {
            Console.WriteLine("РАССЛЕДОВАНИЕ ПРОВАЛЕНО");
            Console.WriteLine("Все показания противоречивы");
        }
        Console.ReadKey();*/

        // Задание 3
        /*Console.WriteLine("Агрегатор отзывов\n");

        string[] otz = {
            "Отличный телефон, батарея держит два дня",
            "Ужасная доставка, ждал неделю",
            "Экран тухнет, батарея садится за час",
            "Быстрая доставка, телефон работает отлично",
            "Батарея плохая, экран хороший",
            "Доставка задержалась, телефон супер",
            "Экран треснул, недоволен"
        };

        string[] ent = { "батарея", "экран", "доставка", "телефон" };
        int[] pos = new int[4];
        int[] neg = new int[4];

        Console.WriteLine("Анализ отзывов:");

        for (int i = 0; i < otz.Length; i++)
        {
            string t = otz[i].ToLower();
            Console.WriteLine($"\nОтзыв {i + 1}: {t}");

            bool p = false;
            bool n = false;

            if (t.Contains("отличн") || t.Contains("хорош") || t.Contains("супер") || t.Contains("быстр"))
                p = true;

            if (t.Contains("ужасн") || t.Contains("плох") || t.Contains("треснул") || t.Contains("недоволен") || t.Contains("садится") || t.Contains("тухнет"))
                n = true;

            for (int j = 0; j < ent.Length; j++)
            {
                if (t.Contains(ent[j]))
                {
                    if (p) pos[j]++;
                    if (n) neg[j]++;
                    Console.WriteLine($"  Нашли: {ent[j]} - позитив: {p}, негатив: {n}");
                }
            }
        }

        Console.WriteLine("\nРезультаты по аспектам:");
        for (int i = 0; i < ent.Length; i++)
        {
            Console.WriteLine($"{ent[i]}: позитив = {pos[i]}, негатив = {neg[i]}");
        }

        Console.WriteLine("\nТОП проблем:");
        int max1 = -1, max2 = -1, max3 = -1;
        int i1 = -1, i2 = -1, i3 = -1;
        for (int i = 0; i < ent.Length; i++)
        {
            if (neg[i] > max1)
            {
                max3 = max2; i3 = i2;
                max2 = max1; i2 = i1;
                max1 = neg[i]; i1 = i;
            }
            else if (neg[i] > max2)
            {
                max3 = max2; i3 = i2;
                max2 = neg[i]; i2 = i;
            }
            else if (neg[i] > max3)
            {
                max3 = neg[i]; i3 = i;
            }
        }
        if (i1 != -1)
            Console.WriteLine($"1. {ent[i1]} ({neg[i1]} негативных)");
        if (i2 != -1)
            Console.WriteLine($"2. {ent[i2]} ({neg[i2]} негативных)");
        if (i3 != -1)
            Console.WriteLine($"3. {ent[i3]} ({neg[i3]} негативных)");

        Console.WriteLine("\nТОП достоинств:");

        max1 = -1; max2 = -1; max3 = -1;
        i1 = -1; i2 = -1; i3 = -1;
        for (int i = 0; i < ent.Length; i++)
        {
            if (pos[i] > max1)
            {
                max3 = max2; i3 = i2;
                max2 = max1; i2 = i1;
                max1 = pos[i]; i1 = i;
            }
            else if (pos[i] > max2)
            {
                max3 = max2; i3 = i2;
                max2 = pos[i]; i2 = i;
            }
            else if (pos[i] > max3)
            {
                max3 = pos[i]; i3 = i;
            }
        }

        if (i1 != -1)
            Console.WriteLine($"1. {ent[i1]} ({pos[i1]} позитивных)");
        if (i2 != -1)
            Console.WriteLine($"2. {ent[i2]} ({pos[i2]} позитивных)");
        if (i3 != -1)
            Console.WriteLine($"3. {ent[i3]} ({pos[i3]} позитивных)");*/

        // Задание 4
        /*Console.WriteLine("Распределение ресурсов между IT-проектами\n");

        string[] names = { "Сайт", "Мобильное приложение", "Аналитика данных", "Чат-бот", "Облачное хранилище" };
        int[] priority = { 3, 2, 1, 2, 3 };
        int[] cost = { 50000, 80000, 30000, 40000, 100000 };
        double[] roi = { 1.5, 2.0, 1.2, 1.8, 2.5 };
        bool[] hasSpecialists = { true, true, false, true, false };

        Console.Write("Введите общий бюджет (руб): ");
        int budget = Convert.ToInt32(Console.ReadLine());
        int remaining = budget;
        bool[] selected = new bool[5];
        int totalSelected = 0;

        int minCost = 1000000;
        for (int i = 0; i < 5; i++)
        {
            if (hasSpecialists[i] && cost[i] < minCost)
                minCost = cost[i];
        }

        if (budget < minCost)
        {
            Console.WriteLine("\nБюджет слишком мал для запуска любого проекта.");
            return;
        }

        Console.WriteLine("\nОтбор проектов по приоритету и доступности...\n");

        for (int p = 3; p >= 1; p--)
        {
            for (int i = 0; i < 5; i++)
            {
                if (priority[i] == p && hasSpecialists[i] && !selected[i])
                {
                    if (cost[i] <= remaining)
                    {
                        selected[i] = true;
                        remaining -= cost[i];
                        totalSelected++;
                        Console.WriteLine($"{names[i]} | Приоритет: {p}, Стоимость: {cost[i]}, ROI: {roi[i]}x");
                    }
                }
            }
        }

        Console.WriteLine($"\nИтог:");
        Console.WriteLine($"Выбрано проектов: {totalSelected}");
        Console.WriteLine($"Использовано средств: {budget - remaining} руб");
        Console.WriteLine($"Остаток бюджета: {remaining} руб");

        if (totalSelected == 0)
        {
            Console.WriteLine("Нет проектов, которые можно запустить в рамках бюджета.");
        }
        else
        {
            Console.WriteLine("Распределение ресурсов завершено.");
        }*/

        // Задание 5
        /*Console.WriteLine("Умный светофор\n");

        DateTime now = DateTime.Now;
        int hour = now.Hour;
        bool isNight = (hour >= 23 || hour < 6);

        if (isNight)
        {
            Console.WriteLine("Ночное время. Режим: мигающий желтый сигнал.");
            return;
        }

        Console.WriteLine($"Текущее время: {now:HH:mm}. Дневной режим работы.\n");

        Random rand = new Random();
        int lastPedTime = -10;
        bool pedRequest = false;
        int cycles = 3;

        for (int cycle = 0; cycle < cycles; cycle++)
        {
            Console.WriteLine($"--- Цикл {cycle + 1} ---");

            if (!pedRequest && rand.Next(100) < 25)
            {
                pedRequest = true;
                Console.WriteLine("Обнаружен запрос пешехода (нажата кнопка).");
            }

            bool bus = (rand.Next(100) < 15);
            if (bus)
            {
                Console.WriteLine("Приоритет: обнаружен общественный транспорт.");
                Console.WriteLine("Включена специальная фаза для автобуса (зеленый 10 сек).");
                Console.WriteLine("Пешеходный запрос отложен.");
                continue;
            }

            if (pedRequest && (cycle - lastPedTime) >= 2)
            {
                Console.WriteLine("Разрешен запрос пешехода.");
                Console.WriteLine("Зеленый для пешеходов (15 сек), красный для автомобилей.");
                pedRequest = false;
                lastPedTime = cycle;
            }
            else
            {
                Console.WriteLine("Стандартная фаза:");
                Console.WriteLine("Зеленый для автомобилей (20 сек) → Желтый (3 сек) → Красный (10 сек)");
                if (pedRequest)
                    Console.WriteLine("(Запрос пешехода ожидает следующего допустимого цикла)");
            }
        }

        Console.WriteLine("\nРабота светофора завершена.");*/

        // Задание 6
        /*Console.WriteLine("Финансовый консультант\n");

        Console.Write("Возраст: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ежемесячный доход (руб): ");
        int income = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ежемесячные траты (руб): ");
        int expenses = Convert.ToInt32(Console.ReadLine());

        Console.Write("Текущие сбережения (руб): ");
        int savings = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Выберите цель:");
        Console.WriteLine("1 - Подушка безопасности");
        Console.WriteLine("2 - Покупка жилья");
        Console.WriteLine("3 - Пенсия");
        Console.Write("Ваш выбор (1-3): ");
        int goal = Convert.ToInt32(Console.ReadLine());

        int free = income - expenses;
        Console.WriteLine($"\nСвободные средства: {free} руб/мес");

        bool needPillow = (savings < expenses * 3);
        if (needPillow)
        {
            Console.WriteLine("\nНедостаточно подушки безопасности!");
            Console.WriteLine($"Рекомендуется: {expenses * 3} руб (3 месяца расходов)");
            Console.WriteLine("Сначала сформируйте подушку безопасности.");
            goal = 1;
        }

        string risk;
        if (age < 30)
            risk = "высокий";
        else if (age < 50)
            risk = "средний";
        else
            risk = "низкий";

        Console.WriteLine($"\nВаш риск-профиль: {risk}");

        Console.WriteLine("\nРекомендуемое распределение активов:");

        if (goal == 1)
        {
            Console.WriteLine("Цель: Подушка безопасности");
            Console.WriteLine("Вклады: 100%");
            Console.WriteLine("Фонды: 0%");
            Console.WriteLine("Акции: 0%");
            Console.WriteLine("Облигации: 0%");
            Console.WriteLine("\nСрок формирования: " + (expenses * 3 - savings) / free + " месяцев");
        }
        else if (goal == 2)
        {
            Console.WriteLine("Цель: Покупка жилья");
            if (risk == "высокий")
            {
                Console.WriteLine("Вклады: 40%");
                Console.WriteLine("Фонды: 30%");
                Console.WriteLine("Акции: 20%");
                Console.WriteLine("Облигации: 10%");
            }
            else if (risk == "средний")
            {
                Console.WriteLine("Вклады: 50%");
                Console.WriteLine("Фонды: 25%");
                Console.WriteLine("Акции: 10%");
                Console.WriteLine("Облигации: 15%");
            }
            else
            {
                Console.WriteLine("Вклады: 60%");
                Console.WriteLine("Фонды: 20%");
                Console.WriteLine("Акции: 5%");
                Console.WriteLine("Облигации: 15%");
            }
            Console.WriteLine("\nУчтена инфляция ~5% годовых для сохранения покупательной способности.");
        }
        else
        {
            Console.WriteLine("Цель: Пенсия");
            if (risk == "высокий")
            {
                Console.WriteLine("Вклады: 20%");
                Console.WriteLine("Фонды: 40%");
                Console.WriteLine("Акции: 30%");
                Console.WriteLine("Облигации: 10%");
            }
            else if (risk == "средний")
            {
                Console.WriteLine("Вклады: 30%");
                Console.WriteLine("Фонды: 35%");
                Console.WriteLine("Акции: 20%");
                Console.WriteLine("Облигации: 15%");
            }
            else
            {
                Console.WriteLine("Вклады: 40%");
                Console.WriteLine("Фонды: 30%");
                Console.WriteLine("Акции: 10%");
                Console.WriteLine("Облигации: 20%");
            }
            Console.WriteLine("\nУчтена инфляция ~5% годовых для долгосрочного роста.");
        }

        Console.WriteLine("\nРекомендация: откладывайте " + free + " руб ежемесячно.");*/

        // Задание 7
        /*Console.WriteLine("Интерпретатор команд умного дома");
        Console.WriteLine("Формат: устройство.действие(параметр)");
        Console.WriteLine("Примеры: light.kitchen.on, thermostat.set(22), oven.off\n");

        string[] validDevices = { "light", "thermostat", "blinds", "oven" };
        string[] validActions = { "on", "off", "set", "open", "close" };

        while (true)
        {
            Console.Write("\nВведите команду (exit для выхода): ");
            string cmd = Console.ReadLine().Trim();

            if (cmd.ToLower() == "exit")
                break;

            Console.WriteLine($"[LOG] Получена команда: {cmd}");

            if (string.IsNullOrEmpty(cmd))
            {
                Console.WriteLine("[LOG] Ошибка: пустая команда");
                continue;
            }

            string device = "", action = "", param = "";
            bool parseOk = false;

            if (cmd.Contains("."))
            {
                string[] parts = cmd.Split('.');
                if (parts.Length >= 2)
                {
                    device = parts[0].ToLower();
                    string actPart = parts[1];

                    if (actPart.Contains("(") && actPart.Contains(")"))
                    {
                        int open = actPart.IndexOf('(');
                        int close = actPart.IndexOf(')');
                        action = actPart.Substring(0, open).ToLower();
                        param = actPart.Substring(open + 1, close - open - 1);
                    }
                    else
                    {
                        action = actPart.ToLower();
                    }
                    parseOk = true;
                }
            }

            if (!parseOk)
            {
                Console.WriteLine("[LOG] Ошибка парсинга: неверный формат команды");
                continue;
            }

            bool devOk = false, actOk = false;
            foreach (string d in validDevices) if (d == device) devOk = true;
            foreach (string a in validActions) if (a == action) actOk = true;

            if (!devOk)
            {
                Console.WriteLine($"[LOG] Ошибка: неизвестное устройство '{device}'");
                continue;
            }
            if (!actOk)
            {
                Console.WriteLine($"[LOG] Ошибка: недопустимое действие '{action}'");
                continue;
            }

            DateTime now = DateTime.Now;
            bool isNight = (now.Hour >= 23 || now.Hour < 6);

            if (device == "oven" && action == "on" && isNight)
            {
                Console.WriteLine("[LOG] БЕЗОПАСНОСТЬ: Запрет включения духовки ночью!");
                continue;
            }

            Console.WriteLine($"[LOG] Выполнение: {device}.{action}({param})");
            Console.WriteLine($"[LOG] Статус: УСПЕХ - команда применена");
            Console.WriteLine($"[LOG] Время: {now:HH:mm:ss}");
        }

        Console.WriteLine("\nРабота интерпретатора завершена.");*/

        // Задание 8
        /*Console.WriteLine("Логика NPC в пошаговой стратегии\n");

        int myUnits = 12;
        int enemyUnits = 15;
        int myWorkers = 6;
        int resources = 400;
        int baseDefenseMin = 4;
        int turn = 1;

        while (enemyUnits >= 0)
        {
            Console.WriteLine($"--- ХОД {turn} ---");
            Thread.Sleep(1000);
            Console.WriteLine($"Состояние: юниты={myUnits}, рабочие={myWorkers}, ресурсы={resources}\n");
            Thread.Sleep(1000);

            string strategy;
            if (myUnits >= enemyUnits + 5)
                strategy = "атака";
            else if (myUnits <= enemyUnits - 5 || myUnits < baseDefenseMin)
                strategy = "оборона";
            else
                strategy = "развитие";

            Console.WriteLine($"Стратегия: {strategy}");
            Thread.Sleep(1000);

            if (strategy == "атака")
            {
                int attackForce = myUnits - baseDefenseMin;
                if (attackForce < 3) attackForce = 3;
                int targetValue = 8;
                Console.WriteLine($"Атака: сформирован отряд из {attackForce} юнитов");
                Thread.Sleep(1000);
                Console.WriteLine($"Цель: вражеский склад (ценность={targetValue}, слабая охрана)");
                Thread.Sleep(1000);
                Console.WriteLine($"Гарнизон базы: {baseDefenseMin} юнитов");
                myUnits -= attackForce / 3;
                enemyUnits -= attackForce / 2;
                resources += 100;
            }
            else if (strategy == "оборона")
            {
                int defenseNow = myUnits;
                if (defenseNow < baseDefenseMin)
                {
                    Console.WriteLine($"Недостаток обороны! Требуется минимум {baseDefenseMin} юнитов.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Приоритет: удержание базы, сбор ресурсов приостановлен.");
                }
                else
                {
                    Console.WriteLine($"Оборона: {defenseNow} юнитов на базе");
                    Thread.Sleep(1000);
                    Console.WriteLine("Рабочие: 50% сбор ресурсов, 50% ремонт укреплений");
                    resources += 30;
                }
            }
            else
            {
                Console.WriteLine("Развитие экономики:");
                Thread.Sleep(1000);
                Console.WriteLine($"Рабочие: {myWorkers / 2} → дерево, {myWorkers / 2} → руда");
                Thread.Sleep(1000);
                resources += myWorkers * 15;

                if (resources >= 250)
                {
                    Console.WriteLine("Строительство: казарма (250 ресурсов)");
                    Thread.Sleep(1000);
                    resources -= 250;
                    myUnits += 5;
                }
                else if (resources >= 150)
                {
                    Console.WriteLine("Строительство: ферма (150 ресурсов)");
                    Thread.Sleep(1000);
                    resources -= 150;
                    myWorkers += 2;
                }

                Console.WriteLine($"Гарнизон базы: {baseDefenseMin} юнитов (резерв)");
            }

            Console.WriteLine($"\nИтог хода {turn}:");
            Console.WriteLine($"Юниты: {myUnits}, Рабочие: {myWorkers}, Ресурсы: {resources}");
            Thread.Sleep(1000);
            Console.WriteLine($"Враг: {enemyUnits} юнитов\n");
            Thread.Sleep(3000);
            turn++;
        }

        Console.WriteLine("Игра завершена.");*/
    }
}