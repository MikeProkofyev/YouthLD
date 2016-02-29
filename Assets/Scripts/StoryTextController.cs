using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryTextController : MonoBehaviour {

	public Text textElement;

	public AudioClip[] chars;


	string startMessage = "One more time into the fray";
	string winMessage = "See you later!";
	string deathMessage = "YOU DIED";

	public bool finishedStartSeq = false;
	public bool finishedWinSeq = false;
	public bool finishedDeathSeq = false;




	AudioSource audioSource;

	void Start () {
	}

	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}

	public void EndMessaging () {
		StopCoroutine("UpdateMessage");
		StopCoroutine("PrintStartMessages");
		gameObject.SetActive(false);
	}

	public IEnumerator PrintStartMessage () {
		yield return StartCoroutine("UpdateMessage", startMessage);	
		yield return new WaitForSeconds(1f);  //Give the player some time to read the message
		finishedStartSeq = true;
	}

	public IEnumerator PrintDeathMessage () {
		yield return StartCoroutine("UpdateMessage", deathMessage);	
		yield return new WaitForSeconds(2f);  //Give the player some time to read the message
		finishedDeathSeq = true;
	}

	public IEnumerator PrintWinMessage () {
		yield return StartCoroutine("UpdateMessage", winMessage);	
		yield return new WaitForSeconds(3f);  //Give the player some time to read the message
		finishedWinSeq = true;
	}

	IEnumerator UpdateMessage (string message) {
		int charIndex = 0;
		textElement.text = "";
		while(charIndex != message.Length) {
			textElement.text += message[charIndex++];
			audioSource.Stop();
			audioSource.PlayOneShot(chars[3]);
			yield return new WaitForSeconds(0.05f);
		}
	}
}
