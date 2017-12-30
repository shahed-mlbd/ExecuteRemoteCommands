using ExecuteRemoteCommands.LIB;
using System.Collections.Generic;

namespace ExecuteRemoteCommands
{
    class Program
    {
        static string IPDesktop = "192.168.0.112";
        static string IPLaptop = "192.168.1.111";
        static void Main(string[] args)
        {
            List<string> listIPAddres = new List<string>();
            listIPAddres.Add(IPDesktop);
            //listIPAddres.Add(IPLaptop);

            foreach (var item in listIPAddres)
            {
                var abc = RemoteExecution.ExecuteRemoteCommands(item, " netstat.exe -ano");
            }
        }
    }
}
