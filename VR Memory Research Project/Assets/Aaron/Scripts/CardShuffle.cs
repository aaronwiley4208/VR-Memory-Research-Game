using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffle : MonoBehaviour {

	public int patternSize = 6;
    public float lightTime = 1;
    float timer = 0;
    Transform[] cards;

    List<string> originalPattern = new List<string>();
    List<int> solutionPattern = new List<int>();
    List<int> pattern = new List<int>();//positions
    List<string> patNames = new List<string>();//names of objects

    Dictionary<string, int> cardMap = new Dictionary<string, int>(); // Holds the cards positions updated when they get shuffled

    int[] selectionStatistics;
    int iterator = 0;
    int patternSelectionCount = 0;

    public Camera mainCamera;


	void Start () {
		cards = GetComponentsInChildren<Transform>();

        ButtonShuffle();
        

        /*foreach (int str in pattern) {
            print(str);
        }
        foreach (string str in patNames)
        {
            print(str);
        }*/
    }

	void Update () {

        if (iterator < patternSize)
        {
            timer += Time.deltaTime;
            if (timer >= lightTime)
            {
                cards[pattern[iterator]].GetComponent<Light>().enabled = true;
                timer = 0;
                //print(pattern[iterator]);
                iterator++;
            }
        }
        else if(iterator == patternSize){
            timer += Time.deltaTime;
            if (timer >= lightTime)
            {
                foreach (int num in pattern)
                {
                    cards[num].GetComponent<Light>().enabled = false;
                }
                timer = 0;
                iterator++;
                Shuffle();
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {

            getClickedObjectPosition();
        }

     

    }

    void Shuffle() {
        Transform tempGO;
        for (int i = 1; i < cards.Length; i++)
        {
            int rnd = Random.Range(1, cards.Length);
            tempGO = cards[rnd];
            cards[rnd] = cards[i];
            cards[i] = tempGO;
        }
        mapCards();
        for (int i = 1; i <= 5; i++)
        {//row 1
            cards[i].localPosition = new Vector3(-6 + (i * 2), 4, cards[i].localPosition.z);
        }
        for (int i = 1; i <= 5; i++)
        {//row 2
            cards[5 + i].localPosition = new Vector3(-6 + (i * 2), 2, cards[5 + i].localPosition.z);
        }
        for (int i = 1; i <= 5; i++)
        {//row 3
            cards[10 + i].localPosition = new Vector3(-6 + (i * 2), 0, cards[10 + i].localPosition.z);
        }
        for (int i = 1; i <= 5; i++)
        {//row 4
            cards[15 + i].localPosition = new Vector3(-6 + (i * 2), -2, cards[15 + i].localPosition.z);
        }
        for (int i = 1; i <= 5; i++)
        {//row 5
            cards[20 + i].localPosition = new Vector3(-6 + (i * 2), -4, cards[20 + i].localPosition.z);
        }
        
    }

	void CreatePat(){
        while (pattern.ToArray().Length != patternSize)
        {
            int ranNum = Random.Range(1, 26);
            if (!pattern.Contains(ranNum))
            {
                pattern.Add(ranNum);
            }
        }
        foreach (int num in pattern) {
            patNames.Add(cards[num].GetComponent<Transform>().name);
            originalPattern.Add(cards[num].GetComponent<Transform>().name);
        }

    }


    void getClickedObjectPosition()
    {

        // Replace logic with wand stuff later
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.collider.gameObject.name);
            string hitObjectName = hit.collider.gameObject.name;
            Debug.Log(hitObjectName);

    
            int cardNum;
            int cardPos;

            // Don't allow anymore selections after they all have been completed
            if (patternSelectionCount < patternSize)
            {
                try
                {
                    // get tthe card number and add it to the solution and light up corresponding light
                    System.Int32.TryParse(hitObjectName.Split(new string[] { "Card" }, System.StringSplitOptions.None)[1], out cardNum);
                    patternSelectionCount++;
                    Debug.Log("Selection Count: " + patternSelectionCount);
                    cardMap.TryGetValue(hitObjectName, out cardPos);
                    cards[cardPos].GetComponent<Light>().enabled = true;
                    solutionPattern.Add(cardNum);

                    //If we have all the required selections then check the solution
                    if (patternSelectionCount == patternSize)
                    {
                        //Store the array containing the hits and misses and we can do something with it later if need be.
                        selectionStatistics = checkSolution();

                    }
                }

                //If we didn't hit a card don't allow a hit
                catch (System.IndexOutOfRangeException e)
                {
                    Debug.Log("Didn't hit a card.");
                }
            }

        }
    }

    int[] checkSolution()
    {

        //Checking for solution one by one here we could just change this to say it was correct or incorrect depends on what we want to do
        int[] array = new int[2];
        int hits = 0;
        int misses = 0;

        for (int i = 0; i < patternSize; i++)
        {


            if (("Card" + solutionPattern[i]).Equals(originalPattern[i])){
                Debug.Log("OP: " + originalPattern[i] + "|  SP: " + solutionPattern[i]);
                hits++;

            }
            else
            {
                misses++;
                Debug.Log("OP " + originalPattern[i] + "|  SP: " + solutionPattern[i]);
            }

        }

        array[0] = hits;
        array[1] = misses;

        Debug.Log("Hits = " + array[0] + "Misses = " + array[1]);


        return array;
    }

    public void ButtonShuffle() {
        resetAndClear();
        Shuffle();
        iterator = 0;
        CreatePat();
    }


   void resetAndClear()
    {
        //Reset the pattern selection count
        patternSelectionCount = 0;

        //Cut off all the lights that are on before the next go around.
        try
        {
            int cardNum;
            for (int i=0; i< solutionPattern.Count; i++)
            {
                cardMap.TryGetValue(("Card" + solutionPattern[i]), out cardNum);
                cards[cardNum].GetComponent<Light>().enabled = false;
            }
        } catch (System.ArgumentOutOfRangeException e)
        {

        }

        //clear solution and pattern lists
        solutionPattern.Clear();
        originalPattern.Clear();

    }

    //Build a map of the card names to their current positions;
    void mapCards() { 
            for(int i = 0; i < cards.Length; i++)
        {

            cardMap[cards[i].name] = i;

        }
    }
}
