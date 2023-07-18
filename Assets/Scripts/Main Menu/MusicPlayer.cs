using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance;
    public VolumePref volumePref;


    void Update()
    {
        instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

//TODO THIS CODE DOESNT WORK, MUSIC DUPLICATES ON RELOAD
    void Awake(){
        if (instance != null)
        {
            Destroy(gameObject);
            print("Duplicate Prevented");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}