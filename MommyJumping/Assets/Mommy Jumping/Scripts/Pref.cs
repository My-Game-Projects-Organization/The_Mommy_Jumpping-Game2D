using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
    public static int bestScore
    {
        set
        {
            int oldScore = PlayerPrefs.GetInt(PrefKey.BestScore.ToString(), 0);

            if (value > oldScore || oldScore == 0)
            {
                PlayerPrefs.SetInt(PrefKey.BestScore.ToString(), value);
            }
        }

        get => PlayerPrefs.GetInt(PrefKey.BestScore.ToString(), 0);
    }

    public static int checkType
    {
        set => PlayerPrefs.SetInt(PrefKey.CheckType.ToString(), value);

        get => PlayerPrefs.GetInt(PrefKey.CheckType.ToString(), 1);
    }

    public static int numberOfCoins
    {
        set => PlayerPrefs.SetInt(PrefKey.NumberOfCoins.ToString(), value);

        get => PlayerPrefs.GetInt(PrefKey.NumberOfCoins.ToString(), 1);
    }

    public static int CurPlayerId
    {
        set => PlayerPrefs.SetInt(PrefKey.Cur_player_id.ToString(), value);

        get => PlayerPrefs.GetInt(PrefKey.NumberOfCoins.ToString(), 0);
    }

    public static int SelectedCharacterId
    {
        set => PlayerPrefs.SetInt(PrefKey.Selected_character_id.ToString(), value);

        get => PlayerPrefs.GetInt(PrefKey.Selected_character_id.ToString(), 0);
    }
    public static int ViewCharacterId
    {
        set => PlayerPrefs.SetInt(PrefKey.View_character_id.ToString(), value);

        get => PlayerPrefs.GetInt(PrefKey.View_character_id.ToString(), 0);
    }

    public static void SetBool(string key, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
