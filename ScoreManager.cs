using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;

public class ScoreManager : MonoBehaviour {

	
	public MusicClipManager clipManager;
    public StateManager stateManager;

	public static int[] score = new int[2];

	public static int[] combo = new int[2];

	public static int[] MaxCombo = new int[2];

	public static int[] perfectcount = new int[2];

	public static int[] greatcount = new int[2];

	public static int[] goodcount = new int[2];

	public static int[] misscount = new int[2];

    public int perfectScore = 1000;
	public int greatScore = 500;
	public int goodScore = 250;
	public int[] missScore = new int[2];

	public int scoreUpRate = 100;

    public GameObject P1win;
	public GameObject P2win;
	public GameObject Draw;

	public Text[] scoreLavel = new Text[2];

	public Text[] comboLavel = new Text[2];

	public Text[] MaxComboLavel = new Text[2];

	public Text[] perfectLavel = new Text[2];
	public Text[] greatLavel = new Text[2];
    public Text[] goodLavel = new Text[2];
	public Text[] missLavel = new Text[2];

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
