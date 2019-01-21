using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Async_Await_Get_Github_Profile_Info
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isImplemented = TryStartToEndAsync().Result;
            if (!isImplemented) Console.WriteLine("We were unable to obtain data for current user.");
            Console.ReadKey();
        }

        static async Task<bool> TryStartToEndAsync()
        {
            try
            {
                string url = @"https://api.github.com/users/" + await ReadUsernameFromConsoleAsync();
                string userJson = await GetDataFromURLAsync(url);
                GithubProfile user = await GetGithubProfileFromJsonAsync(userJson);
                await PrintProfileInfoAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static async Task<string> GetDataFromURLAsync(string url)
        {
            var webRequest = WebRequest.Create(url) as HttpWebRequest;

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = await sr.ReadToEndAsync();
                    return contributorsAsJson;
                }
            }
        }

        static GithubProfile GetGithubProfileFromJson(object jsonObj)
        {
            string jsonString = (string)jsonObj;
            GithubProfile user = JsonConvert.DeserializeObject<GithubProfile>(jsonString);
            return user;
        }

        static async Task<GithubProfile> GetGithubProfileFromJsonAsync(string jsonString)
        {
            return await Task<GithubProfile>.Factory.StartNew(GetGithubProfileFromJson, jsonString);
        }

        static string ReadUsernameFromConsole()
        {
            Console.Write("Please type the Github username: ");
            string username = Console.ReadLine();

            return username;
        }

        static async Task<string> ReadUsernameFromConsoleAsync()
        {
            return await Task<string>.Factory.StartNew(ReadUsernameFromConsole);
        }

        static void TryPrintProfileInfo(object userObj)
        {
            GithubProfile user = (GithubProfile)userObj;
            Console.WriteLine($"Username: {user.login}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"Name: {user.name}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"Number of repositories: {user.public_repos}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"The number of followers: {user.followers}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"Follows: {user.following}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine($"Created account at: {user.created_at}");
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        static async Task PrintProfileInfoAsync(GithubProfile user)
        {
            await Task.Factory.StartNew(TryPrintProfileInfo, user);
        }
    }
}
