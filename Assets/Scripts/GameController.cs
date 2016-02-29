using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	Transform playerT;
	public Transform battleTrigger;
	public bool skipToArena = false;
	public bool showStartSequence = true;
	public StoryTextController storyTxtController;


//	public bool heroDied = false;
//	public bool heroWon = false;


	enum GameState
	{
		STARTSEQUENCE,
		GAMESTARTED,
		WON,
		DIED,
	};

	GameState currentState = GameState.STARTSEQUENCE;



	void Awake () {
		playerT = GameObject.FindGameObjectWithTag("Player").transform;
	}
		
	void Start () {
		if (skipToArena) playerT.position = (Vector2)battleTrigger.position - Vector2.right*4;
		storyTxtController.gameObject.SetActive(showStartSequence);
		if (showStartSequence) storyTxtController.StartCoroutine(storyTxtController.PrintStartMessage());
		currentState = showStartSequence ? GameState.STARTSEQUENCE : GameState.GAMESTARTED;
	}

	void Update () {
		switch (currentState) {
		case GameState.STARTSEQUENCE:
			if (storyTxtController.finishedStartSeq || Input.GetButtonDown("Jump")) {
				storyTxtController.EndMessaging();
				currentState = GameState.GAMESTARTED;
			}
			break;
		case GameState.DIED:
			if (storyTxtController.finishedDeathSeq) {
				SceneManager.LoadScene(1);
			}
			break;
		case GameState.WON:
			if (storyTxtController.finishedStartSeq) {
				SceneManager.LoadScene(0);
			}
			break;
		}
	}

	public void HeroDied () {
		storyTxtController.gameObject.SetActive(true);
		currentState = GameState.DIED;
		storyTxtController.StartCoroutine(storyTxtController.PrintDeathMessage());
	}

	public void HeroWon () {
		storyTxtController.gameObject.SetActive(true);
		currentState = GameState.WON;
		storyTxtController.StartCoroutine(storyTxtController.PrintWinMessage());
	}
}
