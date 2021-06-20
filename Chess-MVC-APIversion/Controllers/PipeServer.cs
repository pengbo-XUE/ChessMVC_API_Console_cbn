using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace Chess_MVC_APIversion
{
    public class PipeServer
    {

        public void sendData(string result)
        {
            try
            {
                using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("chess-pipe-2", PipeDirection.InOut))
                {

                    Console.WriteLine("trying to get connected.");
                    pipeServer.WaitForConnection();

                    Console.WriteLine("Enter a command");
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(pipeServer))
                        {
                            sw.AutoFlush = true;
                            sw.WriteLine(result);

                        }

                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("ERROR: {0}", e.Message);
                    }

                    pipeServer.Dispose();
                }
            }
            catch (Exception ex) 
            {
                sendData(result);
            }
        }


        public bool reciveData()
        {

            using (NamedPipeClientStream pipeClient =
                  new NamedPipeClientStream(".", "chess-pipe-1", PipeDirection.InOut))
            {

                
                // Connect to the pipe or wait until the pipe is available.
                Console.Write("Attempting to connect to pipe...");
                pipeClient.Connect();

                Console.WriteLine("Connected to pipe.");
          

                using (StreamReader sr = new StreamReader(pipeClient))
                {
                    // Display the read text to the console
                    //List<string> temp2;

                    string temp;
                    while ((temp = sr.ReadLine()) != null)
                    {
                        string[] temp3 = temp.Split(',');
                        if (temp3[0] == "moved") return true;


                    }
                    pipeClient.Dispose();

                }
            }
            return false;
        }
    }
}
