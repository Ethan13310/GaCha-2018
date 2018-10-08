using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Gacha/Audio/PitchVolumeAudio")]
public class PitchVolumeAudio : AudioEvent {
	public RangedFloat volume;

	[MinMaxRange(0,2)]
	public RangedFloat pitch;
	public override void Play(AudioSource source){
		source.clip = clips[Random.Range(0,clips.Length)];
		source.volume = Random.Range(volume.minValue, volume.maxValue);
		source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
		source.loop=loop;
		source.Play();
	}
}
