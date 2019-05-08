using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRProgressBar : MonoBehaviour
{
    public GameObject cards;
    public Text text;
    public UnityEngine.UI.Image progressBar;
    float percentage;

    int roundCount;
    int currentRound;

    bool ready = false;



    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
        progressBar.GetComponent<UnityEngine.UI.Image>();
        progressBar.fillAmount = 0;
        percentage = (float)1 / (cards.GetComponent<VRShuffle>().maxRounds);
        roundCount = cards.GetComponent<VRShuffle>().roundCount;
        //Debug.Log("Round Count: " + roundCount);
        //Debug.Log("Current Round: " + currentRound);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {

            roundCount = cards.GetComponent<VRShuffle>().roundCount;
            if (roundCount == 0)
            {
                currentRound = roundCount;
                ready = true;
            }
        }
        else
        {
            roundCount = cards.GetComponent<VRShuffle>().roundCount;

            if (currentRound != roundCount)
            {
                if (currentRound == 0)
                {
                    text.enabled = true;
                }
                increasePercentage();
                currentRound++;
            }

        }

        if (cards.GetComponent<VRShuffle>().completeCheck)
            increasePercentage();

        //Debug.Log("Round Count: " + roundCount);
        //Debug.Log("Current Round: " + currentRound);
        //Debug.Log("Current Fill Amount: " + progressBar.fillAmount);



    }

    public void increasePercentage()
    {
        if (roundCount <= 7)
        {
            progressBar.fillAmount += percentage;
        }
    }
}
