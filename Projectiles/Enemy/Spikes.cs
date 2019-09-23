
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Prophecy.Projectiles.Enemy
{
	public class Spikes : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spikes");
			Main.projFrames[projectile.type] = 8;
		}
		private NPC OwnerNpc {
        get { return Main.npc[(int)projectile.ai[0]]; }
    }


		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.alpha = 0;
			projectile.timeLeft = 600;
			projectile.penetrate = 1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			 projectile.light = 2f;  
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
			//Making player variable "p" set as the projectile's owner
   
projectile.velocity.Y -= 0.4f;
    //Factors for calculations

		
    //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value

			//LookInDirectionP(projectile.velocity);
			
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

		// Calculate new speeds for other projectiles.
		// Rebound at 40% to 70% speed, plus a random amount between -8 and 8
		/*float speedX = -projectile.velocity.X * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
		float speedY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little more obscure.
		// Spawn the Projectile.
		Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, 0, -7, mod.ProjectileType("SmallE"), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
		Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, 0, 7, mod.ProjectileType("SmallE"), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
		Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, 7, 0, mod.ProjectileType("SmallE"), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
		Projectile.NewProjectile(projectile.position.X + speedX, projectile.position.Y + speedY, -7, 0, mod.ProjectileType("SmallE"), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);*/
		
		
			
       // Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("Anim"), 0, 0f, projectile.owner, 0f, 0f);
		
	
}
	}
}


