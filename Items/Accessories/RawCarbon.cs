using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.Items.Accessories
{
    public class RawCarbon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raw Carbon");
            Tooltip.SetDefault("Provides resistance to radiation\n-10 Defense");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 2;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.statDefense -= 10;
            player.GetModPlayer<ProphecyPlayer>().radiationResist = true;
        }
        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Carbon", 10);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

    }
}