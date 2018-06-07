using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : UIButton {

    
    public char mLaneColor;

    private ParticleSystem mParticle;



    //private bool mCanUpdate = false;

    //private bool mTouchOver = false;




    public void Initialize()
    {
        mParticle = GameObjectsScript.instance.mNoteCrunchers[mLaneColor].GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {

        //Save for later...
        /*if (mCanUpdate)
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
        }*/
    }

    

    public override void OnButtonTouch(Touch touch, RectTransform buttonTrans)
    {
        base.OnButtonTouch(touch, buttonTrans);

        GameInput.instance.mGameTouchController.AddTouch(touch, buttonTrans);
    }


    public void HitNote()
    {
        mParticle.Emit(10);
    }

    private void HitLong(Touch touch, RectTransform buttonTrans)
    {

        //Also save for later...
        /*mTouch = touch;
        mCanUpdate = true;
        mButtonRect = buttonTrans;
        RhythmManager.instance.mSpawnedNotes[mLaneColor].Dequeue();     //don't forget to destroy the note when it's fully completed. Maybe put the BoxCollider at the end of the tail?
        mParticle.Emit(10);
        */
    }

}
