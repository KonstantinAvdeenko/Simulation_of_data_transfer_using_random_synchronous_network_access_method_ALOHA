using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SluthainiiDostupKSetiSinhronnoiAloha
{
    class Station
    {  
        public Station(int id, int packetCount)
        {
            this.id = id; // номер текущей станции 
            this.packetCount = packetCount; // пакеты данных текущей станции
        }
        public int packetCount; // оставшиеся пакеты для передачи
        public int nextTime; // время для следующей попытки передачи данных
        public int id; // номер станции передающей пакет
     }

    class Program
    {
        static void Main(string[] args)
        {
            int n, t, k;
            Console.WriteLine("Введите количество пакетов для каждой станции");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите частотную интенсивность передачи данных");
            t = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество станций участвующих в передаче");
            k = int.Parse(Console.ReadLine());
            Station[] stations = new Station[n];
            for (int i = 0; i < n; i++) stations[i] = new Station(i, k);  // распределение пакетов данных для каждой станции
            Random rnd = new Random();
            int globalTicks = 0;  // общее время потраченное на передачу пакетов всех станций
            while (!AllPacketsSended(stations)) // станции передают между собой данные пока не передались все пакеты
            {
                Thread.Sleep(50); // приостанавливает поток передачи данных от одной станции другой после попытки передачи пакета 
                Station stationToStartSending;
                int numberStationsReadyToSend = GetNumberStationsReadyToSend(stations, out stationToStartSending); // получает данные сколько станций передавали в одно и тоже время пакеты 
                
                if (numberStationsReadyToSend == 1) // если пакет данных был передан нужной станции без коллизий
                {
                    DecrementAllTimes(stations, t, ref globalTicks);
                    stationToStartSending.nextTime = 0; // обнуляет время передачи пакета так как он был передан
                    stationToStartSending.packetCount--; // учитывает переданные станцией пакеты
                    Console.WriteLine("Станция {0} Пакет отправлен! {1} осталось пакетов", stationToStartSending.id, stationToStartSending.packetCount);
                }
                else if (numberStationsReadyToSend > 1) // если передача данных идет с коллизией
                {
                    Console.WriteLine("Найдена коллизия");
                    for (int i = 0; i < stations.Count(); i++) // пока не будет передан пакет этой станции
                    {
                        if (stations[i].nextTime <= 0) // если отведенное время для передачи пакета этой станцией вышло
                        {
                            int next = rnd.Next(1, 10); // повторная попытка передачи пакета данных будет предпринята через рандомное время как освободится канал связи
                            stations[i].nextTime = next;
                            Console.WriteLine("Станция {0} отложила передачу пакета на количество времени равное {1}", stations[i].id, next);
                        }
                    }
                }
                else if (numberStationsReadyToSend == 0) // если пакет данных не был передан нужной станции и не было коллизии 
                {
                    Station stationWithNearestTime = FindStationWithNearestTime(stations); // передает пакет более ближней станции 
                    Console.WriteLine("Время передачи до нужной станции {0}", stationWithNearestTime.nextTime); 
                    DecrementAllTimes(stations, stationWithNearestTime.nextTime, ref globalTicks); // учитывает время передачи
                }
            }
            Console.WriteLine("Время: {0}", globalTicks); // показывает сколько длилась вся передача пакетов данных 
            Console.ReadKey();
        }

        static bool AllPacketsSended(Station[] stations) // проверка все ли пакеты от всех станций передались  
        {
            foreach (Station s in stations)
            {
                if (s.packetCount != 0)
                {
                    Console.WriteLine("Не все пакеты были отправлены");
                    return false;
                }
            }
            Console.WriteLine("Все пакеты были отправлены");
            return true;
        }

        static int GetNumberStationsReadyToSend(Station[] stations, out Station st)
        {
            st = null;
            int count = 0;
            foreach (Station s in stations)
            {
                if (s.nextTime <= 0 && s.packetCount > 0) //  если не успели передать все за определенное время
                {
                    count++; //  считает сколько станций передало пакетов за текущий промежуток времени 
                    st = s; //  запоминает новые данные о переданных пакетах каждой станцией и потраченном количестве времени 
                }
            }
            if (count != 1) st = null; // в случае если одновременно передавали пакеты > 1 станции, то произошла коллизия и стираются данные о переданных пакетах
            return count;
        }

        static void DecrementAllTimes(Station[] stations, int time, ref int globalTicks)
        {
            globalTicks += time; // если не было коллизий при передаче данных, то время передачи учитывается
            for (int i = 0; i < stations.Count(); i++)
            {
                stations[i].nextTime -= time * 2; // пока не будут переданы все пакеты этой станции, то после каждого успешно переданного пакета, этой станции приходится ожидать в 2 раза дольше времени для передачи следующего пакета
            }
        }

        static Station FindStationWithNearestTime(Station[] stations) // находит ближайшую станцию как посредника для передачи данных сильно отдаленным станциям
        {
            Station minStation = stations[0]; // расчет ближайшей станции начиная от первой станции
            foreach (Station s in stations)
            {
                if (s.nextTime < minStation.nextTime && s.packetCount > 0) // если выделенное время на передачу данных от одной станции до другой меньше, чем временное расстояние между этими станциями
                    minStation = s; // запоминает новые данные о переданном пакете этой станции более близкой станции для дальнейшей передачи нужной станции и запоминает потраченное количество времени
            }
            return minStation;
        }

    }
}