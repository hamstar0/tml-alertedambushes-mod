using Terraria;
using Terraria.ModLoader;


namespace AlertedAmbushes {
	public partial class AlertedAmbushesMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-alertedambushes-mod";


		////////////////

		public static AlertedAmbushesMod Instance { get; private set; }



		////////////////

		public override void Load() {
			AlertedAmbushesMod.Instance = this;
		}

		public override void Unload() {
			AlertedAmbushesMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			ModContent.GetInstance<AmbushesLogic>()?.RunAmbushes();
		}
	}
}