using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public int movesLeft = 30;
    public int scoreGoal = 10000;

    [FormerlySerializedAs("ScreenFader")] public ScreenFader screenFader;
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI movesLeftText;
    
    Board m_board;
    
    bool m_isReadyToBegin = false;
    bool m_isGameOver = false;
    bool m_isWinner = false;

    private void Start()
    {
        m_board = FindAnyObjectByType<Board>();

        Scene scene = SceneManager.GetActiveScene();
        
        if(levelNameText != null)
        {
            levelNameText.text = scene.name;
        }

        StartCoroutine(ExecuteGameLoop());
    }
    
    IEnumerator ExecuteGameLoop()
    {
        yield return StartCoroutine(StartGameRoutine());
        yield return StartCoroutine(PlayGameRoutine());
        yield return StartCoroutine(EndGameRoutine());
    }

    public void UpdateMoves()
    {
        if (movesLeftText)
            movesLeftText.text = movesLeft.ToString();
    }
    
    IEnumerator StartGameRoutine()
    {
        while (!m_isReadyToBegin)
        {
            yield return null;
            yield return new WaitForSeconds(2f);
            m_isReadyToBegin = true;
        }
        
        movesLeftText.text = movesLeft.ToString();
        
        if(screenFader != null)
            screenFader.FadeOff();

        yield return new WaitForSeconds(0.5f);
        
        if(m_board != null)
            m_board.SetUpBoard();
        
    }
    
    IEnumerator PlayGameRoutine()
    {
        while (!m_isGameOver)
        {
            if (movesLeft == 0)
            {
                m_isGameOver = true;
                m_isWinner = false;
            }

            yield return null;
        }
        yield return null;
    }
    
    IEnumerator EndGameRoutine()
    {
        if (screenFader != null)
        {
            screenFader.FadeOn();
        }
        
        Debug.Log(m_isWinner ? "You win!" : "You lose!");
        yield return null;
    }
}
 