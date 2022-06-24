using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace ZeroXHUD.Core.Config.DataTypes
{
	public enum SimpleEnum
	{
		Weird,
		Odd,
		// Enum members can be individually labeled as well
		[Label("Strange Label")]
		Strange,
		[Label("$Mods.ExampleMod.Config.SampleEnumLabels.Peculiar")]
		Peculiar
	}
}
