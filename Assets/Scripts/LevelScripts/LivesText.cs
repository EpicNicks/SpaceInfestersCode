using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour {
    public float Speed = 1;
    private Renderer rend;
    //Text object was drag-dropped into public slot
    public Text text;
    //public LoseCollider loseCollider;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        //Number of Lives
        //loseCollider = FindObjectOfType<LoseCollider>();
        //text.text = loseCollider.lives.ToString();
    }

    void Update()
    {
        rend.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
        text.color = rend.material.color;
        //Display Number of Lives
        //text.text = "Lives: " + loseCollider.lives.ToString();
    }
}
