using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo.Players;

public class GamePauseButton : UIButton {

    List<NoteScript> mPausedNotes = new List<NoteScript>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnButtonTouch()
    {
        base.OnButtonTouch();
        switch (GameplayManager.instance.mGameplayState)
        {
            case GameplayManager.GameplayState.PLAYING:
                AudioInstance.instance.GetComponent<SimpleMusicPlayer>().Pause();
                GameplayManager.instance.mGameplayState = GameplayManager.GameplayState.PAUSED;
                RhythmManager.instance.enabled = false;
                foreach (KeyValuePair<char, Queue<GameObject>> lane in RhythmManager.instance.mSpawnedNotes)
                {
                    foreach (GameObject note in lane.Value)
                    {
                        NoteScript ns = note.GetComponent<NoteScript>();
                        ns.enabled = false;
                        mPausedNotes.Add(ns);
                    }
                }
                break;
            
                //This may change or not be here later when this function is moved to another button
            case GameplayManager.GameplayState.PAUSED:
                AudioInstance.instance.GetComponent<SimpleMusicPlayer>().Play();
                GameplayManager.instance.mGameplayState = GameplayManager.GameplayState.PLAYING;
                RhythmManager.instance.enabled = true;
                foreach (NoteScript note in mPausedNotes)
                {
                    note.enabled = true;
                }
                mPausedNotes.Clear();
                break;
        }
    }
}
