using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<GameManager>();
                    go.name = "GameManager";
                }
            }
            return _instance;
        }
    }

    [Header("Loading")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenuPanel;

    [Header("GamePanel")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Text gameplayScoreText;

    [Header("GameOverPanel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameoverScoreText;

    private float timeScore;
	private GridManager gridManager;
    private bool isPlaying;

	// Use this for initialization
	void Start () {
        ToggleLoading(false);
        ToggleMainMenu(false);
        ToggleGamePanel(false);
        ToggleGameOverPanel(false);

        gridManager = GetComponent<GridManager>();

        Loading();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying) {
			timeScore += Time.deltaTime;
            gameplayScoreText.text = "Time : " + timeScore.ToString("0.00");
        }

		if (gridManager.currentNumber > gridManager.maxNumber && isPlaying) {
			GameFinish ();
		}
	}

    #region UI Panel
    public void Loading() {
        StartCoroutine(LoadingCoroutine());
    }

    IEnumerator LoadingCoroutine() {
        ToggleLoading(true);

        yield return new WaitUntil(() => FacebookManager.Instance.GetIsLoggedIn());

        while (loadingSlider.value < 1)
        {
            loadingSlider.value += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        ToggleLoading(false);

        ToggleMainMenu(true);
    }

    public void ToggleLoading(bool enabled) {
        loadingPanel.SetActive(enabled);
    }

    public void ToggleMainMenu(bool enabled) {
        mainMenuPanel.SetActive(enabled);
    }

    public void ToggleGamePanel(bool enabled)
    {
        gamePanel.SetActive(enabled);
    }

    public void ToggleGameOverPanel(bool enabled)
    {
        gameOverPanel.SetActive(enabled);
    }
    #endregion
    public void StartGame() {
        timeScore = 0;
        ToggleMainMenu(false);
        ToggleGamePanel(true);
        ToggleGameOverPanel(false);
        isPlaying = true;
    }

    public void GameFinish(){
        ToggleGameOverPanel(true);
        isPlaying = false;
        gameoverScoreText.text = "Time : "+timeScore.ToString("0.00");

		FacebookManager.Instance.SetScore (timeScore);
	}

	public void Restart(){
        gridManager.InitGrid();
        StartGame();
    }

}
