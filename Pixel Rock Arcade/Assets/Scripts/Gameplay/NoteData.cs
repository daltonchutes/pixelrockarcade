using UnityEngine;

public class NoteDataObject {


    private  char mColor;

    private  int mStartingTime;

    private  char mNoteType;

    private  float mLength;

    private  NoteDataObject mLastNote;


    public void Initialize(char color, char note_type, int start_time)
    {
        mColor = color;
        mNoteType = note_type;
        mStartingTime = start_time;
    }

    public void Initialize(char color, char note_type, int start_time, float length)
    {
        mColor = color;
        mNoteType = note_type;
        mStartingTime = start_time;
        mLength = length;
    }

    public void Initialize(char color, char note_type, int start_time, NoteDataObject last_note)
    {
        mColor = color;
        mNoteType = note_type;
        mStartingTime = start_time;
        mLastNote = last_note;
    }

    public int GetStartTime()
    {
        return mStartingTime;
    }

    public char GetColor()
    {
        return mColor;
    }

}
