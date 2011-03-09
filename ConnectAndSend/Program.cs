using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConnectAndSend
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                HowDoIUseThis();
            }
            else
            {
                string strHost = args[0];
                string strPort = args[1];
                string strCommand = args[2];
                strCommand = strCommand.Replace(@"\n", "\n").Replace(@"\\", "\\").Replace(@"\r", "\r").Replace("\\\"","\"");
                int iPort;
                if (int.TryParse(strPort, out iPort))
                {
                    if (iPort < 1 || iPort > 65535)
                    {
                        HowDoIUseThis();
                    }
                    else
                    {
                        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Encoding enc = Encoding.GetEncoding("Windows-1252");
                        sock.Connect(strHost, iPort);
                        sock.Send(enc.GetBytes(strCommand));
                        sock.Close();
                    }
                }
                else
                {
                    HowDoIUseThis();
                }
            }
        }
        static void HowDoIUseThis()
        {
            Console.WriteLine("ConnectAndSend.exe [host] [port] \"[command]\"");
            Console.WriteLine("\thost\t: The Hostname or IP to connect to");
            Console.WriteLine("\tport\t: The Port to connect to (1-65535)");
            Console.WriteLine("\tcommand\t: The command to execute (should probably be in quotes)");
            Console.WriteLine("\t\tNote: allows the following escape sequences: ");
            Console.WriteLine("\t\t\t\\r - Carriage Return");
            Console.WriteLine("\t\t\t\\n - Line Feed");
            Console.WriteLine("\t\t\t\\\" - Quote character");
        }
    }
}
