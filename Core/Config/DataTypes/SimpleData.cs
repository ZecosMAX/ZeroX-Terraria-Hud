using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace ZeroXHUD.Core.Config.DataTypes
{
	[BackgroundColor(255, 7, 7)]
	public class SimpleData
	{
		[Header("Awesome")]
		public int boost;
		public float percent;

		[Header("Lame")]
		public bool enabled;

		[DrawTicks]
		[OptionStrings(new string[] { "Pikachu", "Charmander", "Bulbasaur", "Squirtle" })]
		[DefaultValue("Bulbasaur")]
		public string FavoritePokemon;

		public SimpleData()
		{
			FavoritePokemon = "Bulbasaur";
		}

		public override bool Equals(object obj)
		{
			if (obj is SimpleData other)
				return boost == other.boost && percent == other.percent && enabled == other.enabled && FavoritePokemon == other.FavoritePokemon;
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return new { boost, percent, enabled, FavoritePokemon }.GetHashCode();
		}
	}
}
