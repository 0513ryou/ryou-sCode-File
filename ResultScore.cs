using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{

    public Text[] result_scoreLavel = new Text[2];
	public Text[] result_comboLavel = new Text[2];
	public Text[] result_perfectLavel = new Text[2];
	public Text[] result_greatLavel = new Text[2];
    public Text[] result_goodLavel = new Text[2];
	public Text[] result_missLavel = new Text[2];

    public GameObject[] FullCombo = new GameObject[2];


    public int P1Score(){
        return ScoreManager.score[0];
    }
    public int P2Score(){
        return ScoreManager.score[1];
    }
    public int P1Combo(){
        return ScoreManager.MaxCombo[0];
    }
    public int P2Combo(){
        return ScoreManager.MaxCombo[1];
    }
    public int P1Perfect(){
        return ScoreManager.perfectcount[0];
    }
    public int P2Perfect(){
        return ScoreManager.perfectcount[1];
    }
    public int P1Great(){
        return ScoreManager.greatcount[0];
    }
    public int P2Great(){
        return ScoreManager.greatcount[1];
    }
    public int P1Good(){
        return ScoreManager.goodcount[0];
    }
    public int P2Good(){
        return ScoreManager.goodcount[1];
    }
    public int P1Miss(){
        return ScoreManager.misscount[0];
    }
    public int P2Miss(){
        return ScoreManager.misscount[1];
    }

    void ResultScoreDisplay(){
        result_scoreLavel[0].text = P1Score().ToString();
        result_scoreLavel[1].text = P2Score().ToString();
        result_comboLavel[0].text = P1Combo().ToString();
        result_comboLavel[1].text = P2Combo().ToString();
        result_perfectLavel[0].text = P1Perfect().ToString();
        result_perfectLavel[1].text = P2Perfect().ToString();
        result_greatLavel[0].text = P1Great().ToString();
        result_greatLavel[1].text = P2Great().ToString();
        result_goodLavel[0].text = P1Good().ToString();
        result_goodLavel[1].text = P2Good().ToString();
        result_missLavel[0].text = P1Miss().ToString();
        result_missLavel[1].text = P2Miss().ToString();
    }

    void FullComboDisplay(){
        if ((P1Combo() != 0) && (P1Good() == 0) && (P1Miss() == 0)){
            FullCombo[0].SetActive(true);
        }
        if ((P2Combo() != 0) && (P2Good() == 0) && (P2Miss() == 0)){
            FullCombo[1].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResultScoreDisplay();
        FullComboDisplay();
    }
}
