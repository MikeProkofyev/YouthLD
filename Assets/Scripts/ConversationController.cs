using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour {

	string [] currSequence = {"up", "up", "down", "down", "left", "right", "left", "right"};
	public int currProgressIdx = 0;
	public bool talking = false;


	Image progressBar;
	PlayerMovementController player;


	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		progressBar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Image>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (talking && Input.anyKeyDown) {
			UpdateTalking();	
		}
	}

	void UpdateTalking () {
		switch (currSequence[currProgressIdx]) {
		case "left":
			if (Input.GetKeyDown(KeyCode.LeftArrow))currProgressIdx++;
			break;
		case "right":
			if (Input.GetKeyDown(KeyCode.RightArrow))currProgressIdx++;
			break;
		case "up":
			if (Input.GetKeyDown(KeyCode.UpArrow))currProgressIdx++;
			break;
		case "down":
			if (Input.GetKeyDown(KeyCode.DownArrow))currProgressIdx++;
			break;
		}
		progressBar.fillAmount = (float)currProgressIdx/currSequence.Length;
		if (currProgressIdx == currSequence.Length - 1) {
			talking = false;
			player.inBattle = true;
			GameObject.Destroy(gameObject);
		}
	}
}