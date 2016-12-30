using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pirates;

namespace SkillZ2016
{
    public class MyBot : IPirateBot
    {
        public static PirateGame game;
        private void HandleDrones(PirateGame game)
        {
            // Go over all of my drones
            foreach (Drone drone in game.GetMyLivingDrones())
            {
                // Get my first city
                City destination = game.GetMyCities()[0];
                // Get sail options
                List<Location> sailOptions = game.GetSailOptions(drone, destination);
                // Set sail!
                game.SetSail(drone, sailOptions[0]);
            }
        }
        public bool TryAttack(Pirate pirate, PirateGame game)
        {
            // Go over all enemies
            foreach (Aircraft enemy in game.GetEnemyLivingAircrafts())
            {
                // Check if the enemy is in attack range
                if (pirate.InAttackRange(enemy))
                {
                    // Fire!
                    game.Attack(pirate, enemy);
                    // Print a message
                    game.Debug("pirate " + pirate + " attacks " + enemy);
                    // Did attack
                    return true;
                }
            }

            // Didnt attack
            return false;
        }
        private void HandlePirates(PirateGame game)
        {
            // Go over all of my pirates
            foreach (Pirate pirate in game.GetMyLivingPirates())
            {
                if (!TryAttack(pirate, game))
                {
                    // Get the first island
                    Island destination = game.GetAllIslands()[0];
                    // Get sail options
                    List<Location> sailOptions = game.GetSailOptions(pirate, destination);
                    // Set sail!
                    game.SetSail(pirate, sailOptions[0]);
                    // Print a message
                    game.Debug("pirate " + pirate + " sails to " + sailOptions[0]);
                }
            }
        }

        public void DoTurn(PirateGame game)
        {
            MyBot.game = game;
            // Give orders to my pirates
            HandlePirates(game);
            // Give orders to my drones
            HandleDrones(game);

        }
    }
}
