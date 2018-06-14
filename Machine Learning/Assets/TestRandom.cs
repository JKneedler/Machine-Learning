using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingData {
	public float[] inputs;
	public float[] targets;

	public TrainingData(float[] inputs, float[] targets){
		this.inputs = inputs;
		this.targets = targets;
	}
}

public class TestRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// float[] ar = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
		// Matrix m = new Matrix(3,4,ar);
		// float[] ma1 = {1,2,3,4,5,6,1,2,3};
		// float[] ma2 = {5,2,8,4};
		// float[] ma3 = {2,5,3};
		// Matrix m2 = new Matrix(3,3, ma1);
		// Matrix m3 = new Matrix(2,2,ma2);
		// Matrix m4 = new Matrix(1,3, ma3);
		// Debug.Log(m);
		// m.ScalarMultiply(2);
		// Debug.Log(m);
		// m.ScalarAdd(1);
		// Debug.Log(m);
		// m2.ElementMultiply(m3);
		// Debug.Log(m2);
		// Debug.Log(m4);
		// Matrix m4 = new Matrix(2,2);
		// m4.Randomize(-10, 10);
		// Debug.Log(m4);
		// Debug.Log(Matrix.DotProduct(m4, m2));
		// Debug.Log(m);
		// Debug.Log(Matrix.Transpose(m));
		//runadd2(new System.Func<int, int>(add2));
		// MyBrain nn = new MyBrain(3, 2, 1, 0, 1, .1f);
		// Debug.Log(nn.ihWeights);
		// Debug.Log(nn.hoWeights);
		// float[] ins = {1, 2, 3};
		// float[] targets = {1};
		// Debug.Log(nn.FeedForward(ins)[0]);
		// nn.Train(ins, targets);
		// TrainingData[] trainData = new TrainingData[4];
		// trainData[0] = new TrainingData(new float[] {1,1}, new float[] {0});
		// trainData[1] = new TrainingData(new float[] {0,0}, new float[] {0});
		// trainData[2] = new TrainingData(new float[] {0,1}, new float[] {1});
		// trainData[3] = new TrainingData(new float[] {1,0}, new float[] {1});
		// MyBrain nn = new MyBrain(2, 2, 1, 0, 1, .1f);
		// for(int i = 0; i < 10000; i++){
		// 	foreach(TrainingData data in trainData){
		// 		nn.Train(data.inputs, data.targets);
		// 	}
		// }
    //
		// Debug.Log(nn.FeedForward(new float[] {0, 0})[0]);
		// Debug.Log(nn.FeedForward(new float[] {1, 0})[0]);
		// Debug.Log(nn.FeedForward(new float[] {0, 1})[0]);
		// Debug.Log(nn.FeedForward(new float[] {1, 1})[0]);
	}

	// Update is called once per frame
	void Update () {

	}

	public void runadd2(System.Func<int, int> fun){
		Debug.Log(fun(3));
	}

	public int add2(int num){
		return num + 2;
	}
}
