using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sick player contoller
public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Vector3 CameraOffset;
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;
    [SerializeField] private GameObject Indicator;

    [SerializeField] private bool AdjustCameraX = false;
    [SerializeField] private bool AdjustCameraY = false;
  
    private PlayerMovement CurrentPlayer;
    private PlayerMovement SecondaryPlayer;
    private Camera PlayerCam;
    
    private float MinSize = 5.0f;
    private float MaxSize = 10.0f;
    private float LerpValue = 0.0f;
    private Vector2 Playerinput;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam = Camera.main;
        CurrentPlayer = Player1;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
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
        UpdateCamera();
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
        Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
    }

    void UpdateCamera()
    {
        Vector3 Center = new Vector3(0.0f , transform.position.y, CameraOffset.z);
        Vector3 Halfway = Player1.transform.position - Player2.transform.position;
        if (AdjustCameraX)
        {
            Center.x = Halfway.x * 0.5f + Player2.transform.position.x;
        }

        if (AdjustCameraY)
        {
            Center.y = Halfway.y * 0.5f + Player2.transform.position.y;
        }

        if (Mathf.Abs(Halfway.x) > 14)
        {
            float temp = Mathf.Abs(Halfway.x) - 14;
            LerpValue = temp * 0.1f;
        }
        else if (Mathf.Abs(Halfway.y) > 10)
        {
            float temp = Mathf.Abs(Halfway.x) - 10;
            LerpValue = temp * 0.08f;
        }
        else
        {
            LerpValue = 0.0f;
        }
        PlayerCam.orthographicSize = Mathf.Lerp(MinSize, MaxSize, LerpValue );
        PlayerCam.transform.position = Center;
    }
}
