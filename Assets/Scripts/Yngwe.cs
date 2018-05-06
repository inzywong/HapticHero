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
  LineRenderer guitarString;
  float distToLine;

  public CapsuleCollider cl;

  void Start()
  {
    myState = States.Idle;
    guitarString = GetComponent<LineRenderer>();
    distToLine = transform.position.z - Camera.main.transform.position.z;
  }

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
        // Change position of second position in linerenderer to follow cursor
        case States.Playing:
          // Give depth to touch pos to get correct screenToWorldPoint
          Vector3 worldPos = new Vector3(touchPos.x, touchPos.y, distToLine);
          worldPos = Camera.main.ScreenToWorldPoint(worldPos);
          worldPos.z = 0; // Force same z position
          guitarString.SetPosition(1, worldPos); // Change position of middle point
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
