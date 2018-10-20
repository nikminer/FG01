using System.Collections.Generic;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using System.IO;
using System;

namespace FG01_Test_prototype
{
    class BluetoothScanner
    {
        static BluetoothClient bc = new BluetoothClient();
        public static List<Device> Scan()
        {
            List<Device> devices = new List<Device>();

            foreach (var i in bc.DiscoverDevices())
                devices.Add(new Device(i));

            return devices;            
        }

        public static BluetoothEndPoint GetEndPoint(Device i) => new BluetoothEndPoint(i.DeviceInfo.DeviceAddress, BluetoothService.SerialPort);
        public static Stream Connecting(Device select,ref bool isConnected)
        {
            Console.WriteLine("Connecting....");
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    bc = new BluetoothClient();
                    bc.Connect(BluetoothScanner.GetEndPoint(select));
                    Console.WriteLine("Connected to {0}", select.DeviceInfo.DeviceAddress);
                    if (bc.Connected)
                    {
                        isConnected = bc.Connected;
                        return bc.GetStream();
                    }
                }
                catch
                {
                    Console.WriteLine("Failured!");
                    Console.WriteLine("Reconnecting....");
                }
            }
            return null;
        }
    }
}
