using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private string gameoverSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Out");
            SceneManager.LoadScene(gameoverSceneName);
        }
    }
}
