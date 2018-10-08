using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronSounds : MonoBehaviour {

	AudioSource source;

	public AudioClip[] bubullesSounds;
	public AudioClip finishPotion;

	void Start(){
		source = GetComponent<AudioSource>();
		source.Stop();
	}

	void Update(){
	//	Test();
	}

	public void Test(){
		if(Input.GetKeyDown("a")){
			LaunchBubulleSound(0);
		}else if(Input.GetKeyDown("z")){
			LaunchBubulleSound(1);
		}else if(Input.GetKeyDown("e")){
			LaunchFinishPotionSound();
		}
	}

	public void LaunchBubulleSound(int index){
		//0 premier ingrédient ajouter
		//1 deuxième ingrédient
		source.spatialBlend=1f;
		source.Stop();
		source.clip = bubullesSounds[index];
		source.Play();
		source.loop=true;
	}

	public void LaunchFinishPotionSound(){
		source.spatialBlend=0f;
		source.volume=0.7f;
		source.Stop();
		source.clip = finishPotion;
		source.loop=false;
		source.Play();
	}

	
}
