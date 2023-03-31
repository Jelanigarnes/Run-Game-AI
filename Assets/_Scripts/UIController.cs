using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameManager _gameManager;
    private GameController _controller;
    [SerializeField]
    private TextMeshProUGUI _timer;
    [Header("Header")]
    [SerializeField]
    private GameObject _pauseTitle;
    [SerializeField]
    private GameObject _resumeButton;
    [SerializeField]
    private GameObject _backToMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance; 
        _controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.IsGamePaused)
        {
            if (_pauseTitle.activeInHierarchy)
            {
                PauseMenu(false);
            }
            _timer.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                TimeSpan.FromSeconds(_controller.GameTime).Hours, 
                TimeSpan.FromSeconds(_controller.GameTime).Minutes, 
                TimeSpan.FromSeconds(_controller.GameTime).Seconds,
                TimeSpan.FromSeconds(_controller.GameTime).Milliseconds/100);
        }
        else
        {
            if (!_pauseTitle.activeInHierarchy)
            {
                PauseMenu(true);
            }
        }
    }
    private void PauseMenu(bool visible)
    {
        Debug.Log("PauseMenu " + visible);
        _pauseTitle.SetActive(visible);
        _resumeButton.SetActive(visible);
        _backToMainMenu.SetActive(visible);
    }
    public void ResumeButton()
    {
        GameManager.Instance.IsGamePaused = false;
    }
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("Title");
    }
}
