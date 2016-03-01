using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintsController : MonoBehaviour {

	VariableStorageController variableStorage;
	public Text textElement;

	// Use this for initialization
	void Start () {
		variableStorage = GameObject.FindGameObjectWithTag("VariableStorage").GetComponent<VariableStorageController> ();
		if (variableStorage.battlesCount == 0) {
			textElement.text = "Use arrows to move and dodge, space to attack";
		}else {
			textElement.text = "Once more into the fray..";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
