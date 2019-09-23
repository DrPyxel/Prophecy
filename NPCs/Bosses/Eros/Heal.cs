using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Prophecy.NPCs.Bosses.Eros
{
    public class Heal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heal");
            Main.projFrames[projectile.type] = 3;
        }
        
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 28;
            projectile.friendly = true;
			 projectile.hostile = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.tileCollide = false;
			projectile.penetrate = -1;

        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			
			for (int indexer = 0; indexer < 200; indexer++)
                    {
                        if (Main.npc[indexer].type == mod.NPCType("Eros"))
                        {
        //this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds

	int hpBeforeHeal = Main.npc[indexer].life; // Сохраняем в переменную текущее хп
				Main.npc[indexer].life += 200; // Добавляем в хп моба урон который должны нанести

				
				
					Main.npc[indexer].HealEffect(200); // Показываем эффект лечения на то хп, которое восстановил моб
				projectile.timeLeft = 0;
						}
					}
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
            //projectile.velocity.Y = 10f;
        }
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.rand.Next(2) == 0) // the chance
	{
		player.AddBuff(BuffID.Webbed, 60, true);
	}
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
