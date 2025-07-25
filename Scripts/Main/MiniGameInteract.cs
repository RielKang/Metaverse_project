using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameInteract : MonoBehaviour
{
    public GameObject scorePanel;            // ScorePanel_Flapy
    public TextMeshProUGUI scoreText;        // ���� �ؽ�Ʈ
    public GameObject pressFIndicator;       // F Ű �ȳ� �ؽ�Ʈ

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
                scoreText.text = $"������ ���� ����: {score}";
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
