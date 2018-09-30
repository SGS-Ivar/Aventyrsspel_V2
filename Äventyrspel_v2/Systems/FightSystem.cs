﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Äventyrspel_v2 {
    class FightSystem {

        List<Attack> Attacks = new List<Attack>();

        int EnemiesKilled = 0;
        public int PlayerHealth = 100;
        public int FoodValue = 100;

        public bool IsAlive = true;
        public string CauseOfDeath;

        public Attack GunShot = new Attack();
        public Attack ShotgunShot = new Attack();
        public Attack SniperShot = new Attack();

        //Called when the player should attack an enemy
        public void Attack(Enemy enemyToAttack, int DaysAlive) {

            //While the player and the enemy is still alive
            while (IsAlive && enemyToAttack.IsAlive) {

                //Get the players and the enemys attack
                Attack attackToUse = GetAttack();
                Attack enemyAttack = enemyToAttack.GetEnemyAttack();

                //If the players attack speed is less than the enemys attack speed
                if (attackToUse.AttackSpeed < enemyAttack.AttackSpeed) {

                    //Damage the player
                    PlayerHealth -= enemyAttack.AttackDamage;

                    //If both the player and the enemy is still alive
                    if (PlayerHealth > 0 && enemyToAttack.Healh > 0) {

                        //Damage the enemy
                        enemyToAttack.Healh -= attackToUse.AttackDamage;

                        //Show the enemys and the players health
                        Console.Clear();
                        Console.WriteLine("Attack Successful!");
                        ShowHealth(enemyToAttack);

                        Console.WriteLine("Press ENTER to attack again");
                        Console.ReadKey();

                    }
                    //If one of them is not alive
                    else {

                        //Check who it is that is dead and end the loop
                        if (PlayerHealth <= 0) {
                            IsAlive = false;
                            break;
                        }
                        else if (enemyToAttack.Healh <= 0) {
                            enemyToAttack.IsAlive = false;
                            break;
                        }

                    }
                }
                //If the players attack speed is greater than the enemys
                else if (attackToUse.AttackSpeed > enemyAttack.AttackSpeed) {

                    //Damage the enemy
                    enemyToAttack.Healh -= attackToUse.AttackDamage;

                    //If the player and the enemy is still alive
                    if (enemyToAttack.Healh > 0 && PlayerHealth > 0) {

                        PlayerHealth -= enemyAttack.AttackDamage;

                        //Show the players and the enemys health
                        Console.Clear();
                        Console.WriteLine("Attack Successful!");
                        ShowHealth(enemyToAttack);

                        Console.WriteLine("Press ENTER to attack again");
                        Console.ReadKey();

                    }
                    //If on of them is dead
                    else {

                        //Check who it is that is dead
                        if (PlayerHealth <= 0) {
                            IsAlive = false;
                            break;
                        }
                        else if (enemyToAttack.Healh <= 0) {
                            enemyToAttack.IsAlive = false;
                            break;
                        }

                    }

                }
                //If the attack speeds are the same
                else if (attackToUse.AttackSpeed == enemyAttack.AttackSpeed) {

                    //Tell the player that the attacks have cancelled out
                    Console.Clear();
                    Console.WriteLine("The attack speed is the same! The attacks have cancelled out!");
                    ShowHealth(enemyToAttack);

                    Console.WriteLine("Press ENTER to attack again");
                    Console.ReadKey();

                }

            }

            if (PlayerHealth <= 0) {
                IsAlive = false;
            }
            else if (enemyToAttack.Healh <= 0) {
                IsAlive = true;
            }

            //Check if the player is alive, otherwise the player 
            //died and need to rerun the game
            if (IsAlive) {

                //Tell the player that the enemy died
                Console.Clear();
                Console.WriteLine("Enemy died!");
                Console.WriteLine("Your health: " + PlayerHealth);
                Console.WriteLine("Press ENTER to continue");
                EnemiesKilled++;
                Console.ReadKey();

            }
            else {

                //Tell the player that it died and that it's game over
                Console.Clear();
                Console.WriteLine("You died :(");
                Console.WriteLine("Press ENTER to continue");
                Console.ReadKey();

                CauseOfDeath = "Killed by enemy '" + enemyToAttack.name + "'";
                //Shows the game over screen
                ShowGameOver(DaysAlive);

            }

        }

        //Shows the attack list and lets you choose which to use
        Attack GetAttack() {

            bool hasChosen = false;

            while (!hasChosen) {

                //Clears the console
                Console.Clear();

                for (int i = 0; i < Attacks.Count; i++) {

                    Console.WriteLine((i + 1) + " - " + Attacks[i].AttackName);
                }
                //Get the number to get the attack
                string select = Console.ReadLine();

                //Return the attack that has been selected with the entered number above
                if (select == "1") {
                    return Attacks[0];
                }
                else if (select == "2") {
                    return Attacks[1];
                }
                else if (select == "3") {
                    return Attacks[2];
                }
                else if (select == "4") {
                    return Attacks[3];
                }
                else if (select == "5") {
                    return Attacks[4];
                }
                else {
                    Console.WriteLine("Incorrect input!");
                }

            }

            return new Attack();

        }

        //Prints out the health of the player and the enemy
        void ShowHealth(Enemy enemyToAttack) {

            Console.WriteLine("Enemy health: " + enemyToAttack.Healh);
            Console.WriteLine("Player health: " + PlayerHealth);

        }

        //Shows the game over screen
        public void ShowGameOver(int DaysAlive) {

            Console.Clear();

            var gameOverScreen = new[] {

                @"----------------------------------------------------------------------",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                            Game Over                               -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                      Press ENTER to continue                       -",
                @"-                                                                    -",
                @"----------------------------------------------------------------------",
            };

            //Writes out the game over screen
            foreach (string line in gameOverScreen) {

                Console.WriteLine(line);
                System.Threading.Thread.Sleep(30);
            }

            ShowStats(DaysAlive);

            Console.WriteLine("Press ENTER to restart the game");
            Console.ReadKey();

            // Starts a new instance of the program itself
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);

            // Closes the current process
            Environment.Exit(0);

        }

        //Shows the players stats on death
        void ShowStats(int DaysAlive) {

            Console.WriteLine("Days stayed alive: ");
            Console.WriteLine("Enemies killed: " + EnemiesKilled);
            Console.WriteLine("Cause of death: " + CauseOfDeath);

        }

        //Adds an attack to the attacks list
        public void AddAttack(Attack attack) {

            Attacks.Add(attack);

        }
    }

    class Attack {

        public string AttackName;
        public int AttackSpeed;
        public int AttackDamage;

    }

    class Enemy {

        public string name;

        public bool IsAlive = true;
        public int Healh;
        public int MaxEnemyAttacks;

        public List<Attack> EnemyAttacks = new List<Attack>();

        //Gets a random attack for the enemy
        public Attack GetEnemyAttack() {

            //Returns a random attack from the list if enemy attacks
            Random random = new Random();
            return EnemyAttacks[random.Next(0, (MaxEnemyAttacks))];

        }
    }

}
