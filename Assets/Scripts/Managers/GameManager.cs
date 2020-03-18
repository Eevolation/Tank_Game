using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class GameManager : MonoBehaviour
{
    // Reference to the enemy Text to display winning text, etc
    public Text m_MessageText;
    public Text m_TimerText;

    public GameObject[] m_Tanks;

    public float playerDamage;
    public float enemyDamage;

    private float m_gameTime = 0;
    public float GameTime 
    {
        get 
        {
            return m_gameTime; 
        } 
    }
    
    public enum GameState
    {
        Level_Start, 
        Level_Playing, 
        GameOver,
        Level2,
    };


    private GameState m_GameState;
    public GameState State 
    { 
        get 
        { 
            return m_GameState; 
        } 
    }

    private void Awake()
    {
        
        m_GameState = GameState.Level_Start;
    }

    private void Start()
    {
        for(int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "GetReady";
    }

    private void Update()
    {
        playerDamage = PlayerShell.damage;

        switch (m_GameState)
        {
            case GameState.Level_Start:
                if(Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Level_Playing;

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
            case GameState.Level_Playing:
                bool Level1isGameOver = false;

                m_gameTime += Time.deltaTime;
                int level1seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (level1seconds / 60), (level1seconds % 60));
                
                if(OneTankLeft() == true)
                {
                    Level1isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    Level1isGameOver = true;
                }
                if(Level1isGameOver == true)
                {
                    m_GameState = GameState.GameOver;

                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "you died try again";
                    }
                    else
                    {
                        m_MessageText.text = "into the dungeon!";
                        Application.LoadLevel(1);
                        m_GameState = GameState.Level2;
                    }
                }
                break;
            case GameState.GameOver:
                if(Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Level_Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);
                    Application.LoadLevel(0);

                    for(int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
            case GameState.Level2:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    bool Level2isGameOver = false;

                    m_gameTime += Time.deltaTime;
                    int level2seconds = Mathf.RoundToInt(m_gameTime);
                    m_TimerText.text = string.Format("{0:D2}:{1:D2}", (level2seconds / 60), (level2seconds % 60));

                    if (OneTankLeft() == true)
                    {
                        Level2isGameOver = true;
                    }
                    else if (IsPlayerDead() == true)
                    {
                        Level2isGameOver = true;
                    }
                    if (Level2isGameOver == true)
                    {
                        m_GameState = GameState.GameOver;

                        if (IsPlayerDead() == true)
                        {
                            m_MessageText.text = "you died try again";
                        }
                        else
                        {
                            m_MessageText.text = "Victory";
                        }
                    }
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for(int i = 0; i < m_Tanks.Length;  i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    bool IsPlayerDead()
    {
        for(int i = 0; i < m_Tanks.Length; i++)
        {
            if(m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }
}
