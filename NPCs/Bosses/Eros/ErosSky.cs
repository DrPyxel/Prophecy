using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Prophecy;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Prophecy.NPCs.Bosses.Eros
{
	public class ErosSky : CustomSky
	{
		private bool isActive;

		private float intensity;

		private int ErosIndex = -1;

		public override void Update(GameTime gameTime)
		{
			if (this.isActive && this.intensity < 1f)
			{
				this.intensity += 0.01f;
				return;
			}
			if (!this.isActive && this.intensity > 0f)
			{
				this.intensity -= 0.01f;
			}
		}

		private float GetIntensity()
		{
			if (this.UpdateErosIndex())
			{
				float x = 0f;
				if (this.ErosIndex != -1)
				{
					x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[this.ErosIndex].Center);
				}
				return 1f - Utils.SmoothStep(3000f, 6000f, x);
			}
			return 0f;
		}

		public override Color OnTileColor(Color inColor)
		{
			float num = this.GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(1.0f, 0.0f, 0.0f, 1f), inColor.ToVector4(), 1f - num));
		}

		private bool UpdateErosIndex()
		{
			int num = ModLoader.GetMod("Prophecy").NPCType("Eros");
			if (this.ErosIndex >= 0 && Main.npc[this.ErosIndex].active && Main.npc[this.ErosIndex].type == num)
			{
				return true;
			}
			this.ErosIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == num)
				{
					this.ErosIndex = i;
					break;
				}
			}
			return this.ErosIndex != -1;
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0f && minDepth < 0f)
			{
				float scale = this.GetIntensity();
				spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(139, 0, 0) * scale);
			}
		}

		public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this.isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			this.isActive = false;
		}

		public override void Reset()
		{
			this.isActive = false;
		}

		public override bool IsActive()
		{
			return this.isActive || this.intensity > 0f;
		}
	}
}
