using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Text highScore;
    public int highScoreInt;

    void Start()
    {
        highScoreInt = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = "Highscore " + highScoreInt.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
