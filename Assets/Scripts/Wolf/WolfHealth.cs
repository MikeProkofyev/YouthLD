using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WolfHealth : MonoBehaviour {

	float hp = 100f;

	public Image healthBarImage;
	public bool vulnerable = false;
	CameraController cameraController;

	void Awake () {
		cameraController = Camera.main.GetComponent<CameraController> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowHealthBar () {
		healthBarImage.gameObject.SetActive(true);
	}

	public void RecieveDamage (float damage) {
		if (vulnerable) {
			hp -= damage;
			cameraController.StartShake();
			StartCoroutine("PauseWaitResume", 0.2f);
			healthBarImage.fillAmount = hp/100f;
			if (hp <= 0) {
				//DEAD
			}	
		}
	}

	IEnumerator PauseWaitResume (float pauseDelay) {
		yield return new WaitForSeconds(0.05f);
		Time.timeScale = .0000001f;
		yield return new WaitForSeconds(pauseDelay * Time.timeScale);
		Time.timeScale = 1.0f;
	}
}