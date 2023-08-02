using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Chat;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ZeroXHUD.Core
{
    public class ZeroXPlayer : ModPlayer
    {
        public ZeroXPlayer()
        {
            ZeroXHUD.InitializeModPlayer(this);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            var keybinds = ZeroXHUD.ModSystemInstance.Keybinds;

            foreach (var keybind in keybinds)
            {
                keybind.Value.InvokeAction();
            }
        }
    }
}
