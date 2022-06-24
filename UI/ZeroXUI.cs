using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace ZeroXHUD.UI
{
    public class ZeroXUI : UIState
    {
        List<PlayerPanel> playerPanels = new List<PlayerPanel>();

        public void Refresh()
        {
            try
            {
                var player = Main.LocalPlayer;
                List<Player> sameTeamPlayers = null;
                lock (Main.player)
                {
                    try
                    {
                        sameTeamPlayers = Main.player.Where(p => p.team == player.team && p.active).ToList();
                    }
                    catch
                    {

                    }
                }

                if (sameTeamPlayers == null) return;

                if (playerPanels.Count != sameTeamPlayers.Count && sameTeamPlayers.Count > 0)
                {
                    foreach (UIElement element in Elements)
                    {
                        element.Deactivate();
                        element.Remove();
                    }
                    Elements.Clear();

                    playerPanels = new List<PlayerPanel>();
                    for (int i = 0; i < sameTeamPlayers.Count; i++)
                    {
                        playerPanels.Add(new PlayerPanel(sameTeamPlayers[i]));
                    }

                    InitializePanels();
                }

                for (int i = 0; i < sameTeamPlayers.Count; i++)
                {
                    Player sameTeamPlayer = sameTeamPlayers[i];
                    PlayerPanel playerPanel = playerPanels[i];

                    playerPanel.UpdateValues(sameTeamPlayer);
                }

                int level = (player.CountBuffs() + 10) / 11;
                for (int i = 0; i < playerPanels.Count; i++)
                {
                    PlayerPanel playerPanel = playerPanels[i];
                    playerPanel.Top.Set(86 + 50 * level + 72 * i, 0);
                }
            } 
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{ex}"), Color.White);
                }
            }
        }

        private void InitializePanels()
        {
            for (int i = 0; i < playerPanels.Count; i++)
            {
                PlayerPanel playerPanel = playerPanels[i];
                playerPanel.HAlign = 0.018f;
                //playerPanel.Top.Set(200 + 70 * i, 0);
                playerPanel.Activate();

                Append(playerPanel);
            }
        }

        public override void OnInitialize()
        {
            InitializePanels();
        }
    }
}
