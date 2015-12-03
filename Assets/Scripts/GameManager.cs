﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public Canvas winScreen;
        public Canvas loseScreen;

        private GUIScript gui;

        //Singleton pattern
        void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        void Start()
        {
            gui = GetComponent<GUIScript>();
        }

        public void KillPlayer(string reason)
        {
            loseScreen.gameObject.SetActive(true);
            winScreen.gameObject.SetActive(false);
            var texts = loseScreen.GetComponentsInChildren<Text>();

            foreach (var text in texts)
            {
                if (text.name == "level-text")
                {
                    text.text = "Level " + Application.loadedLevel;
                }
                else if (text.name == "extra-text")
                {
                    text.text = "Death By: " + reason;
                }
            }

            Time.timeScale = 0;
        }

        public void PlayerWon()
        {
            gui.runTimer = false;
            double courseTime = gui.time;
            loseScreen.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(true);
            var texts = winScreen.GetComponentsInChildren<Text>();

            foreach (var text in texts)
            {
                if (text.name == "level-text")
                {
                    text.text = "Level " + Application.loadedLevel;
                }
                else if (text.name == "extra-text")
                {
                    text.text = "Time: " + courseTime.ToString("0.00");
                }
            }

            Time.timeScale = 0;
        }

        public void ReloadLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1;
        }

        public void GoToNextLevel()
        {
            if (Application.loadedLevel + 1 < Application.levelCount)
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
            else
            {
                Application.LoadLevel(0);
            }
            Time.timeScale = 1;
        }

        public void GoToMainMenu()
        {
            Application.LoadLevel(0);
            Time.timeScale = 1;
        }

        public void Quit()
        {
            //If we are running in a standalone build of the game
#if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
