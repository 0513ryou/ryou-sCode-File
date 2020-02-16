using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using System.Linq;
using UnityEngine.SceneManagement;

public class NotesManager : MonoBehaviour
{
    public static int note_count = 16;
    public int NoteCount = note_count;


    public GameObject[] charactor_default = new GameObject[2];
    public GameObject[] Shiflet_default = new GameObject[2];
    public GameObject[] Evan_default = new GameObject[2];
    public GameObject[] Amane_default = new GameObject[2];
    public GameObject[] Delta_default = new GameObject[2];
    public GameObject[] Kagura_default = new GameObject[2];

    public ArduinoButtons player1controller;
    public ArduinoButtons player2controller;

    private static List<NotesManager> instances = new List<NotesManager>();


    [System.Serializable]
    public class Note
    {
        public int p1;
        public int p2;
        public KeyCode p1_key;
        public KeyCode p2_key;
        public int p1_button;
        public int p2_button;
        public AudioClip sound;
        public GameObject bottom_button;
        public GameObject prefab;
        public GameObject[] Shiflet;
        public GameObject[] Evan;
        public GameObject[] Amane;
        public GameObject[] Delta;
        public GameObject[] Kagura;
        public GameObject[] charactor;

        public Note() 
        {
            charactor = new GameObject[2];
            Shiflet = new GameObject[2];
            Evan = new GameObject[2];
            Amane = new GameObject[2];
            Delta = new GameObject[2];
            Kagura = new GameObject[2];
        }
    }

    public AudioClip perfectSound;
    public AudioClip greatSound;
    public AudioClip goodSound;
    public AudioClip missSound;

    public Transform[] chara_points = new Transform[2];

    public Note[] MIDI_notes = new Note[5];
    private int[,] note_entry = new int[2, note_count];
    private int[,] mimic_entry = new int[2, note_count];
    private int[,] note_miss = new int[2, note_count]; // -1: 未処理, 0: OK, 1: ミス

    private GameObject[,] note_objects = new GameObject[2, note_count];
    private GameObject[] default_object = new GameObject[2];
    private GameObject chara_object;
    private GameObject miss_object;

    public Transform spawn_points;
    private Vector3[,] spawn_position = new Vector3[2, note_count];

    public GameObject[] miss_charactor = new GameObject[2];
    public GameObject[] Shiflet_miss = new GameObject[2];
    public GameObject[] Evan_miss = new GameObject[2];
    public GameObject[] Amane_miss = new GameObject[2];
    public GameObject[] Delta_miss = new GameObject[2];
    public GameObject[] Kagura_miss = new GameObject[2];

    public MusicClipManager clipManager;
    public StateManager stateManager;
	public ScoreManager scoreManager;
    public ResultManager resultManager;

	private AudioSource audioSource;
    
    public GameObject[] Judge = new GameObject[4];
    public GameObject Circle;
    private GameObject[,] judge_objects = new GameObject[2, note_count];
    private GameObject[,] CircleAnimation = new GameObject[2, note_count];

    public int[] charactor_number = new int[2];
    public bool[] chara_color = new bool[2];


    void OnEnable()
    {
        instances.Add(this);
    }

    void OnDisable()
    {
        instances.Remove(this);
    }


