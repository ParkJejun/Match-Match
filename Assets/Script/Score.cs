using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int historynum;

    void Start()
    {
        historynum = PlayerPrefs.GetInt("historynum", 0);
    }

    public static void save(int score)
    {
        historynum = historynum + 1;
        PlayerPrefs.SetInt(historynum.ToString(), score);
        PlayerPrefs.SetInt("historynum", historynum);
    }
}
