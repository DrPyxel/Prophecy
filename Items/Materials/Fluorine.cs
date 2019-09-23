using Terraria.ModLoader;
using Terraria.ID;

namespace Prophecy.Items.Materials
{
    public class Fluorine : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fluorine Crystal");
        }

        public override void SetDefaults()
        {
            item.width = 24; // Hitbox Width
            item.height = 24; // Hitbox Height
            item.value = 0;
            item.rare = 3; // Item Tier
            item.maxStack = 999; // The maximum number you can have of this item.
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("Prophecy:Gemstones", 3);
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}