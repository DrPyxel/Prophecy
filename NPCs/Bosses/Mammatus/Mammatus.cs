using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Prophecy.NPCs.Bosses.Mammatus
{
    public class Mammatus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mammatus");
            //Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 130;
            npc.damage = 14;
            npc.defense = 6;
            npc.lifeMax = 2500;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 3;
            aiType = NPCID.Zombie;
            npc.boss = true;
            npc.noGravity = false;
            //animationType = NPCID.Zombie;
        }

        public int lol;
		 public int lol2;
        public int Timer2;
        public int Timer3;
		int FrameYOffset;
        public int Timer4;
		public int Timer5;
		public int Dir;
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustType = Main.rand.Next(139, 143);
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
		public int progression;
        public override void AI()
        {


         



            //LookInDirection(npc.velocity);

            npc.TargetClosest(false);

            Player player = Main.player[npc.target];
            Vector2 moveTo = player.Center; //This player is the same that was retrieved in the targeting section.

            //Vector2 moveTo = player.Center + new Vector2(0f, -200f); //This is 200 pixels above the center of the player.

            npc.TargetClosest(true);
            float speed = 5f;
            Vector2 move = moveTo - npc.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 10f; //the larger this is, the slower the npc will turn
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity.X = move.X;
       
        
			progression++;
			
			if (progression >= 360 && progression < 720)
				Dash();
			
			if (progression == 720 && progression < 1080)
				Smash();
			
		if (progression >= 1080 && progression < 1400)
				 Boom();

			if (progression >=  1440 && progression < 1800)
				Dash2();
            if (progression >= 1800)
               
            if (progression == 1802)
				progression = 0;
			
           
				
				
				
				
        }
        private void LookToPlayer()
        {
            Player player = Main.player[npc.target];
            Vector2 look = Main.player[npc.target].Center - npc.Center;
            LookInDirection(look);
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
		void Crash(){
			npc.velocity.Y -= 1;
			Timer2++;
			if (Timer2 == 30){
				npc.velocity.Y += 8;
				Timer2 = 0;
			}

		}
		public bool canFly;
		public bool canFly2;
		public bool canFly3;
		void Dash(){
			canFly = true;
			lol++;
			if (lol <=60 && lol != 0){
				
			 npc.velocity *= 1.3f;
			 
                //float disX = Main.rand.NextFloat(-200,200);
                //npc.position = player.C enter + new Vector2(disX, -100);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("Sparkle"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
                Main.PlaySound(15, npc.position, 0);
				if (npc.velocity.Y<-4f){
				npc.velocity.Y= -4f;
			}
				
			}
			else
				canFly = false;
			if (lol == 360)
				lol = 0;
			
		}
		void Dash2(){
			if (npc.velocity.X>0){
				canFly2 = true;
				canFly3 = false;
				}
				if (npc.velocity.X<0){
					canFly3 = true;
					canFly2 = false;
				}
			lol2++;
			if (lol2 <=30 && lol2 != 0)
				 npc.velocity *= -1.1f;
			if (lol2 <=60 && lol2 != 0 && lol2>30){
		
		 	if (lol2 > 65)
			 npc.velocity *= 20f;
			
                //float disX = Main.rand.NextFloat(-200,200);
                //npc.position = player.C enter + new Vector2(disX, -100);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("Sparkle"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
                Main.PlaySound(15, npc.position, 0);
				
				
			}
			
				
			if (lol2 == 360){
				lol2 = 0;
			canFly2 = false;
			canFly3 = false;
			}
		}
		void Spikes(){
			canFly = false;
						Timer3++;
			 if (Timer3 >= 120)  // 230 is projectile fire rate
            {
				Player player = Main.player[npc.target];
                float Speed = 25f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Spikes");  //put your projectile
                float DisP = Main.rand.NextFloat(-20f, 20f);
               
                //Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + DisP + (player.height * 0.5f)), vector8.X - (player.position.X + DisP + (player.width * 0.5f)));
                for (int i = 0; i < 6; i++) {
                    float DisPX = Main.rand.NextFloat(-400f, 400f);
                    int num54 = Projectile.NewProjectile(player.position.X+DisPX, player.position.Y + 300, 0, -3f, type, damage, 0f, 0);
                }
                Timer3 = 0;
            }
				 Main.PlaySound(SoundID.Item14, npc.position);
		}
		
       void Smash(){
			if (npc.velocity.X>1){
					Dir=500;
					for (int x = (int)npc.position.X; x < (npc.position.X + npc.width+Dir); x++)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1, 120);
				}
				if (npc.velocity.X<1){
					Dir= -500;
					for (int x = (int)npc.position.X; x > (npc.position.X + npc.width+Dir); x--)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1, 120);
				}
				 Main.PlaySound(SoundID.Item14, npc.position);
		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (canFly == true)
			player.velocity.Y= -15;
		
		if (canFly2 == true)
			player.velocity.X= 15;
		if (canFly3 == true)
			player.velocity.X= -15;
		}
        void Boom()
        {
			
			Timer5++;
			if (Timer5 == 30){
           if (npc.velocity.X>1){
					Dir=500;
					for (int x = (int)npc.position.X; x < (npc.position.X + npc.width+Dir); x++)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1, 120);
				}
				if (npc.velocity.X<1){
					Dir= -500;
					for (int x = (int)npc.position.X; x > (npc.position.X + npc.width+Dir); x--)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1, 120);
				}
				 Main.PlaySound(SoundID.Item14, npc.position);
				 Timer5 = 0;
			}
        }
        
    
   

        /*private void WalkToTarget()
        {
			Player player = Main.player[npc.target];
            int xDistance = (int) npc.position.X - (int) player.position.X;
            int yDistance = (int)npc.position.Y - (int)player.position.Y;
            int offset = xDistance < 0 ? -100 : 100;

            Vector2 vOffset = new Vector2(offset, offset);
            Vector2 targetOffset = player.Center + vOffset;
            Vector2 movement = targetOffset - npc.Center;

            bool shouldMove = (offset > 0 && xDistance > offset) || offset < 0 && xDistance + 10 < offset;

            if (shouldMove)
            {
                float magnitude = NPCHelper.Magnitude(movement);
                if (magnitude > speed)
                {
                    movement *= (speed / magnitude);
                }
                float turnResistance = 10;
                movement = (npc.velocity * turnResistance + movement) / (turnResistance + 1);
                magnitude = NPCHelper.Magnitude(movement);
                if (magnitude > speed)
                {
                    movement *= (speed / magnitude);
                }
                npc.velocity = movement;
            }
            else
            {
                npc.velocity = new Vector2(0, 0);
            }*/
    }
}

