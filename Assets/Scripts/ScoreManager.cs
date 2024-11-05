using System;
using System.Collections;
using TMPro;

namespace DefaultNamespace
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        int m_currentScore = 0;
        private int m_counterValue = 0;
        private int m_increment = 1;
        
        public TextMeshProUGUI scoreText;

        private void Start()
        {
            UpdateScoreText(m_currentScore);
        }

        public void UpdateScoreText(int scoreValue)
        {
            scoreText.text = scoreValue.ToString();
        }

        public void AddScore(int value)
        {
            m_currentScore += value;
            StartCoroutine(CountScoreRoutine());
        }

        IEnumerator CountScoreRoutine()
        {
            int iterations = 0;

            while (m_counterValue < m_currentScore && iterations < 100000)
            {
                m_counterValue += m_increment;
                UpdateScoreText(m_counterValue);
                iterations++;
                yield return null;
            }
            
            m_counterValue = m_currentScore;
            UpdateScoreText(m_counterValue);
        }
    }
}