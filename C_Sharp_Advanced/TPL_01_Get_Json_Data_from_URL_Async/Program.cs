using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

// Task: asynchronously get data from https://jsonplaceholder.typicode.com/comments
// and while loading it, print dots in a console, and when the loading is finished,
// print "the loading is finished"

namespace TPL_01_Get_Json_Data_from_URL_Async
{
    class Program
    {

        static void Main(string[] args)
        {
            Task getJson = new Task(GetJson);
            getJson.Start();
            Loading(getJson);
            getJson.Wait();
            Console.Clear();
            Console.WriteLine("The loading is finished");

            Console.ReadKey();
        }

        // Reading data from url and deserializing
        public static void GetJson()
        {
            string url = @"https://jsonplaceholder.typicode.com/comments";
            string postsJson = GetDataFromURL(url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsJson);
        }

        // Reading data from url and returning a string
        static string GetDataFromURL(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }

        // Prints loading dots in console, until the task a is completed
        public static void Loading(Task a)
        {
            while (!a.IsCompleted)
            {
                Console.Clear();
                for (int i = 0; i < 5 && !a.IsCompleted; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(100);
                }
                Thread.Sleep(200);
            }
        }
    }
}
