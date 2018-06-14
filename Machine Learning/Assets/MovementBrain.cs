using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBrain : MonoBehaviour {

	public Node[] inputs;
	public Node[] hiddenLayer;
	public Node output;
	public int hiddenLayerSize;
	public int inputAmt;
	public float fitness;
	public float minNum;
	public float maxNum;
	public bool go;
	public LayerMask mask;
	public float[] WandB;
	public int numOfWB;

	// Use this for initialization
	void Start () {
		go = false;
		numOfWB = (inputAmt * hiddenLayerSize) + (hiddenLayerSize * 2) + 1;
		//WandB = new float[numOfWB];
	}

	// Update is called once per frame
	void Update () {
		if(go){
			GetInputs();
			ComputeOutput();
			Move();
			CalcFitness();
		} else {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
	}

	public void StartMoving(){
		go = true;
	}

	public void InitiateNetwork(){
		output = new Node(0);
		inputs = new Node[inputAmt];
		hiddenLayer = new Node[hiddenLayerSize];
		for(int i = 0; i < inputAmt; i++){
			inputs[i] = new Node(hiddenLayerSize);
			for(int j = 0; j < inputs[i].weights.Length; j++){
				inputs[i].weights[j] = Random.Range(minNum, maxNum);
			}
		}
		for(int i = 0; i < hiddenLayerSize; i++){
			hiddenLayer[i] = new Node(1);
			hiddenLayer[i].bias = Random.Range(minNum, maxNum);
			hiddenLayer[i].weights[0] = Random.Range(minNum, maxNum);
		}
		output.bias = Random.Range(minNum, maxNum);
		GetWeightsBiases();
	}

	public void ComputeOutput(){
		ClearData();
		for(int i = 0; i < inputAmt; i++){
			for(int j = 0; j < hiddenLayerSize; j++){
				hiddenLayer[j].value = hiddenLayer[j].value + (inputs[i].value * inputs[i].weights[j]);
			}
		}
		output.value = 0;
		for(int i = 0; i < hiddenLayerSize; i++){
			hiddenLayer[i].value = hiddenLayer[i].value + hiddenLayer[i].bias;
			output.value = output.value + (hiddenLayer[i].value * hiddenLayer[i].weights[0]);
		}
		output.value = output.value + output.bias;
		if(output.value > 1000f) output.value = 1000;
		if(output.value < -1000f) output.value = -1000;
		output.value = output.value / 1000f;
	}

	public void ClearData(){
		for(int i = 0; i < hiddenLayerSize; i++){
			hiddenLayer[i].value = 0;
		}
		output.value = 0;
	}

	public void CalcFitness(){
		float total = 0;
		for(int i = 0; i < inputAmt; i++){
			total += (float)inputs[i].value;
		}
		fitness += (float)(total / inputAmt) / 100f;
	}

	public void GetInputs(){
		RaycastHit2D[] hit = new RaycastHit2D[5];
		hit[0] = Physics2D.Raycast(transform.position, Vector2.right * 10, mask.value);
		hit[1] = Physics2D.Raycast(transform.position, Vector2.left * 10, mask.value);
		hit[2] = Physics2D.Raycast(transform.position, Vector2.up * 10, mask.value);
		hit[3] = Physics2D.Raycast(transform.position, new Vector2(10,10), mask.value);
		hit[4] = Physics2D.Raycast(transform.position, new Vector2(-10, 10), mask.value);
		for(int i = 0; i < 5; i++){
			if(hit[i].collider != null){
				inputs[i].value = hit[i].distance;
			}
		}
	}

	public void Move(){
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(output.value, 1f);
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Wall"){
			go = false;
		} else if(col.gameObject.tag == "Bonus Wall"){
			go = false;
			fitness = fitness * 3;
		}
	}

	public void GetWeightsBiases(){
		numOfWB = (inputAmt * hiddenLayerSize) + (hiddenLayerSize * 2) + 1;
		float[] wb = new float[numOfWB];
		for(int i = 0; i < inputAmt; i++){
			for(int j = 0; j < hiddenLayerSize; j++){
				wb[(i * hiddenLayerSize) + j] = inputs[i].weights[j];
			}
		}
		int startIndex = inputAmt * hiddenLayerSize;
		for(int i = 0; i < hiddenLayerSize; i++){
			wb[startIndex + (2 * i)] = hiddenLayer[i].bias;
			wb[startIndex + (2 * i) + 1] = hiddenLayer[i].weights[0];
		}
		wb[numOfWB - 1] = output.bias;
		WandB = wb;
	}

	public void SetWeightsandBiases(float[] wb){
		numOfWB = (inputAmt * hiddenLayerSize) + (hiddenLayerSize * 2) + 1;
		output = new Node(0);
		inputs = new Node[inputAmt];
		hiddenLayer = new Node[hiddenLayerSize];
		for(int i = 0; i < inputAmt; i++){
			inputs[i] = new Node(hiddenLayerSize);
			for(int j = 0; j < hiddenLayerSize; j++){
				inputs[i].weights[j] = wb[(i * hiddenLayerSize) + j];
			}
		}
		int startIndex = inputAmt * hiddenLayerSize;
		for(int i = 0; i < hiddenLayerSize; i++){
			hiddenLayer[i] = new Node(1);
			hiddenLayer[i].bias = wb[startIndex + (2 * i)];
			hiddenLayer[i].weights[0] = wb[startIndex + (2 * i) + 1];
		}
		output.bias = wb[numOfWB - 1];
		WandB = wb;
	}

	public void RemoveFromScene(){
		Destroy(gameObject);
	}
}
