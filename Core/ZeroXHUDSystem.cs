using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using ZeroXHUD.Core.Config;
using ZeroXHUD.UI;

namespace ZeroXHUD.Core
{
    public class ZeroXHUDSystem : ModSystem
    {
        private UserInterface? userInterface;
        private ZeroXUI? UI;

        public Dictionary<string, KeybindAction> Keybinds { get; set; } = new();

        public ZeroXHUDSystem()
        {
            ZeroXHUD.InitializeModSystem(this);
        }

        internal void ShowMyUI()
        {
            userInterface?.SetState(UI);
        }

        internal void HideMyUI()
        {
            userInterface?.SetState(null);
        }

        private bool globalHudVisibility = false;
        private void OnToggleHudPressed()
        {
            globalHudVisibility = !globalHudVisibility;
        }

        public override void OnModLoad()
        {
            Mod.Logger.Debug($"HUDSystem OnModLoad fired with Mod: {Mod.DisplayName}");

            var bind = KeybindLoader.RegisterKeybind(Mod, "Toggle HUD", Microsoft.Xna.Framework.Input.Keys.OemTilde);

            Keybinds.Add("toggle_hud", new() {
                Keybind = bind, 
                Action = OnToggleHudPressed 
            });

            IL_Main.UpdateMinimapAnchors += Hook_IL_Main_UpdateMinimapAnchors;

            if (!Main.dedServ)
            {
                userInterface = new UserInterface();
                UI = new ZeroXUI();

                UI.Activate();
            }
        }

        private static int __minimapX;
        private static int __minimapY;
        private void Hook_IL_Main_UpdateMinimapAnchors(MonoMod.Cil.ILContext il)
        {
            var c = new ILCursor(il);
            c.GotoNext(i => i.MatchRet());

            //c.Index--; // ?

            var anchorLeft = typeof(Main).GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == "_minimapTopRightAnchorOffsetTowardsLeft");
            var anchorBottom = typeof(Main).GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == "_minimapTopRightAnchorOffsetTowardsBottom");

            var mxf = typeof(ZeroXHUDSystem).GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == "__minimapX");
            var myf = typeof(ZeroXHUDSystem).GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == "__minimapY");

            if (anchorLeft == null || anchorBottom == null) return;

            c.EmitDelegate<Action>(() =>
            {
                anchorLeft.SetValue(null, __minimapX);
                anchorBottom.SetValue(null, __minimapY);
            });
        }

        public override void OnWorldLoad()
        {
            if(ZeroXModConfig.Instance.ShowCombatPanelByDefault)
            {
                globalHudVisibility = true;
            }
            else
            {
                globalHudVisibility = false;
            }
        }

        private GameTime lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            if (userInterface == null) return;

            if(userInterface.CurrentState != null)
            {
                userInterface.Update(gameTime);
            }

            lastUpdateUiGameTime = gameTime;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ZeroxHud: Interface",
                    delegate
                    {
                        if (lastUpdateUiGameTime != null && userInterface?.CurrentState != null)
                        {
                            userInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
                        }
                        return true;
                    }, InterfaceScaleType.UI));

                if(!ZeroXModConfig.Instance.ShowGameStatusPanel)
                    layers.RemoveAll(x => x.Name == "Vanilla: Resource Bars");
            }

            
        }

        public override void PreUpdatePlayers()
        {
            Main.miniMapX = 0;
            Main.miniMapY = 0;

            if (ZeroXModConfig.Instance.ShowGameStatusPanel)
            {
                __minimapX = 52 + (int)(240.0 * Main.MapScale);
                __minimapY = 90;
            }
            else
            {
                __minimapX = 32 + (int)(240.0 * Main.MapScale);
                __minimapY = 32;
            }

            //Main.UpdateMinimapAnchors();
        }

        public override void PostUpdatePlayers()
        {
            try
            {
                if (!Main.playerInventory && globalHudVisibility)
                {
                    ShowMyUI();
                }
                else
                {
                    HideMyUI();
                }

                if (userInterface?.CurrentState != null)
                {
                    UI?.Refresh();
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{ex}"), Color.White);
            }

            base.PostUpdatePlayers();
        }
    }
}
