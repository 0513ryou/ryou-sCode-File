using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// AudioSourceコンポーネントがないとダメです。
[RequireComponent(typeof(AudioSource))]
public class MusicClipManager : MonoBehaviour
{
    // Inspectorで表示するパラメータ
    public float offset = 0;　// サウンドファイルの初めに無音があればこの長さを入力する
    public float BPM = 120; // テンポ
    public float gridSize = 0.25f; // グリッドの大きさ（1 = 全音符）

    public float[] BPM_Data;
    public int[] LastGrid;
    public float[] GameTime;

    public Text MusicTitle;
    public string[] music_title;

    // 表示のためにInspectorで表示されるパラメータ
    //public float time = 0; // 現在の時間（秒）

    // UIに情報を表示するためのTextオブジェクト。最終的に削除する。
    public Text timeLabel; // 現在の時間
    public Text gridLabel; // グリッドのマス
    public Text gridOffsetLabel; // グリッドのずれ

    AudioSource audioSource; // AudiSourceコンポーネント
    AudioClip clip; // AudioClipコンポーネント
 

    float sampleRate = 44100; // サンプルレート、AudioClipから取得する

    public static int music;
    public AudioClip[] musics;
    public StateManager state;

    public ChangeBackground background;


    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceとAudioClipを取得する
        audioSource = GetComponent<AudioSource>();

        music = MusicSceneController.music();

        switch (music){
            case 0:
                clip = musics[0];
                BPM = BPM_Data[0];
                state.gameendgrid = LastGrid[0];
                state.MusicEndTime = GameTime[0];
                background.BackgrpundSpriteRenderer.sprite = background.Background[0];
                MusicTitle.text = music_title[0];
                break;
            case 1:
                clip = musics[1];
                BPM = BPM_Data[1];
                state.gameendgrid = LastGrid[1];
                state.MusicEndTime = GameTime[1];
                background.BackgrpundSpriteRenderer.sprite = background.Background[1];
                MusicTitle.text = music_title[1];
                break;
            case 2:
                clip = musics[2];
                BPM = BPM_Data[2];
                state.gameendgrid = LastGrid[2];
                state.MusicEndTime = GameTime[2];
                background.BackgrpundSpriteRenderer.sprite = background.Background[2];
                MusicTitle.text = music_title[2];
                break;
            case 3:
                clip = musics[3];
                BPM = BPM_Data[3];
                state.gameendgrid = LastGrid[3];
                state.MusicEndTime = GameTime[3];
                background.BackgrpundSpriteRenderer.sprite = background.Background[3];
                MusicTitle.text = music_title[3];
                break;
            case 4:
                clip = musics[4];
                BPM = BPM_Data[4];
                state.gameendgrid = LastGrid[4];
                state.MusicEndTime = GameTime[4];
                background.BackgrpundSpriteRenderer.sprite = background.Background[4];
                MusicTitle.text = music_title[4];
                break;
            default:
                Debug.Log("The Music is Nothing.");
                break;
        }

        audioSource.clip = clip;
        Invoke("PlayMusic", 5.0f);

       // music = MusicSceneController.music();

        // サンプルレートを取得する
        sampleRate = clip.frequency;
    }

    void PlayMusic(){
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLabel) {
            timeLabel.text = GetTime().ToString();
        }

        if (gridLabel) {
            gridLabel.text = GetClosestGridIndex().ToString();
        }

        if (gridOffsetLabel) {
            gridOffsetLabel.text = GetGridOffset(GetClosestGridIndex()).ToString();
        }
    }

    public float GetTime()
    {
        if (audioSource) {
            return audioSource.timeSamples / sampleRate - offset;
        }
        else {
            return 0;
        }
    }

    public float GetGridTime()
    {
        return GetTime() * BPM / (60f * gridSize);
    }

    public float GetGridInterval()
    {
        return gridSize * 60f / BPM;
    }

    // 次のグリッドのマス目
    public int GetNextGridIndex()
    {
        return (int)Math.Ceiling(GetGridTime());
    }

    // 前のグリッドのマス目
    public int GetPreviousGridIndex()
    {
        return (int)Math.Floor(GetGridTime());
    }

    // 最も時間が近いグリッドのマス目
    public int GetClosestGridIndex()
    {
        return (int)Math.Round(GetGridTime());
    }

    // グリッドのマス目からどれほどずれているか（秒）
    public float GetGridOffset(int gridIndex)
    {
        float fraction = GetGridTime() - gridIndex;
        return fraction * GetGridInterval();
    }

}
