using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Button startBtn;
  


    // Use this for initialization
    void Start()
    {

        startBtn = GameObject.Find("Button_Start").GetComponent<Button>();
      
     

        startBtn.onClick.AddListener(StartGame);
      

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
       
        SceneManager.LoadScene("Megaman");
        Time.timeScale = 1.0f;
    }

  

}
