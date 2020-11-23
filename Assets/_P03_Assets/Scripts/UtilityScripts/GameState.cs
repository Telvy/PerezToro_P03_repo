using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] AudioClip _gameMusic = null;
    [SerializeField] Text _shinyObjectsCollected = null;
    // Start is called before the first frame update

    int _currentSOCount;
    void Start()
    {
        GameMusic();
    }

    void Update()
    {
        exitGame();
    }

    void exitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Escape");
        }
    }

    void GameMusic()
    {
        OneShotSoundManager.PlayClip2D(_gameMusic, 1, true);
    }

    public void ShinyObjectScore(int Amount)
    {
        _currentSOCount += Amount;
        _shinyObjectsCollected.text = "Shiny Objects Collected: " + _currentSOCount.ToString();
    }

}
