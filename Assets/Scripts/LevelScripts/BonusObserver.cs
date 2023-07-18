using UnityEngine;

public class BonusObserver : MonoBehaviour
{
    private static PlayerMove player;
    private static ScoreKeeper scoreKeeper;

    private void Awake()
    {
        player = GetComponent<PlayerMove>();
        scoreKeeper = GameObject.Find("Canvas").GetComponentInChildren<ScoreKeeper>();
    }

    public static void WaveBonus(int points)
    {
        if(PlayerMove.playerHP == 3)
        {
            //Display some bonus animation
            //Play a special sound (focus less on this since most people wont be listening anyway)
            scoreKeeper.Score(points);
        }
    }
}