    void Shiflet(int player, Note note){
        int color = 0;
        if(chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        note.charactor[player - 1] = note.Shiflet[color];
        charactor_default[player - 1] = Shiflet_default[color];
        miss_charactor[player - 1] = Shiflet_miss[color];    
    }
    void Evan(int player, Note note){
        int color = 0;
        if(chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        note.charactor[player - 1] = note.Evan[color];
        charactor_default[player - 1] = Evan_default[color];
        miss_charactor[player - 1] = Evan_miss[color];
    }

    void Amane(int player, Note note){
        int color = 0;
        if(chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        note.charactor[player - 1] = note.Amane[color];
        charactor_default[player - 1] = Amane_default[color];
        miss_charactor[player - 1] = Amane_miss[color];
    }
    void Delta(int player, Note note){
        int color = 0;
        if(chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        note.charactor[player - 1] = note.Delta[color];
        charactor_default[player - 1] = Delta_default[color];
        miss_charactor[player - 1] = Delta_miss[color];
    }
    void Kagura(int player, Note note){
        int color = 0;
        if(chara_color[player - 1] == false)
        {
            color = 0;
        }
        else
        {
            color = 1;
        }
        note.charactor[player - 1] = note.Kagura[color];
        charactor_default[player - 1] = Kagura_default[color];
        miss_charactor[player - 1] = Kagura_miss[color];
    }


    void Awake(){

        charactor_number[0] = CharacterSceneController.p1_number();
        charactor_number[1] = CharacterSceneController.p2_number();
        chara_color[0] = CharacterSceneController.p1_color();
        chara_color[1] = CharacterSceneController.p2_color();

        for (int note_number = 0; note_number < MIDI_notes.Length; note_number ++)
        {
            Note note = MIDI_notes[note_number];
            for (int player = 1; player <= 2; player ++)
            {

                switch(charactor_number[player - 1])
                {
                    case 0:
                        Shiflet(player, note);
                        break;
                    case 1:
                        Evan(player, note);
                        break;
                    case 2:
                        Amane(player, note);
                        break;
                    case 4:
                        Delta(player, note);
                        break;
                    case 5:
                        Kagura(player, note);
                        break;
                    default:
                        Debug.Log("The Charactor is not Selected.");
                        break;                                              
                }
            }
        }
    }


    void Default_P1(){
        GameObject def0 = Instantiate(charactor_default[0]);
        Transform t = chara_points[0];
        def0.transform.position = t.position;
        def0.transform.localScale = t.localScale;
        default_object[0] = def0;     
    }

    void Default_P2(){
        GameObject def1 = Instantiate(charactor_default[1]);
        Transform u = chara_points[1];
        def1.transform.position = u.position;
        def1.transform.localScale = u.localScale;
        def1.transform.localScale = new Vector3(-1, 1, 1);
        default_object[1] = def1;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int player = 0;
        foreach (Transform p in spawn_points)
        {
            int index = 0;
            foreach (Transform spawn in p)
            {
                spawn_position[player, index] = spawn.position;
                index++;
            }
            player++;
        }

            Default_P1();
            Default_P2();

        DontDestroyOnLoad(this);
    }

    bool GetNoteDown(int player, Note note)
    {
        if (player == 1)
        {
            return MidiMaster.GetKeyDown(note.p1) || Input.GetKeyDown(note.p1_key) || (player1controller && player1controller.GetButtonDown(note.p1_button));
        }
        else
        {
            return MidiMaster.GetKeyDown(note.p2) || Input.GetKeyDown(note.p2_key) || (player2controller && player2controller.GetButtonDown(note.p2_button));
        }
    }

    bool GetNoteUp(int player, Note note)
    {
        if (player == 1)
        {
            return MidiMaster.GetKeyUp(note.p1) || Input.GetKeyUp(note.p1_key) || (player1controller && player1controller.GetButtonUp(note.p1_button));
        }
        else
        {
            return MidiMaster.GetKeyUp(note.p2) || Input.GetKeyUp(note.p2_key) || (player2controller && player2controller.GetButtonUp(note.p2_button));
        }
    }

    StateManager.GameState state;

    void DeleteInfo(int player, int index) {

        int other_player = state.player == 1 ? 2 : 1;

        note_entry[player - 1, index] = -1;

        Destroy(note_objects[player - 1, index]);
        note_objects[player - 1, index] = null;
        Destroy(judge_objects[player - 1, index]);
        judge_objects[player - 1, index] = null;
        Destroy(judge_objects[other_player - 1, index]);
        judge_objects[other_player - 1, index] = null;
    }

    void DesplayJudge(int player, GameObject judge, int grid_index)
    {
        GameObject jud = Instantiate(judge);
        jud.transform.position = spawn_position[player - 1, grid_index];
        judge_objects[player - 1, grid_index] = jud;
        jud.transform.Translate(0, 0.25f, 0);
    }

    // 判定とその表示
    void NoteJudge(int player, int grid_index, float judge_time)
    {

        float perfect_Time = 0.10f;
        float great_Time = 0.19f;

        int scoreUpSpan = 10;

        if(clipManager.GetClosestGridIndex() <= stateManager.gameendgrid )
        {
            
            if(judge_time <= perfect_Time && judge_time >= (- perfect_Time))
            {
                GameObject judge = Judge[0];
                ScoreManager.score[player - 1] += scoreManager.perfectScore;
                ScoreManager.perfectcount[player - 1] += 1;
                ScoreManager.combo[player - 1] += 1;
                DesplayJudge(player, judge, grid_index);
                audioSource.PlayOneShot(perfectSound);
            }
            else if(judge_time <= great_Time && judge_time >= (- great_Time))
            {
                GameObject judge = Judge[1];
                ScoreManager.score[player - 1] += scoreManager.greatScore;
                ScoreManager.greatcount[player - 1] += 1;
                ScoreManager.combo[player - 1] += 1;
                DesplayJudge(player, judge, grid_index);
                audioSource.PlayOneShot(greatSound);
            }
            else
            {
                GameObject judge = Judge[2];
                ScoreManager.score[player - 1] += scoreManager.goodScore;
                ScoreManager.goodcount[player - 1] += 1;
                ScoreManager.combo[player - 1] = 0;
                DesplayJudge(player, judge, grid_index);
                audioSource.PlayOneShot(goodSound);
            }

            for(int i = 0; i < 100; i ++)
            {
                if(ScoreManager.combo[player - 1] > i * scoreUpSpan)
                {
                    ScoreManager.score[player - 1] += i * scoreManager.scoreUpRate;
                }
            }

        
            if(ScoreManager.combo[player - 1] >= ScoreManager.MaxCombo[player - 1])
            {
                ScoreManager.MaxCombo[player - 1] = ScoreManager.combo[player - 1];
            }
		}
    }

        // 簡単にまとめたい
    public int Score(int player)
    {
        return ScoreManager.score[player - 1];
    }
    public int Combo(int player)
    {
        return ScoreManager.combo[player - 1];
    }
    public int Max(int player)
    {
        return ScoreManager.MaxCombo[player - 1];
    }
    public int Perfect(int player)
    {
        return ScoreManager.perfectcount[player - 1];
    }
    public int Great(int player)
    {
        return ScoreManager.greatcount[player - 1];
    }
    public int Good(int player)
    {
        return ScoreManager.goodcount[player - 1];
    }
    public int Miss(int player)
    {
        return ScoreManager.misscount[player - 1];
    }

    // 簡単にまとめたい
    void ScoreDisplay(int player)
    {
        if(scoreManager.scoreLavel[player - 1])
        {
            scoreManager.scoreLavel[player - 1].text = Score(player).ToString();
        }
        if(scoreManager.comboLavel[player - 1])
        {
            scoreManager.comboLavel[player - 1].text = Combo(player).ToString();
        }
        if(scoreManager.MaxComboLavel[player - 1])
        {
            scoreManager.MaxComboLavel[player - 1].text = Max(player).ToString();
        }
        if(scoreManager.perfectLavel[player - 1])
        {
            scoreManager.perfectLavel[player - 1].text = Perfect(player).ToString();
        }
        if(scoreManager.greatLavel[player - 1])
        {
            scoreManager.greatLavel[player - 1].text = Great(player).ToString();
        }
        if(scoreManager.goodLavel[player - 1])
        {
            scoreManager.goodLavel[player - 1].text = Good(player).ToString();
        }
        if(scoreManager.missLavel[player - 1])
        {
            scoreManager.missLavel[player - 1].text = Miss(player).ToString();
        }

        if(ScoreManager.score[player - 1] > 0){
            scoreManager.missScore[player - 1] = -100;
        }
        else
        {
            scoreManager.missScore[player - 1] = 0;
        }
    } // ここまで

    void PushButtonOn(Note note)
    {
        note.bottom_button.gameObject.SetActive(true);
    }

    void PushButtonOff(Note note)
    {
        note.bottom_button.gameObject.SetActive(false);
    }

    // ミスの表示
    void MissDesplay(int player, Note note, int grid_index)
    {
        if(clipManager.GetClosestGridIndex() <= stateManager.gameendgrid )
        {
            GameObject judge = Judge[3];
            DesplayJudge(player, judge, grid_index);
            ScoreManager.score[player - 1] += scoreManager.missScore[player - 1];
            ScoreManager.misscount[player - 1] += 1;
            ScoreManager.combo[player - 1] = 0;
            audioSource.PlayOneShot(missSound);
        }
        DeleteChara(player);            
            
        GameObject miss = Instantiate(miss_charactor[player - 1]);
        Transform t = chara_points[player - 1];
        miss.transform.position = t.position;
        miss.transform.localScale = t.localScale;
        if(stateManager.player == 2)
        {
            miss.transform.localScale = new Vector3(-1, 1, 1);
        }
        miss_object = miss;
    }

    // ノートが残っていたらミスにしたい
    void MissJudge(int player)
    {
        int other_player = player == 1 ? 2 : 1;

        float missJudge_time = 0.20f;
        
        if(stateManager.stage == StateManager.GameState.Stage.Mimic)
        {
            int index = clipManager.GetPreviousGridIndex();
            int entry_index = index % note_count;
            float offset = clipManager.GetGridOffset(index);

            for (int i = 0; i < note_entry.GetLength(1); i++) {
                int entry = note_entry[other_player - 1, i];
                if (entry != -1 && note_miss[player - 1, i] == -1) {
                    if(entry_index > i || (entry_index == i) && offset >= missJudge_time) {
                        Note note = MIDI_notes[entry];
                        MissDesplay(player, note, i);
                        note_miss[player - 1, i] = 1;
                        mimic_entry[other_player - 1, i] = -1;
                    }
                }
            }
        }
    }


    void CharaAnim(int player, Note note)
    {
        GameObject chara = Instantiate(note.charactor[player - 1]);
        Transform t = chara_points[player - 1];
        chara.transform.position = t.position;
        chara.transform.localScale = t.localScale;
        if(stateManager.player == 2)
        {
            chara.transform.localScale = new Vector3(-1, 1, 1);
        }
        chara_object = chara;
    }

    void DeleteChara(int player)
    {
        Destroy(chara_object);
        chara_object = null;
        Destroy(miss_object);
        miss_object = null;
        if(default_object[player - 1] != null){
            if(default_object[player - 1].activeSelf){
                default_object[player - 1].SetActive(false);
            }
        }
    }

    void DeleteCircle(int player, int index)
    {
        int other_player = player == 1 ? 2 : 1;

        Destroy(CircleAnimation[other_player - 1, index]);
        CircleAnimation[other_player - 1, index] = null;
    }

    void GoResult(){
        SceneManager.LoadScene("BeforeResult");
    }

    // エントリーの動作
    void OnNoteEntry(int player, Note note, int note_number, int grid_index, float judge_time)
    {
        if(grid_index > 1)　// 妥協案のときの条件文
        {
            // すでにノートが入力されていないかを確認する
            if (note_entry[player - 1, grid_index] == -1)
            {
                // 入力されたノートを記憶する
                note_entry[player - 1, grid_index] = note_number;
                // 新しいオブジェクトを生成する
                GameObject go = Instantiate(note.prefab);
                go.transform.position = spawn_position[player - 1, grid_index];
                if(stateManager.player == 2){
                    go.transform.localScale = new Vector3(-1, 1, 1);
                }

                note_objects[player - 1, grid_index] = go;

                DeleteChara(player);
                // 判定の処理を行う
                NoteJudge(player, grid_index, judge_time);
                //audioSource.PlayOneShot(note.sound);
                CharaAnim(player, note);
            
            }
        }
    }
    
    // ミミックの動作
    void OnNoteMimic(int player, Note note, int note_number, int grid_index, float judge_time)
    {
        int other_player = player == 1 ? 2 : 1; // プレイヤーが１なら２、そうでないなら１

        if(grid_index > 1)　// 妥協案のときの条件文
        {

            // 相手がノートを既にここで入力したかどうかを確認する
            if(mimic_entry[other_player - 1, grid_index] == -1)
            {
                mimic_entry[other_player - 1, grid_index] = note_number;
                DeleteChara(player);
                note_miss[player - 1, grid_index] = 0;

                // 相手が正しいノートをここで入力したかどうかを確認する
                if (note_entry[other_player - 1, grid_index] == note_number) 
                {
                    // ミミック成功


                    GameObject ring = Instantiate(Circle);
                    ring.transform.position = spawn_position[player - 1, grid_index];
                    CircleAnimation[other_player - 1, grid_index] = ring;

                    DeleteInfo(other_player, grid_index); 
                    // 判定の処理を行う
                    NoteJudge(player, grid_index, judge_time);
                    //audioSource.PlayOneShot(note.sound);
                    CharaAnim(player, note);
                }
                else{
                    MissDesplay(player, note, grid_index);
                }
            }
        }
    }

    bool CanEnterNotes(int player) {
        float t = clipManager.GetGridTime() % note_count;

        if (stateManager.state.stage == StateManager.GameState.Stage.Enter) {
            if (stateManager.player != player) {
                return t < 0.5f;
            }
            else {
                return t < note_count - 0.5f;
            }
        }
        else if (stateManager.state.stage == StateManager.GameState.Stage.Mimic) {
            if (stateManager.player != player) {
                return t > 0.5f + note_count;
            }
            else {
                return t < note_count - 0.5f;
            }
        }
        return false;
    }

    bool CanMimicNotes(int player) {
        float t = clipManager.GetGridTime() % note_count;

        if (stateManager.state.stage == StateManager.GameState.Stage.Mimic) {
            if (stateManager.player != player) {
                return t < 0.5f;
            }
            else {
                return t < note_count - 0.5f;
            }
        }
        else if (stateManager.state.stage == StateManager.GameState.Stage.Enter) {
            if (stateManager.player != player) {
                return t > 0.5f + note_count;
            }
            else {
                return t < note_count - 0.5f;
            }
        }
        return false;
    }


    void ProcessNote(int player, Note note, int note_number) 
    {

        int grid = clipManager.GetClosestGridIndex();
        int grid_index = grid % note_count;
        float judge_time = clipManager.GetGridOffset(grid);

        // if (CanEnterNotes(player)){  動作しないのでいったんコメントアウトしました。
        if (stateManager.stage == StateManager.GameState.Stage.Enter)
        {
            OnNoteEntry(player, note, note_number, grid_index, judge_time); 
        }
        // else if (CanMimicNotes(player)){  動作しないのでいったんコメントアウトしました。
        else if (stateManager.stage == StateManager.GameState.Stage.Mimic)
        {            
            OnNoteMimic(player, note, note_number, grid_index, judge_time); 
        }
        ScoreDisplay(player);
    }


    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainGame")
        {
            // 古いノートデータを消す
            if (state.stage != stateManager.stage)
            {
                int p = state.player - 1;
                int other_player = state.player == 1 ? 2 : 1;

                if (stateManager.stage == StateManager.GameState.Stage.Enter) 
                {
                    for (int i = 0; i < note_count; i++)
                    {
                        note_entry[p, i] = -1;
                        mimic_entry[p, i] = -1;
                        DeleteInfo(other_player, i);
                        DeleteCircle(other_player, i);
                    }
                }
                if (stateManager.stage == StateManager.GameState.Stage.Wait) 
                {
                    for (int i = 0; i < note_count; i++)
                    {
                        note_miss[other_player - 1, i] = -1;
                    }
                }
                if(!default_object[other_player -1].activeSelf){
                    DeleteChara(other_player);
                    default_object[other_player - 1].SetActive(true);
                }          
            }

            state = stateManager.state;

            //foreach (Note note in MIDI_notes)

            if(clipManager.GetTime() > 0){
                if(clipManager.GetClosestGridIndex() <= stateManager.gameendgrid )
                {   
                    for (int note_number = 0; note_number < MIDI_notes.Length; note_number ++)
                    {
                        Note note = MIDI_notes[note_number];

                        for (int player = 1; player <= 2; player++)
                        {
                            if (player != stateManager.player) continue;

                            if (GetNoteDown(player, note) && (stateManager.stage == StateManager.GameState.Stage.Mimic || stateManager.stage == StateManager.GameState.Stage.Enter))
                            {
                                ProcessNote(player, note, note_number);
                                PushButtonOn(note);                   
                            }
                            // if (GetNoteUp(player, note) && (stateManager.stage == StateManager.GameState.Stage.Mimic || stateManager.stage == StateManager.GameState.Stage.Enter))
                            // {
                            //     PushButtonOff(note);
                            // }
                        }                       
                    }
                }

                for (int player = 1; player <= 2; player++) {
                    MissJudge(player);
                    ScoreDisplay(player);
                }
            }

            if(stateManager.MusicEndTime <= clipManager.GetTime())
		    {
                GoResult();
		    }
        }       
    }
}
