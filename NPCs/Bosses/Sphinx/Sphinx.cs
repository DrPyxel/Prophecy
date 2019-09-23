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


namespace Prophecy.NPCs.Bosses.Sphinx
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Sphinx : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sphinx");
            //Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
		
            npc.width = 326;
            npc.height = 182;
            npc.damage = 50;
            npc.defense = 10;
            npc.lifeMax = 4500;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            //npc.aiStyle = 3;
            //aiType = NPCID.Zombie;
            npc.boss = true;
            npc.noGravity = false;
            //animationType = NPCID.Zombie;
        }
		public int Check;
        public int lol;
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
            float speed = 0.1f;
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
       
			if (player.position.X-npc.position.X >0)
				npc.spriteDirection = 1;
			if (player.position.X-npc.position.X <0)
				npc.spriteDirection = -1;
			progression++;
			
	
			
	   //Player player = Main.player[npc.target];
       //If the npc is hostile
     
           //Get the shoot trajectory from the projectile and target
           float shootToX = player.position.X + (float)player.width * 0.5f - npc.Center.X;
           float shootToY = player.position.Y - npc.Center.Y;
           float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

		   Check++;
		  
           //If the distance between the live targeted npc and the projectile is less than 480 pixels
		   if(Check >= 60){
           if(distance < 400f && player.active)
           {
			   
				Dash();
			   
		   }
		   if (Check ==130)
		   Check = 0;
		   }
	
			
			if (progression >= 360 && progression < 720)
				Spikes();
			
		if (progression >= 1080 && progression < 1400)
				Flame();
			if (progression == 1440 )
				Baby();
            if (progression >= 1500)
                Boom();

            if (progression == 1700)
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
		void Dash(){
			
			lol++;
			if (lol <=60 && lol != 0){
			npc.velocity *=240f;
		
			 
			 
                //float disX = Main.rand.NextFloat(-200,200);
                //npc.position = player.C enter + new Vector2(disX, -100);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("Sparkle"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
                Main.PlaySound(15, npc.position, 0);
				if (npc.velocity.Y<-1f){
				npc.velocity.Y= -1f;
			}
				
			}
			if (lol == 360)
				lol = 0;
		}
		void Spikes(){
			
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
		void Flame(){
            Player player = Main.player[npc.target];
			Timer3++;
			npc.velocity.X=0;
			npc.velocity.Y=0;
			 if (Timer3 >= 10)  // 230 is projectile fire rate
            {


                float Speed = 3f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Flame");  //put your projectile
                float DisX = Main.rand.NextFloat(-1000f, 1000f);
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X+DisX, vector8.Y-1000, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                
                Timer3 = 0;
            }
		}
        void Baby()
        {
            for(int x = 0; x <=3; x++)
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 61);

        }
        void Boom()
        {
            Player player = Main.player[npc.target];
            int laserindex = 0;
            laserindex = npc.whoAmI;
			bool ifLaserRight;
			bool ifLaserLeft;
            float Speed = 10f;  //projectile speed
            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            int damage = 10;  //projectile damage
            int type = mod.ProjectileType("ArrowProj");  //put your projectile
            Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
            float DirX = 0;
			
            if (npc.velocity.X > 0)
            {
				ifLaserRight=true;
				ifLaserLeft=false;
			if (ifLaserRight==true){
                DirX = -90;
                for (laserindex = 0; laserindex < 200; laserindex++)
                {
                    if (Main.npc[laserindex].type == mod.NPCType("Sphinx"))
                    {
                        float rotation = (float)Math.Atan2(0, DirX);

                        int num55 = Projectile.NewProjectile(vector8.X, vector8.Y-5, (float)((Math.Cos(rotation) * 1) * -1), (float)((Math.Sin(rotation) * 1) * -1), type, damage, 0f, Main.myPlayer, npc.whoAmI);

                    }
                }
			}
            }
            if (npc.velocity.X < 0)
            {
				ifLaserLeft=true;
				ifLaserRight=false;
				if (ifLaserLeft==true){
                DirX = 90;
                for (laserindex = 0; laserindex < 200; laserindex++)
                {
                    if (Main.npc[laserindex].type == mod.NPCType("Sphinx"))
                    {
                        float rotation = (float)Math.Atan2(0, DirX);

                        int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * 1) * -1), (float)((Math.Sin(rotation) * 1) * -1), type, damage, 0f, Main.myPlayer, npc.whoAmI);

                    }
                }
				}
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
	
