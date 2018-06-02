using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager instance = null;

    public enum GameplayState { INIT, PLAYING, PAUSED, REWINDING, COMPLETE };

    public GameplayState mGameplayState = GameplayState.PLAYING;

	

    private void Awake()
    {
        instance = this;
        
    }

    public void Initialize()
    {
        Screen.fullScreen = false;

    }
    // Update is called once per frame
    void Update () {
		switch (mGameplayState)
        {
            case GameplayState.PAUSED:
                //RhythmManager.instance.enabled = false;

                break;
        }
	}

    public void GetPauseHit()
    {
        if (mGameplayState != GameplayState.PAUSED)
        {
            mGameplayState = GameplayState.PAUSED;
            
        }
    }

}
