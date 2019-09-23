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
    public class NeonBlade : ModItem
    {
        private static int useWait = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neon Blade");
            Tooltip.SetDefault("Charging up the weapon empowers the next attack\n[c/721096:-=Alchemist Class=-]");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.knockBack = 3;
            item.value = 10000;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
            item.shootSpeed = 10f;
        }
        public override void HoldItem(Player player)
        {
            useWait++;
            if (useWait < 120)
            {
                item.damage = 50;
            }
            else if (useWait >= 120)
            {
                item.damage = 75;
            }
        }
        public override bool UseItem(Player player)
        {
            useWait = 0;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.PalladiumBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipeA = new ModRecipe(mod);
            recipeA.AddIngredient(ItemID.FallenStar);
            recipeA.AddIngredient(ItemID.SoulofLight, 5);
            recipeA.AddIngredient(ItemID.CobaltBar, 5);
            recipeA.AddTile(TileID.Anvils);
            recipeA.SetResult(this);
            recipeA.AddRecipe();
        }
    }
}
