using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffle : MonoBehaviour {

	public int patternSize = 6;
    public float lightTime = 1;
    float timer = 0;
	Transform[] cards;
    List<int> pattern = new List<int>();
    List<string> patNames = new List<string>();
    int itertor = 0;

	void Start () {
		cards = GetComponentsInChildren<Transform>();

        Shuffle();
        CreatePat();

        /*foreach (int str in pattern) {
            print(str);
        }
        foreach (string str in patNames)
        {
            print(str);
        }*/
    }

	void Update () {
        if (itertor < patternSize){
            timer += Time.deltaTime;
            if (timer >= lightTime){
                cards[pattern[itertor]].GetComponent<Light>().enabled = true;
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer >= lightTime){
                cards[pattern[itertor]].GetComponent<Light>().enabled = false;
                timer = 0;
                itertor++;
            }
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
        while(pattern.ToArray().Length != patternSize){ 
			int ranNum = Random.Range(1, 26);
            if (!pattern.Contains(ranNum))
                pattern.Add(ranNum);                
		}
        foreach (int num in pattern) {
            patNames.Add(cards[num].GetComponent<Transform>().name);
        }

    }


}
