
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Prophecy.NPCs.Bosses.Dionysus
{
	public class Bottle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bottle");
		}
private NPC OwnerNpc
        {
            get { return Main.npc[(int)projectile.ai[0]]; }
        }
		public override void SetDefaults()
		{
			projectile.width = 72;
			projectile.light = 2f;
			projectile.height = 2;
			projectile.alpha = 0;
			projectile.timeLeft = 200;
			projectile.penetrate = 1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		public int Timer;
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
			
			
			LookInDirectionP(projectile.velocity);
		   
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
           if(distance < 480f && player.active)
           {
               //Divide the factor, 3f, which is the desired velocity
               distance = 3f / distance;
   
               //Multiply the distance by a multiplier if you wish the projectile to have go faster
               shootToX *= distance * 2;
               shootToY *= distance * 2;

               //Set the velocities to the shoot values
               projectile.velocity.X = shootToX;
               projectile.velocity.Y = shootToY;
}

}

		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
        {
        //this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds
		   			if (Main.rand.Next(2) == 0) // the chance
	{
		player.AddBuff(BuffID.Frozen, 20, true);
	}
	int hpBeforeHeal = OwnerNpc.life; // Сохраняем в переменную текущее хп
				OwnerNpc.life += 200; // Добавляем в хп моба урон который должны нанести

				
				
					OwnerNpc.HealEffect(200); // Показываем эффект лечения на то хп, которое восстановил моб
				projectile.timeLeft = 0;
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