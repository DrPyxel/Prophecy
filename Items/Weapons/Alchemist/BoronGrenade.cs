using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace Prophecy.Items.Weapons.Alchemist
{
    public class BoronGrenade : ModItem
    {
        private static int useWait = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Powder Grenade");
            Tooltip.SetDefault("Can be imbued with fire\nUsing this weapon while it is not fully charged will not reset charge\n[c/721096:-=Alchemist Class=-]");
        }
        
        public override void SetDefaults()
        {
            item.damage = 30;
            item.useStyle = 1;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("BoronGrenade");
            item.width = 30;
            item.height = 38;
            item.maxStack = 1;
            item.consumable = false;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 0, 0, 0);
            item.rare = 2;
            item.autoReuse = true;
        }
        public override void HoldItem(Player player)
        {
            useWait++;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (useWait < 240)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BoronGrenade"), damage, knockBack, player.whoAmI, 0f, 0f);
            }
            else if (useWait >= 240)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BoronGrenadeL"), damage, knockBack, player.whoAmI, 0f, 0f);
                useWait = 0;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}