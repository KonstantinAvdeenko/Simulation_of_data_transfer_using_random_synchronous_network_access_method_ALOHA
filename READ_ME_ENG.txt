This program is in the C # programming language,
makes the simulation of the data transfer by the method of random access to the network synchronous Aloha.
The basic concept is that all stations listen to the transmission over the communication line.
A station wishing to transmit a message is contacted only after detecting a free channel condition.
Collisions can occur because the stations are physically separated from one another,
and two or more stations can detect the free channel condition and start transmitting data packets,
which will cause a collision. If stations detect a collision, then they transmit to all other stations.
special signal of interference and cancel their transmission. If a collision is detected and transmission ceases,
then the retransmission attempt is received at random intervals,
this random time period doubles every time a new collision is detected, to some
maximum value.
    
The main module SluthainiiDostupKSetiSinhronnoiAloha contains the following two program classes:
1) Station class - provides the ability to use common variables for methods of the Program class;
2) the main class Program - contains the entire algorithm of the program, consisting of the following components:
1) the main function of the program Main;
2) AllPacketsSended method;
3) GetNumberStationsReadyToSend method;
4) DecrementAllTimes method;
5) FindStationWithNearestTime method.

1) The main function of the Main - performs the task of the user entering the number of stations involved in the transmission,
the number of packets for each station, the frequency intensity of data transmission.
This function distributes data packets for each station, checking the transmission of all data packets.
using the AllPacketsSended method, checking data packets for collisions using the method
GetNumberStationsReadyToSend and taking into account, as well as output to the user, options for the transmitted data packet,
unsent data packet due to collision and unacceptable distance of stations for direct packet transmission
data. In all cases except for a collision, the time spent on transmission is taken into account using the DecrementAllTimes method.
In case the data packet is not transmitted due to unacceptable remoteness of the stations for the direct transmission of the data packet,
then this data packet is sent to the nearest station for subsequent re-sending to the desired station using
FindStationWithNearestTime method. After transferring all data packets between stations, the main function displays
how long the entire data packet transmission took.
2) AllPacketsSended method - checks if all data packets from all stations were transmitted;
3) GetNumberStationsReadyToSend - checks if the data packet was transmitted and if not transmitted
due to a collision or unacceptable remoteness of stations for direct transmission of a data packet;
4) DecrementAllTimes - takes into account and summarizes the data transfer time of each transmitted packet and forces
station to transmit the next packet with the expectation of two times more so that during this time other stations transmit data;
5) FindStationWithNearestTime - finds the nearest station as an intermediary for data transmission to highly distant stations.