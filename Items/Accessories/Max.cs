using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace tf2_mobility.Items.Accessories {
    public class Max : ModItem {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Max's Severed Head");
            Tooltip.SetDefault("A simple Bunny Jump");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.rare = ItemRarityID.Blue;
            item.accessory = true; //Make this item an accessory. 
        }

        //Accesory states
        //enum acc_states {ready = 0, recharging = 1}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.velocity.Y == 0 && Math.Abs(player.velocity.X) >= player.maxRunSpeed) {
                player.velocity.Y = -8;
                
            }
        }
    }
}