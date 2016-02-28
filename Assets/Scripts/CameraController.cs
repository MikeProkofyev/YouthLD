using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

 	public float shake = 0f;
	public Transform playerTransform;

	float shakeAmount = .1f;
	float decreaseFactor = 13f;
	float maxOffset = .15f;
	float maxXValue;
	float minXValue;
	float maxZValue;
	float minZValue;
	Vector3 originalLocalPosition;

	void Start () {
		RecalculateShakeBounds();
	}
	
	// Update is called once per frame
	void Update () {
		if (shake > 0) {
			Vector2 randCPoint = Random.insideUnitCircle * shakeAmount;

			Camera.main.transform.localPosition = new Vector3( Mathf.Clamp(Camera.main.transform.localPosition.x + randCPoint.x, minXValue, maxXValue),
			                                                  Camera.main.transform.localPosition.y,
			                                                  Mathf.Clamp(Camera.main.transform.localPosition.z + randCPoint.y, minZValue, maxZValue));

			shake -= Time.deltaTime * decreaseFactor;
		}
		else {
			Camera.main.transform.localPosition = originalLocalPosition;
		}

	}

	public void MoveToPlayer () {
		transform.position = new Vector3(playerTransform.position.x, 0f, -10f);
		RecalculateShakeBounds();
	}


	void RecalculateShakeBounds () {
		originalLocalPosition = Camera.main.transform.localPosition;
		maxXValue = originalLocalPosition.x + maxOffset;
		minXValue = originalLocalPosition.x - maxOffset;
		maxZValue = originalLocalPosition.z + maxOffset;
		minZValue = originalLocalPosition.z - maxOffset;
	}

	public void StartShake () {
		shake = 5.0f;
	}
}
