using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {


	enum Zone {
		BACK,
		UP,
		DOWN,
		CENTER
	};

	float ttime;

	Zone currZone = Zone.CENTER;
	float walkSpeed  = 5f;

	Animator animController;
	SpriteRenderer sprite;
	public Transform pathBackBlocker;
	public bool inBattle = false;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		animController = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!inBattle) {
			UpdateWalking ();
		}else {
			UpdateDodging ();
		}
	}

	void UpdateDodging () {
		if (currZone == Zone.CENTER && Input.anyKeyDown) {
			TryDodging ();
		}
	}

	void StopDodging () {
		currZone = Zone.CENTER;	
	}


	void TryDodging () {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			if (h == -1) {
				animController.SetTrigger("jumpBack");
				currZone = Zone.BACK;
			}else if (v == 1) {
				animController.SetTrigger("jumpUp");
				currZone = Zone.UP;	
			}else if (v == -1) {
				animController.SetTrigger("dodgeDown");
				currZone = Zone.DOWN;	
			}
	}

	public string inZone() {

		switch (currZone) {
			case Zone.CENTER:
				return "center";
			case Zone.BACK:
				return "back";
			case Zone.UP:
				return "up";
			case Zone.DOWN:
				return "down";
			default:
				Debug.Log("UKNOWN ZONE STATE");
				return "center";
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "BattleTrigger") {
			inBattle = true;
			animController.SetBool("running", false);
		}
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