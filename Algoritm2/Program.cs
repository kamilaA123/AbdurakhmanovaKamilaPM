using System;
using System.Threading;

class Program
{
    static void Main()
    {
        //Алгоритмы 2

        //Задание 1
        Console.WriteLine("Умный навигатор для робота-уборщика\n");

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

        Console.WriteLine("\nРабота завершена. Робот на базе.");
    }
}