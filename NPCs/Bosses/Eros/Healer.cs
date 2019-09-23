using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Eros
{
	public class Healer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Healer");
			Main.npcFrameCount[npc.type] = 2;
		}
public int Timer;
		public override void SetDefaults()
		{
					npc.lifeMax = 150;
			npc.noGravity = true;
			npc.damage = 10;
			npc.defense = 1;
			npc.knockBackResist = 0.05f;
			npc.width = 32;
			npc.height = 40;
			animationType = 176;
			npc.aiStyle = 5;
			aiType = 176;
			npc.npcSlots = 0.5f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath44;
			npc.value = Item.buyPrice(0, 0, 10, 0);
			npc.noTileCollide = true;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
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
		public override void AI(){
			Timer++;
			
				if (npc.life <= 0){
					int a = 0;
					a = npc.whoAmI;
					 for (a = 0; a < 200; a++)
                    {
                        if (Main.npc[a].type == mod.NPCType("Eros"))
                        {
							int hpBeforeHeal = Main.npc[a].life; // Сохраняем в переменную текущее хп
				Main.npc[a].life -= 200; // Добавляем в хп моба урон который должны нанести

				
				
					Main.npc[a].HitEffect(200); // Показываем эффект лечения на то хп, которое восстановил моб
					}
					}
				}
				
			
			  if (Timer >= 230)  // 230 is projectile fire rate
            {
				Player player = Main.player[npc.target];
			int indexer = 0;
			
			//Player player = Main.player[indexer];    
                indexer = npc.whoAmI;
				 float Speed = 15f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 3;  //projectile damage
                int type = mod.ProjectileType("Heal");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
				
              
			 for (indexer = 0; indexer < 200; indexer++)
                    {
                        if (Main.npc[indexer].type == mod.NPCType("Eros"))
                        {
			 float rotation = (float)Math.Atan2(vector8.Y - (Main.npc[indexer].position.Y + (player.height * 0.5f)), vector8.X - (Main.npc[indexer].position.X + (player.width * 0.5f)));
				int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * 3) * -1), (float)((Math.Sin(rotation) * 3) * -1), type, damage, 0f, Main.myPlayer, npc.whoAmI);
              
               
			}
			 Timer=0;
            }
					}
		}
		
	}
}