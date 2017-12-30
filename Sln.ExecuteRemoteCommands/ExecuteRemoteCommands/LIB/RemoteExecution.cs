using System;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace ExecuteRemoteCommands.LIB
{
    public static class RemoteExecution
    {
        //need it
        public static string ExecuteRemoteCommands(string IPOrComputerName, string command)
        {
            string line = null;
            string result = null;
            ConnectionOptions conn = new ConnectionOptions();
            //conn.Username = "Administrator";
            //conn.Password = "admin";

            ManagementScope wmiScope = new ManagementScope(string.Format("\\\\{0}\\root\\cimv2", IPOrComputerName), conn);

            ObjectGetOptions objectGetOptions = new ObjectGetOptions();
            ManagementPath managementPath = new ManagementPath("Win32_Process");
            ManagementClass processClass = new ManagementClass(wmiScope, managementPath, objectGetOptions);


            try
            {

                using (ManagementBaseObject inParams = processClass.GetMethodParameters("Create"))
                {
                    inParams["CommandLine"] = "cmd /c " + command + " > c:\\" + IPOrComputerName + ".txt";
                    ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
                }

                //ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                //inParams["CommandLine"] = "cmd /c " + command + " > c:\\" + IPOrComputerName + ".txt";

                //inParams["CommandLine"] = "cmd /c " + command + " > c:\\" + IPOrComputerName + ".txt";

                //inParams["CommandLine"] = "cmd /c " + command + " > C:\\inetpub\\wwwroot\\tmp.txt";

                //inParams["CommandLine"] = "cmd /c " + command + " > C:\\Users\\shahed\\Desktop\\TMP\\TMPVideoEdit\\Data\\tmp.txt";


                //inParams["CommandLine"] = "cmd /c " + command + " > C$\\Users\\shahed\\tmp.txt";

                //inParams["CommandLine"] = "cmd /c " + command + " > 192.168.191.1\\TMPVideoEdit\\Data\\tmp.txt";

                //inParams["CommandLine"] = "cmd /c " + command + @" > pushd \\192.168.191.1\TMPVideoEdit\Data\tmp.txt";

                //inParams["CommandLine"] = "cmd /c Copy \\192.168.0.107\\C$\\inetpub\\wwwroot\\tmp.txt 192.168.191.1\\D$\\TMP\\AAA.txt";
                //ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
                //uint pid = (uint)outParams["processId"];
                List<string> list = new List<string>();

                using (StreamReader _StreamReader = new StreamReader("\\\\" + IPOrComputerName + "\\c$\\" + IPOrComputerName + ".txt"))
                {
                    while ((line = _StreamReader.ReadLine()) != null)
                    {
                        if (line != null && line != "")
                            list.Add(line);
                    }

                    //_StreamReader.Close();
                    //_StreamReader.Dispose();
                }


                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }


                Console.WriteLine("\n\\*****************************************************\n\n");

                //Thread.Sleep(5000);
                //StreamReader sr = new StreamReader("\\\\" + computerName + "\\c$\\inetpub\\wwwroot\\tmp.txt");
                //line = sr.ReadLine();
                //while (line != null)
                //{
                //    result += line;
                //    line = sr.ReadLine();
                //}
                //sr.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }
            return line;
        }

    }


}
