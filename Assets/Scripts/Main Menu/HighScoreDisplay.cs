using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class HighScoreDisplay : MonoBehaviour
{
    private Text hsText;
    //Color Handling
    private List<Func<Color>> randColors = new List<Func<Color>>();
    private int selectedColor;

    //Flash speed
    public float cooldown;
    private float curCooldown;

    void Start()
    {
        hsText = GetComponent<Text>();
        hsText.text = PlayerPrefs.GetInt("HighScore").ToString();

        randColors.Add(ColorRandomizer.RandomCold);
        randColors.Add(ColorRandomizer.RandomWarm);
        randColors.Add(ColorRandomizer.RandomLight);
        randColors.Add(ColorRandomizer.RandomFabulous);

        selectedColor = UnityEngine.Random.Range(0, randColors.Count - 1);

        curCooldown = cooldown;
    }

    private void Update()
    {
        if(curCooldown <= 0)
        {
            hsText.color = randColors[selectedColor].Invoke();
            curCooldown = cooldown;
        }
        curCooldown -= Time.deltaTime;
    }
}
