using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //public GameObject player;
    public TextMeshProUGUI timerText;
    public int totalSeconds = 12;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = totalSeconds - Time.time - startTime;
        string seconds = (t % 60).ToString("f1");

        if (t <= 0)
        {
            //GameObject.Find("Player").SendMessage("NoMoreTime");
            //player.SendMessage("NoMoreTime");
            GameObject.FindObjectOfType<PlayerController>().NoMoreTime();
        }
        else
        {
            timerText.SetText(seconds);
        }        
    }

}
