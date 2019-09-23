using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Dionysus
{
    public class flailProjectile : ModProjectile
    {


        public override void SetDefaults()
        {
			//projectile.alpha = 255;
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = false;
			projectile.hostile = true;
            projectile.penetrate = -1; // Penetrates NPCs infinitely.
            projectile.melee = true; // Deals melee dmg.
            projectile.timeLeft = 90;
            projectile.aiStyle = 15; // Set the aiStyle to that of a flail.
        }
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			//Player player = Main.player[npc.target];
	//player.TargetClosest(true);
	Vector2 moveTo = OwnerNpc.Center; 
			float speed = 300f;
		Vector2 move = moveTo - player.Center;
		float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
		if(magnitude > speed)
		{
			move *= speed / magnitude;
		}
		float turnResistance = 10f; //the larger this is, the slower the npc will turn
		move = (player.velocity * turnResistance + move) / (turnResistance + 1f);
		magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
		if(magnitude > speed)
		{
			move *= speed / magnitude;
		}
            
                player.velocity = move;
        }
 private NPC OwnerNpc
        {
            get { return Main.npc[(int)projectile.ai[0]]; }
        }
        // Now this is where the chain magic happens. You don't have to try to figure this whole thing out.
        // Just make sure that you edit the first line (which starts with 'Texture2D texture') correctly.
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = ModContent.GetTexture("Prophecy/NPCs/Bosses/Dionysus/flail_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = OwnerNpc.Center;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
