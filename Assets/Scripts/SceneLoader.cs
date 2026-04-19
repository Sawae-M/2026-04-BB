using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
