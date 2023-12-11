using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    public Text totalScoreTxt;
    public Text bestScoreTxt;
    public Text coinInGameText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if(totalScoreTxt && GameManager.Ins)
            totalScoreTxt.text = GameManager.Ins.Score.ToString();
        
        if(bestScoreTxt)
            bestScoreTxt.text = Pref.bestScore.ToString();
        if (coinInGameText)
            Pref.numberOfCoins += GameManager.Ins.NumberOFCoins;
    }

    public void Replay()
    {
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        SceneController.Ins.LoadCurrentScene();

    }

    private void OnSceneLoadEvent(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Ins)
            GameManager.Ins.PlayGame();

        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }
}
