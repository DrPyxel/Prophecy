using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Prophecy.Items.Weapons.Alchemist
{

    public class HydrogenFlask : AlchemistItem
    {
        private static int useWait = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask of Hydrogen");
            Tooltip.SetDefault("Who needs magic when you have nature?\n[c/721096:-=Alchemist Class=-]");
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.crit = 4;
            item.knockBack = 3;
            item.width = 24;
            item.height = 30;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5; // Bow Use Style
            item.noMelee = false; // Doesn't deal damage if an enemy touches at melee range.
            item.value = Item.buyPrice(0, 0, 0, 0); // Another way to handle value of item.
            item.rare = 1;
            item.autoReuse = false;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("HydrogenFlask");
            item.shootSpeed = 5f;
            item.useTurn = false;
            item.noUseGraphic = true;
        }

        public override void HoldItem(Player player)
        {
            useWait++;
            if (useWait < 90)
            {
                item.useTime = 12;
                item.useAnimation = 12;
                item.autoReuse = false;
                item.damage = 5;
                item.shootSpeed = 5f;
            }
            else
            {
                item.useTime = 6;
                item.useAnimation = 6;
                item.autoReuse = false;
                item.damage = 8;
                item.shootSpeed = 10f;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (useWait < 120)
            {

            }
            else if (useWait >= 120)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(1);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 1f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
                useWait = 0;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.Bottle);
            r.AddTile(TileID.SkyMill);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
