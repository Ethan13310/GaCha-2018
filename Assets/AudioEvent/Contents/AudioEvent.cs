using UnityEngine;


[CreateAssetMenu(menuName="Gacha/Audio/AudioEvent")]
public abstract class AudioEvent : ScriptableObject {

	public bool loop;
	public AudioClip[] clips;
	public abstract void Play(AudioSource source);
}
