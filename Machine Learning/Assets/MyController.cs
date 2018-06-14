using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour {

	public RedBallBody[] circle;
	public int populationSize;
	public Object redBall;
	public Transform circleParent;
	public int inputLayerSize;
	public int hiddenLayerSize;
	public int outputLayerSize;
	public bool allDone;
	public int generation;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void StartAll(){
		for(int i = 0; i < populationSize; i++){
			circle[i].go = true;
		}
	}

	public void PopulateScene(){
		for(int i = 0; i < populationSize; i++){
			GameObject ball = (GameObject)Instantiate(redBall, transform.position, Quaternion.identity, circleParent);
			ball.GetComponent<RedBallBody>().nn = new BackBrain(5, 4, 1, 0, 1, .1f);
		}
	}

	IEnumerator WaitTillNext(){
		yield return new WaitForSeconds(2);
		StartAll();
	}

}
