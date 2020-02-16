using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour

{

    public ArduinoButtons player1controller;
    public ArduinoButtons player2controller;

    private AudioSource audioSource;
    public AudioClip goSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool GetControllerDown(int number)
    {   
        return (player1controller && player1controller.GetButtonDown(number)) || (player2controller && player2controller.GetButtonDown(number));
    }

    void StartGame(){
        SceneManager.LoadScene("CharacterSelectScene");
    }

    // Update is called once per frame
    void Update()
    {
        for(int number = 0; number < 5; number ++){
            if(Input.GetKeyDown(KeyCode.Space) || GetControllerDown(number)){
                audioSource.PlayOneShot(goSound);
               Invoke("StartGame", 2.0f);
            }
        }
    }
}
