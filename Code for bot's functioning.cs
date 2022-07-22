using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace TutorialBot
{
    class program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        static void Main(string[] args) => new program().RunBotAsync().GetAwaiter().GetResult();
    
        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            }

            commands = new CommandService()

                ); ;
            client.Log += Log;

            client.Ready += () =>
            {
                Console.WriteLine("Hello there");
                return Task.CompletedTask;
            };

            await InstallComandsAsync();


            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User));
            await client.StartAsync();
            
            await Task.Delay(-1);

        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(AssemblyLoadEventArgs.GetEntryAssembly(), null);
        }

        private async Task HandleCommandAsync(SocketMessage pMessage)
        {
            var message = (SocketUserMessage)pMessage;

            if (message == null) return;

            int argPos = 0;

            if (!message.HasCharPrefix('!', ref argPos)) return;

            var context = new SocketCommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, null);

            //ERROR.
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }
}