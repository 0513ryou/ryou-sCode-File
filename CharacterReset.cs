using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicSceneController.p1_number = 0;
        MusicSceneController.p2_number = 6;
        CharacterSceneController.another_color_p1 = false;
        CharacterSceneController.another_color_p2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
