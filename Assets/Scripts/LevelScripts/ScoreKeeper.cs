using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text myText;
    public AudioClip ptsSFX;

    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
        myText.text = "Score: " + score.ToString();
    }

    public void Score(int points)
    {
        score += points;
        myText.text = "Score: " + score.ToString();
        DontDestroyOnLoad(ptsSFX);
        GetComponent<AudioSource>().Play();
    }

    public static void Reset()
    {
        score = 0;
    }

    public static void SetHighScore(int newScore)
    {
        PlayerPrefs.SetInt("HighScore", newScore > PlayerPrefs.GetInt("HighScore") ? newScore : PlayerPrefs.GetInt("HighScore"));
    }
}
