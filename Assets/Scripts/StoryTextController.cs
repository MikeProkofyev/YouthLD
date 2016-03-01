using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryTextController : MonoBehaviour {

	public Text textElement;

	public AudioClip[] chars;


	string startMessage1 = "[I've got to talk to her]";//"One more time into the fray";
	string startMessage2 = "[Now or never]";
	string winMessage1 = "- See ya )";
	string winMessage2 = "[Exhales]";
	string deathMessage = "Better luck next time";

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
		yield return StartCoroutine("UpdateMessage", startMessage1);	
		yield return new WaitForSeconds(2f);  //Give the player some time to read the message
		yield return StartCoroutine("UpdateMessage", startMessage2);	
		yield return new WaitForSeconds(1f);  
		finishedStartSeq = true;
	}

	public IEnumerator PrintDeathMessage () {
		yield return StartCoroutine("UpdateMessage", deathMessage);	
		yield return new WaitForSeconds(2f);  //Give the player some time to read the message
		finishedDeathSeq = true;
	}

	public IEnumerator PrintWinMessage () {
		yield return StartCoroutine("UpdateMessage", winMessage1);	
		yield return new WaitForSeconds(2f);  //Give the player some time to read the message
		yield return StartCoroutine("UpdateMessage", winMessage2);	
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
