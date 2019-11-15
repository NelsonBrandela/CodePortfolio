using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    bool paused;
    CanvasGroup cg;
    Button titleScreen;
    Button resumeGame;

    AudioSource audioSource;
    public AudioClip sfxPause;

    // Use this for initialization
    void Start()
    {
        GameObject temp = GameObject.Find("Button_Resume");
        if (temp)
        {
            resumeGame = temp.GetComponent<Button>();
        }

        titleScreen = GameObject.Find("Button_Menu").GetComponent<Button>();
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();

        }
        cg.alpha = 0.0f;

      

        resumeGame.onClick.AddListener(Resume);
        titleScreen.onClick.AddListener(Quit);

        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1.0f;

            Debug.Log("No Animator found on " + name);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // if (Application.Platform == RuntimePlatform.WindowsPlayer;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            PauseGame();
            paused = true;
        }

     

    }

    public void PauseGame()
    {
        if (cg.alpha == 0.0f)
        {
            //PlaySound(sfxPause);
            cg.alpha = 1.0f;
            Time.timeScale = 0;
        }

        else
        {
           
            cg.alpha = 0.0f;
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        if (paused == true)
        {
            SceneManager.LoadScene("Title");
            Time.timeScale = 1.0f;
        }

    }



   public void Resume()
    {
       
        if (cg.alpha == 1.0f)
        {
            cg.alpha = 0.0f;
            Time.timeScale = 1;
        }

    }


    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

}
