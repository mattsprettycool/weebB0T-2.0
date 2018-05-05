using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace WeebB0T
{
    class Program
    {
        const int malMax = 36456;
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token = System.IO.File.ReadAllText(@"C:\Users\Matt\Desktop\DISCORD BOT TOKEN\weebbot2.txt");
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private async Task MessageReceived(SocketMessage message)
        {
            if(message.Content == "!do that thing that all programmers do when they first start learning how to code please")
            {
                await message.Channel.SendMessageAsync(@"https://www.youtube.com/watch?v=rOU4YiuaxAM");
            }else if(message.Content == "!anirec")
            {
                int loopCount = 0;
                string title = "";
                string url = "";
                bool hasNoRec = true;
                await message.Channel.SendMessageAsync("hold on, this might take a second...");
                while (hasNoRec)
                {
                    loopCount++;
                    Random randy = new Random();
                    string malString = @"https://myanimelist.net/anime/" + randy.Next(1, malMax);
                    HtmlWeb malWeb = new HtmlWeb();
                    var htmlDoc = malWeb.Load(malString);
                    try
                    {
                        var node = htmlDoc.DocumentNode.SelectSingleNode(@"/html/body/div[1]/div[3]/div[3]/div[1]/h1/span");
                        if (node.InnerHtml != null || loopCount == 10)
                        {
                            hasNoRec = false;
                            title = node.InnerHtml;
                            url = malString;
                            Console.WriteLine("[MAL recommendation, loop #" + loopCount + " ] Returned page was \"" + title + "\"!");
                        }
                        else
                        {
                            Console.WriteLine("[MAL recommendation, loop #" + loopCount + " ] Returned page was null!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("[MAL recommendation, loop #" + loopCount + " ] Returned page was null!");
                    }
                }
                await message.Channel.SendMessageAsync("You should try \""+title+"\"!");
                await message.Channel.SendMessageAsync(url);
            }
            else if (message.Content == "!is zero two great?")
            {
                await message.Channel.SendMessageAsync("!is matt pretty cool?");
            }
            else if (message.Content == "!is matt pretty cool?")
            {
                await message.Channel.SendMessageAsync("yes. the answer is yes.");
            }
            else if (message.Content == "!is the bot dead yet?")
            {
                await message.Channel.SendMessageAsync("I'm honestly not sure at this point");
            }
            else if (message.Content == "!help")
            {
                await message.Channel.SendMessageAsync("!do that thing that all programmers do when they first start learning how to code please - sends a hello world\n!anirec - gives you a random anime recommendation from My Anime List, may be hentai sooooo\n!is zero two great? - there are some stupid questions, you know\n!is matt pretty cool? - tells the truth\n!is the bot dead yet? - do I need to explain everything to you?");
            }
        }
    }
}
