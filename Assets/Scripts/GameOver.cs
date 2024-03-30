using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI GameOverReason;
    public TextMeshProUGUI ScoreField;

    public void Init(int score)
    {
        gameObject.SetActive(true);
        if (score == 10)
        {
            GameOverReason.text = "You have successfully collected 10 pickups, which is awesome.";
        }
        else
        {
            GameOverReason.text = "You touch a pickup with a different color. That's so sad.";
        }
        ScoreField.text = $"Your score: {score}";
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
