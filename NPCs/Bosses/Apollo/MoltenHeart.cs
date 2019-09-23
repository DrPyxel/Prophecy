using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Apollo
{
	public class MoltenHeart : ModItem
	{
		const int XOffset = 300;
		const int YOffset = 100;

		public override void SetDefaults()
		{

			item.width = 40;
			item.height = 28;
			item.maxStack = 20;
			item.value = 100;
			item.rare = 3;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = 4;
			item.consumable = false;

		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Apollo");
			Tooltip.SetDefault("Summons Apollo\n" +
"Will require the hell biome (soon tm)");
		}

		public override bool CanUseItem(Player player)
		{
			return player.position.Y / 16f < Main.maxTilesY-1000 && !NPC.AnyNPCs(mod.NPCType("Apollo"));
		}

		public override void AddRecipes()
		{
			
		}

		public override bool UseItem(Player player)
		{
			NPC.NewNPC((int)player.Center.X+200, (int)player.Center.Y, mod.NPCType("Apollo"));
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}
