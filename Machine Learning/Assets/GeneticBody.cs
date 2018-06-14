using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticBody : MonoBehaviour {

	public GeneticBrain nn;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Currently adding to fitness every frame, can change to wait till end
		if(nn.go){
			CalcFitness();
			DoThing();
		}
	}

	public void CalcFitness(){
		//Add to Fitness each Frame or Change to CalcFitness at end
	}

	public void DoThing(){

	}

	public void EndOfThing(){
		nn.Done();
	}
}
