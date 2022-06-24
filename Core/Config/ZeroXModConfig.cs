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

        public ZeroXModConfig()
        {
            if(Instance == null)
                Instance = this;

            try
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"Config constructor called!"), Color.White);
            }
            catch
            {

            }
        }

        //override 
	}
}
