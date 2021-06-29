using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsGeneral.Libraries.World;


namespace AlertedAmbushes {
	public partial class AmbushesLogic : ILoadable {
		public static bool CanAmbush( Player player ) {
			var config = AlertedAmbushConfig.Instance;
			int tileX = (int)player.Center.X / 16;
			int tileY = (int)player.Center.Y / 16;

			if( !config.Get<bool>(nameof(config.AmbushesOccurNearTowns)) ) {
				if( player.townNPCs >= 1f ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponSurface)) ) {
				if( tileY < WorldLocationLibraries.SurfaceLayerBottomTileY ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponSnow)) ) {
				if( player.ZoneSnow ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponDesert)) ) {
				if( player.ZoneDesert ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponJungle)) ) {
				if( player.ZoneJungle ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponDungeonOrTemple)) ) {
				if( player.ZoneDungeon || Main.tile[tileX, tileY]?.wall == WallID.LihzahrdBrickUnsafe ) {
					return false;
				}
			}
			if( !config.Get<bool>(nameof(config.AmbushesOccurUponUnderworld)) ) {
				if( tileY >= WorldLocationLibraries.UnderworldLayerTopTileY ) {
					return false;
				}
			}
			return true;
		}


		public static bool AttemptAmbush( Player player, int tileX, int tileY, float percentChance, AlertType alertType ) {
			var singleton = ModContent.GetInstance<AmbushesLogic>();

			if( singleton.Ambushes.ContainsKey( player.whoAmI ) ) {
				return false;
			}

			if( !AmbushesLogic.CanAmbush(player) ) {
				return false;
			}

			singleton.OnGetAmbushChance?.Invoke( player, tileX, tileY, alertType, ref percentChance );
			if( percentChance <= 0f ) {
				return false;
			}

			if( Main.rand.NextFloat() >= percentChance ) {
				return false;
			}

			AmbushesLogic.CreateAmbush( player, tileX, tileY, alertType );
			return true;
		}


		public static void CreateAmbush( Player player, int tileX, int tileY, AlertType alertType ) {
			if( !AmbushesLogic.AddAmbush(player, 15 * 60, alertType) ) {
				throw new ModLibsException( "Ambush already exists." );
			}

			var singleton = ModContent.GetInstance<AmbushesLogic>();
			singleton.OnAmbush?.Invoke( player, tileX, tileY, alertType, true );

			var mymod = AlertedAmbushesMod.Instance;
			mymod.AlertIconsToAnimate = 20;

			AlertedAmbushesMod.Instance
				.GetSound( "Sounds/AmbushAlert" )?
				.Play( 0.5f, 0f, 0f );
		}


		////////////////

		public static AlertType? GetCurrentAmbushType( int playerWho ) {
			var singleton = ModContent.GetInstance<AmbushesLogic>();
			return singleton.Ambushes.TryGetValue( playerWho, out AmbushData data )
				? data.Type
				: (AlertType?)null;
		}

		public static bool AddAmbush( Player player, int tickDuration, AlertType type ) {
			var singleton = ModContent.GetInstance<AmbushesLogic>();
			if( singleton.Ambushes.ContainsKey( player.whoAmI ) ) {
				return false;
			}

			singleton.Ambushes[player.whoAmI] = new AmbushData {
				PlayerWho = player.whoAmI,
				TickDuration = tickDuration,
				Type = type
			};

			return true;
		}
	}
}