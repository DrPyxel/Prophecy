using Terraria.ID;
using Terraria.ModLoader;

namespace Reality.Items.Weapons.Melee
{
	public class Primape_Saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primape Saber");
			Tooltip.SetDefault("Drenched in the blood of Royals");
		}
		public override void SetDefaults()
		{
			item.damage = 132;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 10000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 10);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
