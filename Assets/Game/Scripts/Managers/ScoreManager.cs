using System;
using Utils.Singleton;

public class ScoreManager : Singleton<ScoreManager>
{
    public event Action AumentouScore;
    private int score;

    private void Awake()
    {
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

    #endregion
}
