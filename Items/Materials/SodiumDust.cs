using Terraria.ModLoader;
using Terraria.ID;

namespace Prophecy.Items.Materials
{
    public class SodiumDust : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sodium Dust");
        }

        public override void SetDefaults()
        {
            item.width = 24; // Hitbox Width
            item.height = 24; // Hitbox Height
            item.value = 0;
            item.rare = 1; // Item Tier
            item.maxStack = 999; // The maximum number you can have of this item.
        }
    }
}