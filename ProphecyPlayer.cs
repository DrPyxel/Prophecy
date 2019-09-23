using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy
{
    public class ProphecyPlayer : ModPlayer
    {
        private const int saveVersion = 0;
	    public bool radiationResist = false;
	    public bool alchemistFire = false;
	    public bool alchemistElectric = false;
	    public bool alchemistNuclear = false;

        public override void ResetEffects()
        {
            //Minions
            
            //Pets

            //Acessories, Weapons, Armors, etc
            radiationResist = false;
	        alchemistFire = false;
	        alchemistElectric = false;
	        alchemistNuclear = false;
        }
		public override void UpdateBiomeVisuals()
		{
			
			player.ManageSpecialBiomeVisuals("Prophecy:Eros", NPC.AnyNPCs(mod.NPCType("Eros")));
	
		}
    }
}