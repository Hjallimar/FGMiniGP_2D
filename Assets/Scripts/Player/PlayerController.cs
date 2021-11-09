using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sick player contoller
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;
    [SerializeField] private GameObject Indicator;

    private PlayerMovement CurrentPlayer;
    private PlayerMovement SecondaryPlayer;

    private Vector2 Playerinput;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = Player1;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.75f);
        SecondaryPlayer = Player2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Swap();
        }
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwapTarget();
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentPlayer.Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CurrentPlayer.DashDirection(Playerinput);
        }

        Playerinput.x = Input.GetAxisRaw("Horizontal");
        MovePlayer();
    }

    void MovePlayer()
    {
        CurrentPlayer.AddMovement(Playerinput);
        SecondaryPlayer.AddMovement(-Playerinput);
    }

    void Swap()
    {
        Vector2 TempPos = SecondaryPlayer.transform.position;
        SecondaryPlayer.transform.position = CurrentPlayer.transform.position;
        CurrentPlayer.transform.position = TempPos;
    }

    void SwapTarget()
    {
        PlayerMovement temp = CurrentPlayer;
        CurrentPlayer = SecondaryPlayer;
        SecondaryPlayer = temp;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.75f);
    }
}
