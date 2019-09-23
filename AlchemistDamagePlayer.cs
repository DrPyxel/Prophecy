using Terraria;
using Terraria.ModLoader;

namespace Prophecy
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers and additions to knockback and crit.
    public class AlchemistDamagePlayer : ModPlayer
    {
        public static AlchemistDamagePlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<AlchemistDamagePlayer>();
        }

        // Vanilla only really has damage multipliers in code
        // And crit and knockback is usually just added to
        // As a modder, you could make separate variables for multipliers and simple addition bonuses
        public float alchemistDamageAdd;
        public float alchemistDamageMult = 1f;
        public float alchemistKnockback;
        public int alchemistCrit;

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            alchemistDamageAdd = 0f;
            alchemistDamageMult = 1f;
            alchemistKnockback = 0f;
            alchemistCrit = 0;
        }
    }
}