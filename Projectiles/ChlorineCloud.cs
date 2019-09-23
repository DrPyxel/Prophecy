using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.Projectiles       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class ChlorineCloud : ModProjectile
    {
        public int[] fireProjectiles = new int[] { 1, 15, 19, 34, 41, 82, 85, 101, 167, 168, 169, 170, 258, 321, 325, 326, 327, 328, 329, 376, 400, 401, 402, 415, 416, 417, 418, 419, 420, 421, 422, 467, 468, 504, 553 };
        public override void SetDefaults()
        {
            projectile.width = 80;  //Set the hitbox width
            projectile.height = 80; //Set the hitbox height
            projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = false;  //Tells the game whether or not projectile will be affected by water
            projectile.ranged = true;  //Tells the game whether it is a ranged projectile or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed, -1 infinity
            projectile.timeLeft = 200;  //The amount of time the projectile is alive for  
            projectile.tileCollide = false;
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
                if (Main.rand.Next(5) == 0)
                {
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Granite, 0f, 0f, 220, default(Color), 1f);
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.t_SteampunkMetal, 0f, 0f, 220, default(Color), 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            for (int i = 0; i < projectile.timeLeft; i++) //obv for loop
            {

                if (projectile.Distance(Main.projectile[i].Center) < 100 && Main.projectile[i].active) // gets projectiles near its center
                {
                    if (Main.projectile[i].type == mod.ProjectileType("AmmoniaCloud"))
                    {
                        Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 0f, 0f, mod.ProjectileType("MustardGas"), 35, 0f, projectile.owner); // create explosion
                        Main.projectile[i].Kill();
                        projectile.Kill();
                        return;
                    }
                }
            }
            return; // otherwise, return
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item21, (int)projectile.position.X, (int)projectile.position.Y);

            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 211, 0f, 0f, 125, default(Color), 0.5f);
            }
        }
    }
}
