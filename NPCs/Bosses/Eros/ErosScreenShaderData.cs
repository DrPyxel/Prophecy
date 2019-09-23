using System;
using Prophecy;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Eros
{
	public class ErosScreenShaderData : ScreenShaderData
	{
		private int ErosIndex;

		public ErosScreenShaderData(string passName) : base(passName)
		{
		}

		private void UpdateErosIndex()
		{
			int num = ModLoader.GetMod("Prophecy").NPCType("Eros");
			if (this.ErosIndex >= 0 && Main.npc[this.ErosIndex].active && Main.npc[this.ErosIndex].type == num)
			{
				return;
			}
			this.ErosIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == num)
				{
					this.ErosIndex = i;
					return;
				}
			}
		}

		public override void Apply()
		{
			this.UpdateErosIndex();
			if (this.ErosIndex != -1)
			{
				base.UseTargetPosition(Main.npc[this.ErosIndex].Center);
			}
			base.Apply();
		}
	}
}
