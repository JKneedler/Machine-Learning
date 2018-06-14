using UnityEngine;
using System.Collections;

public class Mousetoworld : MonoBehaviour {

	public Vector3 mouse;
	public Vector3 screenPoint;
	public Vector2 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mouse = Input.mousePosition;
		screenPoint = Camera.main.WorldToScreenPoint (transform.localPosition);
		offset = new Vector2 (mouse.x - screenPoint.x, mouse.y - screenPoint.y);
	
	}
}
