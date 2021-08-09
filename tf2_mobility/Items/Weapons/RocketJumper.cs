using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

//Get projectile
using tf2_mobility.Projectiles;

namespace tf2_mobility.Items.Weapons {
    public class RocketJumper : ModItem {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("You were good son, real good. Maybe even the best.");
        }

        public override void SetDefaults()
        {
            item.noMelee = true;
            item.notAmmo = true;

            item.autoReuse = true;
            item.rare = ItemRarityID.Blue;

            //Weapon shoot speed.
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            //For the weapon to not be tiny
            item.scale = 1.2f;

            //Shoot the rocket.
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 16;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-32, 0);
        }
    }
}