using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    [SerializeField] GameObject FireErosion;

    private GameController _gameController;

    // Use this for initialization
    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {        
        if(other.tag=="Player")
        {
            _gameController.BatteryCharge = 100f;
            _gameController.Batteries.Remove(gameObject);

            Destroy(gameObject);
            GameObject particleFX = Instantiate(FireErosion, transform.position, transform.rotation);
            Destroy(particleFX, 2f);
        }        
    }
}
