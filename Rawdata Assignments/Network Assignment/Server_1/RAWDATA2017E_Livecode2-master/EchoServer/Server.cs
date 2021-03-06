﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Server1
{
    internal class Server
    {
        private static void Main(string[] args)
        {
            DataAPI.InitDataModel();
            const int port = 5000;
            var localAddr = IPAddress.Parse("127.0.0.1");

            var server = new TcpListener(localAddr, port);

            server.Start();

            Console.WriteLine("Started");

            while(true)
            {
                var client = server.AcceptTcpClient();

                Console.WriteLine("Client connected");

                var thread = new Thread(HandleClient);

                thread.Start(client);
            }
        }

        private static void HandleClient(object clientObj)
        {
            if (!(clientObj is TcpClient client)) return;

            var stream = client.GetStream();

            try
            {
                var requestObj = Read(stream, client.ReceiveBufferSize);

                var response = new Response();
                Console.WriteLine(requestObj.Method);

                var violatedConstraint = false;
                if (string.IsNullOrEmpty(requestObj.Date))
                {
                    response.Status += "missing date, ";
                    violatedConstraint = true;
                }
                else if (!Helpers.IsUnixTimestamp(requestObj.Date))
                {
                    response.Status += "illegal date, ";
                    violatedConstraint = true;
                }

                switch (requestObj.Method)
                {
                    case "{}":
                        response.Status += "missing method, ";
                        break;
                    case "create":
                        DataAPI.Create(requestObj, ref response, violatedConstraint);
                        break;
                    case "read":
                        DataAPI.Read(requestObj, ref response, violatedConstraint);
                        break;
                    case "update":
                        DataAPI.Update(requestObj, ref response, violatedConstraint);
                        break;
                    case "delete":
                        DataAPI.Delete(requestObj, ref response, violatedConstraint);
                        break;
                    case "echo":
                        DataAPI.Echo(requestObj, ref response, violatedConstraint);
                        break;
                    default:
                        response.Status += "illegal method";
                        break;
                }

                WriteResponse(stream, response);
                stream.Close();
                client.Dispose();
            }
            catch (IOException)
            {
                Console.WriteLine("no request");
            }

        }

        private static void WriteResponse(Stream stream, Response response)
        {
            var jsonResponse = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            stream.Write(jsonResponse, 0, jsonResponse.Length);
        }



        public class RequestObj
        {
            public string Method, Path, Date, Body;
            public RequestObj() { Method = "{}"; }
        }

        private static RequestObj Read(Stream strm, int size)
        {
            var buffer = new byte[size];
            var bytesRead = strm.Read(buffer, 0, buffer.Length);
            var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return JsonConvert.DeserializeObject<RequestObj>(request);
        }

        public class Response
        {
            public string Status { get; set; }
            public string Body { get; set; }
        }
    }
}
