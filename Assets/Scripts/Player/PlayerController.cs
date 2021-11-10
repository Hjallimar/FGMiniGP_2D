using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sick player contoller
public class PlayerController : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] private KeyCode SwapPositionKey = KeyCode.Q;
    [SerializeField] private KeyCode SwapTargetKey = KeyCode.E;
    [Header("Player GameObjects")]
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;
    [Header("Current active indicator")]
    [SerializeField] private GameObject Indicator;

    [Header("Mirror Restrictions")] 
    [SerializeField] private bool MirrorMove = false;
    [SerializeField] private bool MirrorJump = false;
    [SerializeField] private bool MirrorDash = false;
    [SerializeField] private bool MirrorGravity = false;
    [Header("Camera Settings")]
    [SerializeField] private Vector3 CameraOffset = new Vector3(0,0,-10);
    [SerializeField] private bool AdjustCameraX = false;
    [SerializeField] private bool AdjustCameraY = false;
  
    private PlayerMovement CurrentPlayer;
    private PlayerMovement SecondaryPlayer;
    private Camera PlayerCam;

    private float MinSize = 5.0f;
    private float MaxSize = 10.0f;
    private float LerpValue = 0.0f;
    private Vector2 Playerinput;

    public Checkpoint checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        Player1.InitializePlayer(this);
        Player2.InitializePlayer(this);
        PlayerCam = Camera.main;
        MinSize = PlayerCam.orthographicSize;
        MaxSize = MinSize * 2;
        CurrentPlayer = Player1;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
        SecondaryPlayer = Player2;
        if (MirrorGravity)
        {
            Player2.GetComponent<Rigidbody2D>().gravityScale = -1;
            Player2.transform.Rotate(Vector3.right, 180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Playerinput.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(SwapPositionKey))
            Swap();

        if(Input.GetKeyDown(SwapTargetKey))
            SwapTarget();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0))
        {
            CurrentPlayer.DashDirection(Playerinput);
            if(MirrorDash && !SecondaryPlayer.Dead)
                SecondaryPlayer.DashDirection(-Playerinput);
        }

        MovePlayer();
        UpdateCamera();
    }

    void Jump()
    {
        CurrentPlayer.Jump();
        if(MirrorJump && !SecondaryPlayer.Dead)
            SecondaryPlayer.Jump();
    }
    void MovePlayer()
    {
        CurrentPlayer.AddMovement(Playerinput);
        if(MirrorMove && !SecondaryPlayer.Dead)
            SecondaryPlayer.AddMovement(-Playerinput);
    }

    void Swap()
    {
        if (SecondaryPlayer.Dead)
            return;
        Vector2 TempPos = SecondaryPlayer.transform.position;
        SecondaryPlayer.transform.position = CurrentPlayer.transform.position;
        CurrentPlayer.transform.position = TempPos;
    }

    void SwapTarget()
    {
        if (SecondaryPlayer.Dead)
            return;
        PlayerMovement temp = CurrentPlayer;
        CurrentPlayer = SecondaryPlayer;
        SecondaryPlayer = temp;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
    }

    void UpdateCamera()
    {
        if (SecondaryPlayer.Dead)
        {
            PlayerCam.orthographicSize = MinSize;
            PlayerCam.transform.position = new Vector3(CurrentPlayer.transform.position.x, CurrentPlayer.transform.position.y, -10);
            return;
        }
        Vector3 Halfway = Player1.transform.position - Player2.transform.position;
        Vector3 Center = new Vector3(0.0f , transform.position.y, CameraOffset.z) + Halfway * 0.5f;
        if (AdjustCameraX)
            Center.x = Halfway.x * 0.5f + Player2.transform.position.x;
        if (AdjustCameraY)
            Center.y = Halfway.y * 0.5f + Player2.transform.position.y;
        
        if (Mathf.Abs(Halfway.x) > 14)
        {
            float temp = Mathf.Abs(Halfway.x) - 14;
            LerpValue = temp * 0.1f;
        }
        else if (Mathf.Abs(Halfway.y) > 8)
        {
            float temp = Mathf.Abs(Halfway.y) - 8;
            LerpValue = temp * 0.1f;
        }
        else
            LerpValue = 0.0f;

        PlayerCam.orthographicSize = Mathf.Lerp(MinSize, MaxSize, LerpValue );
        PlayerCam.transform.position = Center;
    }

    public void ResetGame()
    {
        if (Player1.Dead && Player2.Dead)
        {
            Player1.OnRespawn();
            Player2.OnRespawn();
            checkpoint.ResetLava();
        }
        else if (CurrentPlayer.Dead)
        {
            PlayerMovement temp = CurrentPlayer;
            CurrentPlayer = SecondaryPlayer;
            SecondaryPlayer = temp;
            Indicator.transform.SetParent(CurrentPlayer.transform);
            Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
        }
    }
}
