using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;



namespace Prophecy.NPCs.Bosses.Eros
{
	public class SkeletonKnight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skeleton Knight");
			Main.npcFrameCount[npc.type] = 7;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;
			npc.damage = 16;
			npc.defense = 16;
			npc.lifeMax = 270;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = Item.buyPrice(0, 0, 5, 7);
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = 434;
			animationType = 434;
		}



		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int k = 0; k < 20; k++)
					Dust.NewDust(npc.position, npc.width, npc.height, 151, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);

				
			}
					if (npc.life <= 0)
			{
				if (ErosHandler.ShieldStrength > 0)
				{
					NPC parent = Main.npc[NPC.FindFirstNPC(mod.NPCType("Eros"))];
					Vector2 Velocity = Helper.VelocityToPoint(npc.Center, parent.Center, 8);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Velocity.X, Velocity.Y, mod.ProjectileType("CurrentLaser"), 1, 1f);
				}
			}
		}

	
	}
}
