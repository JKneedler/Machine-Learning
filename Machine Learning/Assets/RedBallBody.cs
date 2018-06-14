using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallBody : MonoBehaviour {

	public BackBrain nn;
	public bool go;
	public float[] inputs;
	public LayerMask mask;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(go){
			GetInputs();
			Move();
		} else {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}

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
				inputs[i] = hit[i].distance;
			}
		}
	}

	public void Move(){
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(nn.FeedForward(inputs)[0], 1f);
	}

}
