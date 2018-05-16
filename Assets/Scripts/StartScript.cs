using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public Button start;
	public Dropdown strings;
	public Toggle vibration;
	public Toggle sound;


	// Use this for initialization
	void Start () {

		vibration.isOn = (PlayerPrefs.GetInt("Vibration", 1) == 1 ? true : false);
		sound.isOn = (PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		string nrOfStrings = strings.options[strings.value].text;
		PlayerPrefs.SetString("Strings", nrOfStrings);

		bool useSound = sound.isOn;
		bool useVibration = vibration.isOn;

		PlayerPrefs.SetInt("Vibration", (useVibration ? 1 : 0));
		PlayerPrefs.SetInt("Sound", (useSound ? 1 : 0));

		if(nrOfStrings == "6 Strings")
			SceneManager.LoadScene("6 Guitar Strings");
		if (nrOfStrings == "4 Strings")
			SceneManager.LoadScene("4 Guitar Strings");
		if (nrOfStrings == "2 Strings")
			SceneManager.LoadScene("2 Guitar Strings");

	}

}
