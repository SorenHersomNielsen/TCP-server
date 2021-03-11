using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace TCP_server
{
    public class Server
    {
        public void Start()
        {
            while (true)
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");

                TcpListener serverSocket = new TcpListener(ip, 4646);

                serverSocket.Start();
                Console.WriteLine("Server started");

                TcpClient socket = serverSocket.AcceptTcpClient();

                Console.WriteLine("server activated");

                Stream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                Console.WriteLine("Ønsker du at hente alle objekterne fra Beer?");
                Console.WriteLine("Så skal du skrive 'HentAlle'");
                Console.WriteLine("Ønsker du hente et specifkt objektet?");
                Console.WriteLine("Så skal du skrive 'HentefterID'");
                Console.WriteLine("Ønsker du at lave et objektet");
                Console.WriteLine("Så skal du skrive 'LaveEtObjektet'");

                string line = sr.ReadLine();


                if (line == "HentAlle")
                {                      
                        sw.WriteLine("HentAlle");
                    sw.WriteLine(JsonConvert.SerializeObject(liste).ToString());
                        
                }

                else if (line == "HentefterID")
                {

                    sw.WriteLine("HentefterID");
                    sw.WriteLine("Skrive din id");
                    string lineid = sr.ReadLine();
                    int tal = Int32.Parse(lineid);
                    sw.WriteLine(JsonConvert.SerializeObject(liste.Find(liste => liste.ID == tal)));

                }

                else if (line == "LaveEtObjektet")
                {
                    Console.WriteLine("Skrive dit objektet");
                    string beer = sr.ReadLine();

                    liste.Add(JsonConvert.DeserializeObject<ModelBeer>(beer));
                    
                }

                      
                    ns.Close();
                    socket.Close();
                    serverSocket.Stop();
                
            }
        }

        private static readonly List<ModelBeer> liste = new List<ModelBeer>()
        {
            new ModelBeer(){ID = 1, Name = "Carlsberg", Price = 39, Abv = 5},
            new ModelBeer(){ID = 2, Name = "Turborg", Price = 29, Abv = 4}
        };
    }
}
