using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState
{
    public string username;
    public string email;
    public int Damage;
    public int Coin;
    public int MaxScore;
    public int GamePlayTime;
    public int PlayerNumber;

    public PlayerState(string _username, string _email, int _damage, int _coin, int _maxscore, int _gameplaytime,int _playernumber)
    {
        username = _username;
        email = _email;
        Damage = _damage;
        Coin = _coin;
        MaxScore = _maxscore;
        GamePlayTime = _gameplaytime;
        PlayerNumber = _playernumber;
    }
};

public enum GameState
{
    INTRO,
    SHOP,
    STAGE
}


public class GameManger : MonoBehaviour
{


    public static GameManger instance;

    public GameState gamestate;
    public PlayerState PlayerStat;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        

        }
        DontDestroyOnLoad(this);
        gamestate = GameState.INTRO;
        
    }

    private void Start()
    {

    }

    public PlayerState GetPlayerStat()
    {
        return PlayerStat;
    }
    
    public void SetPlayerStat(PlayerState _playerstat)
    {
        PlayerStat = _playerstat;
    }

    

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamestate == GameState.INTRO)
            {
                Application.Quit();
            }
        }
    }
}
