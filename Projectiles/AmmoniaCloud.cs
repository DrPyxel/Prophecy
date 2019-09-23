using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.Projectiles       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class AmmoniaCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 80;  //Set the hitbox width
            projectile.height = 80; //Set the hitbox height
            projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = false;  //Tells the game whether or not projectile will be affected by water
            projectile.ranged = true;  //Tells the game whether it is a ranged projectile or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed, -1 infinity
            projectile.timeLeft = 300;  //The amount of time the projectile is alive for  
            projectile.tileCollide = false;
            //projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            if (projectile.timeLeft > 240)
            {
                projectile.timeLeft = 240;
            }
            if (projectile.timeLeft <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.timeLeft--;
            }
            if (projectile.ai[0] > 12f)  //this defines where the flames starts
            {
                if (Main.rand.Next(3) == 0)
                {
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.GreenBlood, 0f, 0f, 220, default(Color), 1f);
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Copper, 0f, 0f, 220, default(Color), 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            
            return; // otherwise, return
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item8, (int)projectile.position.X, (int)projectile.position.Y);

            for (int d = 0; d < 30; d++)
            {

                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Lead, 0f, 0f, 125, default(Color), 0.5f);
            }
        }
    }
}
