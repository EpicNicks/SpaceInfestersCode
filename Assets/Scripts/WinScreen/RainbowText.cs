using UnityEngine;
using UnityEngine.UI;

public class RainbowText : MonoBehaviour
{
    public float Speed = 1;
    private float startTime;
    private Renderer rend;
    //Text object was drag-dropped into public slot
    public Text text;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
        text.color = rend.material.color;
    }
}