using UnityEngine;
using System.Collections;

public class ReflectionMovement : MonoBehaviour {



	float walkSpeed  = 5f;

	Animator animController;
	SpriteRenderer sprite;
	public Transform pathBackBlocker;


	void Awake () {
		animController = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();

	}

	// Update is called once per frame
	void Update () {
		
		UpdateWalking ();

	}
		

	void UpdateWalking () {
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput != 0) {
			animController.SetBool("running", true);
			transform.Translate(Vector3.right * walkSpeed * horizontalInput * Time.deltaTime);
			if (transform.position.x < pathBackBlocker.position.x) {
				transform.position = new Vector2(pathBackBlocker.position.x, transform.position.y);
			}
			sprite.flipX = horizontalInput == -1 ? true : false;
		}else if(animController.GetBool("running")) {
			animController.SetBool("running", false);
		}		
	}
}