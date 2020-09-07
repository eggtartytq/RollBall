using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countDown;
    public int secondLeft = 30;

    private bool finished = false;
    private bool takingAway = false;
    
    // Start is called before the first frame update
    void Start()
    {
        countDown.text = secondLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(finished == false && takingAway == false && secondLeft > 0)
        {
            StartCoroutine(CountDownStart());
        }
        if(secondLeft <= 0)
        {
            countDown.text = "YOU LOSE!";
        }
    }

    IEnumerator CountDownStart()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondLeft -= 1;
        countDown.text = secondLeft.ToString();
        takingAway = false;
    }

    public void Finish()
    {
        finished = true;
    }
}
