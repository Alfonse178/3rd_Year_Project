using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TPLink_SmartPlug;

using System.Net.NetworkInformation;

namespace client
{
	class Program
	{
        static int port;
        static HS1XX tp;
        static void Main(string[] args)
		{
            //FindDevice()
            //just cycled through the 255 potential addresses until it came up with the one i wanted. Will need to run this if used at a different location.
            port = 147;
            Connect("192.168.0." + port.ToString());
            GetInfo();

			Console.ReadKey();
		}

        static void FindDevice()
        {
            for (int i = 2; i <= 255; i++)
            {
                try
                {

                    Connect("192.168.0." + i);
                    Console.WriteLine("FOUND" + i.ToString());
                    port = i;
                    break;

                }
                catch (Exception ex)
                {

                }
            }
        }

		static void Connect(string ip)
		{
		
			tp = new HS1XX(IPAddress.Parse(ip), 1000, 1000, 1000);
            

		}
        static void GetInfo()
            //getting the information of the smart plug and then issuing a command, the library does most of the work. 
        {
            DeviceInfo dev_info = tp.GetDeviceInfo();
            Console.WriteLine(dev_info.RelayState);
            tp.SwitchRelayState(RelayAction.TurnOn);
        }
	}
}
