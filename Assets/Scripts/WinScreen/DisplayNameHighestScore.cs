using UnityEngine;
using UnityEngine.UI;

public class DisplayNameHighestScore : MonoBehaviour {
    public Text text;

    void Update()
    {
        text.text = PlayerPrefs.GetString("PlayerName_1");
    }
}
