using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo.Players;

public class GamePauseButton : UIButton {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnButtonTouch()
    {
        base.OnButtonTouch();
        AudioInstance.instance.GetComponent<SimpleMusicPlayer>().Pause();
        GameplayManager.instance.mGameplayState = GameplayManager.GameplayState.PAUSED;
        RhythmManager.instance.enabled = false;
        foreach (KeyValuePair<char, Queue<GameObject>> lane in RhythmManager.instance.mSpawnedNotes)
        {
            foreach (GameObject note in lane.Value)
            {
                note.GetComponent<NoteScript>().enabled = false;
            }
        }
    }
}
