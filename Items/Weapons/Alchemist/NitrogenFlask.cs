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

    public class NitrogenFlask : AlchemistItem
    {
        private static int useWait = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask of Nitrogen");
            Tooltip.SetDefault("Imbues an area with Nitrogen\n[c/721096:-=Alchemist Class=-]");
        }

        public override void SetDefaults()
        {
            item.damage = 7;
            item.crit = 2;
            item.knockBack = 0;
            item.width = 18;
            item.height = 36;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5; // Bow Use Style
            item.noMelee = false; // Doesn't deal damage if an enemy touches at melee range.
            item.value = Item.buyPrice(0, 0, 0, 0); // Another way to handle value of item.
            item.rare = 2;
            item.autoReuse = false;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("NitrogenFlask");
            item.shootSpeed = 10f;
            item.useTurn = false;
            item.noUseGraphic = true;
        }

        public override void HoldItem(Player player)
        {
            useWait++;
            if (useWait < 120)
            {
                item.useTime = 12;
                item.useAnimation = 12;
                item.autoReuse = false;
                item.damage = 7;
                item.crit = 2;
                item.shootSpeed = 10f;
            }
            else
            {
                item.useTime = 12;
                item.useAnimation = 12;
                item.autoReuse = false;
                item.damage = 12;
                item.crit = 0;
                item.shootSpeed = 7f;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (useWait < 120)
            {

            }
            else if (useWait >= 120)
            {
                float numberProjectiles = 4;
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
            r.AddIngredient(ItemID.DirtBlock, 10);
            r.AddIngredient(ItemID.Worm);
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
