using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public float changeCameraDelay = 0.75f;
    public float returnDelay = 2f;

    public GameObject gameOverUI;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            FindObjectOfType<Score>().SaveScore();
            gameOverUI.SetActive(true);
            Invoke("ChangeCamera", changeCameraDelay);
            Invoke("ReturnToMenu", returnDelay);
        }
    }

    void ChangeCamera()
    {

        Camera camera = Camera.main;
        camera.depth = 0;
        RenderSettings.fog = false;

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
