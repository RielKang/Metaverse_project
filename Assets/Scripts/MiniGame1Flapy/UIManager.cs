using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public GameObject restartPanel;
    public GameObject restartButton;
    public GameObject returnButton;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Ȥ�� �ߺ� ���� ����
        }

        restartPanel?.SetActive(false);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //  �� �ε� �̺�Ʈ ���
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //  �̺�Ʈ ���� (�޸� ���� ����)
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        Canvas gameCanvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
        if (gameCanvas != null)
        {
            gameCanvas.gameObject.SetActive(true); //  ĵ���� ���ֱ�
        }
    }

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
        
        Debug.Log("ShowRestartPanel() ȣ���");
        if (restartPanel != null)
        {
            restartPanel.SetActive(true);
            Debug.Log("RestartPanel Ȱ��ȭ �Ϸ�");
        }
        else
        {
            Debug.LogWarning("restartPanel�� ������� �ʾҽ��ϴ�!");
        }


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