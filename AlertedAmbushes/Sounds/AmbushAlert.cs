using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;


namespace AlertedAmbushes.Sounds {
	public class AmbushAlertSound : ModSound {
		public override SoundEffectInstance PlaySound( ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type ) {
			soundInstance = this.sound.CreateInstance();
			soundInstance.Volume = volume * 0.25f;
			soundInstance.Pan = pan;
			//soundInstance.Pitch = -1.0f;
			return soundInstance;
		}
	}
}
