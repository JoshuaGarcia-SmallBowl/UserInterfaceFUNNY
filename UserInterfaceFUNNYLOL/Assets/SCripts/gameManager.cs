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

    private float spawnRate = 1.0f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool active = false;
    private int health = 5;
    public GameObject title;
    private int nmChance = 1;
    public int nmCap = 10;
    private int nmCooldown = 3;
    public GameObject nmBackGround;
    public GameObject nmFrontGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (active)
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
                    nmCooldown = 10;
                }
                else
                {
                    Instantiate(targets[index]);
                    nmCap--;
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
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty, bool nm)
    {
        spawnRate *= difficulty;
        active = true;
        scoreText.gameObject.SetActive(true);
        StartCoroutine(SpawnTarget());
        Destroy(title);
        if (nm)
        {
            nightmare = true;
            Destroy(nmFrontGround); 
            nmBackGround.gameObject.SetActive(true);
        }
    }

}
