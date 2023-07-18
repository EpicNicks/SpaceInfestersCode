using UnityEngine;
using UnityEngine.UI;

public class DisplayHighestScore : MonoBehaviour {
    public Text text;

	void Update () {
        text.text = PlayerPrefs.GetFloat("PlayerTime_1").ToString();
	}
}