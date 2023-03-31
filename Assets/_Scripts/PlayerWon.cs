using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class PlayerWon : MonoBehaviour {

    [SerializeField]
    private GameObject _Instructions;
    [SerializeField]
    private Material BloomMaterial;
    [SerializeField]
    private bool isMouseOver;

    private GameController _gameController;

    private readonly float startThreshold = 0.8f;
    private readonly float endThreshold = 0.5f;
    private readonly float amplitude = 0.5f;
    private readonly float duration = 0.5f;
    private float timer = 0f;


    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _Instructions = GameObject.Find("Game Won Instructions");
        _Instructions.SetActive(false);
    }
    void OnMouseOver()
    {
        isMouseOver=true;
        _Instructions.SetActive(true);
    }
    void OnMouseExit()
    {
        isMouseOver=false;
        _Instructions.SetActive(false);
    }
    void OnMouseDown()
    {
        _gameController.GameManager.IsGameWon = true;
    }
    void Update()
    {
        if (isMouseOver)
        {
            timer += Time.deltaTime;

            float threshold = Mathf.Lerp(startThreshold, endThreshold, Mathf.PingPong(timer, duration) / duration);

            if (BloomMaterial && BloomMaterial.HasProperty("_BloomThreshold"))
            {
                BloomMaterial.SetFloat("_BloomThreshold", threshold);
            }
        }
    }
}
