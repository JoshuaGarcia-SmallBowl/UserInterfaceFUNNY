using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 14;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    private gameManager gameManager;
    public ParticleSystem particles;
    public GameObject spawnPrefab;
    private int dodgeChance = 1;
    //characteristic

    public int points = 1;
    public int health = 1;
    public float startthrow;
    public bool split;
    public int damage = 1;
    public bool dodge ;
    public int healAmt = 0;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce() * startthrow, ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        if (startthrow == 1.0f)
        {
            transform.position = RandomSpawnPos();
        }
        
        gameManager = GameObject.Find("Manager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
        
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        if (gameManager.active)
        {
            //subtract health by one
            health--;
            //destroy if no more health is left
            if (health == 0)
            {
                if (healAmt > 0)
                {
                    
                    gameManager.gameOver(-healAmt);
                }
                Destroy(gameObject);
                gameManager.updateScore(points);
                Instantiate(particles, transform.position, particles.transform.rotation);
                //If a splitter, split
                if (split)
                {
                    Split();
                }
                //otherwise go through every check

            }
            else
            {
                targetRb.velocity = Vector3.zero;
                targetRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
                if (dodge)
                {
                    float xtp = Random.Range(3, 4);
                    if (dodgeChance == Random.Range(1, 2))
                    {
                        xtp *= -1;
                    }
                    float goby = transform.position.x + xtp;
                    if (goby > -5 &&  goby < 5)
                    {
                        transform.position = new(goby, transform.position.y, transform.position.z);
                        //Debug.Log("Dodge normal; goby: " + transform.position.x);
                    }
                    else if (goby < -5)
                    {
                        
                        transform.position = new(goby + 5, transform.position.y, transform.position.z);
                        Debug.Log("Goby below -5: initial position was: " + goby + " New position is: " + transform.position.x);
                    }
                    else
                    {
                        
                        transform.position = new(goby - 5, transform.position.y, transform.position.z);
                        Debug.Log("Goby above 5: initial position was: " + goby + " New position is: " + transform.position.x);

                    }
                }
            }
        }         
    }
        private void OnTriggerEnter(Collider other)
        {
       if (other.CompareTag("end"))
        {
            Destroy(gameObject);
            if (!gameObject.CompareTag("bad"))
            {
                gameManager.gameOver(damage);
            }
        }
    }

        private void Split()
        {
            Instantiate(spawnPrefab, new(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(spawnPrefab, new(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
        }
    
}
