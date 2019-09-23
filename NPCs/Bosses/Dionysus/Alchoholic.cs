
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Dionysus
{
    
	public class Alchoholic : ModNPC
	{
		
		

        private int aiPhase;
        private bool transition;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alchoholic");
		}   
        public override void SetDefaults()
        {
           
            npc.aiStyle = -1;
            npc.lifeMax = Main.expertMode ? 1555 : 1040;    //this is the npc health
            npc.damage = Main.expertMode ? 15 : 25 ;  //this is the npc damage
            npc.defense = 6;         //this is the npc defense
            npc.knockBackResist = 0f;
            npc.width = 130; //this is where you put the npc sprite width.     important
            npc.height = 130; //this is where you put the npc sprite height.   important
            npc.boss = true;
            npc.lavaImmune = true;       //this make the npc immune to lava
            npc.noGravity = true;           //this make the npc float
            npc.noTileCollide = true;        //this make the npc go thru walls
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.behindTiles = false;          
			
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            aiPhase = 1;
            //npc.music = (this.music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Dionysus"));
        }
		 public int Timer;
         public override void AI() //this is where you program your AI
        {
			if (npc.life>npc.lifeMax)
				npc.life = npc.lifeMax;
			FindFrame(38);
			if (npc.life > 500){ 
			//LookInDirection(npc.velocity);
			}
								npc.TargetClosest(false);

			Player player = Main.player[npc.target];
			Vector2 moveTo = player.Center; //This player is the same that was retrieved in the targeting section.
		
			//Vector2 moveTo = player.Center + new Vector2(0f, -200f); //This is 200 pixels above the center of the player.
			
			npc.TargetClosest(true);
			float speed = 2.5f;
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
                float Speed = 15f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 3;  //projectile damage
                int type = mod.ProjectileType("Vine");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("flailProjectile") ,  damage, 0f, Main.myPlayer, npc.whoAmI);
                npc.ai[1] = 0;
            }
            if (npc.ai[1] == 200)  // 230 is projectile fire rate
            {
                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 3;  //projectile damage
                int type = mod.ProjectileType("Vine");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
               // int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("Whip"), damage, 0f, Main.myPlayer, npc.whoAmI);
              
            }
            if (npc.ai[2] >= 750) 
		{
			float Speed = 10f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Needle");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
               
				int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                npc.ai[2] = 0;
        }
		if (npc.ai[2] == 450 || npc.ai[2] == 500) 
		{
			npc.velocity.X=0;
			npc.velocity.Y=0;
			float disX = Main.rand.NextFloat(-200f, 200);
		npc.position = player.Center + new Vector2(disX, -400f);
		for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType("Sparkle"), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
			}
			Main.PlaySound(SoundID.Item25, npc.position);
                
        }
		if (npc.ai[2] == 475 || npc.ai[2] == 525) 
		{

			float Speed = 25f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Bottle");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f,Main.myPlayer, npc.whoAmI);
		}
		if (npc.life <= 500){ 
                npc.ai[3]++;
							
		}
		if (npc.ai[3] >= 10) 
		{
        
			float Speed = 15f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("CurrentLaser");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = Main.rand.NextFloat(0f, 360f);
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                npc.ai[3] = 1;
        }
		if (npc.ai[3] >=1)
		{
			npc.velocity.X=0;
			npc.velocity.Y=0;
			npc.rotation += 0.3f;
		}
		Timer++;
		
		if (Timer > 500){
		//Player player = Main.player[npc.target];
	
			Timer3++;
			npc.velocity.X=0;
			npc.velocity.Y=0;
			 if (Timer3 >= 10)  // 230 is projectile fire rate
            {

                float Speed = 5f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Grape");  //put your projectile
                float DisP = Main.rand.NextFloat(-100f, 100f);
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + DisP + (player.height * 0.5f)), vector8.X - (player.position.X + DisP + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);

                Timer3 = 0;
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

