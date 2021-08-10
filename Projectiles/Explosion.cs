using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

using System;

namespace tf2_mobility.Projectiles {
    public class Explosion : ModProjectile {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
        
        public override void SetDefaults()
        {
            //Projectile size
            int projectileSize = 124; 
            projectile.width = projectileSize;
            projectile.height = projectileSize;

            //Projectile pierce
            projectile.penetrate = 8;
            projectile.friendly = true;
            //projectile.hostile = true; //Allows us to hit the player and friendly NPCs
            
            projectile.tileCollide = false;

            projectile.timeLeft = 1;

        }
        
        public override bool PreAI(){
            
            /* Loop through all NPCs and Launch them if:
             * 1. They're active
             * 2. The projectile's Hitbox.Intersects with the npc's Hitbox;
            */
            //All npc's list.
            #region NPC_Detection
            NPC[] npcList = Main.npc;
            //Loop for each NPC
            for (int i = 0; Main.maxNPCs > i; i++) {
                //We only want active NPCs, not the Old Man on the other side of the map
                if (npcList[i].active) {
                    //Check if its hitbox intersects with the projectile's hitbox..
                    if (projectile.Hitbox.Intersects(npcList[i].Hitbox)) {
                        //Remember that -Y is up
                        npcList[i].velocity += LaunchDirection(projectile.Center, npcList[i].Center);
                    }
                }
            }
            #endregion

            //TODO: Same as NPC_Detection but with Players
            //Currently only affects projectile owner
            Player player = Main.player[projectile.owner];
            Vector2 jumpMultiplier = new Vector2(1, 1);
            
           
            //Check if player's hitbox intersects with the projectile's.
            if (projectile.Hitbox.Intersects(player.Hitbox)) {
                //Remember that -Y is up
                jumpMultiplier.X = (player.velocity.Y != 0) ? 1.6f : 1f;
                jumpMultiplier.Y = (player.velocity.Y != 0) ? 1.2f : 1f;
                
                player.velocity += jumpMultiplier * LaunchDirection(projectile.Center, player.Center);
            }
            
            return false;
        }
         
       public Vector2 LaunchDirection(Vector2 projPos, Vector2 tarPos) {
           Vector2 LaunchOutput = Vector2.Zero;
            Vector2 LaunchForce = new Vector2(12,12);

            //Get distance of target from projectile
            LaunchOutput = tarPos - projPos;

            Vector2 AbsLaunchOutput = new Vector2(Math.Abs(LaunchOutput.X), Math.Abs(LaunchOutput.X));

            //Make the following divisions less severe. Aka add power;
            AbsLaunchOutput /= 32;
            //Prevent multipliction by extremely small decimal numbers
            AbsLaunchOutput.X = (AbsLaunchOutput.X <= 1) ? 1 : AbsLaunchOutput.X;
            AbsLaunchOutput.Y = (AbsLaunchOutput.Y <= 1) ? 1 : AbsLaunchOutput.Y;
            /* LaunchForce is calculated by dividing itself by an Abs instance of
             * LaunchOutput. So the farther you are from the Center of the projectile.
             * the less it will push you in both X and Y directions.
             * LaunchForce then gets multiplied to a Normalized instance of LaunchOutput.
            */ 
            LaunchForce /= AbsLaunchOutput;
            //mod.Logger.Info(LaunchForce);
            
            LaunchOutput = Vector2.Normalize(LaunchOutput);
            
            
           return LaunchOutput * LaunchForce;
       }

       
        
    }
}