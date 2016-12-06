using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void playGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
