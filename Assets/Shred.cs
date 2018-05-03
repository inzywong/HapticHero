using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shred : MonoBehaviour {

	int count;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButton(0)) {
			// Vector2 touchPos = Input.GetTouch(0).position;
			Vector2 touchPos = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(touchPos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000))
			{
				// string name = hit.transform.gameObject.name;
				Debug.Log("Hej: "+ ++count);
			}
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shred : MonoBehaviour {

	int count;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButton(0)) {
			// Vector2 touchPos = Input.GetTouch(0).position;
			Vector2 touchPos = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(touchPos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000))
			{
				// string name = hit.transform.gameObject.name;
				Debug.Log("Hej: "+ ++count);
			}
		}
	}
}
