using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticBrain {

		public int inputSize;
		public int hiddenLayerSize;
		public int outputSize;
		public Matrix ihWeights;
		public Matrix hoWeights;
		public Matrix hBias;
		public Matrix oBias;
		public float fitness;
		public bool go;
		public GeneticController control;

		public GeneticBrain(int inputSize, int hiddenLayerSize, int outputSize, int minRandom, int maxRandom, GeneticController control){
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
			this.fitness = 0;
			this.control = control;
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void Go(){
			go = true;
		}

		public void Done(){
			go = false;
			control.AddDone();
		}

		public float[] FeedForward(float[] input){
			Matrix inputM = Matrix.FromArray(input);
			Matrix hiddenValues = Matrix.DotProduct(ihWeights, inputM);
			hiddenValues.ElementAdd(hBias);
			hiddenValues.Map(new System.Func<float, float>(Activation));
			Matrix outputValues = Matrix.DotProduct(hoWeights, hiddenValues);
			outputValues.ElementAdd(oBias);
			outputValues.Map(new System.Func<float, float>(Activation));
			float[] outputValuesArray = outputValues.ToArray();
			return outputValuesArray;
		}

		public float Activation(float num){
			return 1 / (1 + Mathf.Exp(-num));
		}
}
