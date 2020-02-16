using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{

    public SpriteRenderer BackgrpundSpriteRenderer;

    public Sprite[] Background = new Sprite[4];


    // Start is called before the first frame update
    void Start()
    {
        BackgrpundSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
