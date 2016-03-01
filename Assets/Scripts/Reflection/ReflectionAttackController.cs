using UnityEngine;
using System.Collections;

public class ReflectionAttackController : MonoBehaviour {

	Animator animController;

	float attackFrequency = 0.2f;
	float lastAttackTime;



	// Use this for initialization
	void Awake () {
		animController = GetComponent<Animator> ();
	
	}

	// Update is called once per frame
	void Update () {
		bool attacking =  Input.GetButtonDown("Jump") && Time.time > lastAttackTime + attackFrequency;
		if (attacking) {
			lastAttackTime = Time.time;
			animController.SetTrigger("slash");	
		}

	}

	void TryHitWolf () {
		return;
	}

	void PlaySwordSwingSound () {
		return;
	}
}
