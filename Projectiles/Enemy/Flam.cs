﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Prophecy.Projectiles.Enemy
{
    public class Flam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame");
            Main.projFrames[projectile.type] = 3;
        }
        
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 28;
            projectile.friendly = false;
			 projectile.hostile = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.tileCollide = false;

        }

        public override void AI()
        {
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 74,
					projectile.velocity.X, projectile.velocity.Y, 200, Scale: 1f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation(); // projectile faces sprite right
            projectile.velocity.Y = 4f;
        }
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.position);
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, 0.0f, 0.0f, 100, new Color(), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
    }
}
