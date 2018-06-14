using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GeneticController : SerializedMonoBehaviour {

	public GeneticBrain[] pop;
	public int populationSize;
	public int inputSize;
	public int hiddenLayerSize;
	public int outputSize;
	public float mutationRate;
	public int minRandom;
	public int maxRandom;
	public Object item;
	public Transform itemParent;
	public bool genDone;
	public int generationNum;
	public float maxFitness;
	public List<float> avgFitness;
	public int totalDone;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void StartAll(){
		for(int i = 0; i < populationSize; i++){
			pop[i].Go();
		}
	}

	public void AddDone(){
		totalDone++;
		if(totalDone == populationSize){
			StartCoroutine("Done");
			totalDone = 0;
		}
	}

	IEnumerator Done(){
		GetFitnessData();
		yield return new WaitForSeconds(5);
		NextGeneration();
	}

	IEnumerator WaitForStart(){
		yield return new WaitForSeconds(2);
		StartAll();
	}

	public void InitiatePopulation(){
		for(int i = 0; i < populationSize; i++){
			pop[i] = new GeneticBrain(inputSize, hiddenLayerSize, outputSize, minRandom, maxRandom, this);
		}
		StartCoroutine("WaitForStart");
	}

	public void PopulateScene(){
		for(int i = 0; i < populationSize; i++){
			GameObject itemInstance = (GameObject)Instantiate(item, itemParent.position, Quaternion.identity, itemParent);
			itemInstance.GetComponent<GeneticBody>().nn = pop[i];
		}
		StartCoroutine("WaitForStart");
	}

	public void NextGeneration(){
		RemovePrevGen();
		generationNum++;
		List<GeneticBrain> pool = CreatePool();
		GeneticBrain[] newPop = new GeneticBrain[populationSize];
		for(int i = 0; i < populationSize; i++){
			newPop[i] = new GeneticBrain(inputSize, hiddenLayerSize, outputSize, minRandom, maxRandom, this);
			GeneticBrain parent1 = pool[Random.Range(0, pool.Count)];
			GeneticBrain parent2 = pool[Random.Range(0, pool.Count)];
			newPop[i].hoWeights = Matrix.MixedMatrix(parent1.hoWeights, parent2.hoWeights, mutationRate, minRandom, maxRandom);
			newPop[i].ihWeights = Matrix.MixedMatrix(parent1.ihWeights, parent2.ihWeights, mutationRate, minRandom, maxRandom);
			newPop[i].oBias = Matrix.MixedMatrix(parent1.oBias, parent2.oBias, mutationRate, minRandom, maxRandom);
			newPop[i].hBias = Matrix.MixedMatrix(parent1.hBias, parent2.hBias, mutationRate, minRandom, maxRandom);
		}
		pop = newPop;
		PopulateScene();
	}

	public void RemovePrevGen(){
		for(int i = 0; i < populationSize; i++){
			Destroy(itemParent.GetChild(i).gameObject);
		}
	}

	public List<GeneticBrain> CreatePool(){
		List<GeneticBrain> probPool = new List<GeneticBrain>();
		for(int i = 0; i < populationSize; i++){
			for(int j = 0; j < pop[i].fitness; j++){
				probPool.Add(pop[i]);
			}
		}
		return probPool;
	}

	public void GetFitnessData(){
		float sum = 0;
		for(int i = 0; i < pop.Length; i++){
			if(pop[i].fitness > maxFitness) maxFitness = pop[i].fitness;
			sum += pop[i].fitness;
		}
		avgFitness.Add(sum / populationSize);
	}
}
