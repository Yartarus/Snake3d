using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Text ScoreText;

    public int score = 0;
    public int highScoreInt;

    public GameObject newHighScoreUI;

    public void IncrementScore()
    {

        score++;

    }

    public void SaveScore()
    {
        highScoreInt = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScoreInt)
        {
            newHighScoreUI.SetActive(true);
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = score.ToString();
    }
}
