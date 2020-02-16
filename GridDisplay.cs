using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    public MusicClipManager clipManager;
    public StateManager stateManager;

    public float offset = 20f;
    public float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        //p.y = offset + clipManager.GetGridTime() * speed;
        p.y = offset + stateManager.state_time * -speed;
        if (stateManager.player == 1 && stateManager.CanPlayNotes) {
            p.x = -5;
        }
        else if (stateManager.player == 2 && stateManager.CanPlayNotes) {
            p.x = 5;
        }
        else {
            p.x = 0;
        }
        transform.position = p;
    }
}
