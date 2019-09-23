
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Eros
{
    
	public class Succubus : ModNPC
	{
		
		

        private int aiPhase;
        private bool transition;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Succubus");
		}   
        public override void SetDefaults()
        {
           
            npc.aiStyle = -1;
            npc.lifeMax = Main.expertMode ? 200 : 300;    //this is the npc health
            npc.damage = Main.expertMode ? 15 : 25 ;  //this is the npc damage
            npc.defense = 6;         //this is the npc defense
            npc.knockBackResist = 0f;
            npc.width = 130; //this is where you put the npc sprite width.     important
            npc.height = 130; //this is where you put the npc sprite height.   important
            npc.lavaImmune = true;       //this make the npc immune to lava
            npc.noGravity = true;           //this make the npc float
            npc.noTileCollide = false;        //this make the npc go thru walls
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.behindTiles = false;          
			
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            aiPhase = 1;
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
		 public int Timer;
         public override void AI() //this is where you program your AI
        {
			FindFrame(38);
			if (npc.life > 500){ 
			//LookInDirection(npc.velocity);
			}
								npc.TargetClosest(false);

			Player player = Main.player[npc.target];
			Vector2 moveTo = player.Center + new Vector2(0,-300); //This player is the same that was retrieved in the targeting section.
		
			//Vector2 moveTo = player.Center + new Vector2(0f, -200f); //This is 200 pixels above the center of the player.
			
			npc.TargetClosest(true);
			float speed = 5f;
		Vector2 move = moveTo - npc.Center;
		float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
		if(magnitude > speed)
		{
			move *= speed / magnitude;
		}
		float turnResistance = 10f; //the larger this is, the slower the npc will turn
		move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
		magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
		if(magnitude > speed)
		{
			move *= speed / magnitude;
		}
		npc.velocity = move;
		npc.ai[1]++;
		npc.ai[2]++;
            if (npc.ai[1] >= 230)  // 230 is projectile fire rate
            {
				 float Speed = 4f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 3;  //projectile damage
                int type = mod.ProjectileType("Flam");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				npc.ai[1] = 0;
				
               
            }
            if (npc.ai[1] == 200)  // 230 is projectile fire rate
            {
               
              
            }
            if (npc.ai[2] >= 750) 
		{
			
        }
		if (npc.ai[2] == 450 || npc.ai[2] == 500) 
		{
			
                
        }
		if (npc.ai[2] == 475 || npc.ai[2] == 525) 
		{

			
		}
		if (npc.life <= 500){ 
                //npc.ai[3]++;
							
		}
		if (npc.ai[3] >= 10) 
		{
        
			
        }
		if (npc.ai[3] >=1)
		{
			
		}
		Timer++;
		
		if (Timer > 500){
		//Player player = Main.player[npc.target];
	
			
			 if (Timer3 >= 10)  // 230 is projectile fire rate
            {

              
            }
	if (Timer == 650)
		Timer=0;
		}
		
		}
		
public int Timer3;
private void LookToPlayer()
		{
			Player player = Main.player[npc.target];
			Vector2 look = Main.player[npc.target].Center - npc.Center;
			//LookInDirection(look);
		}

		private void LookInDirection(Vector2 look)
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
			npc.rotation = angle;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}
		

		
   
}
}

