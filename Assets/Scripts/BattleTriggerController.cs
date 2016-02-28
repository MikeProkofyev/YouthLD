using UnityEngine;
using System.Collections;

public class BattleTriggerController : MonoBehaviour {


	public PlayerHealth playerHealthController;
	public WolfHealth wolfHealthController;
	public WolfFightController wolfFightController;

	// Use this for initialization
	void Start () {
	
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			playerHealthController.ShowHealthBar();
			wolfHealthController.ShowHealthBar();
			wolfFightController.StartCoroutine(wolfFightController.Fight());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
