using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public GameObject mainMenu;
    public GameObject gamePlay;
    public Text scoreText;
    public Text coinTextInGame;
    public Text coinTextTotal;
    public GameoverDialog gameoverDialog;
    public PauseDialog pauseDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
        if (coinTextTotal)
            coinTextTotal.text = Pref.numberOfCoins.ToString("000");
    }

    public void ShowGamePlay(bool isShow)
    {
        if (gamePlay)
        {
            gamePlay.SetActive(isShow);
        }
        if (mainMenu)
        {
            mainMenu.SetActive(!isShow);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
    public void UpdateCoin(int coin)
    {
        coinTextInGame.text = coin.ToString("000");
    }

    public void UpdateCoinTotal()
    {
        coinTextTotal.text = Pref.numberOfCoins.ToString();
    }
}
