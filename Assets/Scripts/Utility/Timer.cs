using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    bool started;
    bool complete;

    public Timer(int time)
    {
        this.time = time;
    }

    public void TimerStart()
    {
        started = true;
    }

    public bool isComplete()
    {
        return complete;
    }

    void Update()
    {
        if (started)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                complete = true;
            }
        }
    }
}
