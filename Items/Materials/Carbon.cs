using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.Items.Materials
{
    public class Carbon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carbon");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 2;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Furnaces);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

    }
}