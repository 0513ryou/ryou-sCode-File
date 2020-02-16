using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MidiJack; // MidiJackを使う

[RequireComponent(typeof(MusicClipManager))]
public class PlayerInputManager : MonoBehaviour
{
    public Text gridNumberLabel = null;
    public Text gridOffsetLabel = null;

    MusicClipManager clipManager;

    // Start is called before the first frame update
    void Start()
    {
        clipManager = GetComponent<MusicClipManager>();
    }



    // string[] buttonNames = {"39","48","45","36","38","43","55","49","44","46"};
    // int button = -1;
    // float buttonOnTime = 0f;
    // float buttonOffTime = 0f;

    
    void Update()
    {

        // if(button > -1 && !Input.GetKey(buttonNames[button])){
        //     button = -1;
        //     buttonOffTime = Time.time;
        // }
        // for(int i = 0; i < buttonNames.Length; i++){
        //     if(Input.GetKey(buttonNames[i])){
        //         button = i;
        //         buttonOnTime = Time.time;
        //     }
        // }


        // MIDIのノート番号は0~127
        for (int i = 0; i < 128; i++) {
            // MidiJackを使って入力を確認する
            if (MidiMaster.GetKeyDown(i)) {
                NoteOn(i); // ここではvelocityを無視する。使うなら以下のようなコードなる：
                // NoteOne(i, MidiMaster.GetKey(i));
            }

            if (MidiMaster.GetKeyUp(i)) {
                NoteOff(i);
            }
        }
    }


    public void NoteOn(int note) {
        Debug.Log("Note on: " + note);

        int gridIndex = clipManager.GetClosestGridIndex();

        if (gridNumberLabel != null) {
            gridNumberLabel.text = gridIndex.ToString();
        }

        if (gridOffsetLabel != null) {
            gridOffsetLabel.text = clipManager.GetGridOffset(gridIndex).ToString("0.0000");
        }
    }

    public void NoteOff(int note) {
        Debug.Log("Note off: " + note);
    }
}
