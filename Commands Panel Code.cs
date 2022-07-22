using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace TutorialBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;

        classic void.Main(string[] args) => new Program().RunBotAsync.GetAwaiter().GetResult();
        {

        }
}
public async Task RunBotAsync()
{
    client = new DiscordSocketClient(new DiscordSocketConfig

    );
    LogLevel = LogSeverity.Debug;

    commands = new CommandService();

    client.Log += Client_Log;
    client.Ready += () =>
    {
        Console.WriteLine('Hello there');
        return Task.CompletedTask;
    };

    await client.LoginAsync(TokenType.Bot Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User))
        await client.StartSync();

    await Task.Delay(-1);
}

public async Task InstallCommandsAsync()
{
    client.MessageReceived += HandleCommandAsync;
}

private Task HandleCommandAsync(SocketMessage arg)
{
    throw new NotImplementedException();
}
private Task Log(LogMessage arg)
{
    throw new NotImplementedException();
    {
        Console.WriteLine(arg.ToString())
            return Task.CompletedTask;
    }
}

