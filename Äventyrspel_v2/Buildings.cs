﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Äventyrspel_v2 {
    class Buildings {

        public List<Enemy> Enemies = new List<Enemy>();
        List<Attack> Attacks = new List<Attack>();

        public int RandomEnemyCountMax;
        public int RandomEnemyCountMin;

        //Generates random buildings when player 
        //decides to go out and venture
        public void GenerateRandomBuilding(List<Attack> attacks) {

            //Clear the list of enemies in the building at start
            Enemies.Clear();
            Random randomEnemies = new Random();

            //Generate a random amount of enemies
            int enemies = randomEnemies.Next(RandomEnemyCountMin, RandomEnemyCountMax);

            //Generates the enemies
            for (int i = 0; i < enemies; i++) {

                //Called to generate an enemy

                Enemies.Add(GenerateEnemy(attacks));

            }

        }

        //Generates an enemy
        Enemy GenerateEnemy(List<Attack> attacks) {

            List<string> names = new List<string>();

            names.Add("Damaged");
            names.Add("Ruthless");
            names.Add("Deatheater");
            names.Add("Killerman");
            names.Add("Tester");

            Random random = new Random();

            //Create the new enemy
            Enemy newEnemy = new Enemy();

            newEnemy.Healh = random.Next(80, 200);
            newEnemy.MaxEnemyAttacks = random.Next(1, 5 + 1);
            newEnemy.name = names[random.Next(0, 5)];

            //Add all the attacks to the enemys attack array
            for (int i = 0; i < newEnemy.MaxEnemyAttacks; i++) {

                newEnemy.EnemyAttacks.Add(attacks[random.Next(0, attacks.Count)]);

            }

            return newEnemy;

        }

    }
}
