using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace AlertedAmbushes {
	class AlertedAmbushesItem : GlobalItem {
		public override bool CanUseItem( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.MagicMirror:
			case ItemID.IceMirror:
			case ItemID.RecallPotion:
			case ItemID.CellPhone:
				if( AmbushesLogic.GetCurrentAmbushType(player.whoAmI).HasValue ) {
					if( player.whoAmI == Main.myPlayer ) {
						Timers.SetTimer( 2, false, () => {
							Main.NewText( "Enemy ambush disrupting warp signal.", Color.Yellow );
							return false;
						} );
					}
					return false;
				}
				break;
			}

			return base.CanUseItem( item, player );
		}
	}
}