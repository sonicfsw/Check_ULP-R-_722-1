using System;

namespace OptimalRoute
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задаем номера начальной и конечной аудиторий
            Console.Write("Введите номер начальной аудитории: ");
            string startRoomNumber = Console.ReadLine();

            Console.Write("Введите номер конечной аудитории: ");
            string endRoomNumber = Console.ReadLine();

            // Определяем этажи и номера аудиторий для начальной и конечной аудиторий
            int startFloor = int.Parse(startRoomNumber.Substring(0, 1));
            int startRoom = int.Parse(startRoomNumber.Substring(1));

            int endFloor = int.Parse(endRoomNumber.Substring(0, 1));
            int endRoom = int.Parse(endRoomNumber.Substring(1));

            // Определяем стороны и крыла здания для начальной и конечной аудиторий
            string startSide = startRoom % 2 == 1 ? "Левая" : "Правая";
            string endSide = endRoom % 2 == 1 ? "Левая" : "Правая";

            string startWing = startRoom <= 7 ? "Левое" : "Правое";
            string endWing = endRoom <= 7 ? "Левое" : "Правое";

            // Выводим информацию о начальной аудитории
            Console.WriteLine("\nНачальная аудитория: " + startRoomNumber);
            Console.WriteLine("Этаж: " + startFloor);
            Console.WriteLine("Крыло здания: " + startWing);
            Console.WriteLine("Стена здания: " + startSide);

            // Выводим информацию о конечной аудитории
            Console.WriteLine("\nКонечная аудитория: " + endRoomNumber);
            Console.WriteLine("Этаж: " + endFloor);
            Console.WriteLine("Крыло здания: " + endWing);
            Console.WriteLine("Стена здания: " + endSide);

            // Определяем расстояние по этажу до лифта для начальной и конечной аудиторий
            double startFloorDistanceToLift = Math.Abs(startRoom - 8);
            double endFloorDistanceToLift = Math.Abs(endRoom - 8);

            // Определяем расстояние по лифту между этажами
            double distanceBetweenFloors = Math.Abs(startFloor - endFloor);

            // Вычисляем общее расстояние до конечной аудитории
            double totalDistance = startFloorDistanceToLift + endFloorDistanceToLift + distanceBetweenFloors;

            // Выводим оптимальный маршрут
            Console.WriteLine("\nОптимальный маршрут:");

            // Шаг 1: Подойти к лифту на расстояние startFloorDistanceToLift аудиторий
            Console.WriteLine("1. Подойти к лифту на расстояние " + startFloorDistanceToLift + " аудиторий.");

            // Шаг 2: Проехать на лифте до этажа endFloor
            Console.WriteLine("2. Проехать на лифте до " + endFloor + " этажа.");

            // Шаг 3: Подойти к аудитории на расстояние endFloorDistanceToRoom аудиторий
            Console.WriteLine("3. Подойти к аудитории на расстояние " + endFloorDistanceToLift + " аудиторий.");

            // Выводим информацию о выбранных аудиториях и общем расстоянии до конечной аудитории
            Console.WriteLine("\nИнформация о выбранных аудиториях:");
            Console.WriteLine("Начальная аудитория: " + startRoomNumber + " (этаж " + startFloor + ")");
            Console.WriteLine("Конечная аудитория: " + endRoomNumber + " (этаж " + endFloor + ")");
            Console.WriteLine("\nОбщее расстояние до конечной аудитории: " + totalDistance + " аудиторий.");

            Console.ReadKey();
        }
    }
}
