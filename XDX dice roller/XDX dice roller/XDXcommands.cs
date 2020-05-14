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
            wta,
            aventure,
            MAXVALUE,
        }

        public enum MOONPHASE
        {
            none,
            New,
            Crescent,
            Half,
            Gibbous,
            Full,
            MAXVALUE
        }

        STATE state = STATE.none;
        int reroll = 0;

        //SavageWorld
        Dictionary<string, int> tokens = new Dictionary<string, int>();




        static int[] MyRollMethod(int nbDice = 0, int nbFaces = 0)
        {
            Random random = new Random();

            int[] result = new int[nbDice];


            for (int i = 0; i < nbDice; i++)
            {
                result[i] = random.Next(1, nbFaces + 1);
            }

            return result;
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
        public async Task Roll(CommandContext commandContext, int firstParam = 0, int secondParam = 0)
        {
            switch (state)
            {
                
                case STATE.none:
                    await commandContext.RespondAsync($"You need to set the bot before using a rolling mehtod");
                    await commandContext.RespondAsync($"Please refer to the !h or !table to apply a setting");
                    break;

                case STATE.normal:

                    
                    int[] Crohmsultat = MyRollMethod(firstParam, secondParam);

                    string allResults = "All results : ";

                    allResults += string.Join(" | ", Crohmsultat);

                    await commandContext.RespondAsync(allResults);

                    break;

                case STATE.savage:

                    Random random = new Random();
                    int nbDiceFaceSavage = firstParam;
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
                    int nbDiceRolloto = firstParam;
                    int[] resultRolloto = Rolloto(nbDiceRolloto);

                    await commandContext.Message.RespondAsync($"DES : " + nbDiceRolloto + ", SUCCES : " + resultRolloto[0] + ", SIX : " + resultRolloto[1]);
                    break;

                case STATE.wta:

                    bool botch = false;
                    bool botchPrevent = false;
                    const int nbDicesFaceWta = 10;
                    int difficulty = secondParam;
                    int nbDicesRoll = firstParam;
                    int[] results = new int[nbDicesRoll];
                    int succes = 0;

                    if (nbDicesRoll > 0 && difficulty > 0)
                    {
                        for (int i = 0; i < nbDicesRoll; ++i)
                        {

                            results[i] = new Random().Next(1, nbDicesFaceWta + 1);

                            if (results[i] >= difficulty)
                            {
                                ++succes;
                                if (!botchPrevent)
                                {
                                    botchPrevent = true;
                                }
                            }
                            else if (results[i] == 1)
                            {
                                --succes;
                            }
                        }

                        for (int i = 0; i < results.Length; i++)
                        { 
                            if ( succes <= 0 && results[i] == 1 && !botchPrevent)
                            {
                                botch = true;
                            }
                        }

                        string debugNb = "All results : ";

                        debugNb += string.Join(" | ", results);

                        if (succes > 0)
                        {
                            await commandContext.Message.RespondAsync($"You rolled : { nbDicesRoll} dices at difficulty {difficulty} \nYou earned : { succes} succes \n{debugNb}");
                        }
                        else if (botch)
                        {
                            await commandContext.Message.RespondAsync($"You rolled : {nbDicesRoll} dices at difficulty {difficulty} \nBotch !... Let's see what the StoryTeller will have for you :)... \n{debugNb}");
                        }
                        else
                        {
                            await commandContext.Message.RespondAsync($"You rolled : {nbDicesRoll} dices at difficulty {difficulty} \nYou failed... sorry \n{debugNb}");
                        }

                    }
                    else
                    {
                        await commandContext.Message.RespondAsync($"You entered a false parameter. The role command is : \"!roll Difficulty Number_Of_Dices.\"");
                    }
                    break;

                case STATE.aventure:

                    Random Aventurand = new Random();
                    int nbFaces = secondParam;
                    int nbdices = firstParam;

                    if (nbFaces != 6)
                    {

                    }
                    else
                    {

                    }

                    break;

                default:
                    break;
            }
        }


        [Command("tabletop")]
        public async Task SetTableTop(CommandContext commandContext, string tableTop = "")
        {
            if (tableTop == "")
            {
                STATE tempState;
                string tempString = "";
                
                await commandContext.Message.RespondAsync($"Commands :");

                for (int i = 1; i < (int)STATE.MAXVALUE; i++)
                {
                    tempState = (STATE)i;
                    tempString += $"'!tabletop " + tempState.ToString().ToLower() + "' for the " + tempState.ToString() + " preset.\n";
                }
                    await commandContext.Message.RespondAsync(tempString);
            }

            if (tableTop.ToLower() == STATE.savage.ToString().ToLower())
            {
                state = STATE.savage;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Savage mode.");
            }

            if (tableTop.ToLower() == STATE.hexagon.ToString().ToLower())
            { 
                state = STATE.hexagon;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Hexagon mode.");
            }

            if (tableTop.ToLower() == STATE.normal.ToString().ToLower())
            {
                state = STATE.normal;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Normal mode.");
            }

            if (tableTop.ToLower() == STATE.wta.ToString().ToLower())
            {
                state = STATE.wta;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : WereWolf The Apocalypse mode.");
            }

            if (tableTop.ToLower() == STATE.aventure.ToString().ToLower())
            {
                state = STATE.aventure;
                await commandContext.Message.RespondAsync($"The dice bot has been set on : Aventure mode.");
            }
        }

        [Command("h")]
        public async Task HelpMe(CommandContext commandContext)
        {
            await commandContext.Message.RespondAsync($"Commands :");
            await commandContext.Message.RespondAsync($"'!succes' X to set the number that its need to get a succes from the roll.");
            await commandContext.Message.RespondAsync($"'!reroll' X to set the number that a reroll will be add to the pool.");
            await commandContext.Message.RespondAsync($"'!roll' XDX to set the number of dice and faces, first X stand for dice number.");
            await commandContext.Message.RespondAsync($"'!Tabletop Name' To use a preset of one of the table top rules. Please use '!Tabletop' to display all the TableTop available.");
        }

        #region SW
        //Commands SavageWorld
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

        [Command("token")]
        public async Task TokenUser(CommandContext commandContext, string name = "", int tokenNumber = 3)
        {
            if (state == STATE.savage)
            {
                if (name != "")
                {
                    tokens.Add(name.ToLower(), tokenNumber);
                    await commandContext.Message.RespondAsync($"Token created for : " + name + ", you have a total of : " + tokenNumber.ToString() + " coins !");
                }
                else
                {
                    await commandContext.Message.RespondAsync($"Please, enter a name for your character");
                }
            }            
        }

        [Command("list")]
        public async Task CharacterList(CommandContext cc)
        {
            if (state == STATE.savage && tokens.Count > 0)
            {
                List<string> keys = new List<string>(this.tokens.Keys);
                string temp = "";
                for (int i = 0; i < tokens.Count; i++)
                {
                    temp += keys[i] + "\n";
                }
                await cc.Message.RespondAsync(" list of characters : \n" + temp);
            }
            else
            {
                await cc.Message.RespondAsync("There is no character listed yet.");
            }
        }

        [Command("usetoken")]
        public async Task UseToken(CommandContext cc, string name = "")
        {
            name = name.ToLower();

            if (name == "")
            {
                await cc.Message.RespondAsync("You need a character name for this command");
                return;
            }

            if (state == STATE.savage && tokens[name] > 0)
            {
                --tokens[name];
                await cc.Message.RespondAsync("Token remaining : " + tokens[name]);
            }
            else if (tokens[name] <= 0)
            {
                await cc.Message.RespondAsync("You have no token left.");
            }
            else
            {
                await cc.Message.RespondAsync("This character has not been created, please verify the name, or create one");
            }
        }

        [Command("checktoken")]
        public async Task CheckToken(CommandContext cc, string name = "")
        {
            if (name == "")
            {
                await cc.Message.RespondAsync("You need a character name for this command");
                return;
            }

            if (state == STATE.savage)
            {
                await cc.Message.RespondAsync("Token remaining : " + tokens[name]);
            }
        }
        #endregion

        #region WTA
        [Command("moon")]
        public async Task RollMoon(CommandContext commandContext)
        {
            if (state == STATE.wta)
            {
                Random random = new Random();

                MOONPHASE enum_moonPhase = (MOONPHASE)random.Next(1, (int)MOONPHASE.MAXVALUE);

                await commandContext.Message.RespondAsync($"The moon is a : {enum_moonPhase} moon");
            }
        }

        [Command("rolldmg")]
        public async Task RollDMG(CommandContext commandContext, int firstParam, int secondParam)
        {
            if (state == STATE.wta)
            {
                const int nbDicesFaceWta = 10;
                int difficulty = secondParam;
                int nbDicesRoll = firstParam;
                int[] results = new int[nbDicesRoll];
                int succes = 0;

                if (nbDicesRoll > 0 && difficulty > 0)
                {
                    for (int i = 0; i < nbDicesRoll; ++i)
                    {

                        results[i] = new Random().Next(1, nbDicesFaceWta + 1);

                        if (results[i] >= difficulty)
                        {
                            ++succes;
                        }
                    }

                    string debugNb = "All results : ";

                    debugNb += string.Join(" | ", results);

                    if (succes > 0)
                    {
                        await commandContext.Message.RespondAsync($"You rolled : { nbDicesRoll} dices at difficulty {difficulty} \nYou earned : { succes} succes \n{debugNb}");
                    }
                    else
                    {
                        await commandContext.Message.RespondAsync($"You rolled : {nbDicesRoll} dices at difficulty {difficulty} \nNo successes \n{debugNb}");
                    }
                }
            }
        }

        #endregion
    }
}