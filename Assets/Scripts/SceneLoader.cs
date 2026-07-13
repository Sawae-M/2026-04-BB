using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public string nextSceneName;

    public void OnClick()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    private System.Collections.IEnumerator PlaySoundAndChangeScene()
    {
        audioSource.PlayOneShot(clickSound);
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene(nextSceneName);
    }
}
