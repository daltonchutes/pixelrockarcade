using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public enum GameplayState { INIT, PLAYING, PAUSED, REWINDING, COMPLETE };

    private GameplayState mGameplayState = GameplayState.INIT;

	// Use this for initialization
	void Start () {
        Screen.fullScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
