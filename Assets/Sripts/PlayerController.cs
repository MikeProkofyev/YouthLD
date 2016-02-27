using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	float speed  = 5f;
	Animator animController;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		animController = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput != 0) {
			animController.SetBool("running", true);
			transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
			sprite.flipX = horizontalInput == -1 ? true : false;
		}else if(animController.GetBool("running")) {
			animController.SetBool("running", false);
		}
	}
}
