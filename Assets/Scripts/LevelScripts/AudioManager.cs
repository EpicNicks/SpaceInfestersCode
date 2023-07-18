using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource bgm, bgmRock, bgmOC;

	void Start ()
    {
        bgm = GameObject.Find("bgMusic").GetComponent<AudioSource>();
        bgmOC = GameObject.Find("bgMusicOC").GetComponent<AudioSource>();
        bgmRock = GameObject.Find("bgMusicRock").GetComponent<AudioSource>();
    }
	
	void Update ()
    {
	    if(EnemySpawnManager.wavesCleared < 5)
        {
            bgm.mute = false;
            bgmRock.mute = true;
            bgmOC.mute = true;
        }
        else if(EnemySpawnManager.wavesCleared < 10)
        {
            bgm.mute = true;
            bgmRock.mute = false;
            bgmOC.mute = true;
        }
        else if(EnemySpawnManager.wavesCleared > 15)
        {
            bgm.mute = true;
            bgmRock.mute = true;
            bgmOC.mute = false;
        }
	}
}
