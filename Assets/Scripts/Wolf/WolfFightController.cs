using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WolfFightController : MonoBehaviour {

	public PlayerMovementController playerController;
	public PlayerHealth playerHealth;
	WolfHealth wolfHealth;
	Animator animController;
	VariableStorageController variableStorage;

	AudioSource audioSource;
	public AudioClip pawHit;
	public AudioClip pawSwing;

	float ttime;

	public Dictionary<string, bool> zonesUnderAttack = new Dictionary<string, bool>(){
		{ "center", false },
		{ "up", false },
		{ "down", false },
		{ "back", false }
		
	};

	int lastAttackIdx = 0;
	int currAttackIdx = 1;



	void Awake () {
		wolfHealth = GetComponent <WolfHealth> ();
		animController = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		variableStorage = GameObject.FindGameObjectWithTag("VariableStorage").GetComponent<VariableStorageController> ();
	}

	public IEnumerator Fight (){
		for (;;) {
			yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));

			do {
				currAttackIdx = Random.Range(0, 3);
			} while (currAttackIdx == lastAttackIdx);
			lastAttackIdx = currAttackIdx;


			switch (lastAttackIdx) {
			case 0:
				StartHighAttack();
				break;
			case 1:
				StartLongAttack();
				break;
			case 2:
				StartLowAttack();
				break;
			}
		}
//		yield return null;
	}

	
	// Update is called once per frame
	void Update () {
		if (zonesUnderAttack[playerController.inZone()]) {
			audioSource.PlayOneShot(pawHit);
			int damage = Mathf.Clamp(20 - variableStorage.battlesCount * 3, 5, 20);
			playerHealth.RecieveDamage(damage);
			ResetZones();
		}
	}

	void ResetZones () {
		SetAttackZones();
	}

	void BecomeInvincible () {
		wolfHealth.vulnerable = false;
	}
		

	void SetAttackZones (bool center=false, bool up=false, bool down=false, bool back=false) {
		zonesUnderAttack["center"] = center;
		zonesUnderAttack["up"] = up;
		zonesUnderAttack["down"] = down;
		zonesUnderAttack["back"] = back;
	}

	void EnableLongAttackDamage () {
		SetAttackZones(true, true, true);
		wolfHealth.vulnerable = true;
	}

	void EnableHighAttackDamage () {
		SetAttackZones(true, true, false, true);
		wolfHealth.vulnerable = true;
	}

	void EnableLowAttackDamage () {
		SetAttackZones(true, false, true, true);
		wolfHealth.vulnerable = true;
	}

	void StartHighAttack () {
		//		ttime = Time.time;
		animController.SetTrigger("highAttack");
	}

	void PlayPawSwingSound () {
		audioSource.PlayOneShot (pawSwing);
	}


	void StartLongAttack () {
//		ttime = Time.time;
		animController.SetTrigger("longAttack");
	}

	void StartLowAttack () {
		//		ttime = Time.time;
		animController.SetTrigger("lowAttack");
	}


}