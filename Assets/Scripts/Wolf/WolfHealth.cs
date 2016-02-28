using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WolfHealth : MonoBehaviour {

	float hp = 100f;

	public Image healthBarImage;
	public bool vulnerable = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowHealthBar () {
		healthBarImage.gameObject.SetActive(true);
	}

	public void RecieveDamage (float damage) {
		if (!vulnerable) {
			hp -= damage;
			healthBarImage.fillAmount = hp/100f;
			if (hp <= 0) {
				//DEAD
			}	
		}
	}
}
