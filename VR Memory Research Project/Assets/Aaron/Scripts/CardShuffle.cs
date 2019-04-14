using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffle : MonoBehaviour {

	public int patternSize = 6;
	Transform[] cards;
	int[] pattern;


	void Start () {
		cards = GetComponentsInChildren<Transform>();
		Transform tempGO;
		for (int i = 1; i < cards.Length; i++) {
			int rnd = Random.Range (1, cards.Length);
			tempGO = cards [rnd];
			cards [rnd] = cards [i];
			cards [i] = tempGO;
		}
		
		for(int i = 1; i <= 5; i++){//row 1
			cards[i].localPosition = new Vector3 (-6 + (i * 2), 4, cards[i].localPosition.z);
		}
		for(int i = 1; i <= 5; i++){//row 2
			cards[5+i].localPosition = new Vector3 (-6 + (i * 2), 2, cards[5+i].localPosition.z);
		}
		for(int i = 1; i <= 5; i++){//row 3
			cards[10+i].localPosition = new Vector3 (-6 + (i * 2), 0, cards[10+i].localPosition.z);
		}
		for(int i = 1; i <= 5; i++){//row 4
			cards[15+i].localPosition = new Vector3 (-6 + (i * 2), -2, cards[15+i].localPosition.z);
		}
		for(int i = 1; i <= 5; i++){//row 5
			cards[20+i].localPosition = new Vector3 (-6 + (i * 2), -4, cards[20+i].localPosition.z);
		}
	}

	void Update () {
		
	}

	void lightPat(){
		pattern = new int[patternSize];
		for(int i = 0; i < patternSize; i++){
			int ranNum = Random.Range(1, 25);

				pattern[i] = ranNum;
		}
	}
}
