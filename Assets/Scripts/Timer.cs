using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float countdown = 30f;
    public Text timeText;
    public string nextScene;

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;

        timeText.text = countdown.ToString("F0");

        if (countdown <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
