using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MovementPop : MonoBehaviour {

	public MovementBrain[] population;
	//public float[][] populationNums;
	public int populationSize;
	public float mutationRate;
	public Object redBall;
	public Transform ballParent;
	public int inputs;
	public int hiddenLayerSize;
	public int numOfWB;
	public float maxFitness;
	public bool allDone;
	public int generation;
	private float totalfitness;
	public float avgFitness;
	public float totalAmt;

	// Use this for initialization
	void Start () {
		population = new MovementBrain[populationSize];
		numOfWB = (inputs * hiddenLayerSize) + (hiddenLayerSize * 2) + 1;
		//populationNums = new float[populationSize][];
	}

	// Update is called once per frame
	void Update () {

	}

	public void PopulateScene(){
		for(int i = 0; i < populationSize; i++){
			GameObject ball = (GameObject)Instantiate(redBall, transform.position, Quaternion.identity, ballParent);
			population[i] = ball.GetComponent<MovementBrain>();
			population[i].InitiateNetwork();
			population[i].ComputeOutput();
		}
		StartCoroutine("WaitTillNext");
	}

	public void StartAll(){
		for(int i = 0; i < population.Length; i++){
			population[i].StartMoving();
		}
		StartCoroutine("WaitForTrial");
	}

	public void FindMaxFitness(){
		totalAmt = 0;
		totalfitness = 0;
		for(int i = 0; i < populationSize; i++){
			totalfitness += population[i].fitness;
			totalAmt++;
			if(population[i].fitness > maxFitness){
				maxFitness = population[i].fitness;
			}
		}
		avgFitness = totalfitness/totalAmt;
	}

	IEnumerator WaitForTrial(){
		while(!allDone){
			yield return new WaitForSeconds(3);
			allDone = CheckIfDone();
		}
	}

	public bool CheckIfDone(){
		for(int i = 0; i < populationSize; i++){
			if(population[i].go){
				return false;
			}
		}
		//Debug.Log("Start Again");
		//NextGeneration();
		return true;
	}

	public void NextGeneration(){
		StopCoroutine("WaitTillNext");
		generation++;
		float[][] newPop = new float[populationSize][];
		List<MovementBrain> probPool = CreatePool();
		for(int i = 0; i < populationSize; i++){
			MovementBrain parent1 = probPool[Random.Range(0, probPool.Count)];
			MovementBrain parent2 = probPool[Random.Range(0, probPool.Count)];
			float[] gene = new float[numOfWB];
			Debug.Log(numOfWB);
			for(int j = 0; j < numOfWB; j++){
				if(Random.Range(0f, 1f) < mutationRate){
					gene[j] = Random.Range(parent1.minNum, parent1.maxNum);
				} else {
					if(Random.Range(0, 2) == 1){
						gene[j] = parent1.WandB[j];
					} else {
						gene[j] = parent2.WandB[j];
					}
				}
			}
			newPop[i] = gene;
		}
		for(int i = 0; i < populationSize; i++){
			population[i].RemoveFromScene();
			GameObject ball = (GameObject)Instantiate(redBall, transform.position, Quaternion.identity, ballParent);
			population[i] = ball.GetComponent<MovementBrain>();
			population[i].SetWeightsandBiases(newPop[i]);
			population[i].ComputeOutput();
		}
		StartCoroutine("WaitTillNext");
	}

	IEnumerator WaitTillNext(){
		yield return new WaitForSeconds(2);
		StartAll();
		yield return new WaitForSeconds(20);
		FindMaxFitness();
		NextGeneration();
	}

	public List<MovementBrain> CreatePool(){
		List<MovementBrain> probPool = new List<MovementBrain>();
		for(int i = 0; i < populationSize; i++){
			for(int j = 0; j < population[i].fitness; j++){
				probPool.Add(population[i]);
			}
		}
		return probPool;
	}
}
