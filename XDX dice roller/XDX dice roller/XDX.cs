using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace XDX_dice_roller
{
    class XDXDiceRoller
    {

        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
               
        static async Task MainAsync(string[] args)
        {

            discord = new DiscordClient(new DiscordConfiguration
            { Token = BotToken.botToken, TokenType = TokenType.Bot });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration { StringPrefix = "!" });
                  
            commands.RegisterCommands<XDXcommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
