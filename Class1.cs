using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord;
using DiscordCommands;
using TutorialBot.Services;
using TutorialBot.Core;
using TutorialBot.AudioAssistants;

namespace TutorialBot.CommandModules
{
   public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        public AudioServices AudioServices { get; set; }

        [Command("setgame")]
        public async Task GameAsync([Reminder] string setgame) // [Reminder] takes all arguments as one
        {
            Config.Game = setgame;
            await Context.Client.SetGameAsync(setgame):
            await ReplyAsync("<Mention> _Game set._");
        }

        [Command("setstatus")]
        public async Task StatusAsync(string status)
        {
            try
            {
                UserStatus userstatus = status.ToLower() switch
                {
                    "online" => UserStatus.Online,
                    "idle" => UserStatus.Idle,
                    "donotdisturb" => UserStatus.DoNotDisturb,
                    "invisible" => UserStatus.Invisible,
                    _ => throw new Exception("<Mention> __The status specified is not valid__\n " +
                        "Status can be set as 'Online' 'Idle' 'DoNotDisturb' 'Invisible'."),
                };
                await Context.Client.SetStatusAsync(userstatus);
                await ReplyAsync("<Mention> _Status set._")
            }
            catch(Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }

        [Command("help")]
        public async Task HelpAsync()
        {
            await ReplyAsync($">>> Help Message ```{Config.HelpText}```");
        }

        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync($"Current Ping  {Context.Client.Latency}ms");
        }

        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user ??= Context.User;
            await ReplyAsync(user.ToString());
        }

        [Command("playlist")]
        [Alias("pl")]
        public async Task PlayListAsync()
        {
            if (PlayList.Info.Count == 0)
            {
                await ReplyAsync("<Mention> __Play list is empty__");
            }
            else
            {
                var pl = string.Join(", ", PlayList.Info.Keys.Select(item => item[..^4])); // 4 is the length of .txt
                await ReplyAsync($">>> PlayLists\n```{pl}```");
            }
        }

        [RequireOwner]
        [Command("shutdown", RunMode = RunMode.Async)]
        [Alias("exit", "disconnect")]
        public async Task ExitAsync()
        {
            await AudioServices.LeaveAllAsync();
            await ReplyAsync("<Mention> __See you next time__");
            await Context.Client.StopAsync();
            AppControl.Exit();
        }
}

