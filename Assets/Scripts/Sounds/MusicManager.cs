using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public float volume;
	public AudioSource piste1;
	public AudioSource piste2;
	public AudioSource piste3;

	void Start(){
		piste1.volume=volume;
		piste2.volume=0f;
		piste3.volume=0f;
	}

	public IEnumerator fadeInEffect(AudioSource piste){
		while (piste.volume < volume){
			piste.volume += Time.deltaTime;
			yield return new WaitForSeconds(0.05f);
		}
	}

	public IEnumerator FadeOutEffect(AudioSource piste){
		while (piste.volume > 0f){
			piste.volume -= Time.deltaTime;
			yield return new WaitForSeconds(0.05f);
		}
	}

	public IEnumerator FadeOutMusic(){
		piste1.volume-=Time.deltaTime;
		piste2.volume-=Time.deltaTime;
		piste3.volume-=Time.deltaTime;
		yield return new WaitForSeconds(0.1f);
	}

	private static MusicManager s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static MusicManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(MusicManager)) as MusicManager;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
				Debug.LogError("No player in scene");
				/* 
                GameObject obj = Instantiate(Resources.Load("PlayerController") as GameObject);
                s_Instance = obj.GetComponent<PlayerController>();*/
            }
            return s_Instance;
        }
	}
	
}
