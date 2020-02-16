using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    public StateManager stateManager;
	public ScoreManager scoreManager;
    public NotesManager notesManager;
    public GameObject[] win_charactor = new GameObject[2];
    public GameObject[] Shiflet_win = new GameObject[2];
    public GameObject[] Evan_win = new GameObject[2];
    public GameObject[] Amane_win = new GameObject[2];
    public GameObject[] Delta_win = new GameObject[2];
    public GameObject[] Kagura_win = new GameObject[2];
    public GameObject[] lose_charactor = new GameObject[2];
    public GameObject[] Shiflet_lose = new GameObject[2];
    public GameObject[] Evan_lose = new GameObject[2];
    public GameObject[] Amane_lose = new GameObject[2];
    public GameObject[] Delta_lose = new GameObject[2];
    public GameObject[] Kagura_lose = new GameObject[2];
    public GameObject[] WinnerSpotLight = new GameObject[2];
    public GameObject[] Win = new GameObject[2];
    public GameObject[] Lose = new GameObject[2];
    public GameObject Draw;
    public Transform[] chara_points = new Transform[2];

    public int[] result_charactor_number = new int[2];
    public bool[] result_chara_color = new bool[2];
    private bool check = false;



    void Shiflet(int player){
        int color = 0;
        if(result_chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        win_charactor[player - 1] = Shiflet_win[color];
        lose_charactor[player - 1] = Shiflet_lose[color];
    }

    void Evan(int player){
        int color = 0;
        if(result_chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        win_charactor[player - 1] = Evan_win[color];
        lose_charactor[player - 1] = Evan_lose[color];
    }

    void Amane(int player){
        int color = 0;
        if(result_chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        win_charactor[player - 1] = Amane_win[color];
        lose_charactor[player - 1] = Amane_lose[color];

    }
    void Delta(int player){
        int color = 0;
        if(result_chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        win_charactor[player - 1] = Delta_win[color];
        lose_charactor[player - 1] = Delta_lose[color];
    }

    void Kagura(int player){
        int color = 0;
        if(result_chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        win_charactor[player - 1] = Kagura_win[color];
        lose_charactor[player - 1] = Kagura_lose[color];

    }

    void Awake(){
        result_charactor_number[0] = CharacterSceneController.p1_number();
        result_charactor_number[1] = CharacterSceneController.p2_number();
        result_chara_color[0] = CharacterSceneController.p1_color();
        result_chara_color[1] = CharacterSceneController.p2_color();

            for (int player = 1; player <= 2; player ++)
            {

                switch(result_charactor_number[player - 1])
                {
                    case 0:
                        Shiflet(player);
                        break;
                    case 1:
                        Evan(player);
                        break;
                    case 2:
                        Amane(player);
                        break;
                    case 4:
                        Delta(player);
                        break;
                    case 5:
                        Kagura(player);
                        break;
                    default:
                        Debug.Log("The Charactor is not Selected.");
                        break;    
                }
            }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    void ResultJudge()
    {

        if (ScoreManager.score[0] > ScoreManager.score[1])
            {
                GameObject win_p1 = Instantiate(win_charactor[0]);
                Transform t = chara_points[0];
                win_p1.transform.position = t.position;
                win_p1.transform.localScale = t.localScale;
                win_p1.transform.localScale = new Vector3(1.5f, 1.5f, 1);

                GameObject lose_p2 = Instantiate(lose_charactor[1]);
                Transform u = chara_points[1];
                lose_p2.transform.position = u.position;
                lose_p2.transform.localScale = u.localScale;
                lose_p2.transform.localScale = new Vector3(-1, 1, 1);

                WinnerSpotLight[0].SetActive(true);
                Win[0].SetActive(true);
                Lose[1].SetActive(true);                  
            }

            else if (ScoreManager.score[0] < ScoreManager.score[1])
            {
                GameObject lose_p1 = Instantiate(lose_charactor[0]);
                Transform v = chara_points[0];
                lose_p1.transform.position = v.position;
                lose_p1.transform.localScale = v.localScale;

                GameObject win_p2 = Instantiate(win_charactor[1]);
                Transform w = chara_points[1];
                win_p2.transform.position = w.position;
                win_p2.transform.localScale = w.localScale;
                win_p2.transform.localScale = new Vector3(-1.5f, 1.5f, 1);

                WinnerSpotLight[1].SetActive(true);
                Win[1].SetActive(true);
                Lose[0].SetActive(true);                 
            }

            else
            {
                GameObject lose_p1 = Instantiate(lose_charactor[0]);
                Transform v = chara_points[0];
                lose_p1.transform.position = v.position;
                lose_p1.transform.localScale = v.localScale;

                GameObject lose_p2 = Instantiate(lose_charactor[1]);
                Transform u = chara_points[1];
                lose_p2.transform.position = u.position;
                lose_p2.transform.localScale = u.localScale;
                lose_p2.transform.localScale = new Vector3(-1, 1, 1);

                Draw.SetActive(true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "result")
        {
            if(check == false){
                ResultJudge();
                check = true;
            }
        }
    }
}
