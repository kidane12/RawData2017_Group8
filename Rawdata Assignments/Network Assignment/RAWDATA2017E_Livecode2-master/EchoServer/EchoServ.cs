﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace EchoServer
{
    internal class EchoServ
    {
        private static void Main(string[] args)
        {
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

            var buffer = new byte[client.ReceiveBufferSize];

            try
            {
                var bytesRead = stream.Read(buffer, 0, buffer.Length);

                var request = Encoding.UTF8.GetString(buffer);

                var requestObj = JsonConvert.DeserializeObject<RequestObj>(request.Trim('\0'));
                var response = new Response();
                Console.WriteLine(requestObj.Date);
                if (string.IsNullOrEmpty(requestObj.Date)) response.Status += "missing date, ";
                else if (!IsUnixTimestamp(requestObj.Date)) response.Status += "illegal date, ";

                switch (requestObj.Method)
                {
                    case "{}":
                        response.Status += "missing method, ";
                        break;
                    case "create":
                        Create(requestObj, ref response);
                        break;
                    case "read":
                        Read(requestObj, ref response);
                        break;
                    case "update":
                        Update(requestObj, ref response);
                        break;
                    case "delete":
                        Delete(requestObj, ref response);
                        break;
                    default:
                        response.Status = "illegal method";
                        break;
                }

                WriteRepsonse(stream, response);
                stream.Close();
                client.Dispose();
            }
            catch (IOException)
            {
                Console.WriteLine("no request");
            }

        }

        private static void Create(RequestObj requestObj, ref Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body)) response.Status += "missing resource, ";
            
        }

        private static void Read(RequestObj requestObj, ref Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body)) response.Status += "missing resource, ";
        }

        private static void Update(RequestObj requestObj, ref Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body)) response.Status += "missing resource, ";
        }

        private static void Delete(RequestObj requestObj, ref Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body)) response.Status += "missing resource, ";
        }

        private static void WriteRepsonse(Stream stream, Response response)
        {
            var jsonResponse = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            stream.Write(jsonResponse, 0, jsonResponse.Length);
        }

        private static bool IsUnixTimestamp(string str)
        {
            //only works for the next many many years
            return str.Length <= 10 && str.All(c => c >= '0' && c <= '9');
        }

        public class RequestObj
        {
            public string Method, Path, Date, Body;

            public RequestObj(string method, string path, string date, string body)
            {
                Method = method;
                Path = path;
                Date = date;
                Body = body;
            }

            public RequestObj()
            {
                Method = "{}";
            }
        }

        public class Response
        {
            public string Status { get; set; }
            public string Body { get; set; }
        }
    }
}