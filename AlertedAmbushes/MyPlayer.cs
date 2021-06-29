using Terraria;
using Terraria.ModLoader;


namespace AlertedAmbushes {
	class AlertedAmbushesPlayer : ModPlayer {
		public override bool Autoload( ref string name ) {
			this.InitializePickaxeHook();

			return base.Autoload( ref name );
		}

		////

		private void InitializePickaxeHook() {
			void PickTile( On.Terraria.Player.orig_PickTile orig, Player self, int x, int y, int pickPower) {
				var config = AlertedAmbushConfig.Instance;
				int tileId = self.hitTile.HitObject( x, y, 1 );
				int dmg = self.hitTile.AddDamage( tileId, 0, false );
				bool wasTileActive = Main.tile[x, y].active();

				orig.Invoke( self, x, y, pickPower );

				if( wasTileActive != Main.tile[x, y].active() ) {
					float alertChance = config.Get<float>( nameof(config.TileBreakAlertChance) );

					AmbushesLogic.AttemptAmbush( self, x, y, alertChance, AlertType.Mining );
				} else if( dmg != self.hitTile.AddDamage(tileId, 0, false) ) {
					float alertChance = config.Get<float>( nameof(config.TileHarmAlertChance) );

					AmbushesLogic.AttemptAmbush( self, x, y, 1f, AlertType.Mining );
				}
			}
			
			//

			On.Terraria.Player.PickTile += PickTile;
		}
	}
}