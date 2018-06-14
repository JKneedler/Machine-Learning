using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {

	public float[] weights;
	public float bias;
	public float value;

	public Node(int weightAmt){
		weights = new float[weightAmt]; 
	}
}
