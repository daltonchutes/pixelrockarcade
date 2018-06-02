using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : UIButton {

    private RhythmManager mRhythmManager;

    [SerializeField]
    private char mLaneColor;

    private ParticleSystem mParticle;

    
    private int mNoteBeforeHitThreshold;

    private int mNoteAfterHitThreshold;

    public void Initialize()
    {
        mParticle = GameObjectsScript.instance.mNoteCrunchers[mLaneColor].GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetThreshold(float before, float after)
    {
        mNoteBeforeHitThreshold = (int)(before * 44100);
        mNoteAfterHitThreshold = (int)(after * 44100);
    }
    public override void OnButtonTouch()
    {
        base.OnButtonTouch();
        print(mLaneColor);
        if (CheckIfNoteIsClose(mLaneColor))
            HitNote();
    }

    private void HitNote()
    {
        if (RhythmManager.instance.mSpawnedNotes[mLaneColor].Count > 0)
        {
            Destroy(RhythmManager.instance.mSpawnedNotes[mLaneColor].Dequeue());
            mParticle.Emit(10);
        }
    }


    private bool CheckIfNoteIsClose(char lane)
    {
        if (RhythmManager.instance.mSpawnedNotes[lane].Count > 0)
        {
            int nextNoteTime = RhythmManager.instance.mSpawnedNotes[lane].Peek().GetComponent<NoteScript>().mStartTime;
            if (RhythmManager.instance.mKoreo.GetLatestSampleTime() + mNoteBeforeHitThreshold >= nextNoteTime && nextNoteTime >= RhythmManager.instance.mKoreo.GetLatestSampleTime() - mNoteAfterHitThreshold)
                return true;
        }
        return false;
    }
}
