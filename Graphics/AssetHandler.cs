using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroXHUD.Graphics
{
    public class AssetHandler
    {
        public static AssetHandler Instance { get; private set; } = new AssetHandler();

        public List<Asset<Texture2D>> Textures { get; private set; }

        public void Initialize()
        {
            Textures = new List<Asset<Texture2D>>();
        }
    }
}
