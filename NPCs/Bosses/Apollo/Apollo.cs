
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Apollo
{
    
	public class Apollo : ModNPC
	{
		
		

        private int aiPhase;
        private bool transition;
		public int Timer3;
		public int Timer2;
		public int cooldown;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Apollo");
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
            //npc.music = (this.music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Apollo"));
        }
		 public int Timer;
         public override void AI() //this is where you program your AI
        {
			npc.TargetClosest(true);
		npc.velocity.X = -7;
		npc.velocity.Y=0;
		cooldown++;
		
		if (cooldown>60 &&cooldown<120){
		Shoot();
		}
		if (cooldown>120 && cooldown <410){
		LaserAimShoot();
		}
		if (cooldown == 410)
			cooldown = 0;
		}
		
private void LookToPlayer()
		{
			Player player = Main.player[npc.target];
			Vector2 look = Main.player[npc.target].Center - npc.Center;
			//LookInDirection(look);
		}
		
		void Shoot (){
			Timer3++;
			if (Timer3 >= 10)  // 230 is projectile fire rate
            {
					Player player = Main.player[npc.target];
                float Speed = 18f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 25;  //projectile damage
                int type = mod.ProjectileType("GhostlyArrow");  //put your projectile
                float DisP = Main.rand.NextFloat(-3f, 3f);
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + DisP + (player.height * 0.5f)), vector8.X - (player.position.X + DisP + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);

                Timer3 = 0;
            }		
		}
		void LaserAimShoot(){
			Player player = Main.player[npc.target];
			Timer2++;
			 if (Timer2 >= 1)
            {
                int indexer = 1;
                float Speed = 25f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Arrow");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rot = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));


                if (Timer2 == 61)
                {

                    indexer = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rot) * Speed) * -1), (float)((Math.Sin(rot) * Speed) * -1), type, damage, 0f, 0);

                }

                if (Timer2 >= 281)
                {
                    int laserindex = 0;
                    laserindex = npc.whoAmI;

                    Speed = 10f;  //projectile speed
                    vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    damage = 10;  //projectile damage
                    type = mod.ProjectileType("ArrowProj1");  //put your projectile
                    Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                    for (indexer = 0; indexer < 200; indexer++)
                    {
                        if (Main.projectile[indexer].type == mod.ProjectileType("Arrow"))
                        {
                            for (laserindex = 0; laserindex < 200; laserindex++)
                            {
                                if (Main.npc[laserindex].type == mod.NPCType("Apollo"))
                                {
                                    float rotation = (float)Math.Atan2(vector8.Y - (Main.projectile[indexer].position.Y + (player.height * 0.5f)), vector8.X - (Main.projectile[indexer].position.X + (player.width * 0.5f)));

                                    int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * 1) * -1), (float)((Math.Sin(rotation) * 1) * -1), type, damage, 0f, Main.myPlayer, npc.whoAmI);
                                }
                                Timer2 = 0;

                            }
                        }
                    }
                }

            }
			
			
			
			
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



