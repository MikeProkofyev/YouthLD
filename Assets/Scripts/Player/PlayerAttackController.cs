using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

	PlayerMovementController playerMovement;
	public WolfHealth wolfHealth;
	Animator animController;

	float attackFrequency = 0.2f;
	float lastAttackTime;

	// Use this for initialization
	void Awake () {
		playerMovement = GetComponent<PlayerMovementController> ();
		animController = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool attacking =  Input.GetButtonDown("Jump") && playerMovement.inZone() == "center" && Time.time > lastAttackTime + attackFrequency;
		if (attacking) {
			lastAttackTime = Time.time;
			animController.SetTrigger("slash");	
			wolfHealth.RecieveDamage(5);
		}

	}
}
