using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {
    public float Speed = 1;
    private float startTime;
    public LevelManager levelManager;
    private Renderer rend;
    //Text object was drag-dropped into public slot
    public Text text;

    void Start () {
        rend = GetComponent<Renderer>();
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	void Update () {
        rend.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
        text.color = rend.material.color;
        text.text = (Time.unscaledTime - PlayerPrefs.GetFloat("TimeSinceMain")).ToString();
    }
}