using UnityEngine;
using UnityEngine.UI;

public class OptionsToggle : MonoBehaviour {
    public Transform startPosition;
    public Transform origin;
    public Transform target;
    private InputField NameInput;
    private LevelManager levelManager;
    public string playerName;
    public bool inputComplete = false;
    public bool toOptions = false;
    public bool toMain = false;
    public float StartTime;
    public float TotalDistance;

    //speed divides distance to decrease time spent travelling
    public float speed;

    void Start (){
        StartTime = Time.time;
        TotalDistance = Vector3.Distance(startPosition.position, target.position) / speed;
        NameInput = FindObjectOfType<InputField>();
    }

    void Update () {

        //deltaTime and velocity calculations
        float currentDuration = Time.time - StartTime;
        float JourneyFraction = currentDuration / TotalDistance;

        //Ensures correct LinearIntERPolation Occurs
        if (toOptions) {
            transform.position = Vector3.Lerp(startPosition.position, target.position, JourneyFraction);
        } else if (toMain) {
            transform.position = Vector3.Lerp(startPosition.position, origin.position, JourneyFraction);
        }
	}
    public void toOptionsMenu(){
        //Set Selected Button
        GameObject eventSystem = GameObject.Find("EventSystem");
        GameObject VolumeEnable = GameObject.Find("VolumeEnable");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(VolumeEnable.gameObject);
        //LERP
        toOptions = true;
        toMain = false;
        speed = 20f;
        StartTime = Time.time;
        TotalDistance = Vector3.Distance(startPosition.position, target.position) / speed;
    }
    public void toMainMenu(){
        //Set Selected Button
        GameObject eventSystem = GameObject.Find("EventSystem");
        GameObject Options = GameObject.Find("OptionsMenuButton");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Options.gameObject);
        //LERP
        toMain = true;
        toOptions = false;
        speed = 20f;
        StartTime = Time.time;
        TotalDistance = Vector3.Distance(startPosition.position, origin.position) / speed;
    }
    public void toHighScore()
    {
        if (NameInput.text != null)
        {
            //Set PlayerName Entered
            playerName = NameInput.text;
            Debug.Log(playerName);
            inputComplete = true;
            //Set Selected Button
            GameObject eventSystem = GameObject.Find("EventSystem");
            GameObject ReturnToStart = GameObject.Find("ReturnToStart");
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ReturnToStart.gameObject);
            //LERP
            toOptions = true;
            toMain = false;
            speed = 20f;
            StartTime = Time.time;
            TotalDistance = Vector3.Distance(startPosition.position, target.position) / speed;
        }
    }
}