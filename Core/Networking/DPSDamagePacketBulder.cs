using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ZeroXHUD.Core.Networking
{
    public class DPSDamagePacketBulder : IPacketBuilder
    {
        public Player Player { get; set; }

        public ModPacket GetModPacket()
        {
            var packet = ZeroXHUD.Instance.GetPacket();
            packet.WriteNullTerminatedString("ZeroX.DPS");
            packet.Write(Player.dpsDamage);
            return packet;
        }

        public void SendPacket()
        {
            using var packet = GetModPacket();

            var sameTeamPlayers = Main.player.Where(p => p != Main.LocalPlayer && p.team == Player.team && p.active);

            foreach ( var player in sameTeamPlayers )
            {
                packet.Send(player.whoAmI);
            }
        }
    }
}
