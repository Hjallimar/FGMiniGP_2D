using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTimer : MonoBehaviour
{
    [SerializeField] private float GameTime;
    [SerializeField] private Text Timer;
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;

    [SerializeField] private bool AddInsteadOfPause = true;
    
    private bool GameOver = false;
    private float currentTime;
    private float PauseTime;
    private Coroutine PauseCorutine;

    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        Timer.color = Color.black;
        currentTime = GameTime;
        PauseTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
            return;
        
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            GameOver = true;
            Player1.Die();
            Player2.Die();
        }

        DisplayTime();
    }

    public void PauseKillTimer(float Time)
    {
        if (AddInsteadOfPause)
        {
            currentTime += Time;
            return;
        }
        PauseTime += Time;
        if (PauseCorutine == null)
        {
            PauseCorutine = StartCoroutine(PauseCounter());
        }
    }
    
    void DisplayTime()
    {
        int ScaledTime = (int)currentTime * 10;
        float oneDecimal = ScaledTime / 10;
        Timer.text = oneDecimal + "s";
    }

    private IEnumerator PauseCounter()
    {
        Timer.color = Color.red;
        isPaused = true;
        float PausetimeCounter = 0.0f;
        while (PausetimeCounter < PauseTime)
        {
            PausetimeCounter += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        PauseTime = 0.0f;
        PauseCorutine = null;
        isPaused = false;
        Timer.color = Color.black;
    }
}
