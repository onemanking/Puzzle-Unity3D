using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public GameObject gameUI;
	private FBScirpt fbScirpt;
	private Text scoreText;
	public float timeScore;
	private GridScript gridScript;
	// Use this for initialization
	void Start () {
		gridScript = GetComponent<GridScript>();
		scoreText = gameUI.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gridScript.enabled==true) {
			timeScore += Time.deltaTime;
		}
		if (gridScript.currentNumber > gridScript.maxNumber) {
			GameFinish ();
		}
	}

	public void GameFinish(){
		gameUI.transform.GetChild (0).gameObject.SetActive (true); //panelGameOver enable
		scoreText.text = "Time : "+timeScore.ToString("0.00");

		fbScirpt = GetComponent<FBScirpt> ();
		fbScirpt.SetScore ();
		Time.timeScale = 0;
	}

	public void StartGame(){
		gameUI.transform.GetChild (1).gameObject.SetActive (false);
		gridScript.enabled = true;
	}

	public void Restart(){
		Time.timeScale = 1;
		SceneManager.LoadScene (0);

	}
}
