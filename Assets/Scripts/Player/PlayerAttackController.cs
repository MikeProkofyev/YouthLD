using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

	PlayerMovementController playerMovement;
	public WolfHealth wolfHealth;
	Animator animController;

	float attackFrequency = 0.2f;
	float lastAttackTime;

	AudioSource audioSource;
	public AudioClip swordHit;

	// Use this for initialization
	void Awake () {
		playerMovement = GetComponent<PlayerMovementController> ();
		animController = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool attacking =  Input.GetButtonDown("Jump") && playerMovement.inZone() == "center" && Time.time > lastAttackTime + attackFrequency;
		if (attacking) {
			lastAttackTime = Time.time;
			animController.SetTrigger("slash");	
		}

	}

	void TryHitWolf () {
		if (wolfHealth.RecieveDamage(5)) {
			audioSource.PlayOneShot(swordHit);
		}

	}
}
