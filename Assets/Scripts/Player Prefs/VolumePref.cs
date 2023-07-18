using UnityEngine;
using UnityEngine.UI;

public class VolumePref : MonoBehaviour {
    public Slider sliderVal;
    public float sliderValFloat = 1f;

	void Update () {
        sliderValFloat = sliderVal.value;
        PlayerPrefs.SetFloat("Volume", sliderValFloat);
	}
}
