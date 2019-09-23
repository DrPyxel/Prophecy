using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;

namespace Prophecy.NPCs.Bosses.Eros
{
	public class ErosHandler : ModWorld
	{
		public static int TowerX = -1;
		public static int TowerY = -1;
		public static bool TowerActive;
		public static int ShieldStrength;

		public static bool LunarApocalypseLastTick;

		public override void Initialize()
		{
			LunarApocalypseLastTick = NPC.LunarApocalypseIsUp;
			ShieldStrength = NPC.ShieldStrengthTowerMax;
			TowerX = -1;
			TowerY = -1;
		}

		public override void PreUpdate()
		{
			TowerActive = NPC.AnyNPCs(mod.NPCType("Eros"));
		}

		public override TagCompound Save()
		{
			var tag = new TagCompound
			{
				{"ErosActive", TowerActive}
			};
			if (TowerX != -1)
			{
				tag.Add("ErosX", TowerX);
				tag.Add("ErosY", TowerY);
			}
			return tag;
		}

		public override void Load(TagCompound tag)
		{
			TowerActive = tag.GetBool("ErosActive");
			if (tag.ContainsKey("ErosX"))
			{
				TowerX = tag.GetInt("ErosX");
				TowerY = tag.GetInt("ErosY");
				NPC.NewNPC(TowerX, TowerY, mod.NPCType("Eros"));
			}
		}

		

		void MovePillar(int position, int whoAmI)
		{
		
			//else
			//{
			//Tremor.Log("Moving " + Main.npc[whoAmI].displayName);
			// }

			int x = Main.maxTilesX / 6 * (1 + position);
			var spawnPos = new Vector2(x * 16, (float)(Main.worldSurface - 40) * 16);

			bool success = false;
			for (int attempts = 0; attempts < 30; attempts++)
			{
				int offsetX = Main.rand.Next(-100, 101);
				for (int y = (int)Main.worldSurface; y > 100; y--)
				{
					if (!Collision.SolidTiles(x + offsetX - 10, x + offsetX + 10, y - 20, y + 15) && !WorldGen.PlayerLOS(x + offsetX - 10, y) && !WorldGen.PlayerLOS(x + offsetX + 10, y) && !WorldGen.PlayerLOS(x + offsetX - 10, y - 20) && !WorldGen.PlayerLOS(x + offsetX + 10, y - 20))
					{
						spawnPos = new Vector2((x + offsetX) * 16, y * 16);
						success = true;
						break;
					}
				}
				if (success)
				{
					break;
				}
			}

			if (whoAmI == -1)
			{
				whoAmI = NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, mod.NPCType("Eros"));
				TowerX = (int)spawnPos.X;
				TowerY = (int)spawnPos.Y;
			}
			else
			{
				Main.npc[whoAmI].Center = spawnPos;
				ShieldStrength = NPC.ShieldStrengthTowerMax;
				TowerActive = true;
			}
			if (Main.netMode == 2 && whoAmI < 200)
			{
				NetMessage.SendData(MessageID.SyncNPC, number: whoAmI);
			}
		}

		static readonly string[] ErosNPCs =
		{
			"NovaAlchemist",
			"Varki",
			"Youwarkee",
			"Deadling",
			"NovaFlier"
		};


	}
}
