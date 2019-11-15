using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject obstacle1;
  

    public int score;
    public Text scoreUI;
    // Use this for initialization
    void Start()
    {
        // Can do CoRoutine, Or Time Stamp Methods

        InvokeRepeating("CreateObstacle", 3.0f, 1.5f);
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CreateObstacle()
    {

        Instantiate(obstacle1, transform.position, Quaternion.identity);
      yield return new WaitForSeconds(5); 

        
    }

}
