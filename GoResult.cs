using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GoResult : MonoBehaviour
{


    public GameObject mimikun;
    public GameObject[] URA;

    public bool[] check = new bool[4];

    public ArduinoButtons player1controller;
    public ArduinoButtons player2controller;
    public bool finish = false;

    void ToResult(){
        SceneManager.LoadScene("result");
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ToResult", Random.Range(5.0f, 8.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(check[2] == true){
            if(Input.GetKeyDown(KeyCode.B) || player1controller.GetButtonDown(0) || player2controller.GetButtonDown(0)){
                check[3] = true;
            }
            else if(player1controller.GetButtonDown(1) || player2controller.GetButtonDown(1)
                 || player1controller.GetButtonDown(2) || player2controller.GetButtonDown(2)
                 || player1controller.GetButtonDown(3) || player2controller.GetButtonDown(3)
                 || player1controller.GetButtonDown(4) || player2controller.GetButtonDown(4)){
                check[2] = false;
                check[1] = false;
                check[0] = false;
            }
        }
        else if(check[1] == true){
            if(Input.GetKeyDown(KeyCode.A) || player1controller.GetButtonDown(2) || player2controller.GetButtonDown(2)){
                check[2] = true;
            }
            else if(player1controller.GetButtonDown(0) || player2controller.GetButtonDown(0)
                 || player1controller.GetButtonDown(1) || player2controller.GetButtonDown(1)
                 || player1controller.GetButtonDown(3) || player2controller.GetButtonDown(3)
                 || player1controller.GetButtonDown(4) || player2controller.GetButtonDown(4)){
                check[1] = false;
                check[0] = false;
            }
        }
        else if(check[0] == true){
            if(Input.GetKeyDown(KeyCode.RightArrow) || player1controller.GetButtonDown(3) || player2controller.GetButtonDown(3)){
                check[1] = true;
            }
            else if(player1controller.GetButtonDown(0) || player2controller.GetButtonDown(0)
                 || player1controller.GetButtonDown(1) || player2controller.GetButtonDown(1)
                 || player1controller.GetButtonDown(2) || player2controller.GetButtonDown(2)
                 || player1controller.GetButtonDown(4) || player2controller.GetButtonDown(4)){
                check[0] = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || player1controller.GetButtonDown(1) || player2controller.GetButtonDown(1)){
            check[0] = true;
        }

        if(check[3] == true){
            if(finish == false){
                mimikun.SetActive(false);
                URA[Random.Range(0, URA.Length)].SetActive(true);
                finish = true;
            }
        }
    }
}
