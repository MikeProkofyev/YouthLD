using UnityEngine;
using System.Collections;

public class VariableStorageController : MonoBehaviour {

	private static VariableStorageController storageControllerInstance;

	public int battlesCount = 0;

	void Awake () {
		DontDestroyOnLoad (this);
		if (storageControllerInstance == null) {
			storageControllerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	

}
