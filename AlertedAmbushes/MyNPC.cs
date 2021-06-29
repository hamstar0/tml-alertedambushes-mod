using Terraria;
using Terraria.ModLoader;


namespace AlertedAmbushes {
	class AlertedAmbushesNPC : GlobalNPC {
		public override void EditSpawnRate( Player player, ref int spawnRate, ref int maxSpawns ) {
			if( !AmbushesLogic.GetCurrentAmbushType(player.whoAmI).HasValue ) {
				return;
			}

			var config = AlertedAmbushConfig.Instance;
			float spawnRateScale = config.Get<float>( nameof(config.AmbushSpawnRateScale) );
			float spawnMaxScale = config.Get<float>( nameof(config.AmbushSpawnMaxScale) );

			spawnRate = (int)((float)spawnRate / spawnRateScale);
			maxSpawns = (int)((float)maxSpawns * spawnMaxScale);
		}
	}
}