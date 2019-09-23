
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Prophecy.NPCs.Bosses.Dionysus
{
	public class Grape : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grape");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.alpha = 0;
			projectile.timeLeft = 600;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.alpha =133;
		}
		private void LookToPlayer()
		{
			Player player = Main.player[projectile.owner];
			Vector2 look = Main.player[projectile.owner].Center - projectile.Center;
			LookInDirectionP(look);
		}
		public int Timer;
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
		public bool GetRek;
		public override void AI()
		{
			for(int i = 0; i < 200; i++)
    {
			 NPC target = Main.npc[i];
	   Player player = Main.player[projectile.owner];
       //If the npc is hostile
     
           //Get the shoot trajectory from the projectile and target
           float shootToX = player.position.X + (float)player.width * 0.5f - projectile.Center.X;
           float shootToY = player.position.Y - projectile.Center.Y;
           float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

		   
           //If the distance between the live targeted npc and the projectile is less than 480 pixels
           if(distance < 200f && player.active)
           {
			   GetRek = true;
			   if (GetRek = true){
	projectile.velocity = new Vector2(0,0);
			   projectile.alpha--;
			   Timer++;
			   if(projectile.alpha == 0)
			   projectile.Kill();
			   }
			   
	}
	}
		   
			Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("Sparkle"), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			LookInDirectionP(projectile.velocity);
		}
		
		public override void Kill(int timeLeft)
        {
			
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 10;     //this is the explosion radius, the highter is the value the bigger is the explosion

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                    {
                        Dust.NewDust(position, 22, 22, DustID.Gold, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                    }
                }
            }
        }
		
	}
}