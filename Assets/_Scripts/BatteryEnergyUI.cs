using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryEnergyUI : MonoBehaviour
{
    private GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameController.GameManager.IsGamePaused && !_gameController.GameManager.IsDebuging)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsFlashLightOn)
                GetComponent<Image>().fillAmount = _gameController.BatteryCharge/100;
            
        }
    }
}
