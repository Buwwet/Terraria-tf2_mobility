using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace tf2_mobility.Projectiles {
    public class Rocket : ModProjectile {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket");
        }

        public override void SetDefaults() {

            //We use the Bullet because test;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            

        }

        //Rocket on hitting tiles will summon an explosion projectile.
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            /*We use oldVelocity to jamm the Explosion into the wall, as it uses
             * its center for calculations we want to keep it in the border of the
             * tiles.
            */
            SpawnExplosion(oldVelocity);
            //Kills the projectile
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //spawn explosion on npc hit to bully them
            SpawnExplosion(Vector2.Zero);
        }

        //Spawn explosion on a function
        public void SpawnExplosion(Vector2 oldVelocity) {
            //We spawn the explosion on the projectile's center and add its velocity to get closer to where it would've been without colliding.
            Projectile.NewProjectile((projectile.Center + oldVelocity), Vector2.Zero, mod.ProjectileType("Explosion"), 0, 0f, projectile.owner);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return true;
        }
    }
}