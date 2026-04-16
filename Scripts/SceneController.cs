using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string nextSceneName = "DroughtScene";

    // 👇 THIS METHOD NAME MATCHES NextZoneTrigger
    public void GoToNextScene()
    {
        Debug.Log("SceneController: Loading " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
