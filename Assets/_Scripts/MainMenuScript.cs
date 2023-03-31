using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour {
    private GameManager _gameManager;

    // PUBLIC INSTANCE VARIABLES

    [Header("Text")]
    public TextMeshProUGUI VersionLabel;

    [Header("Buttons")]
    [SerializeField]
    public Button StartButton;
    public Button ExitButton;

    [Header("ScoreBoard")]
    public ScrollRect Scoreboard;
    public GameObject ScorePrefab;
    
    void Start () {
        VersionLabel.text = "Version: " + Application.version;       
        _gameManager = GameManager.Instance;

       // int i = 0;
        foreach(var score in _gameManager.ScoreManager.GetTop10Scores())
        {
            GameObject scoreGO = Instantiate(ScorePrefab, Scoreboard.content);
            TextMeshProUGUI[] textComponents = scoreGO.GetComponentsInChildren<TextMeshProUGUI>();
            textComponents[0].text = score.PlayerName;
            textComponents[1].text = score.Score.ToString();
            //scoreGO.transform.localPosition = new Vector3(0, -i * scoreGO.GetComponent<RectTransform>().sizeDelta.y, 0);
            //i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    // PUBLIC METHODS
    public void Start_Game()
    {
        SceneManager.LoadScene("Main");
    }
    public void Close_Game()
    {
        _gameManager.Quit();
    }
    public void EnterUserName(TMP_InputField value)
    {
        _gameManager.UserName = value.text;
    }
    public void SelectedDifficulty(TMP_Dropdown target)
    {       
        switch (target.value)
        {
            case 0:
                _gameManager.Difficulty = GameManager.DifficultyLevel.Easy;
                break;
            case 1:
                _gameManager.Difficulty = GameManager.DifficultyLevel.Normal;
                break;
            case 2:
                _gameManager.Difficulty = GameManager.DifficultyLevel.Hard;
                break;
        }
    }
}
