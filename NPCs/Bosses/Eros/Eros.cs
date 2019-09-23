using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Prophecy.NPCs.Bosses.Eros;
namespace Prophecy.NPCs.Bosses.Eros
{
    
	public class Eros : ModNPC
	{
		
		

        private int aiPhase;
        private bool transition;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eros");
		}   
        public override void SetDefaults()
        {
			
            npc.alpha = 255;
            npc.aiStyle = -1;
            npc.lifeMax = Main.expertMode ? 4000 : 4000;    //this is the npc health
            npc.damage = Main.expertMode ? 15 : 25 ;  //this is the npc damage
            npc.defense = 3000;         //this is the npc defense
            npc.knockBackResist = 0f;
            npc.width = 130; //this is where you put the npc sprite width.     important
            npc.height = 130; //this is where you put the npc sprite height.   important
            npc.boss = true;
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
            //npc.music = (this.music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Eros"));
        }
		private const int sphereRadius = 300;
		 public int Timer;
		 public int Timer2;
         public override void AI() //this is where you program your AI
        {
			if (npc.life>npc.lifeMax)
				npc.life = npc.lifeMax;
			npc.alpha--;
			FindFrame(38);
			if (npc.life > 500){ 
			//LookInDirection(npc.velocity);
			}
								npc.TargetClosest(false);

			Player player = Main.player[npc.target];
			Vector2 moveTo = player.Center; //This player is the same that was retrieved in the targeting section.
		
			//Vector2 moveTo = player.Center + new Vector2(0f, -200f); //This is 200 pixels above the center of the player.
			
			npc.TargetClosest(true);
			float speed = 0f;
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
		Timer++;
		Timer2++;
          if (npc.ai[2] >=600){
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Succubus"));
			npc.ai[2] = 0;  
			}
			 if (npc.ai[1] >=1200){
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("LavaTitan"));
			npc.ai[1] = 0;  
			}
			if (Timer >=400){
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SkeletonKnight"));
			Timer = 0; 
			}
			if (Timer2 >=1000){
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Healer"));
			Timer2 = 0; 
			}
		
		}
		
public int Timer3;
private void LookToPlayer()
		{
			Player player = Main.player[npc.target];
			Vector2 look = Main.player[npc.target].Center - npc.Center;
			//LookInDirection(look);
		}
		/*public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) {
			spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Eros/HolySphere"), npc.Center - Main.screenPosition, null, Color.White * (70f / 255f), 0f, new Vector2(sphereRadius, sphereRadius), 1f, SpriteEffects.None, 0f);
			return true;
		}*/
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
		
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			
			float num88 = ErosHandler.ShieldStrength / (float)NPC.ShieldStrengthTowerMax;
			if (ErosHandler.ShieldStrength > 0)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

				var center = npc.Center - Main.screenPosition;
				float num89 = 0f;
				if (npc.ai[3] > 0f && npc.ai[3] <= 30f)
				{
					num89 = 1f - npc.ai[3] / 30f;
				}
				Filters.Scene["Prophecy:Eros"].GetShader().UseIntensity(1f + num89).UseProgress(0f);
				DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center - new Vector2(0, 10), new Rectangle(0, 0, 600, 600), Color.White * (num88 * 0.8f + 0.2f), npc.rotation, new Vector2(300f, 300f), npc.scale * (1f + num89 * 0.05f), SpriteEffects.None, 0);
				GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + num89 * 0.5f));
				GameShaders.Misc["ForceField"].Apply(drawData);
				drawData.Draw(Main.spriteBatch);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin();
				return;
			}
		}
		
   
}
}

