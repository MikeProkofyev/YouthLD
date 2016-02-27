using UnityEngine;
using System.Collections;

public class PalaverManagerController : MonoBehaviour {

	string [] sequence = {"up", "up", "down", "down", "left", "right", "left", "right"};
	int progressIdx = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		float horizontalInput = Input.GetAxisRaw("Horizontal");
//		float verticalInput = Input.GetAxisRaw("Vertical");
		if (Input.anyKeyDown) {
			UpdateProgress();
		}
	}

	void UpdateProgress () {
		switch (sequence[progressIdx]) {
			case "left":
			if (Input.GetKeyDown(KeyCode.LeftArrow))progressIdx++;
			break;
			case "right":
			if (Input.GetKeyDown(KeyCode.RightArrow))progressIdx++;
			break;
			case "up":
			if (Input.GetKeyDown(KeyCode.UpArrow))progressIdx++;
			break;
			case "down":
			if (Input.GetKeyDown(KeyCode.DownArrow))progressIdx++;
			break;
		}
		Debug.Log("Progress: " + 100*progressIdx/sequence.Length);
	}
}