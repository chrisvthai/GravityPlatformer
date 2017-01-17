using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour {

    public GameObject player;
    public GameObject laserEmitter;
    public GameObject laserBeam;
    public string laserDirection; //Can be UP, DOWN, LEFT, or RIGHT
    public string color; //Can be BLUE, RED, GREEN

    private bool laserOn;
    private bool playerIntersect;

	// Use this for initialization
	void Start () {
        laserOn = false;
        playerIntersect = false;

        switch (color) //For changing the sprite of the switch
        {
            case "BLUE":
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchBlueOff", typeof(Sprite)) as Sprite;
                break;
            case "RED":
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchRedOff", typeof(Sprite)) as Sprite;
                break;
            case "GREEN":
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchGreenOff", typeof(Sprite)) as Sprite;
                break;
        }
        
    }
	
    void Update()
    {
        if (playerIntersect && Input.GetKeyDown(KeyCode.E))
        {
            if (laserOn == false)
            {
                laserOn = true;
            }
            else laserOn = false;
        }

        if (laserOn == false)
        {
            laserBeam.SetActive(false);

            switch (color) //For changing the sprite of the switch
            {
                case "BLUE":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchBlueOff", typeof(Sprite)) as Sprite;
                    break;
                case "RED":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchRedOff", typeof(Sprite)) as Sprite;
                    break;
                case "GREEN":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchGreenOff", typeof(Sprite)) as Sprite;
                    break;
            }

            switch (laserDirection) //For changing the sprite of the laser emitter
            {
                case "UP":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserUp", typeof(Sprite)) as Sprite;
                    break;
                case "DOWN":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserDown", typeof(Sprite)) as Sprite;
                    break;
                case "LEFT":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserLeft", typeof(Sprite)) as Sprite;
                    break;
                case "RIGHT":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserRight", typeof(Sprite)) as Sprite;
                    break;
            }

        }
        else {
            laserBeam.SetActive(true);

            switch (color) //For changing the sprite of the switch
            {
                case "BLUE":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchBlueOn", typeof(Sprite)) as Sprite;
                    break;
                case "RED":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchRedOn", typeof(Sprite)) as Sprite;
                    break;
                case "GREEN":
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchGreenOn", typeof(Sprite)) as Sprite;
                    break;
            }

            switch (laserDirection) //For changing the sprite of the laser emitter
            {
                case "UP":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserUpShoot", typeof(Sprite)) as Sprite;
                    break;
                case "DOWN":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserDownShoot", typeof(Sprite)) as Sprite;
                    break;
                case "LEFT":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserLeftShoot", typeof(Sprite)) as Sprite;
                    break;
                case "RIGHT":
                    laserEmitter.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserRightShoot", typeof(Sprite)) as Sprite;
                    break;
            }
        }
    }
   

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerIntersect = true;

        /*if (other.gameObject.CompareTag("Player") &&  Input.GetKeyDown(KeyCode.E))
        {
            if (laserOn == false)
            {
                laserOn = true;
                //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchBlueOn", typeof(Sprite)) as Sprite;

            } 

            if (laserOn == true)
            {
                laserOn = false;
                //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("laserSwitchBlueOff", typeof(Sprite)) as Sprite;
            }
        }*/
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerIntersect = false;
    }
    
		
	
}
