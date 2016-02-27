using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



//	enum State {
//		RUNNING,
//		DODGING,
//
//	}


	float runSpeed  = 5f;
	float dodgeDistance = 1f;
	float dodgeTime = 0.075f;
	float currDodgeTime = 0f;

	Animator animController;
	SpriteRenderer sprite;


	Vector2 nextPosition;
	Vector2 currPosition;
	bool dodging = false;
	bool dodgingBack = false;



	Vector2 centerPosition;
	Vector2 leftPosition;
	Vector2 rightPosition;
	Vector2 upPosition;
	Vector2 downPosition;

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
		if (!dodging && centerPosition == (Vector2)transform.position) {
			TryDodging ();	
		}else if(dodging) {
			if (currDodgeTime > dodgeTime) {
				dodging = false;
				Invoke("BeginDodgingBack", 0.35f);
			}else  {
				currDodgeTime += Time.deltaTime;
				transform.position = Vector2.Lerp(currPosition, nextPosition, currDodgeTime/dodgeTime);
			}	
		}else if(dodgingBack) {
			if (currDodgeTime > dodgeTime) {
				dodgingBack = false;
			}else {
				currDodgeTime += Time.deltaTime;
				transform.position = Vector2.Lerp(currPosition, nextPosition, currDodgeTime/dodgeTime);
			}
		}	
	}

	void BeginDodgingBack() {
		dodgingBack = true;
		currPosition = nextPosition;
		nextPosition = centerPosition;
		currDodgeTime = 0f;
	}


	void TryDodging () {
		if (Input.anyKeyDown) {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			if (Mathf.Abs(h) + Mathf.Abs(v) != 0) {
				if (h == 1) nextPosition = rightPosition;
				else if (h == -1) nextPosition = leftPosition;
				else if (v == 1) nextPosition = upPosition;
				else if (v == -1) nextPosition = downPosition;
				dodging = true;
				currDodgeTime = 0f;
				currPosition = centerPosition;
//				StartCoroutine(Dodge());
			}
		}	
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "BattleTrigger") {
			inBattle = true;
			animController.SetBool("running", false);
			centerPosition = transform.position;
			leftPosition = transform.TransformPoint(-1 * dodgeDistance, 0, 0);
			rightPosition = transform.TransformPoint(dodgeDistance, 0, 0);
			upPosition = transform.TransformPoint(0, 1, 0);
			downPosition = transform.TransformPoint(0, -1, 0);
		}
	}

	void UpdateWalking () {
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput != 0) {
			animController.SetBool("running", true);
			transform.Translate(Vector3.right * runSpeed * horizontalInput * Time.deltaTime);
			sprite.flipX = horizontalInput == -1 ? true : false;
		}else if(animController.GetBool("running")) {
			animController.SetBool("running", false);
		}		
	}



}
