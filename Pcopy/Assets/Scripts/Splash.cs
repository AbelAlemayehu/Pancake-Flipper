using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public string nextScene = "MainScene"; // <-- change to your game scene name

    void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(nextScene);
    }
}
