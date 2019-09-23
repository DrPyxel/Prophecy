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


namespace Prophecy.NPCs.Bosses.Aposnius
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Aposnius : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aposnius");
            //Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
			npc.knockBackResist = 0f;
            npc.width = 130;
            npc.height = 130;
            npc.damage = 40;
            npc.defense = 6;
             npc.lifeMax = Main.expertMode ? 1250 : 1500;   
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 60f;
       
            npc.aiStyle = 3;
            aiType = NPCID.Zombie;
            npc.boss = true;
            npc.noGravity = false;
            //animationType = NPCID.Zombie;
        }
Rectangle getFrame(int Index)
		{
			Index--;
			Rectangle rect = new Rectangle(0, 93 * Index, 155, 93);
			if (++Index > 13)
				rect.Y += 2;
			else
				rect.Y += 1;
			return rect;
		}
        public int lol;
        public int Timer2;
        public int Timer3;
		int FrameYOffset;
        public int Timer4;
		public int Timer5;
		public int Dir;
		public float speed;
		public Vector2 move;
		public Vector2 moveTo;
		public float magnitude;
		public float turnResistance;
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
		public int Timer;
        public override void AI()
        {
			//if (npc.velocity.X <0.1)
				
			Timer++;
				if (Timer ==60){
				 int radius = 500;     //this is the explosion radius, the highter is the value the bigger is the explosion
 
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                  int Num5 = (int)((x+npc.position.X + npc.width / 2 + (npc.width / 2 + 6) * npc.direction) / 16f);
					int Num6 = (int)((y+npc.position.Y + npc.height - 15f) / 16f);
 
                    if (Main.tile[Num5, Num6].active() && Main.tile[Num5, Num6].type == 19 || Main.tile[Num5, Num6].active() && Main.tile[Num5, Num6].type == 215 || Main.tile[Num5, Num6].active() && Main.tile[Num5, Num6].type == 42)   //this make so the explosion radius is a circle
                    {
                        WorldGen.KillTile(Num5, Num6, false, false, false);  //this make the explosion destroy tiles  
                      //  Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                    }
                }
            }
			Timer = 0;
				}
//if (Main.tile[(npc.position.X + (16 * npc.spriteDirection)) / 16, (npc.position.Y) / 16].active())

        



            //LookInDirection(npc.velocity);

            npc.TargetClosest(false);

            Player player = Main.player[npc.target];
             moveTo = player.Center; //This player is the same that was retrieved in the targeting section.

            //Vector2 moveTo = player.Center + new Vector2(0f, -200f); //This is 200 pixels above the center of the player.

            npc.TargetClosest(true);
            speed = 8f;
             move = moveTo - npc.Center;
             magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
             turnResistance = 10f; //the larger this is, the slower the npc will turn
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity.X = move.X;
       
        
			progression++;
			
			if (progression >= 180 && progression < 240)
				Crash();
			
			if (progression >= 360 && progression < 720)
				Dash();
			
			if (progression >= 540 && progression < 720)
				Smash();
			if (progression >= 720 )
				Throw();
			
			if (progression == 900)
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
			
			
			Timer2++;
			if (Timer2 ==1)
				npc.velocity.Y = -20;
				
				if (Timer2>1)
					npc.velocity.Y *= 0.99f;
			if (Timer2 >= 40){
				npc.velocity.Y += 26;
				npc.velocity.X = move.X*2;
				
				if (Timer2 == 60)
				Timer2 = 0;
			}

		}
		void Dash(){
			
			lol++;
			if (lol <=60 && lol != 0){
			 npc.velocity *= 2f;
			 
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
			if (lol == 360)
				lol = 0;
		}
		void Smash(){
			if (npc.velocity.X>1){
					Dir=500;
					for (int x = (int)npc.position.X; x < (npc.position.X + npc.width+Dir); x++)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1,  mod.DustType("Sandy"));
				}
				if (npc.velocity.X<1){
					Dir= -500;
					for (int x = (int)npc.position.X; x > (npc.position.X + npc.width+Dir); x--)
								Dust.NewDust(new Vector2(x, npc.position.Y + npc.height), 1, 1,  mod.DustType("Sandy"));
				}
				 Main.PlaySound(SoundID.Item14, npc.position);
		}
		void Throw(){
            Player player = Main.player[npc.target];
			Timer3++;
			npc.velocity.X=0;
			npc.velocity.Y=0;
			 if (Timer3 >= 30)  // 230 is projectile fire rate
            {

                float Speed = 18f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 25;  //projectile damage
                int type = mod.ProjectileType("Rockz");  //put your projectile
                float DisP = Main.rand.NextFloat(-3f, 3f);
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + DisP + (player.height * 0.5f)), vector8.X - (player.position.X + DisP + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);

                Timer3 = 0;
            }
		}
		public override void NPCLoot()
		{

			if (Main.expertMode)
			{
				//npc.DropBossBags();
			}

			if (Main.netMode != 1)
			{
				int centerX = (int)(npc.position.X + npc.width / 2) / 16;
				int centerY = (int)(npc.position.Y + npc.height / 2) / 16;
				int halfLength = npc.width / 2 / 16 + 1;

				if (!Main.expertMode && Main.rand.NextBool(4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Primape_Saber"));
				}
				if (!Main.expertMode && Main.rand.NextBool(4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Clone"));
				}
				if (!Main.expertMode && Main.rand.NextBool(4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningStaff"));
				}
				if (!Main.expertMode && Main.rand.NextBool(4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QuetzalcoatlStave"));
				}
				if (!Main.expertMode && Main.rand.NextBool(4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TreasureGlaive"));
				}
				if (!Main.expertMode && Main.rand.Next(25) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FallenSnake"));
				}
				if (!Main.expertMode && Main.rand.NextBool())
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Aquamarine"), Main.rand.Next(10, 18));
				}
				if (!Main.expertMode && Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrangeEgg"));
				}
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrutalliskTrophy"));
				}
				if (!Main.expertMode && Main.rand.NextBool(7))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrutalliskMask"));
				}
				//TremorWorld.Boss.Brutallisk.Downed();
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

