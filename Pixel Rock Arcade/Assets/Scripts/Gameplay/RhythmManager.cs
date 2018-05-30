using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class RhythmManager : MonoBehaviour {

    [SerializeField]
    private GameObject mNotePrefab;

    [SerializeField]
    private Transform mCameraTransform;


    [SerializeField]
    private Transform mGreenNoteCruncher;
    [SerializeField]
    private Transform mRedNoteCruncher;
    [SerializeField]
    private Transform mYellowNoteCruncher;
    
    Dictionary<char, Transform> mNoteCrunchers = new Dictionary<char, Transform>();


    private Dictionary<char, Queue<NoteDataObject>> mNotesToSpawn = new Dictionary<char, Queue<NoteDataObject>>();

    public Dictionary<char, Queue<GameObject>> mSpawnedNotes = new Dictionary<char, Queue<GameObject>>();

    public Koreography mKoreo;

    int mNoteToHitTravelTime = 0;

	// Use this for initialization
	void Start () {
        mNotesToSpawn['R'] = new Queue<NoteDataObject>();
        mNotesToSpawn['G'] = new Queue<NoteDataObject>();
        mNotesToSpawn['Y'] = new Queue<NoteDataObject>();

        mSpawnedNotes['R'] = new Queue<GameObject>();
        mSpawnedNotes['G'] = new Queue<GameObject>();
        mSpawnedNotes['Y'] = new Queue<GameObject>();

        mKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);

        mNoteCrunchers['R'] = mRedNoteCruncher;
        mNoteCrunchers['G'] = mGreenNoteCruncher;
        mNoteCrunchers['Y'] = mYellowNoteCruncher;

        InitializeNotes();

        mNoteToHitTravelTime = 2 * 1 * 44100;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Queue<NoteDataObject> lane in mNotesToSpawn.Values)
        {
            if (lane.Count > 0)
            {

                if (mKoreo.GetLatestSampleTime() + mNoteToHitTravelTime >= lane.Peek().GetStartTime())
                {
                    NoteDataObject spawnedNote = lane.Dequeue();
                    spawnNote(spawnedNote.GetColor(), 1, spawnedNote.GetStartTime());   //hard coded speed for now. Will later depend on difficulty
                }
            }
        }
	}

    public Dictionary<char, Transform> GetNoteCrunchers()
    {
        return mNoteCrunchers;
    }

    private void spawnNote(char color, float velocity, int start_time)
    {
        GameObject newNote = Instantiate(mNotePrefab);
        
        NoteScript newScript = newNote.GetComponent<NoteScript>();
        
        newScript.Initialize(velocity, start_time, color, mNoteCrunchers[color]);
        mSpawnedNotes[color].Enqueue(newNote);
    }


    private void InitializeNotes()
    {
        List<KoreographyEvent> rawEvents = mKoreo.GetTrackByID("FurEliseRhythmEASY").GetAllEvents();
        foreach (KoreographyEvent kevent in rawEvents)
        {
            if (kevent.HasTextPayload())
            {

                char[] payload = kevent.GetTextValue().ToCharArray();

                NoteDataObject newNote = new NoteDataObject();

                if (payload[0].Equals('N'))
                {
                    //payload[1] == color
                    newNote.Initialize(payload[1], payload[0], kevent.StartSample);
                }


                mNotesToSpawn[payload[1]].Enqueue(newNote);
            }
        }
    }
}
