using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : UIButton {

    
    [SerializeField]
    private char mLaneColor;

    private ParticleSystem mParticle;

    private Touch mTouch;


    private int mNoteBeforeHitThreshold;

    private int mNoteAfterHitThreshold;


    private RectTransform mButtonRect;



    private bool mCanUpdate = false;

    private bool mTouchOver = false;




    public void Initialize()
    {
        mParticle = GameObjectsScript.instance.mNoteCrunchers[mLaneColor].GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (mCanUpdate)
        {
            if (mTouch.phase != TouchPhase.Ended && mTouch.phase != TouchPhase.Canceled)
            {
                mTouchOver = true;
            }
            
            if (!mTouchOver)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(mButtonRect, mTouch.position))
                {
                    //currently holding long note
                }
            }
        }
    }

    public void SetThreshold(float before, float after)
    {
        mNoteBeforeHitThreshold = (int)(before * 44100);
        mNoteAfterHitThreshold = (int)(after * 44100);
    }

    public override void OnButtonTouch(Touch touch, RectTransform buttonTrans)
    {
        base.OnButtonTouch(touch, buttonTrans);
        
        if (CheckIfNoteIsClose(mLaneColor))
            HitNote(touch, buttonTrans);
    }

    private void HitNote(Touch touch, RectTransform buttonTrans)
    {
        if (RhythmManager.instance.mSpawnedNotes[mLaneColor].Count > 0)
        {
            NoteScript nextNote = RhythmManager.instance.mSpawnedNotes[mLaneColor].Peek().GetComponent<NoteScript>();
            switch (nextNote.mType)
            {
                case 'R':
                    HitRegular();
                    break;
                case 'L':
                    HitLong(touch, buttonTrans);
                    break;
            }
        }
    }

    private void HitRegular()
    {
        Destroy(RhythmManager.instance.mSpawnedNotes[mLaneColor].Dequeue());
        mParticle.Emit(10);
    }

    private void HitLong(Touch touch, RectTransform buttonTrans)
    {
        mTouch = touch;
        mCanUpdate = true;
        mButtonRect = buttonTrans;
        RhythmManager.instance.mSpawnedNotes[mLaneColor].Dequeue();     //don't forget to destroy the note when it's fully completed. Maybe put the BoxCollider at the end of the tail?
        mParticle.Emit(10);
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
