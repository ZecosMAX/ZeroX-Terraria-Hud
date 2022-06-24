using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ZeroXHUD.Utils
{
    public static class Extensions
    {
        public static float GetActualDamageModifier<T>(this Player player) where T : DamageClass
        {
            var damageModifier = player.GetTotalDamage<T>();

            return damageModifier.Additive * damageModifier.Multiplicative;
        }
    }
}
