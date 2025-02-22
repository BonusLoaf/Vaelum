﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public Animator anim;

    public GameObject ripple;

    public GameObject particles;

    private int noteType;

    private int hitType = 2;

    public GameObject glow;

    public GameObject scoreController;

    private Renderer noteRenderer;

    public GameObject okayTimingText;

    public GameObject perfectTimingText;

    public GameObject lateTimingText;

    public GameObject missedTimingText;

    public GameObject line;

    private int noteIndex;

    private GameObject realLine;

    public int noteNumber;

    private Color noteColor;

    private GameObject[] activeNotes;

    GameObject tutorial;

    bool tutorialActive = false;

    bool glowSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreController = GameObject.Find("Score Controller");
        noteRenderer = GetComponent<Renderer>();


        transform.Translate(0, 0, (SpawnNote.currentAmountOfNotes));

        noteIndex = SpawnNote.currentAmountOfNotes;

        if (SongSelectMenu.song != "Tutorial")
        {
            NoteListHandler.noteObjects.Add(noteIndex);
            NoteListHandler.notes.Add(transform.position);


            noteNumber = NoteListHandler.notes.Count;

            Vector3 targ = NoteListHandler.notes[noteNumber - 2];

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

            
                Instantiate(line, transform.position, Quaternion.Euler(0, 0, angle), transform);

                realLine = transform.GetChild(2).gameObject;


                Vector3 target = NoteListHandler.notes[noteNumber - 2];

                realLine.SendMessage("setTarget", target);
            
        }

        //gameObject.transform.GetComponentInChildren<Text>().text = SpawnNote.currentAmountOfNotes.ToString();

        if (tutorial = GameObject.Find("Tutorial(Clone)"))
        {
            tutorialActive = true;
        }


        if (gameObject.tag == "Q notes")
        {
            noteColor = new Color32(0xA5, 0x00, 0x00, 0xFF);
            noteType = 1;
        }
        else if (gameObject.tag == "W notes")
        {
            noteColor = Color.blue;
            noteType = 2;
        }
        else if (gameObject.tag == "E notes")
        {
            noteColor = new Color32(0x39, 0xE5, 0x82, 0xFF);
            noteType = 3;
        }
        else if (gameObject.tag == "S notes")
        {
            noteColor = Color.yellow;
            noteType = 0;
        }
        else if (gameObject.tag == "A notes")
        {
            noteColor = Color.magenta;
            noteType = 4;
        }
        
        if(PlayerPrefs.GetString("mod") == "Memento")
        {
            noteColor = Color.white;
        }

        realLine.SendMessage("setColour", noteColor);

    }



    void clickedOn()
    {
        if(hitType == 0)
        {
            if (tutorialActive == true)
            {
                tutorial.SendMessage("perfect");
            }
            Instantiate(perfectTimingText, transform.position, transform.rotation);
        }
        else if (hitType == 1)
        {
            if (tutorialActive == true)
            {
                tutorial.SendMessage("late");
            }
            Instantiate(lateTimingText, transform.position, transform.rotation);
        }
        else
        {
            if (tutorialActive == true)
            {
                tutorial.SendMessage("okay");
            }
            Instantiate(okayTimingText, transform.position, transform.rotation);
        }

        Instantiate(particles, new Vector3(transform.position.x, transform.position.y, -3), transform.rotation);

        Instantiate(ripple, transform.position, transform.rotation);

        anim.SetTrigger("hit");
        scoreController.SendMessage("addScore", hitType);
        ScoreController.noteCount++;
        removeFromList();
        transform.tag = "Untagged";
        Invoke("destroy", 0.1f);

    }


    void perfectNote()
    {

        hitType = 0;

    }

    void lateNote()
    {

        hitType = 1;

    }

    void perfectColour()
    {

        if (gameObject.tag == "Q notes")
        {
            noteRenderer.material.color = Color.red;
        }
        else if (gameObject.tag == "W notes")
        {
            noteRenderer.material.color = Color.blue;
        }
        else if (gameObject.tag == "E notes")
        {
            noteRenderer.material.color = Color.cyan;
        }
        else if (gameObject.tag == "S notes")
        {
            noteRenderer.material.color = Color.grey;
        }


    }

    void missNote()
    {
        if(tutorialActive == true)
        {
            tutorial.SendMessage("miss");
        }

        anim.SetTrigger("missed");
        Instantiate(missedTimingText, transform.position, transform.rotation);
        ScoreController.noteCount++;
        scoreController.SendMessage("missedNote");
        removeFromList();
        transform.tag = "Untagged";
        Invoke("destroy", 1f);
    }

    void removeFromList()
    {
        if(SongSelectMenu.song != "Tutorial")
        {
            NoteListHandler.noteObjects.Remove(noteIndex);

            NoteListHandler.noteObjects.Sort();
        }
        

    }

    private void destroy()
    {
        Destroy(gameObject);
    }



    private void FixedUpdate()
    {

        if (SongSelectMenu.song != "Tutorial")
        {
            if (PlayerPrefs.GetString("mod") == "Memento")
            {

            }
            else
            {


                if (glowSpawned == false && NoteListHandler.noteObjects[0] == noteIndex)
                {

                    print("SPAWN");

                    Instantiate(glow, transform);

                    glowSpawned = true;

                }
            }
        }

    }

}
