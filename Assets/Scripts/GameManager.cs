using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
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

    public MessageWindow messageWindow;

    public Sprite loseIcon;
    public Sprite winIcon;
    public Sprite goalIcon;

    Board m_board;

    bool m_isReadyToBegin = false;
    bool m_isGameOver = false;
    bool m_isWinner = false;
    bool m_isReadyToReload = false;

    private void Start()
    {
        m_board = FindAnyObjectByType<Board>();

        Scene scene = SceneManager.GetActiveScene();

        if (levelNameText != null)
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

    public void BeginGame()
    {
        m_isReadyToBegin = true;
    }

    IEnumerator StartGameRoutine()
    {
        if (messageWindow != null)
        {
            messageWindow.GetComponent<RectXformMover>().MoveOnscreen();
            messageWindow.ShowMessage(goalIcon, "Score goal is\n" + scoreGoal + " points", "start!");
        }

        while (!m_isReadyToBegin)
        {
            yield return null;
        }

        movesLeftText.text = movesLeft.ToString();

        if (screenFader != null)
            screenFader.FadeOff();

        yield return new WaitForSeconds(0.5f);

        if (m_board != null)
            m_board.SetUpBoard();
    }

    IEnumerator PlayGameRoutine()
    {
        while (!m_isGameOver)
        {
            if (ScoreManager.Instance != null)
            {
                m_isGameOver = m_isWinner = ScoreManager.Instance.CurrentScore >= scoreGoal;
            }

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
        m_isReadyToReload = false;
        
        if (m_isWinner)
        {
            if (messageWindow != null)
            {
                messageWindow.GetComponent<RectXformMover>().MoveOnscreen();
                messageWindow.ShowMessage(winIcon, "YOU WIN!", "AWESOME!");
            }
            
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlayRandomWin();
        }
        else
        {
            if (messageWindow != null)
            {
                messageWindow.GetComponent<RectXformMover>().MoveOnscreen();
                messageWindow.ShowMessage(loseIcon, "YOU LOSE!", "BUMMER!");
            }
            
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlayRandomLose();
        }

        yield return new WaitForSeconds(1f);
        
        if (screenFader != null)
        {
            screenFader.FadeOn();
        }

        while (!m_isReadyToReload)
        {
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReloadScene()
    {
        m_isReadyToReload = true;
    }
}