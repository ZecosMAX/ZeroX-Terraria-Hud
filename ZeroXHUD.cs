using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ZeroXHUD.Core;
using ZeroXHUD.Core.Config;
using ZeroXHUD.UI;

namespace ZeroXHUD
{
    public class ZeroXHUD : Mod
    {
        public static ZeroXHUDSystem ModSystemInstance { get; set; }
        public static ZeroXPlayer ModPlayerInstance { get; set; }
        public static ZeroXHUD Instance { get; set; }

        public static void InitializeModSystem(ZeroXHUDSystem modSystem)
        {
            ModSystemInstance = modSystem;
        }

        public static void InitializeModPlayer(ZeroXPlayer modPlayer)
        {
            ModPlayerInstance = modPlayer;
        }

        public ZeroXHUD()
        {
            Instance = this;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
        }
    }
}