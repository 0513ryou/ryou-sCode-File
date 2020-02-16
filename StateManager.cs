using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public int gameendgrid = 0;
    public float MusicEndTime = 0;
    
    [System.Serializable]
    public struct GameState {
        public enum Stage
        {
            Prepare,
            Enter,
            Wait,
            Mimic
        }
        public Stage stage;
        public int player;
        public float start_time;
        public float end_time;
        public float duration;
        

        public GameState(int player, Stage stage, float duration) {
            this.player = player;
            this.stage = stage;
            this.duration = duration;
            this.start_time = 0;
            this.end_time = 0;
        }
    }


    GameState[] states = {
        new GameState(1, GameState.Stage.Prepare, 0),
        new GameState(1, GameState.Stage.Enter, NotesManager.note_count),
        new GameState(2, GameState.Stage.Wait, 0),
        new GameState(2, GameState.Stage.Mimic, NotesManager.note_count),
        new GameState(2, GameState.Stage.Prepare, 0),
        new GameState(2, GameState.Stage.Enter, NotesManager.note_count),
        new GameState(1, GameState.Stage.Wait, 0),
        new GameState(1, GameState.Stage.Mimic, NotesManager.note_count)
    };

    public MusicClipManager clipManager;
    public GameState state;
    public bool CanPlayNotes
    {
        get
        {
            return state.stage == GameState.Stage.Enter || state.stage == GameState.Stage.Mimic;
        }
    }
    public int player
    {
        get
        {
            return state.player;
        }
    }
    public GameState.Stage stage
    {
        get
        {
            return state.stage;
        }
    }

    public float state_time = 0;

    public int state_index = -1;
    void NextState() {
        state_index = (state_index + 1) % states.Length;
        states[state_index].start_time = clipManager.GetGridTime();
        states[state_index].end_time = clipManager.GetGridTime() + states[state_index].duration;

        Debug.Log("Next start: " + states[state_index].start_time);
        Debug.Log("Next end: " + states[state_index].end_time);

        state = states[state_index];
    }

    

    // Start is called before the first frame update
    void Start()
    {
        NextState();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (clipManager.GetGridTime() >= states[state_index].end_time) {
            NextState();
        }

        state_time = clipManager.GetGridTime() - states[state_index].start_time;
    }
}
