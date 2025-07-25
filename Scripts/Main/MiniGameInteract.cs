using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameInteract : MonoBehaviour
{
    public GameObject scorePanel;            // ScorePanel_Flapy
    public TextMeshProUGUI scoreText;        // 점수 텍스트
    public GameObject pressFIndicator;       // F 키 안내 텍스트

    private bool isGamerNear = false;
    private bool isPanelOpen = false;

    void Start()
    {
        scorePanel.SetActive(false);
        pressFIndicator.SetActive(false);
    }

    void Update()
    {
        if (isGamerNear && Input.GetKeyDown(KeyCode.F))
        {
            isPanelOpen = !isPanelOpen;
            scorePanel.SetActive(isPanelOpen);

            if (isPanelOpen)
            {
                int score = PlayerPrefs.GetInt("LastMiniGameScore", 0);
                scoreText.text = $"마지막 생존 점수: {score}";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isGamerNear = true;
            pressFIndicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isGamerNear = false;
            pressFIndicator.SetActive(false);
            scorePanel.SetActive(false);
            isPanelOpen = false;
        }
    }

}
