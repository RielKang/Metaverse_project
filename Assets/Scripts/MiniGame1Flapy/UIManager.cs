using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject restartPanel;
    public GameObject restartButton;
    public GameObject returnButton;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        if (restartPanel == null)
        {
            Debug.LogError("restartPanel is null");
            return;
        }

        if (restartButton == null)
            Debug.LogWarning("restartButton is not assigned");
        if (returnButton == null)
            Debug.LogWarning("returnButton is not assigned");

        restartPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }

    public void HideRestartPanel()
    {
        restartPanel.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}