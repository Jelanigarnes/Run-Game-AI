using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonController : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField]
    private TextMeshProUGUI _UserScore;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _gameManager = GameManager.Instance;
        _UserScore.text = _gameManager.UserName + ": " + _gameManager.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Title");
    }
    public void Exit()
    {
        _gameManager.Quit();
    }
}
