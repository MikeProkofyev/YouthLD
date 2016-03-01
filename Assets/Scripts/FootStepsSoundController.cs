using UnityEngine;
using System.Collections;

public class FootStepsSoundController : MonoBehaviour {

	public AudioClip footstepAudioClip;
	AudioSource audioSource;

	void Awake () {
		audioSource = GetComponent <AudioSource> ();
	}

	public void PlayFootStepSound () {
		audioSource.pitch = Random.Range(0.95f, 1.05f);
		audioSource.volume = Random.Range(0.9f, 1f);
		audioSource.PlayOneShot(footstepAudioClip);
	}
}
