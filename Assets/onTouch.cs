using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTouch : MonoBehaviour
{
  public int[] vibLength = new int[6];
  private string[] stringNames = new string[] { "E", "A", "D", "G", "B", "e" };
  private Dictionary<string, int> stringVib;

  // Use this for initialization
  void Start()
  {
    stringVib = new Dictionary<string, int>();
    for (int i = 0; i < stringNames.Length; i++)
    {
      stringVib.Add(stringNames[i], vibLength[i]);
    }
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 touchPos = Input.GetTouch(0).position;
    Ray ray = Camera.main.ScreenPointToRay(touchPos);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100))
    {
      string name = hit.transform.gameObject.name;
      PlayString(name);
    }
  }

  private void PlayString(string name)
  {
    Vibration.CreateOneShot(stringVib[name]);
  }
}
