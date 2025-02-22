﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


public class ScoreController : MonoBehaviour
{

    public Volume volume;

    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite plat;
    public Sprite diamond;
    public Sprite vae;

    bool maxComboReached = false;

    private GameObject updatePelletHUD;

    public Image ratingIndicator;

    public Text comboUI;

    public Text scoreUI;

    public Text percentageUI;

    public Slider healthUI;

    public Image healthUIFill;

    //public GameObject noteList;

    public static float noteCount = 1;

    public float numOfHitNotes = 1;

    public static float notePercent = 100;

    bool invincible = false;

    public int combo = 1;

    public int health = 100;

    public static float score = 0;

    public AudioSource perfectHitSound;

    public AudioSource okayHitSound;

    public AudioSource missedHitSound;

    public GameObject visualiser;

    //public Image flash;




    //private AudioSource songSource;

    Color32 qC = new Color32(0x76, 0x00, 0x00, 0xFF);


    void Start()
    {

        

        notePercent = 100;
        score = 0;
        numOfHitNotes = 1;
        noteCount = 2;

        updatePelletHUD = GameObject.Find("Combo Bar");

        //noteList = GameObject.FindGameObjectWithTag("NoteList");

        //songSource = noteList.GetComponent<AudioSource>();


        updateUI();
    }

    void addScore(int hitType)
    {

        score = score + (10 * combo);


        if (health < 100)
        {
            health = health + combo;
        }

        if (hitType != 0)
        {
            okayHitSound.Play();

            

            print("OK");
            if(invincible == true)
            {
                numOfHitNotes++;
                invincible = false;
                //notePercent = ((numOfHitNotes / noteCount) * 100);
                healthUIFill.color = qC;
            }
            else if (invincible == false)
            { 
                numOfHitNotes = numOfHitNotes + 0.6f;
                health = health - 5;
            }

            combo = 1;
            maxComboReached = false;

        }
        else
        {
            //PERFECT HIT

            volume.weight = 1f;

            visualiser.SendMessage("hitNote");

            Invoke("resetHitMarker", 0.1f);
            
            perfectHitSound.Play();
            numOfHitNotes++;

            notePercent = ((numOfHitNotes / noteCount) * 100);

            if (combo < 11)
            {
                combo++;
            }
            

        }

        if (combo == 11 && maxComboReached == false)
        {

            invincible = true;
            healthUIFill.color = Color.white;
            visualiser.SendMessage("fullCombo");
            maxComboReached = true;

        }


        updateUI();

    }


    void resetHitMarker()
    {
        volume.weight = 0;
    }

    void missedNote()
    {


        missedHitSound.Play();
        maxComboReached = false;
        combo = 1;
        if (invincible == true)
        {
            numOfHitNotes++;

            //notePercent = ((numOfHitNotes / noteCount) * 100);
            invincible = false;
            healthUIFill.color = qC;

        }
        else
        {


            notePercent = ((numOfHitNotes / noteCount) * 100);

            

            health = health - 10;            

            

            if (health < 1)
            {

                SceneManager.LoadScene(4);

            }

        }

        updateUI();

    }

    void updateUI()
    {


        updatePelletHUD.SendMessage("updatePellets", combo);

        scoreUI.text = score.ToString();

        healthUI.value = health;

        comboUI.text = "x" + combo.ToString();

        if (notePercent > 100)
        {
            notePercent = 100;
        }

        updateRating(notePercent);

    }

    void updateRating(float percent)
    {



        if (percent >= 100)
        {
            ratingIndicator.sprite = vae;
        }
        else if (percent > 95)
        {
            ratingIndicator.sprite = diamond;
        }
        else if (percent > 90)
        {
            ratingIndicator.sprite = plat;
        }
        else if (percent > 80)
        {
            ratingIndicator.sprite = gold;
        }
        else if (percent >= 70)
        {
            ratingIndicator.sprite = silver;
        }
        else if (percent < 70)
        {
            ratingIndicator.sprite = bronze;
        }



    }
    


}
