using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Gacha/Audio/SimpleAudio")]
public class SimpleAudio : AudioEvent {

	public override void Play(AudioSource source){
		source.clip = clips[Random.Range(0,clips.Length)];
		source.loop=loop;
		source.Play();
	}
}
