using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AlertedAmbushes {
	class AlertedAmbushesTile : GlobalTile {
		public override void HitWire( int i, int j, int type ) {
			switch( type ) {
			case TileID.Explosives:
			case TileID.Traps:
			case TileID.GeyserTrap:
			case TileID.OpenDoor:
			case TileID.ClosedDoor:
			case TileID.TrapdoorOpen:
			case TileID.TrapdoorClosed:
				var wldPos = new Vector2( i*16, j*16 );
				Player nearestPlr = null;
				float nearestSqr = Single.MaxValue;
				
				foreach( Player plr in Main.player ) {
					if( plr?.active != true ) {
						continue;
					}

					float lenSqr = (plr.Center - wldPos).LengthSquared();
					if( lenSqr < nearestSqr ) {
						nearestSqr = lenSqr;
						nearestPlr = plr;
					}
				}

				if( nearestPlr != null && nearestSqr < 144 ) {
					var config = AlertedAmbushConfig.Instance;
					float alertChance = config.Get<float>( nameof(config.WireTriggerAlertChance) );

					AmbushesLogic.AttemptAmbush( nearestPlr, i, j, alertChance, AlertType.Wire );
				}

				break;
			}
		}
	}
}