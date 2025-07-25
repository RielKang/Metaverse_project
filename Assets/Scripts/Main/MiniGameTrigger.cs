using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject interactUI;
    public string miniGameSceneName = "MiniGameScene";
    private bool canInteract = false;

    void Start()
    {
        interactUI.SetActive(false); // Ã³À½¿£ ¼û°ÜµÒ
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactUI.SetActive(true);
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
            
            canInteract = false;
        }
    }
}


