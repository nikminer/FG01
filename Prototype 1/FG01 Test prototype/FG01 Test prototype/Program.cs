using System;
using System.Collections.Generic;
using InTheHand.Net.Sockets;
using System.IO;

namespace FG01_Test_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //super cool ascii logo
            Console.WriteLine(" ______ _____  ___  __");
            Console.WriteLine("|  ____/ ____|/ _ \\/_ |");
            Console.WriteLine("| |__ | |  __| | | || |");
            Console.WriteLine("|  __|| | |_ | | | || |");
            Console.WriteLine("| |   | |__| | |_| || |");
            Console.WriteLine("|_|    \\_____|\\___/ |_|");
            Console.WriteLine("————————————————————————");
            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Created by nikminer ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" |");
            Console.WriteLine("————————————————————————\n");

            //stage 1, finding and connecting to bluetooth devices

            //finding bluetooth devices
            Console.WriteLine("Finding bluetooth devices...");
            List<Device> devices = BluetoothScanner.Scan();
            //output devices list
            Console.WriteLine("№ \tBluetooth addr\t  name");
            for (int i = 1; i <= devices.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0:d2}   ", i );
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\t{0}\t", devices[i - 1].DeviceInfo.DeviceAddress);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  {0}", devices[i - 1].DeviceName);
            }
                
            int s; //slected device №
            bool isWrong = false; //is incorrect input?

            do
            {
                Console.Write("Input device № or input 0 to exit:");
                isWrong = !int.TryParse(Console.ReadLine(), out s);
                if (isWrong)
                    Console.WriteLine("Incorrect input!");
            } while (isWrong);

            if (s == 0)
                return;
            Device select = devices[s - 1];//selected bluetooth device
            devices = null;

            //connecting to bluetooth device
            bool isConnected=false;

            Stream peerStream =  BluetoothScanner.Connecting(select,ref isConnected);

            if (peerStream == null) return;

            //stage 2, reciving and using bluetooth data
            //infinity cycle
            while (isConnected)
            {
                string[] output = RecivingBluetoothData.Recive(peerStream, ref isConnected).Split(';');
                if (!isConnected)
                        peerStream =BluetoothScanner.Connecting(select,ref isConnected);
                if (output.Length == 4)
                {
                    try
                    {
                        Mouse.moveCursor(Convert.ToInt32(output[1]), Convert.ToInt32(output[0]));
                        Mouse.mouseState(Convert.ToInt32(output[2]), Convert.ToInt32(output[3]));
                    }
                    catch { continue; }
                }
            }
            Console.WriteLine("Disconnected");
        }
    }

}
