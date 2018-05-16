using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuitarStringInteraction : MonoBehaviour
{
  public float swingSpeed = 10;
  public float swingDecay = 2;
  public float maxRingTime = 1.5f;
	private float maxDistance = 0.5f;
  // String states
  enum States { Idle, Playing, Ringing };
  States myState;
  public Text currentState;
  LineRenderer guitarString;
  public float distToLine;
  float ringingAmplitude;
  float ringTime;
  AudioSource audioSource;

	bool useVibration;
	bool useSound;

  void Start()
  {
    myState = States.Idle;
    guitarString = GetComponent<LineRenderer>();
    distToLine = transform.position.z - Camera.main.transform.position.z;
    audioSource = GetComponent<AudioSource>();

		useVibration = (PlayerPrefs.GetInt("Vibration", 0) == 1 ? true : false);
		useSound = (PlayerPrefs.GetInt("Sound", 0) == 1 ? true : false);
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
						// Check if ray hits collider attached to this object, otherwize all strings will trigger
            if(hit.collider == GetComponent<BoxCollider>()){
              myState = States.Playing;
              SetText("Playing");
            }
          }
          break;
        // Change position of second position in linerenderer to follow cursor
        case States.Playing:
          // Give depth to touch pos to get correct screenToWorldPoint
          Vector3 worldPos = new Vector3(touchPos.x, touchPos.y, distToLine);
          worldPos = Camera.main.ScreenToWorldPoint(worldPos);
          worldPos.z = 0; // Force z position to z, which is the default z pos of the string
					Vector3 relativePos = worldPos - transform.position;
          guitarString.SetPosition(1, relativePos); // Change position of middle point in linerenderer¨

					VibrationA7.Cancel(); // Cancel if string is grabbed whilst ringing
					audioSource.Stop();
					if (Mathf.Abs(relativePos.x) > maxDistance)
					{
						ReleaseString();
					}

          break;
      }
    }

    // When the string is released
    if (Input.GetMouseButtonUp(0) && myState == States.Playing)
    {
			ReleaseString();
    }

    // Make the string move back and forth like a spring
    if (myState == States.Ringing)
    {
      Vector3 gp = guitarString.GetPosition(1);
      if (ringTime > maxRingTime)
      {
        gp.x = 0;
        guitarString.SetPosition(1, gp);
        myState = States.Idle;
        audioSource.Stop();
        SetText("Idle");
        // Vibration.Cancel();
        VibrationA7.Cancel();
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

	void ReleaseString()
	{
		SetText("Ringing");
		myState = States.Ringing;
		ringingAmplitude = guitarString.GetPosition(1).x;
		ringTime = 0;
		if(useSound)
			audioSource.Play();
		if(useVibration)
			VibrationA7.Vibrate(2000);
	}

}
