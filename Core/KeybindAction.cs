using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using ZeroXHUD.Core.Enums;

namespace ZeroXHUD.Core
{
    public class KeybindAction
    {
        public ModKeybind Keybind { get; set; }
        public Action Action { get; set; }

        public void InvokeAction(KeybindPressedState pressedState = KeybindPressedState.JustPressed)
        {
            try
            {
                switch (pressedState)
                {
                    case KeybindPressedState.JustPressed:
                        if (Keybind.JustPressed) Action.Invoke();
                        break;
                    case KeybindPressedState.Pressed:
                        if (Keybind.Current) Action.Invoke();
                        break;
                    case KeybindPressedState.WasPressed:
                        if (Keybind.Old) Action.Invoke();
                        break;
                    case KeybindPressedState.JustReleased:
                        if (Keybind.JustReleased) Action.Invoke();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                // TODO: Add errors logging
            }
        }
    }
}
