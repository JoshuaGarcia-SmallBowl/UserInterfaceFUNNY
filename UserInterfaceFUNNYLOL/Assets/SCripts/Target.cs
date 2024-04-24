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

    //characteristic

    public int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
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
        //subtract health by one
        health--;
        //destroy if no more health is left
        if (health == 0)
        {
            Destroy(gameObject);
        }
        //otherwise go through every check
        else
        {
            targetRb.velocity = Vector3.zero;
            targetRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
