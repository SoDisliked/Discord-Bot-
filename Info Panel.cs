using System.Text;
using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;

namespace TutorialBot.Commands.Modules;

public class InfoModule : ModuleBase<SocketCommandContext>
{
    private readonly CommandHandler _commandHandler;

    private readonly Color _embedColour = new(102, 255, 102)

    public InfoModule(Bot bot)
    {
        _commandHandler = bot.CommandHandler;
    }

    [Command("info", RunMode = RunModeAsync)]
    [Alias("Nerd", "Commands")]
    [Summary("Just random infos, dont ask the creator, just the bot.")]
    public async Task Info()
    {
        var embed = new EmbedBuilder();
        embed.WithColor(_embedColour);
        embed.WithTitle("TutorialBot");
        embed.WithDescription("Random bot? Yes, only on this server.")

        embed.AddField("Author", "SoDisliked, who this?");
        embed.AddField("Version", "1.O.O");

        List<CommandInfo> commandInfos = _commandHandler.CommandService.Commands.ToList();

        embed.AddField("Commands", commandInfos.Count);

        //TODO(Disliked): Put it random, just in case.
        foreach (var commandInfo in commandInfos)
        {
            embed.AddField(commandInfo.Name, $"{commandInfo.Summary ?? "No description provided."}\n" +
                                             $"Aliases: {string.Join(", ", commandInfo.Aliases)}", true);

        }

        embed.WithFooter(x =>
        {
            x.Text = "TutorialBot";
            // Disliekd: Yes, it is me again.

        });

        embed.WithCurrentTimeStamp();

        await ReplyAsync("", false, embed.Build());
    }
}