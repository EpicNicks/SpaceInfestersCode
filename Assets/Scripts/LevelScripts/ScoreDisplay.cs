using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start ()
    {
        ScoreKeeper.SetHighScore(ScoreKeeper.score);
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
        EnemySpawnManager.wavesCleared = 0;
	}
}
