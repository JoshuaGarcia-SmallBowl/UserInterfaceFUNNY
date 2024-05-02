using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject[] nmTargets;
    private bool nightmare = false;
    public GameObject pauseScreen;
    private bool paused;
    public AudioClip funny;
    private AudioSource source;

    private float spawnRate = 1.0f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool active = false;
    private int health = 5;
    public GameObject title;
    private int nmChance = 1;
    public int nmCap = 10;
    private int nmCooldown = 3;
    public GameObject nmBackGround;
    public GameObject nmFrontGround;
    private bool allNm;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
        {
            ChangePaused();
        } 
    }

    IEnumerator SpawnTarget()
    {
        while (active)
        {
            if (allNm)
            {
                yield return new WaitForSeconds(spawnRate);
                int nmIndex = Random.Range(0, nmTargets.Length);
                Instantiate(nmTargets[nmIndex]);
            }
            else
            {
                yield return new WaitForSeconds(spawnRate);
                int index = Random.Range(0, targets.Length);
                int nmIndex = Random.Range(0, nmTargets.Length);

                if (nightmare)
                {
                    if (nmChance == Random.Range(1, nmCap) && nmCooldown == 0)
                    {
                        Instantiate(nmTargets[nmIndex]);
                        nmCap = 10;
                        nmCooldown = 11;
                    }
                    else
                    {
                        if (nmCooldown <= 10)
                        {
                            Instantiate(targets[index]);
                            nmCap--;
                        }

                        if (nmCooldown != 0)
                        {
                            nmCooldown--;
                        }
                    }
                }
                else
                {
                    Instantiate(targets[index]);
                }
            }
        }
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void gameOver(int val)
    {
        
        
            
        
        health -= val;
        
        if (health == 0)
        {
            gameOverText.gameObject.SetActive(true);
            active = false;
        }
        livesText.text = "Lives: " + health;
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty, bool nm, bool all)
    {
        spawnRate *= difficulty;
        
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        
        Destroy(title);
        if (nm)
        {
            source.Pause();
            source.clip = funny;
            source.Play();
            nightmare = true;
            Destroy(nmFrontGround); 
            nmBackGround.gameObject.SetActive(true);
        }
        if (all)
        {
            allNm = true;
        }
        active = true;
        StartCoroutine(SpawnTarget());
    }

    void ChangePaused()
    {
        if (active)
        {
            if (!paused)
            {
                paused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
       
    }

}
