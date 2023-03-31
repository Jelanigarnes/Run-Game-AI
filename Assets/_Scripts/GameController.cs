using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {
    private GameManager _gameManager;
    private float _gameTime;
    private float _batteryCharge;
    private GameObject _respawnPoint;
    private float _batteryDischargeRate;
    private List<Transform> _BatteryPositions;
    

    public List<GameObject> Spooks;
    public List<GameObject> Batteries;
    public GameObject Spook;
    public GameObject Battery;
    public GameObject Player;
    public GameObject TestPlayer;

    public GameManager GameManager
    {
        get { return _gameManager; }
        set { _gameManager = value; }
    }
  
    public float GameTime
    {
        get
        {
            return this._gameTime;
        }
        set
        {
            this._gameTime = value;
        }
    }
    public float BatteryDischargeRate
    {
        get
        {
            return this._batteryDischargeRate;
        }
    }
    public float BatteryCharge
    {
        get
        {
            return this._batteryCharge;
        }
        set
        {
            this._batteryCharge = value;
            if (_batteryCharge <= 0f)
            {
                GameManager.IsGameLost  = true;                
            }
        }
    }

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.Instance;        
        _gameTime = 0;
        _batteryCharge = 100f;
        _gameDifficulty(_gameManager.Difficulty);

        //lock cursor to screen
        Cursor.lockState = CursorLockMode.Locked;

        _spawnSpooks();
        _spawnBatteries();

        if (_gameManager.IsDebuging)
        {
            //GameObject.Find("Directional Light").SetActive(true);
            Instantiate(TestPlayer, GameObject.FindGameObjectWithTag("Respawn").transform);
        }
        else
        {
            Instantiate(Player, GameObject.FindGameObjectWithTag("Respawn").transform);
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (_gameManager.IsDebuging)
            return;
        else
        {
            if (!GameManager.IsGamePaused)
            {
                _gameTime += Time.deltaTime;
                BatteryCharge -= _batteryDischargeRate;
                _gameManager.Score = _calculateScore(GameTime, Spooks.Count, Batteries.Count);
            }
        }
    }

    // Private METHODS*******************************
    private void _spawnSpooks()
    {
        GameObject[] spookPositions = GameObject.FindGameObjectsWithTag("EnemyPosition");

        foreach(GameObject pos in spookPositions)
        {
            Spooks.Add(Instantiate(Spook,pos.transform));
        }
       
    }
    private void _spawnBatteries()
    {
        GameObject[] batteryPositions = GameObject.FindGameObjectsWithTag("BatteryPosition");
        _BatteryPositions = batteryPositions.Select(batteryPosition => batteryPosition.transform).ToList();

        foreach (Transform position in _BatteryPositions)
        {
            Batteries.Add(Instantiate(Battery, position));
        }
    }
    private void _gameDifficulty(GameManager.DifficultyLevel Difficulty)
    {
        switch(Difficulty)
        {
            case GameManager.DifficultyLevel.Easy:
                _batteryDischargeRate = 0.01f;
                break;
            case GameManager.DifficultyLevel.Normal:
                _batteryDischargeRate = 0.04f;
                break;
            case GameManager.DifficultyLevel.Hard:
                _batteryDischargeRate = 0.8f;
                break;
            default:
                _batteryDischargeRate = 0.02f;
                break;
        }
    }
    /// <summary>
    /// Calcualtes Score based on factors
    /// </summary>
    /// <param name="_timeToEnd">Time it took for player to end</param>
    /// <param name="SpooksLeft">Spooks Left In Game</param>
    /// <param name="BatteriesLeft">How many batties they used</param>
    /// <returns></returns>
    private float _calculateScore(float _time,int SpooksLeft, int BatteriesLeft)
    {
        float score = 0;
        float timeFactor = 100 / _time;
        float enemiesFactor = 100 / (SpooksLeft + 1);
        float batteriesFactor = 100 / (BatteriesLeft + 1);

        score = timeFactor + enemiesFactor + batteriesFactor;

        return score;

    }

}
