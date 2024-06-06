using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName="TrishiaLabelingScene";

    // This method will be called when the button is clicked
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
