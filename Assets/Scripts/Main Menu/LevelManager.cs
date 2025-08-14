using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public OptionsToggle optionsToggle;
    public GameObject pauseLabel;
    private GameObject playerMove;

    bool isPaused = false;

    private void Start()
    {
        playerMove = GameObject.Find("Player");
        pauseLabel = GameObject.Find("PauseUI");
        if(pauseLabel)
            pauseLabel.SetActive(isPaused);
    }

    void Update()
    {
        StartCoroutine(PlayerDies());
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseLabel.SetActive(isPaused);
    }

    IEnumerator PlayerDies()
    {
        if (playerMove == null && SceneManager.GetActiveScene().name == "Level_1")
        { 
            print("Player is dead, load requested");
            yield return new WaitForSeconds(1);
            LoadLevel("Win");
        }
    }

    public void LoadLevel(string name) 
    {
        if (isPaused)
        {
            Pause();
        }
        Debug.Log("Level Load Requested For: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

	public void QuitRequest(){
		Debug.Log ("Bye:");
		Application.Quit ();
	}
	//Load Next Level using Build Settings
	public void LoadNextLevel()
    {
        if (isPaused)
        {
            Pause();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
	}
}