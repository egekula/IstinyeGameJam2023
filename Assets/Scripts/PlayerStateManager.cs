using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinText;
    public int score = 0;


    private void Awake()
    {
        int findPlayerStateManager = FindObjectsOfType<PlayerStateManager>().Length;
        if (findPlayerStateManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        coinText.text = score.ToString();

    }

    
    public void ProcessOfPlayerDeath()
    {
        if (playerLives>1)
        {
            StartCoroutine("TakeDeathForTime");
        }
        else
        {
            StartCoroutine("ResetGameSessionForTime");
        }
    }

    private void TakeDeath()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    public void TakeCoin(int points)
    {
        score += points;
        coinText.text = score.ToString();
    }

    IEnumerator TakeDeathForTime()
    {
        yield return new WaitForSeconds(1f);
        TakeDeath();
    }

    IEnumerator ResetGameSessionForTime()
    {
        yield return new WaitForSeconds(1f);
        ResetGameSession();
        
    }
    
}
