using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
    public Text text;
    public LevelManager levelManager;

	// Use this for initialization
	void Start ()
    {
        text.text = "Your Time: " + (Time.unscaledTime - PlayerPrefs.GetFloat("TimeSinceMain")).ToString();
    }

}
