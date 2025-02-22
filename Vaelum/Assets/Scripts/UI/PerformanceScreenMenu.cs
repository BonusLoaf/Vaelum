﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerformanceScreenMenu : MonoBehaviour
{

    public Image discSticker;
    public Sprite[] discRating;
    public Text ratingText;
    public Text percentageText;
    public Text scoreText;
    public Text gamemodeText;
    public Text songNameText;
    public Image ratingIndicator;
    public GameObject newHighscore;
    public GameObject celebration;
    string song;
    float percent;



    public void stop()
    {
        SceneManager.LoadScene(1);
    }

    public void loadSplashScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void reset()
    {
        SceneManager.LoadScene(2);
    }

    // Start is called before the first frame update
    void Start()
    {

        if(PlayerPrefs.GetString("mod") == "Vaelocity")
        {
            ScoreController.score = ScoreController.score * 1.5f;
        }
        if (PlayerPrefs.GetString("mod") == "Memento")
        {
            ScoreController.score = ScoreController.score * 1.5f;
        }
        if (PlayerPrefs.GetString("mod") == "Achromatic")
        {
            ScoreController.score = ScoreController.score * 0.4f;
        }
        if (PlayerPrefs.GetString("mod") == "Decelerate")
        {
            ScoreController.score = ScoreController.score * 0.8f;
        }


        song = PlayerPrefs.GetString("currentSong");

        songNameText.text = song;

        discSticker.sprite = Resources.Load<Sprite>("Album Covers/" + song);

        percent = float.Parse(ScoreController.notePercent.ToString("#.##"));

        gamemodeText.text = PlayerPrefs.GetString("mod");

        percentageText.text = percent.ToString() + "%";

        scoreText.text = ScoreController.score.ToString();

        if (percent == 100)
        {
            ratingIndicator.sprite = discRating[5];
            ratingText.text = "Vaelescent";
        }
        else if (percent > 95)
        {
            ratingIndicator.sprite = discRating[4];
            ratingText.text = "Diamond";
        }
        else if (percent > 90)
        {
            ratingIndicator.sprite = discRating[3];
            ratingText.text = "Platinum";
        }
        else if (percent > 80)
        {
            ratingIndicator.sprite = discRating[2];
            ratingText.text = "Gold";
        }
        else if (percent >= 70)
        {
            ratingIndicator.sprite = discRating[1];
            ratingText.text = "Silver";
        }
        else if (percent < 70)
        {
            ratingIndicator.sprite = discRating[0];
            ratingText.text = "Bronze";
        }

        

        if(PlayerPrefs.GetString(song + "score") != "")
        {
            if ((int.Parse(scoreText.text)) > (int.Parse(PlayerPrefs.GetString(song + "score"))))
            {

                Instantiate(newHighscore, transform);
                Instantiate(celebration);

                PlayerPrefs.SetFloat(song + "rating", percent);
                PlayerPrefs.SetString(song + "percentage", percent.ToString());
                PlayerPrefs.SetString(song + "rank", ratingText.text);
                PlayerPrefs.SetString(song + "score", scoreText.text);
                PlayerPrefs.SetString(song + "songMode", gamemodeText.text);

            }
        }
        else
        {
            Instantiate(newHighscore, transform);
            Instantiate(celebration);

            PlayerPrefs.SetFloat(song + "rating", percent);
            PlayerPrefs.SetString(song + "percentage", percent.ToString());
            PlayerPrefs.SetString(song + "rank", ratingText.text);
            PlayerPrefs.SetString(song + "score", scoreText.text);
            PlayerPrefs.SetString(song + "songMode", gamemodeText.text);
        }

            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
