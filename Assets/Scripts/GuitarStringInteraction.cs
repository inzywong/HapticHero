using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitarStringInteraction : MonoBehaviour
{
  public float swingSpeed = 10;
  public float swingDecay = 2;
  public float maxRingTime = 1.5f;
  // String states
  enum States { Idle, Playing, Ringing };
  States myState;
  public Text currentState;
  LineRenderer guitarString;
  float distToLine;
  float ringingAmplitude;
  float ringTime;
  AudioSource audioSource;

  void Start()
  {
    myState = States.Idle;
    guitarString = GetComponent<LineRenderer>();
    distToLine = transform.position.z - Camera.main.transform.position.z;
    audioSource = GetComponent<AudioSource>();
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
          guitarString.SetPosition(1, worldPos); // Change position of middle point in linerenderer¨
          break;
      }
    }

    // When the string is released
    // TODO: Add for phone too
    if (Input.GetMouseButtonUp(0) && myState == States.Playing)
    {
      SetText("Ringing");
      myState = States.Ringing;
      ringingAmplitude = guitarString.GetPosition(1).x;
      ringTime = 0;
      audioSource.Play();
      // TODO: Start vibration
    }

    // Make the string move back and forth like a spring
    if (myState == States.Ringing)
    {
      Vector3 gp = guitarString.GetPosition(1);
      if (ringTime > maxRingTime)
      {
        // TODO: Stop vibration
        gp.x = 0;
        guitarString.SetPosition(1, gp);
        myState = States.Idle;
        audioSource.Stop();
        return;
      }
      gp.x = ringingAmplitude / (Mathf.Pow(1 + ringTime, swingDecay)) * Mathf.Cos(ringTime * swingSpeed);
      guitarString.SetPosition(1, gp);
      ringTime += Time.deltaTime;
    }
  }

  void SetText(string text)
  {
    currentState.text = text;
  }

}
