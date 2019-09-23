
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Prophecy.NPCs.Bosses.Aposnius
{
	public class Rockz : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rockz");
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.alpha = 0;
			projectile.timeLeft = 600;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		private void LookToPlayer()
		{
			Player player = Main.player[projectile.owner];
			Vector2 look = Main.player[projectile.owner].Center - projectile.Center;
			LookInDirectionP(look);
		}
private void LookInDirectionP(Vector2 look)
		{
			float angle = 0.5f * (float)Math.PI;
			if (look.X != 0f)
			{
				angle = (float)Math.Atan(look.Y / look.X);
			}
			else if (look.Y < 0f)
			{
				angle += (float)Math.PI;
			}
			if (look.X < 0f)
			{
				angle += (float)Math.PI;
			}
			projectile.rotation = angle;
		}
		public override void AI()
		{
			projectile.rotation += 0.3f * (float)projectile.direction;
			//LookInDirectionP(projectile.velocity);
			 projectile.velocity.Y = projectile.velocity.Y + 0.15f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
            {
                projectile.velocity.Y = 16f;
            }
		}
		
	}
}