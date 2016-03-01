using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TitleEndingsGeneratorController : MonoBehaviour {

	public Text textElement;

	string [] endings = {"Jude", "you", "why do", "I'm", "my name", "do you", "what's the", "what's your", "when I saw", "don't you", "every day", "don't know", "wanna"};
	int lastEndingIdx = 0;
	int currEndingIdx = 1;

	// Use this for initialization
	void Awake () {
		textElement = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Start () {
		StartCoroutine(Generate());
	}

	IEnumerator Generate () {
		for(;;) {
			do {
				currEndingIdx = Random.Range(0, endings.Length);
			} while (currEndingIdx == lastEndingIdx);
			lastEndingIdx = currEndingIdx;
			yield return StartCoroutine("UpdateMessage", endings[currEndingIdx] + "...");	
			yield return new WaitForSeconds(1f);  //Give the player some time to read the message	
		}
	}


	IEnumerator UpdateMessage (string message) {
		int charIndex = 0;
		textElement.text = "";
		while(charIndex != message.Length) {
			textElement.text += message[charIndex++];
			yield return new WaitForSeconds(0.05f);
		}
	}
}
