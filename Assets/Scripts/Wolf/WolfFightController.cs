using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WolfFightController : MonoBehaviour {

	public PlayerMovementController playerController;
	public PlayerHealth playerHealth;
	WolfHealth wolfHealth;
	Animator animController;

	AudioSource audioSource;
	public AudioClip pawHit;

	float ttime;

	public Dictionary<string, bool> zonesUnderAttack = new Dictionary<string, bool>(){
		{ "center", false },
		{ "up", false },
		{ "down", false },
		{ "back", false }
		
	};



	void Awake () {
		wolfHealth = GetComponent <WolfHealth> ();
		animController = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	public IEnumerator Fight (){
		for (;;) {
			yield return new WaitForSeconds(Random.Range(2f, 3f));
			StartHighAttack();	
		}
//		yield return null;
	}

	
	// Update is called once per frame
	void Update () {
		if (zonesUnderAttack[playerController.inZone()]) {
			audioSource.PlayOneShot(pawHit);
			playerHealth.RecieveDamage(20);
			ResetZones();
		}
	}

	void ResetZones () {
		SetAttackZones();
	}

	void BecomeInvincible () {
		wolfHealth.vulnerable = false;
//		Debug.Log("Became Invincible");
	}
		

	void SetAttackZones (bool center=false, bool up=false, bool down=false, bool back=false) {
		zonesUnderAttack["center"] = center;
		zonesUnderAttack["up"] = up;
		zonesUnderAttack["down"] = down;
		zonesUnderAttack["back"] = back;
	}

	void EnableHighAttackDamage () {
		SetAttackZones(true, true);
		wolfHealth.vulnerable = true;
	}

	void StartHighAttack () {
//		ttime = Time.time;
		animController.SetTrigger("longAttack");
	}


}