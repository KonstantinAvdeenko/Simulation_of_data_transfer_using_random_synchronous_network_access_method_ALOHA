﻿Данная программа на языке программирования C#, 
производит моделирование работы передачи данных по методу случайного доступа к сети синхронной Алоха. 
Основная концепция в том, что все станции прослушивают передачу по линии связи. 
Станция, желающая передать сообщение, выходит на связь только после обнаружения свободного состояния канала. 
Столкновения могут возникнуть, поскольку станции физически разнесены одна от другой, 
и две или несколько станций могут обнаружить свободное состояние канала и начать передавать пакеты данных, 
что вызовет столкновение. Если станции обнаруживают столкновение, то они передают всем остальным станциям 
специальный сигнал о помехе и отменяют свои передачи. Если обнаруживается столкновение и передача прекращается, 
то попытка повторной передачи принимается через случайный промежуток времени, 
этот случайный промежуток времени удваивается каждый раз после обнаружения нового столкновения до некоторой 
максимальной величины.
    
Основной модуль SluthainiiDostupKSetiSinhronnoiAloha содержит следующие два программных класса:  
1)	класс Station – представляет возможность пользоваться общими переменными для методов класса Program;
2)	основной класс Program – содержит весь алгоритм работы данной программы, состоящий из следующих компонентов:
1) основная функция работы программы Main;
2) метод AllPacketsSended;
3) метод GetNumberStationsReadyToSend;
4) метод DecrementAllTimes;
5) метод FindStationWithNearestTime.

1)	Основная функция Main – выполняет задачу ввода пользователем количества станций участвующих в передаче, 
количества пакетов для каждой станции, частотной интенсивности передачи данных. 
В данной функции распределяются пакеты данных для каждой станции, проверка передачи всех пакетов данных 
используя метод AllPacketsSended, проверка передачи пакетов данных на коллизии используя метод 
GetNumberStationsReadyToSend и учитывая, а также вывода пользователю варианты переданного пакета данных, 
непереданного пакета данных из-за коллизии и из-за недопустимой удаленности станций для прямой передачи пакета 
данных. Во всех случаях кроме коллизии учитывается потраченное время на передачу используя метод DecrementAllTimes. 
В случае если пакет данных не передается из-за недопустимой удаленности станций для прямой передачи пакета данных, 
то этот пакет данных пересылается на ближайшую станцию для последующей переотправки на нужную станцию используя 
метод FindStationWithNearestTime. После передачи всех пакетов данных между станциями основная функция выводит
сколько длилась вся передача пакетов данных. 
2)	Метод AllPacketsSended – проверяет все ли пакеты данных со всех станций были переданы;
3)	GetNumberStationsReadyToSend – проверяет передался ли пакет данных и если не передался то 
из-за коллизии или из-за недопустимой удаленности станций для прямой передачи пакета данных; 
4)	DecrementAllTimes – учитывает и суммирует время передачи данных каждого переданного пакета и заставляет 
станцию передавать следующий пакет с ожиданием в два раза больше, чтобы за это время передавали данные другие станции;
5)	FindStationWithNearestTime – находит ближайшую станцию как посредника для передачи данных сильно отдаленным станциям.
