using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace XDX_dice_roller
{
    class XDXDiceRoller
    {
        //public enum STATE
        //{
        //    none,
        //    normal,
        //    savage,
        //    hexagone,
        //}

        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        //static int[] MyRollMethod(int nbDice = 0, int nbFaces = 0, int succesCap = 0, int reroll = 0)
        //{
        //    Random random = new Random();
            
        //    int[] result = new int[nbDice];
        //    int totalSucces = 0;
        //    int totalRerolls = 0;
        //    int[] sendResult = new int[2];


        //    for (int i = 0; i < nbDice; i++)
        //    {
        //        result[i] = random.Next(1, nbFaces + 1);

        //        if (result[i] == reroll)
        //            totalRerolls++;

        //        if (result[i] >= succesCap)
        //            totalSucces++;
        //    }

        //    sendResult[0] = totalSucces;
        //    sendResult[1] = totalRerolls;

        //    return sendResult;
        //}

        //static int[] Rolloto(int nbDice)
        //{
        //    Random random = new Random();
        //    int[] result = new int[nbDice];
        //    int[] sendMeResult = new int[2];

        //    int nbSucces = 0;
        //    int nbSix = 0;

        //    for (int i = 0; i < nbDice; i++)
        //    {
        //        result[i] = random.Next(1, 6 + 1);

        //        if (result[i] == 6)
        //            nbSix++;
        //        if (result[i] >= 3)
        //            nbSucces++;
        //    }

        //    sendMeResult[0] = nbSucces;
        //    sendMeResult[1] = nbSix;

        //    return sendMeResult;
        //}

        static async Task MainAsync(string[] args)
        {
            //STATE state = STATE.none;
            //int succesCap = 0;
            //int reroll = 0;

            discord = new DiscordClient(new DiscordConfiguration
            { Token = BotToken.botToken, TokenType = TokenType.Bot });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration { StringPrefix = "!" });

            //discord.MessageCreated += async e =>
            //{
            //    if (e.Message.Content.ToLower().StartsWith("!savage"))
            //    {
            //        state = STATE.savage;
            //        await e.Message.RespondAsync("The dice bot has been set on : Savage mode.");
            //    }
            //    else if(e.Message.Content.ToLower().StartsWith("!hexagon"))
            //    {
            //        state = STATE.hexagone;
            //        await e.Message.RespondAsync("The dice bot has been set on : Hexagone mode.");
            //    }
            //    else if (e.Message.Content.ToLower().StartsWith("!reset"))
            //    {
            //        state = STATE.normal;
            //        await e.Message.RespondAsync("The dice bot has been reset to his normal state.");
            //    }

            //    if (e.Message.Content.ToLower().StartsWith("!xdx"))
            //        await e.Message.RespondAsync("Stop poking me, I'm here kiddo.");

            //    if (e.Message.Content.ToLower().StartsWith("!help"))
            //    {
            //        await e.Message.RespondAsync("Commands :");
            //        await e.Message.RespondAsync("'!succes' X to set the number that its need to get a succes from the roll.");
            //        await e.Message.RespondAsync("'!reroll' X to set the number that a reroll will be add to the pool.");
            //        await e.Message.RespondAsync("'!roll' XDX to set the number of dice and faces, first X stand for dice number.");
            //        await e.Message.RespondAsync("'!TabletopName' To use a preset of one of the table top rules. Please use '!Tabletop' to display all the TableTop available.");
            //    }

            //    if (e.Message.Content.ToLower().StartsWith("!tabletop"))
            //    {
            //        await e.Message.RespondAsync("Commands :");
            //        await e.Message.RespondAsync("'!Savage' for the Savage World preset.");
            //        await e.Message.RespondAsync("'!Hexagon' for the Hexagon preset.");
            //        await e.Message.RespondAsync("'!reset' to reset to the XDX dice roller preset.");
            //    }

            //    if (e.Message.Content.ToLower().StartsWith("!succes"))
            //    {
            //        string[] split = e.Message.Content.ToLower().Split(" ");

            //        succesCap = int.Parse(split[1]);

            //        await e.Message.RespondAsync("The succes cap has been set on : " + succesCap + ".");
            //    }

            //    if (e.Message.Content.ToLower().StartsWith("!reroll") && state == STATE.normal)
            //    {
            //        string[] split = e.Message.Content.ToLower().Split(" ");


            //        if (reroll < succesCap)
            //        {
            //            reroll = int.Parse(split[1]);
            //            await e.Message.RespondAsync("All " + reroll + " will be place in the reroll pool.");
            //        }
            //        else
            //        {
            //            await e.Message.RespondAsync("Reroll < succes, please enter an equal or inferior value.");
            //        }
            //    }

            //    if (e.Message.Content.ToLower().StartsWith("!roll"))
            //    {
            //        switch (state)
            //        {
            //            case STATE.none:

            //                await e.Message.RespondAsync("The bot statement isn't set, use !<insert tabletop name here> to set it");
            //                await e.Message.RespondAsync("You can see them by typing : !tabletop");
            //                break;

            //            case STATE.normal:

            //                string[] split = e.Message.Content.ToLower().Split(" ");

            //                split = split[1].Split("d");

            //                int nbDice = int.Parse(split[0]);
            //                int nbFaces = int.Parse(split[1]);
            //                if (succesCap > 0)
            //                {
            //                    if (nbFaces >= succesCap)
            //                    {
            //                        int[] results = MyRollMethod(nbDice, nbFaces, succesCap, reroll);

            //                        if (reroll > 0)
            //                        {
            //                            await e.Message.RespondAsync("You rolled " + nbDice + "D" + nbFaces + "." + " Succes are set on : " + succesCap + ", Reroll are set on : " + reroll + ".");
            //                            await e.Message.RespondAsync("Succes : " + results[0] + ", Rerolls : " + results[1] + ".");
            //                        }
            //                        else
            //                        {
            //                            await e.Message.RespondAsync("You rolled " + nbDice + "D" + nbFaces + "." + " Succes are set on : " + succesCap + ".");
            //                            await e.Message.RespondAsync("Succes : " + results[0] + ".");
            //                        }
            //                    }
            //                    else
            //                    {
            //                        await e.Message.RespondAsync("Your faces are inferior to the succes cap, please check your numbers.");
            //                        await e.Message.RespondAsync("Succes are set on : " + succesCap + ".");
            //                    }
            //                }
            //                else
            //                    await e.Message.RespondAsync("The succes cap is set on 0 or less. Please be sure that it's atleast at 1.");

            //                break;

            //            case STATE.hexagone:

            //                if (e.Message.Content.ToLower().StartsWith("!roll"))
            //                {
            //                    string[] test = e.Message.Content.ToLower().Split(" ");
            //                    int nbDiceRolloto = int.Parse(test[1]);

            //                    int[] resultRolloto = Rolloto(nbDiceRolloto);

            //                    await e.Message.RespondAsync("DES : " + nbDiceRolloto + ", SUCCES : " + resultRolloto[0] + ", SIX : " + resultRolloto[1]);
            //                }

            //                break;

            //            case STATE.savage:

            //                string[] splitSavage = e.Message.Content.ToLower().Split(" ");

            //                splitSavage = splitSavage[1].Split("d");
            //                Random random = new Random();
            //                int nbDiceSavage = int.Parse(splitSavage[0]);
            //                int nbFacesSavage = int.Parse(splitSavage[1]);
            //                int result = random.Next(1, nbFacesSavage + 1);

            //                reroll = 0;

            //                await e.Message.RespondAsync("You rolled : " + nbDiceSavage + "D" + nbFacesSavage + ".");
            //                if (nbFacesSavage == result)
            //                {
            //                    await e.Message.RespondAsync("Result : " + result + ". You can roll a dice again !");
            //                    reroll = nbFacesSavage;                                
            //                }
            //                else
            //                    await e.Message.RespondAsync("Result : " + result);
                            
            //                break;
            //        }                    
            //    }
            //    else if(e.Message.Content.ToLower().StartsWith("!reroll") && state == STATE.savage && reroll != 0)
            //    {
            //        Random random = new Random();
            //        int result = random.Next(1, reroll + 1);

            //        await e.Message.RespondAsync("You rerolled : " + "D" + reroll + ".");
            //        if (reroll == result)
            //            await e.Message.RespondAsync("Result : " + result + ". You can roll a dice again !");
            //        else
            //        {
            //            await e.Message.RespondAsync("Result : " + result);
            //            reroll = 0;
            //        }
            //    }               
            //};

            commands.RegisterCommands<XDXcommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
