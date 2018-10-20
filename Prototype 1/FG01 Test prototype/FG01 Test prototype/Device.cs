using System;
using InTheHand.Net.Sockets;

namespace FG01_Test_prototype
{
    public class Device
    {
        public string DeviceName { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsConnected { get; set; }

        public ushort Nap { get; set; }
        public uint Sap { get; set; }

        public DateTime LastSeen { get; set; }

        public DateTime LastUsed { get; set; }

        public bool Remembered { get; set; }

        public BluetoothDeviceInfo DeviceInfo { get; set; }

        public Device(BluetoothDeviceInfo device_info)
        {
            if (device_info != null)
            {
                DeviceInfo = device_info;
                IsAuthenticated = device_info.Authenticated;
                IsConnected = device_info.Connected;
                DeviceName = device_info.DeviceName;
                LastSeen = device_info.LastSeen;
                LastUsed = device_info.LastUsed;
                Nap = device_info.DeviceAddress.Nap;
                Sap = device_info.DeviceAddress.Sap;
                Remembered = device_info.Remembered;
            }
        }
        public override string ToString() => DeviceName;
    }
}