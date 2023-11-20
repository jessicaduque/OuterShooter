using System;
using UnityEngine;
using Utils.Singleton;

public class ScoreManager : Singleton<ScoreManager>
{
    private const string KEY_BESTSCORE = "BestScore";
    public event Action AumentouScore;
    private int score;
    private int bestScore;

    private new void Awake()
    {
        bestScore = (PlayerPrefs.HasKey(KEY_BESTSCORE) ? PlayerPrefs.GetInt(KEY_BESTSCORE) : 0);
        score = 0;
    }


    #region Score
    public void AdicionarPontosScore(int score)
    {
        this.score += score;
        AumentouScore?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    #endregion

    #region BestScore

    private bool ChecarBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(KEY_BESTSCORE, bestScore);
            return true;
        }
        return false;
    }

    public int GetBestScore()
    {
        ChecarBestScore();
        return bestScore;
    }

    #endregion
}
