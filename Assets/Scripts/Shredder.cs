﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 1)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				//MY CODE
				Debug.Log("YO");
			}
		}
		if (Application.isEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{
				
			}

		}
	}



}