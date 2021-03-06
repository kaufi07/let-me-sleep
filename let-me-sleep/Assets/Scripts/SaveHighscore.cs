﻿using UnityEngine;
using System.Collections;

public class SaveHighscore : MonoBehaviour
{
    private const int maxHighScoreLength = 5;
    private string[] currentHighScores;

    // Use this for initialization
    void Start()
    {
        currentHighScores = GameManager.getHighscores();
    }

    public void saveHighScore(string name, int score)
    {
        int newHighscoreIndex = -1;

        int loop = maxHighScoreLength;
        if (currentHighScores.Length < maxHighScoreLength)
        {
            loop = currentHighScores.Length;
        }

        //enlarge array if not 5
        if (currentHighScores.Length < maxHighScoreLength)
        {
            string[] newArray = new string[currentHighScores.Length + 1];
            for (int i = 0; i < currentHighScores.Length; i++)
            {
                newArray[i] = currentHighScores[i];
            }
            currentHighScores = newArray;
            //override loop to have new length.
            loop = currentHighScores.Length;
        }


        bool indexFound = false;
        for (int i = 0; i < loop; i++)
        {
            //jump over last values if array was extended and highscore can be null at last index.
            if (currentHighScores[i] == null)
            {
                //if null this should be last index. if newsHighscoreIndex is still 0 then use this last index.
                if (newHighscoreIndex == -1)
                {
                    newHighscoreIndex = i;
                }

            }
            else if (indexFound == false)
            {
                int temp = int.Parse(currentHighScores[i].Split(',')[1]);
                if (temp <= score)
                {
                    newHighscoreIndex = i;
                    indexFound = true;
                }
            }
        }


        if (newHighscoreIndex != -1)
        {
            string highscore = name + ',' + score.ToString();

            //save last highscore as next list value.
            string nextValue = currentHighScores[newHighscoreIndex];

            //save new highscore at correct position.
            currentHighScores[newHighscoreIndex] = highscore;

            for (int i = (newHighscoreIndex + 1); i < currentHighScores.Length; i++)
            {
                string temp = currentHighScores[i];
                currentHighScores[i] = nextValue;
                nextValue = temp;
            }

            GameManager.saveHighScores(currentHighScores);
        }
    }
}
