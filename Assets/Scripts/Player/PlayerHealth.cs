using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	float hp = 100f;

	public Image healthBarImage;
	CameraController cameraController;


	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		cameraController = Camera.main.GetComponent<CameraController> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			RecieveDamage(5);
//		}
	}

	public void ShowHealthBar () {
		healthBarImage.gameObject.SetActive(true);
//		healthBarImage.fillAmount = hp;
	}

	public void RecieveDamage (float damage) {
		hp -= damage;
		cameraController.StartShake();
		healthBarImage.fillAmount = hp/100f;
		StartCoroutine("PauseWaitResume", 0.1f);
		// TODO: test for damage more that ramaining hp
		if (hp <= 0) {
			Debug.Log("PLAYER DEAD");
		}
	}


	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}
}
