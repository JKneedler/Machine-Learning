using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackBrain {

	public int inputSize;
	public int hiddenLayerSize;
	public int outputSize;
	public Matrix ihWeights;
	public Matrix hoWeights;
	public Matrix hBias;
	public Matrix oBias;
	public float learningRate;
	public float cost;

	public BackBrain(int inputSize, int hiddenLayerSize, int outputSize, int minRandom, int maxRandom, float learningRate){
		this.inputSize = inputSize;
		this.hiddenLayerSize = hiddenLayerSize;
		this.outputSize = outputSize;
		ihWeights = new Matrix(hiddenLayerSize, inputSize);
		hoWeights = new Matrix(outputSize, hiddenLayerSize);
		ihWeights.Randomize(minRandom, maxRandom);
		hoWeights.Randomize(minRandom, maxRandom);
		hBias = new Matrix(hiddenLayerSize, 1);
		oBias = new Matrix(outputSize, 1);
		hBias.Randomize(minRandom, maxRandom);
		oBias.Randomize(minRandom, maxRandom);
		this.learningRate = learningRate;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public float[] FeedForward(float[] input){
		Matrix inputM = Matrix.FromArray(input);
		Matrix hiddenValues = Matrix.DotProduct(ihWeights, inputM);
		hiddenValues.ElementAdd(hBias);
		hiddenValues.Map(new System.Func<float, float>(Sigmoid));
		Matrix outputValues = Matrix.DotProduct(hoWeights, hiddenValues);
		outputValues.ElementAdd(oBias);
		outputValues.Map(new System.Func<float, float>(Sigmoid));
		float[] outputValuesArray = outputValues.ToArray();
		return outputValuesArray;
	}

	public void Train(float[] inputs, float[] targets){
		Matrix inputM = Matrix.FromArray(inputs);
		Matrix hiddenValues = Matrix.DotProduct(ihWeights, inputM);
		hiddenValues.ElementAdd(hBias);
		hiddenValues.Map(new System.Func<float, float>(Sigmoid));
		Matrix outputs = Matrix.DotProduct(hoWeights, hiddenValues);
		outputs.ElementAdd(oBias);
		outputs.Map(new System.Func<float, float>(Sigmoid));

		Matrix targetsM = Matrix.FromArray(targets);

		Matrix outError = Matrix.ElementSubtract(targetsM, outputs);
		Matrix weightTransposeOut = Matrix.Transpose(hoWeights);
		Matrix hiddenError = Matrix.DotProduct(weightTransposeOut, outError);

		Matrix gradient = Matrix.MapStatic(outputs, new System.Func<float, float>(DSigmoid));
		gradient.ElementMultiply(outError);
		gradient.ScalarMultiply(learningRate);

		Matrix hiddenTranspose = Matrix.Transpose(hiddenValues);
		Matrix weightHoDeltas = Matrix.DotProduct(gradient, hiddenTranspose);
		this.hoWeights.ElementAdd(weightHoDeltas);
		this.oBias.ElementAdd(gradient);

		Matrix hiddenGradient = Matrix.MapStatic(hiddenValues, new System.Func<float, float>(DSigmoid));
		hiddenGradient.ElementMultiply(hiddenError);
		hiddenGradient.ScalarMultiply(learningRate);

		Matrix inputTranspose = Matrix.Transpose(inputM);
		Matrix weightIhDeltas = Matrix.DotProduct(hiddenGradient, inputTranspose);
		this.ihWeights.ElementAdd(weightIhDeltas);
		this.hBias.ElementAdd(hiddenGradient);
	}

	public float Sigmoid(float num){
		return 1 / (1 + Mathf.Exp(-num));
	}

	public float DSigmoid(float num){
		return num * (1 - num);
	}

}
