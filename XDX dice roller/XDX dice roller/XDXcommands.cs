using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace XDX_dice_roller
{
    class XDXcommands
    {
        public enum STATE
        {
            none,
            normal,
            savage,
            hexagon,
        }

        STATE state = STATE.none;
        int reroll = 0;
        int succesCap = 0;


        static int[] MyRollMethod(int nbDice = 0, int nbFaces = 0, int succesCap = 0, int reroll = 0)
        {
            Random random = new Random();

            int[] result = new int[nbDice];
            int totalSucces = 0;
            int totalRerolls = 0;
            int[] sendResult = new int[2];


            for (int i = 0; i < nbDice; i++)
            {
                result[i] = random.Next(1, nbFaces + 1);

                if (result[i] == reroll)
                    totalRerolls++;

                if (result[i] >= succesCap)
                    totalSucces++;
            }

            sendResult[0] = totalSucces;
            sendResult[1] = totalRerolls;

            return sendResult;
        }

        static int[] Rolloto(int nbDice)
        {
            Random random = new Random();
            int[] result = new int[nbDice];
            int[] sendMeResult = new int[2];

            int nbSucces = 0;
            int nbSix = 0;

            for (int i = 0; i < nbDice; i++)
            {
                result[i] = random.Next(1, 6 + 1);

                if (result[i] == 6)
                    nbSix++;
                if (result[i] >= 3)
                    nbSucces++;
            }

            sendMeResult[0] = nbSucces;
            sendMeResult[1] = nbSix;

            return sendMeResult;
        }

        [Command("roll")]
        public async Task Roll(CommandContext commandContext, int diceFaces = 0, int numberOfDices = 0)
        {
            switch (state)
            {
                case STATE.none:
                    await commandContext.RespondAsync($"You need to set the bot before using a rolling mehtod");
                    await commandContext.RespondAsync($"PLease refer to the !h or !table to apply a setting");
                    break;

                case STATE.normal:
                    //string[] split = commandContext.Message.Content.ToLower().Split(" ");

                    //split = split[1].Split("d");

                    //int nbDice = int.Parse(split[0]);
                    //int nbFaces = int.Parse(split[1]);
                    //if (succesCap > 0)
                    //{
                    //    if (nbFaces >= succesCap)
                    //    {
                    //        int[] results = MyRollMethod(nbDice, nbFaces, succesCap, reroll);

                    //        if (reroll > 0)
                    //        {
                    //            await commandContext.Message.RespondAsync("You rolled " + nbDice + "D" + nbFaces + "." + " Succes are set on : " + succesCap + ", Reroll are set on : " + reroll + ".");
                    //            await commandContext.Message.RespondAsync("Succes : " + results[0] + ", Rerolls : " + results[1] + ".");
                    //        }
                    //        else
                    //        {
                    //            await commandContext.Message.RespondAsync("You rolled " + nbDice + "D" + nbFaces + "." + " Succes are set on : " + succesCap + ".");
                    //            await commandContext.Message.RespondAsync("Succes : " + results[0] + ".");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await commandContext.Message.RespondAsync("Your faces are inferior to the succes cap, please check your numbers.");
                    //        await commandContext.Message.RespondAsync("Succes are set on : " + succesCap + ".");
                    //    }
                    //}
                    //else
                    //    await commandContext.Message.RespondAsync("The succes cap is set on 0 or less. Please be sure that it's atleast at 1.");
                    break;

                case STATE.savage:

                    Random random = new Random();
                    int nbDiceFaceSavage = diceFaces;
                    int result;

                    result = random.Next(1, nbDiceFaceSavage + 1);


                    await commandContext.RespondAsync($"You rolled : D" + nbDiceFaceSavage + " dice");
                    if (nbDiceFaceSavage == result)
                    {
                        await commandContext.RespondAsync($"Result : " + result + ". You can roll a dice again !");
                        reroll = nbDiceFaceSavage;
                    }
                    else
                    {
                        await commandContext.RespondAsync($"Result : " + result);
                    }

                    break;

                case STATE.hexagon:
                    int nbDiceRolloto = diceFaces;

                    int[] resultRolloto = Rolloto(nbDiceRolloto);

                    await commandContext.Message.RespondAsync($"DES : " + nbDiceRolloto + ", SUCCES : " + resultRolloto[0] + ", SIX : " + resultRolloto[1]);
                    break;

                default:
                    break;
            }
        }

        [Command("reroll")]
        public async Task Reroll(CommandContext commandContext)
        {
            if (state == STATE.savage && reroll > 0)
            {
                Random random = new Random();
                int result = random.Next(1, reroll + 1);

                await commandContext.RespondAsync($"You've rerolled : D" + reroll);
                if (reroll == result)
                {
                    await commandContext.RespondAsync($"Result : " + result + ". You can roll a dice again !");
                }
                else
                {
                    await commandContext.RespondAsync($"Result : " + result);
                    reroll = 0;
                }
            }
        }

        [Command("tabletop")]
        public async Task SetTableTop(CommandContext commandContext, string tableTop = "")
        {
            if (tableTop == "")
            {
                await commandContext.Message.RespondAsync($"Commands :");
                await commandContext.Message.RespondAsync($"'!tabletop Savage' for the Savage World preset.");
                await commandContext.Message.RespondAsync($"'!tabletop Hexagon' for the Hexagon preset.");
                await commandContext.Message.RespondAsync($"'!tabletop normal' for the normal preset.");
            }

            if (tableTop.ToLower() == "savage")
            {
                state = STATE.savage;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Savage mode.");
            }

            if (tableTop.ToLower() == "hexagon")
            {
                state = STATE.hexagon;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Hexagon mode.");
            }

            if (tableTop.ToLower() == "normal")
            {
                state = STATE.normal;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Normal mode.");
            }
        }

        [Command("h")]
        public async Task HelpMe(CommandContext commandContext)
        {
            await commandContext.Message.RespondAsync($"Commands :");
            await commandContext.Message.RespondAsync($"'!succes' X to set the number that its need to get a succes from the roll.");
            await commandContext.Message.RespondAsync($"'!reroll' X to set the number that a reroll will be add to the pool.");
            await commandContext.Message.RespondAsync($"'!roll' XDX to set the number of dice and faces, first X stand for dice number.");
            await commandContext.Message.RespondAsync($"'!TabletopName' To use a preset of one of the table top rules. Please use '!Tabletop' to display all the TableTop available.");
        }
    }
}
