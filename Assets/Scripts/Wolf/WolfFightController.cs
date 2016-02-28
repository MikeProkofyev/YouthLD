using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WolfFightController : MonoBehaviour {

	public PlayerMovementController playerController;
	public PlayerHealth playerHealth;
	WolfHealth wolfHealth;
	Animator animController;

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
	}

	public IEnumerator Fight (){
		for (;;) {
			yield return new WaitForSeconds(Random.Range(2f, 3f));
			StartCoroutine(HighAttack());	
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (zonesUnderAttack[playerController.inZone()]) {
			playerHealth.RecieveDamage(5);
			ResetZones();
		}
	}

	void ResetZones () {
		SetAttackZones();
//		Debug.Log("Damage ended");
		StartCoroutine(BecomeInvincibleInSec(1.6f));	
	}

	IEnumerator BecomeInvincibleInSec (float time) {
		yield return new WaitForSeconds(time);
		wolfHealth.vulnerable = false;
		Debug.Log("Became Invincible");
		Debug.Log(Time.time - ttime);
		yield return null;
	}

	IEnumerator BecomeVulnurableInSec (float time) {
		yield return new WaitForSeconds(time);
		wolfHealth.vulnerable = true;
		Debug.Log("Became vulnurable");
		yield return null;
	}

	void SetAttackZones (bool center=false, bool up=false, bool down=false, bool back=false) {
		zonesUnderAttack["center"] = center;
		zonesUnderAttack["up"] = up;
		zonesUnderAttack["down"] = down;
		zonesUnderAttack["back"] = back;
	}

	IEnumerator HighAttack () {
		ttime = Time.time;
		animController.SetTrigger("longAttack");
		StartCoroutine(BecomeVulnurableInSec(0.6f));	
		yield return new WaitForSeconds(0.6f);
		SetAttackZones(true, true);
//		Debug.Log("High damage start");
		Invoke("ResetZones", 0.2f);
	}

}