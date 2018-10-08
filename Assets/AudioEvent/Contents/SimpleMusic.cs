using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Gacha/Audio/SimpleMusic")]
public class SimpleMusic : AudioEvent {
	private bool isPlaying;
	private int index;

	public override void Play(AudioSource source){
		source.clip = clips[Random.Range(0,clips.Length)];
		source.loop=loop;
		source.Play();
	}
	
	public void PlayNext(AudioSource source){
		index = index < clips.Length-1?index+1:0;
		source.clip = clips[index];
		source.loop=false;
		source.Play();
	}

	public IEnumerator StartMusic(AudioSource source){
		this.isPlaying = true;
		index = -1;
		ArrayExtension.Shuffle(this.clips);//On randomize les music
        while (true) {
			if(this.isPlaying && !source.isPlaying)
            	PlayNext(source);

			if(!this.isPlaying && source.isPlaying)//On arrete la music si 
				source.Stop();

            yield return null;
        }
	}

	public void StopMusic(){
		this.isPlaying = false;
	}
	public void RestartMusic(){
		this.isPlaying = true;
	}
}
