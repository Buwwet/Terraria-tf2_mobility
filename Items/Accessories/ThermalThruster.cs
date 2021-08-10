using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
 

namespace tf2_mobility.Items.Accessories {
    public class ThermalThruster : ModItem {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Death from above! Fires a short-duration blast\nthat launches the Pyro in the direction they are aiming");
        }

        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 16;

            item.accessory = true; //Makes this item an accessory
            item.rare = ItemRarityID.Blue;
        }

        
        enum acc_state {
            ready = 0,
            recharging = 1,
        }
        acc_state acc = acc_state.ready;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //If acc is recharging and player is in ground set state to ready
            if (acc == acc_state.recharging && player.velocity.Y == 0) {
                acc = acc_state.ready;
            }

            /* Thermal Thruster jump:
             * Requires: acc = ready and player in air moving downwards;
             * When going downwards, push player.
            */ 
            if (acc == acc_state.ready && player.velocity.Y >= 1) {
                player.velocity = new Vector2(12 * player.Directions.X, -10);
                acc = acc_state.recharging;
            }
            base.UpdateAccessory(player, hideVisual);

            
        }
    }
}