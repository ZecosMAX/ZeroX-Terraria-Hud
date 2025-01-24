using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ZeroXHUD.Core.Networking
{
    public interface IPacketBuilder
    {
        ModPacket GetModPacket();
        void SendPacket();
    }
}
