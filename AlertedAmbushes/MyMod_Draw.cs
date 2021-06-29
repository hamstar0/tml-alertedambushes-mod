using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsGeneral.Services.AnimatedColor;


namespace AlertedAmbushes {
	public partial class AlertedAmbushesMod : Mod {
		internal int AlertIconsToAnimate = 0;

		private IDictionary<Vector2, int> AlertIconPositions = new Dictionary<Vector2, int>();

		private int AlertIconAnimateCooldown = 0;



		////////////////

		public static Vector2 GetAlertFlagPosition() {
			int x = 16 + Main.rand.Next( Main.screenWidth - 32 );
			int y = 16 + Main.rand.Next( Main.screenHeight - 32 );

			int xDistFromEdge;
			if( x < Main.screenWidth / 2 ) {
				xDistFromEdge = x;
			} else {
				xDistFromEdge = Main.screenWidth - x;
			}

			int yDistFromEdge;
			if( y < Main.screenHeight / 2 ) {
				yDistFromEdge = y;
			} else {
				yDistFromEdge = Main.screenHeight - y;
			}

			if( xDistFromEdge < yDistFromEdge ) {
				int randX = Main.rand.Next( 32 );
				if( x < Main.screenWidth / 2 ) {
					x = 16 + randX;
				} else {
					x = (Main.screenWidth - randX) - 16;
				}
			} else {
				int randY = Main.rand.Next( 32 );
				if( y < Main.screenHeight / 2 ) {
					y = 16 + randY;
				} else {
					y = (Main.screenHeight - randY) - 16;
				}
			}

			return new Vector2( x, y );
		}



		////////////////
		
		public override void PostDrawInterface( SpriteBatch spriteBatch ) {
			if( this.AlertIconAnimateCooldown-- <= 0 ) {
				this.AlertIconAnimateCooldown = 5;

				if( this.AlertIconsToAnimate-- >= 1 ) {
					this.AlertIconPositions[ AlertedAmbushesMod.GetAlertFlagPosition() ] = 60;
				}
			}

			foreach( Vector2 flagPos in this.AlertIconPositions.Keys.ToArray() ) {
				if( this.AlertIconPositions[flagPos]-- <= 0 ) {
					this.AlertIconPositions.Remove( flagPos );
				}

				this.DrawAlertIconAt( spriteBatch, flagPos );
			}
		}


		////////////////

		private void DrawAlertIconAt( SpriteBatch sb, Vector2 pos ) {
			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: "!",
				x: pos.X - 4,
				y: pos.Y,
				textColor: AnimatedColors.Alert.CurrentColor,
				borderColor: Color.Black,
				origin: default( Vector2 ),
				scale: 2f
			);
		}
	}
}