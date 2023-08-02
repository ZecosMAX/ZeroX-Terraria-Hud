using Microsoft.Xna.Framework;
using SteelSeries.GameSense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using ZeroXHUD.Core.Config.DataTypes;

namespace ZeroXHUD.Core.Config
{
    public class ZeroXModConfig : ModConfig
    {
        public static ZeroXModConfig Instance;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Show combat panel by default")]
        [Tooltip("If tunred on, hud will be visible as soon as you start the game")]
        [DefaultValue(true)]
        public bool ShowCombatPanelByDefault { get; set; } = true;

        [Label("Combat panel")]
        [Tooltip("Combat panel settings")]
        public CombatPanelConfig CombatPanel { get; set; } = new CombatPanelConfig();

        [Label("Show game status panel")]
        [Tooltip("If tunred off, game's native hud for health and mana will NOT be visible")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool ShowGameStatusPanel { get; set; } = true;

        public ZeroXModConfig()
        {
            Instance ??= this;
        }
	}
}
