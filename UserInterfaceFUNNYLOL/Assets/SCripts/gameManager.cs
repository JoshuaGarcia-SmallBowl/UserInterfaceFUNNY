using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public GameObject[] targets;

    private float spawnRate = 1.0f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);    
        }
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
