using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.Projectiles       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class NitrogenFlask : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 12;  //Set the hitbox width
            projectile.height = 12; //Set the hitbox height
            projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = false;  //Tells the game whether or not projectile will be affected by water
            projectile.ranged = true;  //Tells the game whether it is a ranged projectile or not
            projectile.penetrate = 1; //Tells the game how many enemies it can hit before being destroyed, -1 infinity
            projectile.timeLeft = 75;  //The amount of time the projectile is alive for  
            //projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            if (projectile.timeLeft > 75)
            {
                projectile.timeLeft = 75;
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
                if (Main.rand.Next(2) == 0)     //this defines how many dust to spawn
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Silver, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 200, new Color(153, 0, 225), 1.25f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            return;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item107, (int)projectile.position.X, (int)projectile.position.Y);
            for (int d = 0; d < 15; d++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.PlatinumCoin, 0f, 0f, 125, default(Color), 2f);
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Platinum, 0f, 0f, 125, default(Color), 2f);
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0f, 0f, mod.ProjectileType("NitrogenCloud"), projectile.damage, 0f, projectile.owner);
            }

        }
    }
}
