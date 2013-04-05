// Copyright (c) 2011 John Trimis 
//
// MIT license:
// Permission is hereby granted, free of charge, to any person obtaining a copy of 
// this software and associated documentation files (the "Software"), to deal in 
// the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
// the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConnectAndSend
{   // namespace ConnectAndSend

    /// <summary>
    ///     ConnectAndSend.exe
    ///     Connects out to a TCP/IP connection and sends a specific string before disconnecting.
    /// </summary>
    class Program
    {   // class Program

        /// <summary>
        ///     Main execution path
        /// </summary>
        /// <param name="args">
        ///     Command Line arguments
        ///         args[0] = Hostname or IP to connect to
        ///         args[1] = Port to connect on
        ///         args[2] = data to send once connected.
        ///         args[3] = How long to wait before disconnecting and exiting (milliseconds).
        /// </param>
        static void Main( string[] args )
        {   // Main

            if ( args.Length < 3 )
            {   // Not enough arguments

                HowDoIUseThis();

            }   // Not enough arguments
            else
            {   // Proper number of arguments

                string strHost      = args[0];
                string strPort      = args[1];
                string strCommand   = args[2];

                string strWait      = args.Length > 3 ? args[3] : "";

                int iPort;
                int wait = 0;
                strCommand = strCommand.Replace( @"\n", "\n" ).Replace( @"\\", "\\" ).Replace( @"\r", "\r" ).Replace( "\\\"", "\"" );
                
                if ( int.TryParse( strPort, out iPort ) )
                {   // Numeric port number

                    if ( iPort < 1 || iPort > 65535 )
                    {   // Invalid Port Number

                        HowDoIUseThis();

                    }   // Invalid Port Number
                    else
                    {   // Valid Port Number

                        int.TryParse(strWait, out wait);

                        try
                        {   // TRY

                            Socket sock = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                            Encoding enc = Encoding.GetEncoding( "Windows-1252" );
                            sock.Connect( strHost, iPort );
                            sock.Send( enc.GetBytes( strCommand ) );

                            if (wait > 0)
                            {   // Disconnect Delay specified
                                
                                Thread.Sleep(wait);

                            }   // Disconnect Delay specified
                            sock.Close();
                            
                        }   // TRY
                        catch (Exception ex)
                        {   // CATCH : Exception

                            Console.WriteLine( string.Format( "Error when connecting.  Error:{0}", ex.Message ) );

                        }   // CATCH : Exception

                    }   // Valid Port Number

                }   // Numeric port number
                else
                {   // Non-Numeric port number

                    HowDoIUseThis();

                }   // Non-Numeric port number
            }   // Proper number of arguments

        }   // Main

        static void HowDoIUseThis()
        {   // HowDoIUseThis

            Console.WriteLine( "ConnectAndSend.exe [host] [port] \"[command]\" [Wait] " );
            Console.WriteLine( "\t Connects to a specified TCP/IP port and sends some specific " );
            Console.WriteLine( "\t data before disconnecting" );
            Console.WriteLine( "\thost\t: The Hostname or IP to connect to" );
            Console.WriteLine( "\tport\t: The Port to connect to (1-65535)" );
            Console.WriteLine( "\tcommand\t: The command to execute (should probably be in quotes)" );
            Console.WriteLine( "\t\tNote: allows the following escape sequences: " );
            Console.WriteLine( "\t\t\t\\r - Carriage Return" );
            Console.WriteLine( "\t\t\t\\n - Line Feed" );
            Console.WriteLine( "\t\t\t\\\" - Quote character" );
            Console.WriteLine("\tWait\t: (Optional) Number of milliseconds to pause after sending before ");
            Console.WriteLine("\t\t\tdisconnecting and exiting.");
        }   // HowDoIUseThis

    }   // class Program

}   // namespace ConnectAndSend
