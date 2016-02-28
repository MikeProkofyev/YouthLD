using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {



	enum Zone {
		BACK,
		UP,
		DOWN,
		CENTER
	};

	Zone currZone = Zone.CENTER;
	float walkSpeed  = 5f;
	float jmpBckDistance = 1f;
	float dodgeTime = 0.35f;
	float returnAfterJmpBckTime = 0.75f;
	float currDodgeTime = 0f;

	Animator animController;
	SpriteRenderer sprite;

	Vector2 nextPosition, currPosition;
	bool jumping = false;
	bool jumpingBack = false;

	Vector2 centerPosition, backPosition, upPosition, downPosition;

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
		if (!jumping && currZone == Zone.CENTER) {
			TryDodging ();	
		}else if(jumping) {
			if (currDodgeTime > dodgeTime) {
				jumping = false;
				Invoke("BeginReturnAfterJump", 0.5f);
			}else  {
				currDodgeTime += Time.deltaTime;
				transform.position = Vector2.Lerp(currPosition, nextPosition, currDodgeTime/dodgeTime);
			}	
		}else if(jumpingBack) {
			if (currDodgeTime > returnAfterJmpBckTime) {
				jumpingBack = false;
				currZone = Zone.CENTER;

			}else {
				currDodgeTime += Time.deltaTime;
				transform.position = Vector2.Lerp(currPosition, nextPosition, currDodgeTime/dodgeTime);
			}
		}	
	}

	void BeginReturnAfterJump() {
		jumpingBack = true;
		currPosition = backPosition;
		nextPosition = centerPosition;
		currDodgeTime = 0f;
	}


	void TryDodging () {
		if (Input.anyKeyDown) {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			if (h == -1) {
				animController.SetTrigger("jumpBack");
				nextPosition = backPosition;
				currPosition = centerPosition;
				jumping = true;
				currDodgeTime = 0f;
				currZone = Zone.BACK;
			}

			//UPDATE THIS FOR OTHER TIPES OF DODGING
//			if (h == -1 || Mathf.Abs(v) != 0) {
//				animController.SetTrigger("jumpBack");
//				if (h == -1) nextPosition = leftPosition;
//				else if (v == 1) nextPosition = upPosition;
//				else if (v == -1) nextPosition = downPosition;
//				dodging = true;
//				currDodgeTime = 0f;
//				currPosition = centerPosition;
//			}
		}	
	}

	public string inZone() {
//		if (currPosition == backPosition || nextPosition == backPosition) return "back";
//		if (currPosition == upPosition || nextPosition == upPosition) return "up";
//		if (currPosition == downPosition || nextPosition == downPosition) return "down";
//		return "center";
//		Debug.Log(currZone);

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
			centerPosition = transform.position;
			backPosition = transform.TransformPoint(-1 * jmpBckDistance, 0, 0);
			upPosition = transform.TransformPoint(0, 1, 0);
			downPosition = transform.TransformPoint(0, -1, 0);
		}
	}

	void UpdateWalking () {
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput != 0) {
			animController.SetBool("running", true);
			transform.Translate(Vector3.right * walkSpeed * horizontalInput * Time.deltaTime);
			sprite.flipX = horizontalInput == -1 ? true : false;
		}else if(animController.GetBool("running")) {
			animController.SetBool("running", false);
		}		
	}
}