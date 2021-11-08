using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sick player contoller
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;

    private PlayerMovement CurrentPlayer;
    private PlayerMovement SecondaryPlayer;

    private Vector2 Playerinput;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = Player1;
        SecondaryPlayer = Player2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Swap();
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
        PlayerMovement temp = CurrentPlayer;
        CurrentPlayer = SecondaryPlayer;
        SecondaryPlayer = temp;
    }
}
