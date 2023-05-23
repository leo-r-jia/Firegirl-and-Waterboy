using UnityEngine;

//Simple class for storing global scores
public class GlobalScore : ScriptableObject
{
    public int ScoreValue { get; private set; }
    public string PlayerName { get; private set; }

    public void SetScore(int score, string name)
    {
        ScoreValue = score;
        PlayerName = name;
    }
}