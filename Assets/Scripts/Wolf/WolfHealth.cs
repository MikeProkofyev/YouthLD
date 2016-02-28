using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WolfHealth : MonoBehaviour {

	float hp = 100f;

	public Image healthBarImage;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowHealthBar () {
		healthBarImage.gameObject.SetActive(true);
	}

	void RecieveDamage (float damage) {
		hp -= damage;
		healthBarImage.fillAmount = damage/100f;
		// TODO: test for damage more that ramaining hp
		if (hp <= 0) {
			//DEAD
		}
	}
}
