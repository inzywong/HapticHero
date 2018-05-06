using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yngwe : MonoBehaviour
{
  int count;
  // String state
  enum States { Idle, Playing, Ringing };
  States myState;
  public Text currentState;

  void Start()
  {
    myState = States.Idle;
  }

  // Split line into 2 and set endpoints at finger location
  // When finger is released use spring model and set state to Ringing. 
  // When string stops moving, set it to Idle again.
  void Update()
  {
    if (Input.touchCount > 0 || Input.GetMouseButton(0))
    {
      // Vector2 touchPos = Input.GetTouch(0).position; // For phone
      Vector2 touchPos = Input.mousePosition;						// For desktop

      switch (myState)
      {
        // If string is idle and is touched set to Playing
        case States.Idle:
        case States.Ringing:
          Ray ray = Camera.main.ScreenPointToRay(touchPos);
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit, 10000))
          {
            myState = States.Playing;
            SetText("Playing");
          }
          break;
        case States.Playing:
          Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

          break;
      }
    }

    // TODO: Add for phone too
    if (Input.GetMouseButtonUp(0) && myState == States.Playing)
    {
      SetText("Ringing");
      myState = States.Ringing;
    }
  }

  void SetText(string text)
  {
    currentState.text = text;
  }

}
