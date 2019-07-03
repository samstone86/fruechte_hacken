﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float speed = 1f;
	private float startTime, journeyLength;
    public bool runningGame = false;
    public bool noclip = false;
    public int score = 0;
    public int turn = 0;
    ScoreScript scoreTxt;
    headlineTxt headlineTxt;
    public Text extralives;
    public Text textinvulnarebility;
    StartButton button1;
    public Random rng;
    public int extralife;
    public int remaining_invulnarebility;
    //public GameObject startbutton;

    public ShowSplashImageCanvas splashgroup;

    void Awake() {
        scoreTxt = FindObjectOfType<ScoreScript>();
        button1 = FindObjectOfType<StartButton>();
        headlineTxt = FindObjectOfType<headlineTxt>();
        
    }
	// Use this for initialization
	void Start () {
        //newGame();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)){
            noclip = !noclip;
        }
        if(Input.GetKey(KeyCode.N) && !runningGame)
            newGame();            

        if (remaining_invulnarebility > 0)
            remaining_invulnarebility--;
        if(runningGame)
            Time.timeScale += 0.0001f; //speeding up the game
	}

    public void newGame(){
        //Debug.Log("New Game(Manager)");
        headlineTxt.changeState(false);
        FindObjectOfType<FruitSpawn>().newGame();
        addScore(-score);
        turn = 0;
        extralife = 0;
        setExtralife(10);
        button1.transform.position = new Vector3(0.5f, 1.5f, -5f);
        runningGame = true;
        Time.timeScale = 1;
        splashgroup.hideAllSplashImage();

    }
    public void addScore(int i){
        score += i;
        scoreTxt.setScore(score);
    }

    public void setScore(int i)
    {
        score = i;
        scoreTxt.setScore(score);
    }

    public void setExtralife(int l)
    {
        extralife = l;
        // TODO delte
        extralives.text = "Leben: " + extralife;
    }

    public void addExtralife(int l)
    {
        extralife += l;
        // TODO delete
        extralives.text = "Leben: " + extralife;
    }


    public bool checkforitem()
    {
        //  Zusammenfassung:
        //Returns true, if there is an item going to be spawned
        if ((turn) % 3 == 0 && turn != 0)
            return true;
        return false;
    }

    public float propability()
    {
        //Returns a propability whats the chance of an high tier obstacle to spawn
        if (turn < 5)
            return 0;
        if (turn < 10)
            return 0.2f;
        return 0.4f;
    }

    public void increaseInvulnarebility(int i) {
        remaining_invulnarebility += i;
    }

    public void setInvulnarebility(int i){
        remaining_invulnarebility = i;
    }

    public void EndGame() {
        runningGame = false;
        headlineTxt.changeState(true);
        button1.transform.position = new Vector3(0.5f, 1.5f, -1.75f);

    }
}
