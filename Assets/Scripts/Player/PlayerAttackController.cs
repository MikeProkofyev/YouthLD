using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

	PlayerMovementController playerMovement;
	Animator animController;

	// Use this for initialization
	void Awake () {
		playerMovement = GetComponent<PlayerMovementController> ();
		animController = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump") && playerMovement.inZone() == "center") {
			animController.SetTrigger("slash");	
		}
	}
}
