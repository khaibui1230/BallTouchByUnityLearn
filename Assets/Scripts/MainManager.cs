using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TMP_Text BestScore;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject InputField;
    public TMP_InputField BestPlayerName;

    

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        BestScore.text = $"Best Score : {PlayerPrefs.GetString("PlayerName")} : {PlayerPrefs.GetInt("BestScore")}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
                AddBestScore();
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                AddBestScore();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";   

    }
    void AddBestScore()
    {
        int saveBestScore = PlayerPrefs.GetInt("BestScore", 0);
        //neu diem nguoi choi cao hon diem da luu th�
        if(m_Points > saveBestScore)
        {
            PlayerPrefs.SetInt("BestScore", m_Points);
            PlayerPrefs.SetString("PlayerName", BestPlayerName.text);
            
        }
        
    }
    // ham giup reset diem nguoi choi
    public void ResetTheScore()
    {
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.DeleteKey("PlayerName");
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        InputField.SetActive(true);
       
    }
}
