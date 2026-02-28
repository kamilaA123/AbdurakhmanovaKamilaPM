using System;
using System.Collections.Generic;
class Programm
{
    static void Main()
    {
        //1 задание
        /*Random random = new Random();
        int[] numbers = new int[10];

        for (int i = 0; i < 10; i++)
        {
            numbers[i] = random.Next(0, 10);
        }

        Console.Write("Текущий массив: [");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(numbers[i]);
            if(i < numbers.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
        bool notFound = true;
        while (notFound)
        {
            Console.Write("Введите число X: ");
            if (int.TryParse(Console.ReadLine(), out int x))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (numbers[i] == x)
                    {
                        Console.WriteLine($"Найдено на позиции {i}");
                        return;
                    }
                }
                Console.WriteLine("Не найдено");
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
            }*/
        //2 задание
        /*Random random = new Random();
        int[] numbers = new int[10];

        for (int i = 0; i < 10; i++)
        {
            numbers[i] = random.Next(0, 10);
        }
        Console.Write("Исходный массив: [");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(numbers[i]);
            if (i < numbers.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
        int n = numbers.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    int temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }
        Console.Write("Отсортированный массив: [");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i]);
            if (i < numbers.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }*/
        //3 задание
        /*int balanceInUser = 60000;
        bool checkIn = true;
        int limitInBankomat = 10000;

        Console.WriteLine("Банкомат СберБанк");
        while (checkIn)
        {
            Console.WriteLine($"Ваш баланс: {balanceInUser}");
            Console.WriteLine($"Макс. сумма для снятия: {limitInBankomat}");
            Console.Write("Введите сумму для снятия (кратную 100): ");
            int sum = Convert.ToInt32(Console.ReadLine());
            if (sum % 100 != 0)
            {
                Console.WriteLine("Сумма должна быть кратна 100");
                continue;
            }
            if (sum > limitInBankomat)
            {
                Console.WriteLine($"Сумма превышает лимит");
                continue;
            }
            if (sum > balanceInUser)
            {
                Console.WriteLine("Нет столько денег на балансе");
                continue;
            }
            balanceInUser -= sum;
            Console.WriteLine("Банкомат выдал вам деньги");
            Console.WriteLine($"Остаток на балансе: {balanceInUser}");
            checkIn = false;
        }*/

        // Задание 4


        //Console.WriteLine("Решение квадратного уравнения ax² + bx + c = 0");
        //Console.Write("Введите коэффициент a: ");
        //int a = Convert.ToInt32(Console.ReadLine());
        //if (a == 0)
        //{
        //    Console.WriteLine("Коэффициент 'a' не может быть нулём.");
        //    return;
        //}

        //Console.Write("Введите коэффициент b: ");
        //int b = Convert.ToInt32(Console.ReadLine());

        //Console.Write("Введите коэффициент c: ");
        //int c = Convert.ToInt32(Console.ReadLine());

        //double discriminant = b * b - 4 * a * c;

        //Console.WriteLine($"\nДискриминант D = {discriminant}");


        //if (discriminant > 0)
        //{
        //    double sqrtD = Math.Sqrt(discriminant);
        //    double x1 = (-b + sqrtD) / (2 * a);
        //    double x2 = (-b - sqrtD) / (2 * a);
        //    Console.WriteLine("Уравнение имеет два корня:");
        //    Console.WriteLine($"x1 = {x1}");
        //    Console.WriteLine($"x2 = {x2}");
        //}
        //else if (discriminant == 0)
        //{
        //    double x = -b / (2 * a);
        //    Console.WriteLine("Уравнение имеет один корень:");
        //    Console.WriteLine($"x = {x}");
        //}
        //else
        //{
        //    Console.WriteLine("Действительных корней нет (дискриминант < 0).");
        //}



        // Задание 5

        /*Console.WriteLine("ВИКТОРИНА\n");

        int score = 0;

         Console.WriteLine("Вопрос 1: Столица Франции?");
         Console.WriteLine("1. Лондон");
         Console.WriteLine("2. Берлин");
         Console.WriteLine("3. Париж");
         Console.Write("Ваш ответ (1-3): ");
         string answer1 = Console.ReadLine();
         if (answer1 == "3")
         {
             Console.WriteLine("Правильно!\n");
             score++;
         }
         else
         {
             Console.WriteLine("Неверно. Правильный ответ: Париж\n");
         }

         Console.WriteLine("Вопрос 2: Сколько планет в Солнечной системе?");
         Console.WriteLine("1. 7");
         Console.WriteLine("2. 8");
         Console.WriteLine("3. 9");
         Console.Write("Ваш ответ (1-3): ");
         string answer2 = Console.ReadLine();
         if (answer2 == "2")
         {
             Console.WriteLine("Правильно!\n");
             score++;
         }
         else
         {
             Console.WriteLine("Неверно. Правильный ответ: 8\n");
         }

         Console.WriteLine("Вопрос 3: Какой язык программирования начинается с 'C'?");
         Console.WriteLine("1. Python");
         Console.WriteLine("2. Java");
         Console.WriteLine("3. C#");
         Console.Write("Ваш ответ (1-3): ");
         string answer3 = Console.ReadLine();
         if (answer3 == "3")
         {
             Console.WriteLine("Правильно!\n");
             score++;
         }
         else
         {
             Console.WriteLine("Неверно. Правильный ответ: C#\n");
         }
         Console.WriteLine($"Правильных ответов: {score} из 3");

         if (score == 3)
         {
             Console.WriteLine("Отлично!");
         }
         else if (score == 2)
         {
             Console.WriteLine("Хорошо!");
         }
         else
         {
             Console.WriteLine("Нужно повторить материал...");
         }

         Console.WriteLine("\nНажмите любую клавишу для выхода...");
         Console.ReadKey();*/



        // Задание 6


        /*string word = "КОМПЬЮТЕР";
        int length = word.Length;

        bool[] guessed = new bool[length];
        int attempts = length;
        int guessedLetters = 0;

        Console.WriteLine("Игра «Угадай слово»");
        Console.WriteLine($"Загадано слово из {length} букв. У вас {attempts} попыток.\n");

        while (attempts > 0 && guessedLetters < length)
        {
            Console.Write("Текущее состояние: ");
            for (int i = 0; i < length; i++)
            {
                if (guessed[i])
                    Console.Write(word[i] + " ");
                else
                    Console.Write("_ ");
            }
            Console.WriteLine($"\nОсталось попыток: {attempts}");
            Console.Write("Введите букву: ");
            string input = Console.ReadLine().ToUpper();

            if (input.Length != 1)
            {
                Console.WriteLine("Введите только одну букву.\n");
                continue;
            }

            char letter = input[0];
            bool found = false;

            for (int i = 0; i < length; i++)
            {
                if (word[i] == letter && !guessed[i])
                {
                    guessed[i] = true;
                    guessedLetters++;
                    found = true;
                }
            }

            if (found)
            {
                Console.WriteLine("Есть такая буква!\n");
            }
            else
            {
                attempts--;
                Console.WriteLine("Нет такой буквы.\n");
            }
        }

        Console.Write("Загаданное слово: ");
        for (int i = 0; i < length; i++)
        {
            Console.Write(word[i] + " ");
        }
        Console.WriteLine();

        if (guessedLetters == length)
        {
            Console.WriteLine("Поздравляем! Вы угадали слово.");
        }
        else
        {
            Console.WriteLine("К сожалению, вы не угадали слово.");
        }*/



        // Задание 7


        //Console.Write("Введите текущий этаж (1-9): ");
        //int currentFloor = Convert.ToInt32(Console.ReadLine());

        //if (currentFloor < 1 || currentFloor > 9)
        //{
        //    Console.WriteLine("Неверный этаж. Этаж должен быть от 1 до 9.");
        //    return;
        //}

        //Console.Write("Введите нужный этаж (1-9): ");
        //int targetFloor = Convert.ToInt32(Console.ReadLine());

        //if (targetFloor < 1 || targetFloor > 9)
        //{
        //    Console.WriteLine("Неверный этаж. Этаж должен быть от 1 до 9.");
        //    return;
        //}

        //if (currentFloor == targetFloor)
        //{
        //    Console.WriteLine("Вы уже на этом этаже.");
        //    return;
        //}

        //Console.WriteLine("\nДвери закрываются...");
        //Thread.Sleep(1000);

        //int step = currentFloor < targetFloor ? 1 : -1;


        //while (currentFloor != targetFloor)
        //{
        //    currentFloor += step;
        //    Console.WriteLine($"Этаж: {currentFloor}");
        //    Thread.Sleep(1000);
        //}

        //Console.WriteLine("\nДвери открываются...");
        //Thread.Sleep(1000);
        //Console.WriteLine($"Вы прибыли на этаж {currentFloor}.");


        // Задание 8

        //bool success = true;

        //Console.WriteLine("Этап 1: Берем хлеб");
        //Thread.Sleep(1000);
        //Console.WriteLine("Хлеб готов");
        //Console.WriteLine();

        //if (success)
        //{
        //    Console.WriteLine("Этап 2: Наносим масло");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Масло нанесено");
        //    Console.WriteLine();
        //}

        //if (success)
        //{
        //    Console.WriteLine("Этап 3: Кладем сыр");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Сыр уложен");
        //    Console.WriteLine();
        //}

        //if (success)
        //{
        //    Console.WriteLine("Этап 4: Кладем ветчину");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Ветчина уложена");
        //    Console.WriteLine();
        //}

        //if (success)
        //{
        //    Console.WriteLine("Этап 5: Накрываем вторым ломтем хлеба");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Верхний ломоть установлен");
        //    Console.WriteLine();
        //}

        //if (success)
        //{
        //    Console.WriteLine("Этап 6: Проверка качества");
        //    Thread.Sleep(1500);
        //    Console.WriteLine("Проверка пройдена. Бутерброд готов!");
        //    Console.WriteLine();
        //    Console.WriteLine("Результат: Сборка успешно завершена.");
        //}
        //else
        //{
        //    Console.WriteLine("Результат: Сборка прервана. Брак.");
        //}
    }
}