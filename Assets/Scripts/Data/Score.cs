using UnityEngine;

//Simple class for storing scores
public class Score : ScriptableObject
{
    public int ScoreValue { get; private set; }
    public float Time { get; private set; }
    public int Coins { get; private set; }
    public int Stars { get; private set; }

    public void SetScore(int score, float time, int coins, int stars)
    {
        ScoreValue = score;
        Time = time;
        Coins = coins;
        Stars = stars;
    }
}
