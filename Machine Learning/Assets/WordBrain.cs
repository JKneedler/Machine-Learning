using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBrain : MonoBehaviour {

	public Element[] population;
	public int popSize;
	public float mutationRate;
	public int generation;
	public string targetWord;
	public char[] targetWordChars;
	public string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public List<Element> probPool;
	public Element bestElement;
	public Text bestElText;
	public int mutations;
	public bool go;


	// Use this for initialization
	void Start () {
		go = false;
	}

	// Update is called once per frame
	void Update () {
		if(bestElement.fitness < targetWord.Length && go){
			NewGeneration();
		} else {
			go = false;
		}
	}

	public void Go(){
		SetVariablesFromButtons();
		go = true;
	}

	public void SetVariablesFromButtons(){
		targetWordChars = targetWord.ToCharArray ();
		InitiatePopulation ();
	}

	public void InitiatePopulation(){
		population = new Element[popSize];
		for(int i = 0; i < popSize; i++){
			population [i] = new Element(targetWordChars.Length);
			for (int j = 0; j < targetWordChars.Length; j++) {
				population [i].word [j] = st [Random.Range (0, st.Length)];
			}
		}
		bestElement = population[0];
		ComputeFitnesses();
		bestElText.text = "";
		for(int i = 0; i < targetWordChars.Length; i++){
			bestElText.text = bestElText.text + bestElement.word[i];
		}
	}

	public void ComputeFitnesses(){
		probPool.Clear();
		for(int i = 0; i < popSize; i++){
			population[i].ComputeFitness(targetWordChars);
			if(population[i].fitness >= bestElement.fitness){
				bestElement = population[i];
			}
			for(int j = 0; j < population[i].fitness; j++){
				probPool.Add(population[i]);
			}
		}
	}

	public void NewGeneration(){
		NewPopulation();
		ComputeFitnesses();
		bestElText.text = "";
		for(int i = 0; i < targetWordChars.Length; i++){
			bestElText.text = bestElText.text + bestElement.word[i];
		}
		generation++;
	}

	public void NewPopulation(){
		Element[] tempPopulation = new Element[popSize];
		for(int i = 0; i < popSize; i++){
			Element parent1 = population[0];
			Element parent2 = population[1];
			if(probPool.Count > 0){
				parent1 = probPool[(int)Random.Range(0, probPool.Count)];
				parent2 = probPool[(int)Random.Range(0, probPool.Count)];
			}
			tempPopulation[i] = new Element(targetWord.Length);
			for(int j = 0; j < targetWord.Length; j++){
				if(Random.Range(0f, 1f) > .5f){
					tempPopulation[i].word[j] = parent1.word[j];
				} else {
					tempPopulation[i].word[j] = parent2.word[j];
				}
				if(Random.Range(0f, 1f) < mutationRate){
					mutations++;
					tempPopulation[i].word[j] = st[Random.Range(0, st.Length)];
				}
			}
		}
		population = tempPopulation;
	}
}
