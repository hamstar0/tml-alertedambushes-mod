using System.Collections.Generic;
using System.Linq;
using Terraria;
using ModLibsCore.Classes.Loadable;


namespace AlertedAmbushes {
	public enum AlertType {
		Other = 0,
		Mining = 1,
		Wire = 2
	}




	public class AmbushData {
		public int PlayerWho;
		public int TickDuration;
		public AlertType Type;
	}




	public delegate void GetAmbushChanceEvent( Player player, int tileX, int tileY, AlertType alertType, ref float percent );

	public delegate void AmbushEvent( Player player, int tileX, int tileY, AlertType alertType, bool begins );




	public partial class AmbushesLogic : ILoadable {
		public event GetAmbushChanceEvent OnGetAmbushChance;
		public event AmbushEvent OnAmbush;


		////////////////

		private IDictionary<int, AmbushData> Ambushes = new Dictionary<int, AmbushData>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void RunAmbushes() {
			foreach( int playerWho in this.Ambushes.Keys.ToArray() ) {
				AmbushData ambush = this.Ambushes[playerWho];
				Player plr = Main.player[playerWho];

				if( plr?.active != true ) {
					this.Ambushes.Remove( playerWho );
					continue;
				}

				if( !AmbushesLogic.CanAmbush(plr) ) {
					this.Ambushes.Remove( playerWho );
					continue;
				}

				ambush.TickDuration--;

				if( ambush.TickDuration <= 0 ) {
					this.Ambushes.Remove( playerWho );
					continue;
				}

				this.OnAmbush?.Invoke(
					player: plr,
					tileX: (int)plr.Center.X / 16,
					tileY: (int)plr.Center.Y / 16,
					alertType: ambush.Type,
					begins: false
				);
			}
		}
	}
}