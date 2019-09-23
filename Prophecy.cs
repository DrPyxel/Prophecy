using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System;
using Prophecy.NPCs.Bosses.Eros;
namespace Prophecy
{
	public class Prophecy : Mod
	{
        FieldInfo WorldGen_lastMaxTilesX;
        FieldInfo WorldGen_lastMaxTilesY;

        public override void Load()
        {
			Filters.Scene["Prophecy:Eros"] = new Filter(new ErosScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.5f, 0.2f).UseOpacity(0.6f), EffectPriority.VeryHigh);
			SkyManager.Instance["Prophecy:Eros"] = new ErosSky();
            On.Terraria.WorldGen.clearWorld += WorldGen_clearWorld;
            WorldGen_lastMaxTilesX = typeof(WorldGen).GetField("lastMaxTilesX", BindingFlags.Static | BindingFlags.NonPublic);
            WorldGen_lastMaxTilesY = typeof(WorldGen).GetField("lastMaxTilesY", BindingFlags.Static | BindingFlags.NonPublic);
            
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gemstones", new int[]
            {
                ItemID.Amber,
                ItemID.Amethyst,
                ItemID.Diamond,
                ItemID.Emerald,
                ItemID.Ruby,
                ItemID.Sapphire,
                ItemID.Topaz
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("Prophecy:Gemstones", group0);
        }

    }
}
