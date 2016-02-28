using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WolfFightController : MonoBehaviour {

	public PlayerMovementController playerController;
	public PlayerHealth playerHealth;
	public Dictionary<string, bool> zonesUnderAttack = new Dictionary<string, bool>()
	{
		{ "center", false },
		{ "up", false },
		{ "down", false },
		{ "back", false }
		
	};

	void Start () {
		
	}

	public IEnumerator Fight (){
		for (;;) {
			yield return new WaitForSeconds(Random.Range(1f, 2f));
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
		Debug.Log("Attack ended");
	}

	void SetAttackZones (bool center=false, bool up=false, bool down=false, bool back=false) {
		zonesUnderAttack["center"] = center;
		zonesUnderAttack["up"] = up;
		zonesUnderAttack["down"] = down;
		zonesUnderAttack["back"] = back;
	}

	IEnumerator HighAttack () {
		yield return new WaitForSeconds(0.1f);
		SetAttackZones(true, true);
		Debug.Log("High attack");
		Invoke("ResetZones", 0.2f);
	}

}