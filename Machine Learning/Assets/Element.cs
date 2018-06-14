using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Element {

	public char[] word;
	public float fitness;
	public float prob;

	public Element (int wordLength){
		word = new char[wordLength];
	}

	public void ComputeFitness(char[] targetWordChars){
		for(int i = 0; i < targetWordChars.Length; i++){
			if(word[i] == targetWordChars[i]){
				fitness++;
			}
		}
		ComputeProbability();
	}

	public void ComputeProbability(){
		prob = fitness / word.Length;
	}
}
