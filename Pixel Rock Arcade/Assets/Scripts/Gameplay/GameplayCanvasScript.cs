using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvasScript : MonoBehaviour {

    public static GameplayCanvasScript instance;

	// Use this for initialization
	void Awake () {
        instance = this;
	}

    public void Initialize()
    {
        transform.Find("GreenButton").GetComponent<NoteHit>().Initialize();
        transform.Find("RedButton").GetComponent<NoteHit>().Initialize();
        transform.Find("YellowButton").GetComponent<NoteHit>().Initialize();
    }

}
