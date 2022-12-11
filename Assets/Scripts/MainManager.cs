using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    private string m_Name;
    private int m_Points;
    private bool m_Started = false;
    private bool m_GameOver = false;
    
    void Start()
    {
        // Set player name 
        m_Name = GameManager.Instance.PlayerName;

        // Set high score text
        SetHighScoreText(GameManager.Instance.HighScoreName, GameManager.Instance.HighScorePoints);

        // Spawn Bricks
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.LoadHighScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if(m_Points > GameManager.Instance.HighScorePoints)
        {
            SetHighScoreText(m_Name, m_Points);
            GameManager.Instance.SaveHighScore(m_Name, m_Points);
        }
    }

    public void SetHighScoreText(string name, int points)
    {
        HighScoreText.text = $"Best Score: {name}: {points}";
    }
}
