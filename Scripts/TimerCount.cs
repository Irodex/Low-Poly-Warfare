using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{

    public GameObject textDisplay;
    public int secondsLeft = 600;
    public bool takingAway = false;
    public GameObject player;
    public float playermovent;
  

     void Start()
    {
        textDisplay.GetComponent<Text>().text = "Timer: " + secondsLeft;
        player = GameObject.FindGameObjectWithTag("Player");

        
    }
    void Update()
    {
        if(takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

        if(secondsLeft == 0)
        {
            textDisplay.GetComponent<Text>().text = "Game Over";
            playermovent = player.GetComponent<MPPlayerMovement>().movementSpeed = 0;
           
          
            
            
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "Timer: " + secondsLeft;
        takingAway = false;
    }
}
