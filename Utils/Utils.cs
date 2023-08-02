using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ZeroXHUD.Utils
{
    public class Utils
    {
        public static FieldInfo? GetFieldInfo<T>(string fieldName)
        {
            return typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == fieldName);
        }
    }
}
