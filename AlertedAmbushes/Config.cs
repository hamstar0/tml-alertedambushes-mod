using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace AlertedAmbushes {
	class MyFloatInputElement : FloatInputElement { }




	public partial class AlertedAmbushConfig : ModConfig {
		public static AlertedAmbushConfig Instance => ModContent.GetInstance<AlertedAmbushConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue( false )]
		public bool AmbushesOccurNearTowns { get; set; } = false;

		[DefaultValue( false )]
		public bool AmbushesOccurUponSurface { get; set; } = false;

		[DefaultValue( true )]
		public bool AmbushesOccurUponSnow { get; set; } = true;

		[DefaultValue( true )]
		public bool AmbushesOccurUponDesert { get; set; } = true;

		[DefaultValue( true )]
		public bool AmbushesOccurUponJungle { get; set; } = true;

		[DefaultValue( false )]
		public bool AmbushesOccurUponDungeonOrTemple { get; set; } = false;

		[DefaultValue( false )]
		public bool AmbushesOccurUponUnderworld { get; set; } = false;

		//
		
		[Range(0f, 1f)]
		[DefaultValue(0.05f)]
		public float TileBreakAlertChance { get; set; } = 0.05f;

		[Range( 0f, 1f )]
		[DefaultValue(0.02f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TileHarmAlertChance { get; set; } = 0.02f;

		[Range( 0f, 1f )]
		[DefaultValue(0.25f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WireTriggerAlertChance { get; set; } = 0.25f;
		
		//

		[Range(0.01f, 100f)]
		[DefaultValue(20f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float AmbushSpawnRateScale { get; set; } = 20f;

		[Range(0.01f, 100f)]
		[DefaultValue(15f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float AmbushSpawnMaxScale { get; set; } = 15f;
	}
}