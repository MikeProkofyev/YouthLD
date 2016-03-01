using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	Transform playerT;
	public Transform battleTrigger;
	public bool skipToArena = false;
	public bool showStartSequence = true;
	public StoryTextController storyTxtController;
	public AudioClip winSound;
	public AudioClip loseSound;
	public AudioClip battleTheme;
	VariableStorageController variableStorage;
	AudioSource audioSource;


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
		audioSource = GetComponent <AudioSource> ();
	}
		
	void Start () {
		variableStorage = GameObject.FindGameObjectWithTag("VariableStorage").GetComponent<VariableStorageController> ();

		if (skipToArena){
			playerT.position = (Vector2)battleTrigger.position - Vector2.right*4;
//			Debug.Log("player pos " + playerT.position);
		}
		bool showIntroductionText = showStartSequence && variableStorage.battlesCount == 0;
		storyTxtController.gameObject.SetActive(showIntroductionText);	
		if (showIntroductionText) {
			storyTxtController.StartCoroutine(storyTxtController.PrintStartMessage());
		}
		currentState = showIntroductionText ? GameState.STARTSEQUENCE : GameState.GAMESTARTED;
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
			if (storyTxtController.finishedWinSeq) {
				SceneManager.LoadScene(0);
			}
			break;
		}
	}

	public void PlayBattleTheme () {
//		audioSource.PlayOneShot(battleTheme);
		audioSource.Play();
	}

	void DestroyEntitites () {
		GameObject.FindGameObjectWithTag("Player").SetActive(false);
		GameObject.FindGameObjectWithTag("Wolf").SetActive(false);
	}

	public void HeroDied () {
		variableStorage.battlesCount++;
		DestroyEntitites();
		audioSource.Stop();
		audioSource.PlayOneShot(loseSound);
		storyTxtController.gameObject.SetActive(true);
		currentState = GameState.DIED;
		storyTxtController.StartCoroutine(storyTxtController.PrintDeathMessage());
	}

	public void HeroWon () {
		variableStorage.battlesCount++;
		DestroyEntitites(); //DEBATALBE
		audioSource.Stop();
		audioSource.PlayOneShot(winSound);
		storyTxtController.gameObject.SetActive(true);
		currentState = GameState.WON;
		storyTxtController.StartCoroutine(storyTxtController.PrintWinMessage());
	}
}