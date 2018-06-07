using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTouchController : MonoBehaviour {

    class TouchObject
    {
        public Touch mTouch;
        public RectTransform mButtonRect;
        public Vector2 mStartingPosition;       //used to determine swipe direction for swipe-notes

        public TouchObject (Touch touch, RectTransform rect)
        {
            mTouch = touch;
            mStartingPosition = touch.position;
            mButtonRect = rect;
        }
    }
    
    Dictionary<int, TouchObject> mTouches = new Dictionary<int, TouchObject>();

    Dictionary<char, RectTransform> mButtons = new Dictionary<char, RectTransform>();
    
    private int mNoteBeforeHitThreshold;

    private int mNoteAfterHitThreshold;


    List<int> toRemoveIDs = new List<int>();



    // Initializes button transforms for touch input checking
    public void Initialize(RectTransform green, RectTransform red, RectTransform yellow)
    {
        mButtons['G'] = green;
        mButtons['R'] = red;
        mButtons['Y'] = yellow;
    }



    // Checks if the given touch is close to the next note in its lane
	private void CheckHitNote(TouchObject touch)
    {        
        foreach (KeyValuePair<char, RectTransform> pair in mButtons)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(pair.Value, touch.mTouch.position))
            {
                if (CheckIfNoteIsClose(pair.Key))
                    HitNextNote(pair.Key, touch);
                continue;
            }
        }
    }

    // Same as above, but is typically only called when TouchObject is first created
    private void CheckHitNote(TouchObject touch, RectTransform rect)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rect, touch.mTouch.position))
        {
            char lane = rect.gameObject.GetComponent<NoteHit>().mLaneColor;
            if (CheckIfNoteIsClose(lane))
                HitNextNote(lane, touch);
            return;
        }
    }

    // Decides which type of note hit function to call
    private void HitNextNote(char lane, TouchObject touch)
    {
        switch (RhythmManager.instance.mSpawnedNotes[lane].Peek().GetComponent<NoteScript>().mType)
        {
            case 'R':
                HitRegularNote(lane, touch);
                break;
            case 'L':
                HitLongNote(lane);
                break;
        }
    }


    private void HitLongNote(char lane)
    {
        //long note hit setup here
    }

    // Simple note hit. Destroys the note and activates the appropriate lane's cruncher's animation/particle effect
    private void HitRegularNote(char lane, TouchObject touch)
    {
        mButtons[lane].gameObject.GetComponent<NoteHit>().HitNote();
        Destroy(RhythmManager.instance.mSpawnedNotes[lane].Dequeue());
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTouchID();

        toRemoveIDs.Clear();

        foreach (int id in mTouches.Keys)
        {
            if (mTouches[id].mTouch.phase == TouchPhase.Ended || mTouches[id].mTouch.phase == TouchPhase.Canceled)
            {
                toRemoveIDs.Add(id);
            }
        }
        foreach (int i in toRemoveIDs)
            mTouches.Remove(i);
        
	}


    private void UpdateTouchID()
    {
        foreach (Touch touch in Input.touches)
        {
            if (mTouches.ContainsKey(touch.fingerId))
            {
                mTouches[touch.fingerId].mTouch = touch;
            }
        }
    }

    // Adds a new touch to the list every time the screen is touched
    public void AddTouch(Touch touch, RectTransform rect)
    {
        TouchObject newTouch = new TouchObject(touch, rect);
        mTouches[touch.fingerId] = newTouch;
        CheckHitNote(newTouch, rect);
    }

    // Checks if there is a note in the given lane that will hit within the assigned time thresholds
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

    // Sets the amount of lee-way time given to the player when they tap a note
    // So the player doesn't have to be perfectly spot on with the note timing
    public void SetThreshold(float before, float after)
    {
        mNoteBeforeHitThreshold = (int)(before * 44100);
        mNoteAfterHitThreshold = (int)(after * 44100);
    }
}
