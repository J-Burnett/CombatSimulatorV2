using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulatorV2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sets console size
            Console.SetWindowSize(Console.LargestWindowWidth - 122, Console.LargestWindowHeight - 30);
            //Calls game
            Game game = new Game();
            game.PlayGame();
        }
    }

    public class Actor
    {
        public string Name { get; set; }
        public double HP { get; set; }
        public bool IsAlive
        {
            get
            {
                if (this.HP > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public Random RNG = new Random();

        public Actor(string name, int startingHP)
        {
            this.RNG = new Random();
            this.HP = startingHP;
            this.Name = name;
        }

        public virtual void DoAttack(Actor actor)
        {

        }

        public virtual void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
     
    }

    public class Enemy : Actor
    {
        
        public Enemy(string name, int startingHP) : base(name, startingHP)
        {
            this.Name = name;
            this.HP = startingHP;
        }

        public override void centerText(string text)
        {
            base.centerText(text);
        }
        public override void DoAttack(Actor actor)
        {
            //contains all the logic for determining a hit. The combat text for the attack also happens here
            int playersTurn = this.RNG.Next(1, 5);
            //string to store damage done to player
            string damageToArcher = string.Empty;

            switch (playersTurn)
            {

                case 1:
                    damageToArcher = "Lana slapped you for not listening again!";
                    break;
                case 2:
                    damageToArcher = "Krenshaw, I mean Kremenski, has come back from the dead to attack you!";
                    break;
                case 3:
                    damageToArcher = "They REALLY want those blueprints!";
                    break;
                case 4:
                    damageToArcher = "You should probably drink more if you are going to defeat the KGB";
                    break;
            }

            //randomly generates foe's damage amount and chance of hit

            int damageAttack = this.RNG.Next(5, 16);
            int damageChance = this.RNG.Next(1, 6);

            //if he successfully attacks

            if (damageChance <= 4)
            {
                //subtracts damage amount from players HP
                actor.HP -= damageAttack;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                //prints hit message
                centerText(damageToArcher);
                centerText("The KGB agents cost you " + damageAttack + "HP!");
            }

            //otherwise,
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                //prints failed attack message
                centerText("They missed you! Maybe the KGB has been drinking too!");
            }
          

        }
    }

   public class Player : Actor
    {
       public double AccuracyIncrease { get; set; }
       public double SwigsOfScotch { get; set; }
       public double TurnCounter { get; set; }

       /// <summary>
       /// Plays audio files in game
       /// </summary>
       /// <param name="strWavName">Audio file to play</param>
       
       public static void PlaySound(string strWavName)
       {
           try
           {
               System.Media.SoundPlayer player = new System.Media.SoundPlayer(strWavName);
               player.Play();
           }
           catch
           {

           }
         
       }
       public enum AttackType  
       {
           ShootWithSubmachineGun = 1,
           KravMaga,
           DrinkScotch,
           ShootWhileDrinking,
           TurtleNeck,
           IgnoreLana

       }
       
       public Player(string name, int startingHP) : base(name, startingHP)
       {
           //Initializes properties for player
           this.Name = name;
           this.HP = startingHP;
           this.AccuracyIncrease = 1;
           this.SwigsOfScotch = 0;
           this.TurnCounter = 0;
       }

       public override void DoAttack(Actor actor)
       {
           //Counters
           double amountOfDamage = 0;
           double liverDamage = 0;
           double increaseHP = 0;
           double decreaseEnemyAttack = 0;
           

           //determines what type of attack is taking place
           switch (ChooseAttack())
           {
               case AttackType.ShootWithSubmachineGun:
                   if (RNG.Next(1, 11) <= 7)
                   {
                       amountOfDamage = RNG.Next(40, 61);
                       actor.HP -= amountOfDamage;

                       //Plays excited audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\ArcherWooHoo.wav");

                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("LANA! We need to use these more often! WOOOHOOOO!");
                       //Prints damage info
                       centerText("You dealt " + amountOfDamage + " HP damage to the KGB");
                   }
                   else
                   {
                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;

                       //Plays Krieger audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\KriegerBreakingGuns.wav");

                       //otherwise, player missed
                       centerText("Damn it! Krieger has been messing with my guns again");
                       System.Threading.Thread.Sleep(3200);
                   }
                   break;

               case AttackType.KravMaga:
                   amountOfDamage = RNG.Next(15, 21);
                   actor.HP -= amountOfDamage;

                   //Plays Krieger audio
                   PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\TotallyNinja.wav");

                   //Prints message in cyan
                   Console.ForegroundColor = ConsoleColor.Cyan;

                   centerText("Those Krav Maga classes on Thursday are really paying off");
                   //Prints damage info
                   centerText("You dealt " + amountOfDamage + " HP damage to the KGB");
               
                   break;

               case AttackType.DrinkScotch:
                   //adds to Scotch counter
                   this.SwigsOfScotch++;

                   if(this.SwigsOfScotch > 3 && (RNG.Next(1,4) == 2))
                   {
                       //damage to enemy
                       amountOfDamage = 100;
                       actor.HP -= amountOfDamage;

                       //consequential damange to player
                       liverDamage = RNG.Next(10, 16);
                       this.HP -= liverDamage;

                       //Plays rampage audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\RampageTime.wav");

                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("RAMPAGE TIME!!!");
                       //Times results
                       System.Threading.Thread.Sleep(13000);
                       //Prints damage info
                       centerText("You dealt " + amountOfDamage +  " HP damage to the KGB");
                       centerText("But you now have severe liver damage and caused " + liverDamage + " damage to yourself");
                   }
                   else
                   {
                       increaseHP = RNG.Next(10, 21);
                       this.HP += increaseHP;
                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("Don't look at me like that, Lana.");
                       centerText("Scotch is the cure for all ailments.");
                       System.Threading.Thread.Sleep(2000);
                       //Plays drinking audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\ArcherDrinking.wav");
                       System.Threading.Thread.Sleep(3200);

                   }
                 
                   break;

               case AttackType.ShootWhileDrinking:
                   this.SwigsOfScotch++;

                   if(RNG.Next(1, 11) <= 8)
                   {
                       //Adds damage to Enemy
                       amountOfDamage = RNG.Next(15, 21);
                       actor.HP -= amountOfDamage;

                       //Plays excited audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\ArcherWooHoo.wav");

                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("WOOOOOOO HOOOOOOOOOOOOO!");
                       //Prints damage info
                       centerText("You dealt " + amountOfDamage + " HP damage to the KGB");
                   }
                   else
                   {
                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("Oh God, I need more to drink.");

                       //Plays Woodhouse audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\CallingForWoodhouse.wav");
                   }
                   break;

               case AttackType.TurtleNeck:
                   //adds to HP
                   decreaseEnemyAttack = RNG.Next(10, 21);
                   this.HP += decreaseEnemyAttack;

                   //Plays Turtleneck audio
                   PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\TactalNeck.wav");
                 
                   //Prints message in cyan
                   Console.ForegroundColor = ConsoleColor.Cyan;
                   centerText("I'm not saying I invented the turtleneck. But I was");
                   centerText("the first person to realize its potential as a tactical"); 
                   centerText("garment. The tactical turtleneck! The... tactleneck!");
                   //Prints health message
                   centerText("You gained " + decreaseEnemyAttack + " HP from your tactleneck");
                   System.Threading.Thread.Sleep(4000);
                   break;

               case AttackType.IgnoreLana:

                   //Increase chance of accuracy
                   this.AccuracyIncrease += 0.2;

                   if((RNG.Next(1, 101)*this.AccuracyIncrease) <= 70 )
                   {
                       //Adds damage to Enemy
                       amountOfDamage = RNG.Next(20, 26);
                       actor.HP -= amountOfDamage;

                       //Plays Lana audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\LanaHittingArcher.wav");
                 

                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("Seriously Lana, I'm at least TRYING to kill these KGB agents.");
                       //Prints damage info
                       centerText("You dealt " + amountOfDamage + " HP damage to the KGB");
                   }
                   else
                   {
                       //Adds damage to Enemy
                       amountOfDamage = RNG.Next(10, 16);
                       actor.HP -= amountOfDamage;

                       //Prints message in cyan
                       Console.ForegroundColor = ConsoleColor.Cyan;
                       centerText("Lana, in case you haven't noticed, we are in the middle of a");
                       //Plays Danger Zone audio
                       PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\DangerZone.wav");
                       System.Threading.Thread.Sleep(1000);
                       centerText("\nDAAANGER ZONE!");
                   }
                   break;

               default: 
                   centerText("Sorry, not a valid input");
                   break;
           }

           //if (TurnCounter >= 3 && SwigsOfScotch < 2)
           //{
           //    Console.WriteLine("Archer! You need to keep drinking or you'll die from your hangover!");
           //    TurnCounter = 0;

           //    if (TurnCounter == 1 && SwigsOfScotch < 2)
           //    {
           //        amountOfDamage = 20;
           //        this.HP -= amountOfDamage;
           //        Console.WriteLine("Your hangover is creeping up on you and did {0} HP damage", amountOfDamage );
           //        Console.WriteLine("I told you! You need to keep drinking!!");
           //    }
           //}
       }

       private AttackType ChooseAttack()
       {
           //Player options
           switch(Console.ReadLine())
           {
               case "1":
                   return AttackType.ShootWithSubmachineGun;
               case "2":
                   return AttackType.KravMaga;
               case "3":
                   return AttackType.DrinkScotch;
               case "4":
                   return AttackType.ShootWhileDrinking;
               case "5":
                   return AttackType.TurtleNeck;
               case "6":
                   return AttackType.IgnoreLana;
               default:
                   return 0;
           }
       }
              
    }

    public class Game
    {

        public Player Player { get; set; }
        public Enemy Enemy { get; set; }

        /// <summary>
        /// Initializes the game with the player and enemy
        /// </summary>
        public Game()
        {
            this.Player = new Player("Sterling Archer", 100);
            this.Enemy = new Enemy("KGB Agents", 500);
        }

        /// <summary>
        /// Allows audio files to play in game
        /// </summary>
        /// <param name="strWavName">Audio file to play</param>
        public static void PlaySound(string strWavName)
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(strWavName);
                player.Play();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handles the basic game logic until game ending condition is met
        /// </summary>
        public void PlayGame()
        {
            //displays the game title
            gameName();
            PlaySound(@"C:\Users\Technologist\Documents\GitHub\CombatSimulatorV2\CombatSimulatorV2\ArcherThemeSong .wav");
            //displays game intro
            DisplayGameIntro();

            //game play condition
            while(this.Player.IsAlive && this.Enemy.IsAlive)
            {
                //Displays game info
                DisplayCombatInfo();
                Console.WriteLine();
                //Player attack
                this.Player.DoAttack(this.Enemy);
                //Times attack results
                System.Threading.Thread.Sleep(4000);
                Console.Clear();

                //Displays game info
                DisplayCombatInfo();
                Console.WriteLine();
                //Enemy attack
                this.Enemy.DoAttack(this.Player);
                //Times attack results
                System.Threading.Thread.Sleep(4000);
            }

            //Player wins the game
            if(this.Player.IsAlive)
            {
                Console.Clear();
                //Prints message
                Console.ForegroundColor = ConsoleColor.Green;
                //Winning message
                Console.WriteLine("\n             You made it back to the agency with the prints!");
                Console.WriteLine("               Time for another drink!");
                playAgain();
            }
            //Player lost
            else
            {
                Console.Clear();
                //Prints message
                Console.ForegroundColor = ConsoleColor.Green;
                //prints losing message
                Console.WriteLine("\n               You lost the blueprints to the KGB and ran out of scotch.");
                Console.WriteLine("                 Now you'll have to deal both Mother && Lana being right...");
                playAgain();
            }

            //Keeps console open
            Console.ReadKey();
        }

        static void gameName()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"                              
               ___                       _   _                         
              / _ \ _ __   ___ _ __ __ _| |_(_) ___  _ __  _           
             | | | | '_ \ / _ \ '__/ _` | __| |/ _ \| '_ \(_)          
             | |_| | |_) |  __/ | | (_| | |_| | (_) | | | |_           
              \___/| .__/ \___|_|  \__,_|\__|_|\___/|_| |_(_)          
             |  _ \|_|_ _ _ __   __ _  ___ _ __  |__  /___  _ __   ___ 
             | | | |/ _` | '_ \ / _` |/ _ \ '__|   / // _ \| '_ \ / _ \
             | |_| | (_| | | | | (_| |  __/ |     / /| (_) | | | |  __/
             |____/ \__,_|_| |_|\__, |\___|_|    /____\___/|_| |_|\___|
                                |___/ ");

            Console.ResetColor();
        }

        static void oldTimeyTextPrinter(string inputText, int pauseDuration)
        {
            //loop over each character
            for (int i = 0; i < inputText.Length; i++)
            {
                //get a letter
                char letter = inputText[i];
                //print the letter to the screen
                Console.Write(letter);
                //create a pause
                System.Threading.Thread.Sleep(pauseDuration);
            }

            //after the text is complete, write a line break
            Console.WriteLine();
        }
        public void DisplayGameIntro()
        {
            //introduces game to player
            oldTimeyTextPrinter(@"
        Your name is Sterling Archer and you are a 37 year old
        spy who works for Mallory Archer, better known as Mother.

        You are on a mission in Cambodia with Secret Agent Lana Kane, but
        while fighting with Lana over your level of drunkenness, you are
        surrounded by KGB agents who are trying to steal the same Cambodian
        military blueprints as you.

        You must now prove to Lana that you can defeat the KGB agents,
        despite being on a 24 hour drinking binge, keep your buzz going
        to avoid a fatal hangover, steal the blueprints, and get yourself
        and Lana back to the agency in one piece.", 10);

            //allows them to start playing the game
            Console.ReadLine();
            //clears the console
            Console.Clear();
        }

        public void DisplayCombatInfo()
        {
            Console.Clear();
            gameName();
            //displays the current HP of the player and the enemy
            //sets text color to green
            Console.ForegroundColor = ConsoleColor.DarkGray;

            //prints game options and HP levels
            Console.WriteLine(@"
Sterling's HP: {0}                                                 KGB HP: {1}", this.Player.HP, this.Enemy.HP);
            //resets text color
            Console.ResetColor();

            Console.WriteLine(@"
                            Choose one:

                            1) Shoot at KGB with submachine gun
                            2) Attack with Krav Maga
                            3) Down a swig of Glengoolie Blue scotch
                            4) Shoot at KGB while drinking
                            5) Put on your Tactical Turtleneck
                            6) Ignore Lana's nagging
");
        }

        static void playAgain()
        {

            //play again message
            Console.Write(@"
                            Want to play again?
                            Yes/No: ");
            //players answer
            string playersAnswer = Console.ReadLine();
            
            //checks players answer
            if (playersAnswer.ToLower() == "yes")
            {
                //if yes, clear console, 
                Console.Clear();

                Game game = new Game();
                game.PlayGame();

            }
            //otherwise
            else if (playersAnswer.ToLower() == "no")
            {
                Console.WriteLine("              Press a key to close the game");
                Console.ReadKey();
            }
            else
            {
                //prints error message
                Console.Write("Sorry, not a valid answer");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();

                //asks player if they want to play again
                playAgain();
            }

        }



    }
}
