using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoundLauncher : MonoBehaviour {

	public AudioEvent audioData;

	AudioSource source;

	void Awake(){
		source = this.gameObject.AddComponent<AudioSource>();
		source.spatialBlend=0.7f;
		source.loop=false;
	}

	public void LaunchSound(){
		audioData.Play(GetComponent<AudioSource>());
	}
}
