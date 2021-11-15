using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Sick player contoller
public class PlayerController : MonoBehaviour
{
    [Header("Keybinds")] 
    [SerializeField] private KeybindHolder KeyBinds;
    [Header("Player GameObjects")]
    [SerializeField] private PlayerMovement Player1;
    [SerializeField] private PlayerMovement Player2;
    
    [Header("MultiPlayer Mode")] 
    [SerializeField] private bool MultiPlayer = false;
    private Vector2 SecondPlayerInput;
    
    [Header("Current active indicator")]
    [SerializeField] private GameObject Indicator;

    [Header("Mirror Restrictions")] 
    [SerializeField] private bool MirrorMove = false;
    [SerializeField] private bool CONSTANTMIRRORMOVE = false;
    [SerializeField] private bool MirrorJump = false;
    [SerializeField] private bool MirrorDash = false;
    [SerializeField] private bool MirrorGravity = false;
    [SerializeField] private bool SwapGravityOnSwap = false;
    [SerializeField] private bool KeepVelocityOnSwap = false;

    [Header("Camera Settings")] 
    [SerializeField] private bool HorizontalCams = false;
    private bool previousBool;
    [SerializeField] private bool AdjustCameraX = false;
    [SerializeField] private bool AdjustCameraY = false;
    [SerializeField] private Camera PrimaryCamera;
    [SerializeField] private Camera SecondaryCamera;
    private Vector3 CameraOffset = new Vector3(0,0,-10);

    [Header("Super power stuffs")] 
    [SerializeField] private float Duration = 5.0f;
    [SerializeField, Range(0.1f, 2.0f)] private float JumpForceMultiplier = 5.0f;
    [SerializeField] private int ExtraJumps = 2;
    private float PowerUpTimer = 0.0f;
    private Coroutine PowerUpCorutine;
    
    private PlayerMovement CurrentPlayer;
    private PlayerMovement SecondaryPlayer;

    //SinglePlayer Camera settings
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

        CurrentPlayer = Player1;
        SecondaryPlayer = Player2;
        
        if (MirrorGravity)
        {
            Player2.GetComponent<Rigidbody2D>().gravityScale = -1;
            Player2.transform.Rotate(Vector3.right, 180);
        }

        previousBool = HorizontalCams;
        if (MultiPlayer)
        {
            SetupMultiPlayer();
            return;
        }
        SetupSinglePlayer();
    }

    void SetupSinglePlayer()
    {
        SecondaryCamera.enabled = false;
        MinSize = PrimaryCamera.orthographicSize;
        MaxSize = MinSize * 2;
        Indicator.transform.SetParent(CurrentPlayer.transform);
        Indicator.transform.localPosition = new Vector2(0.0f, 0.0f);
    }

    void SetupMultiPlayer()
    {
        Indicator.SetActive(false);
        if(HorizontalCams)
            SetUpHorizontalCameras();
        else
            SetupVerticalCameras();
        MirrorDash = false;
        MirrorJump = false;
        MirrorMove = false;
    }
    
    void MultiPlayerUpdate()
    {
        if (Input.GetKeyDown(KeyBinds.Quit))
        {
            Application.Quit();
        }
        Playerinput.x = Input.GetKey(KeyBinds.PlayerOneLeft) ? -1 : Input.GetKey(KeyBinds.PlayerOneRight) ? 1 : 0;
        SecondPlayerInput.x = Input.GetKey(KeyBinds.PlayerTwoLeft) ? -1 : Input.GetKey(KeyBinds.PlayerTwoRight) ? 1 : 0;
        
        if (Input.GetKeyDown(KeyBinds.PlayerOneSwapPlace) || Input.GetKeyDown(KeyBinds.PlayerTwoSwapPlace))
            Swap();

        if (Input.GetKeyDown(KeyBinds.PlayerOneJump))
            CurrentPlayer.Jump();
        if(Input.GetKeyDown(KeyBinds.PlayerTwoJump))
            SecondaryPlayer.Jump();

        CurrentPlayer.AddMovement(Playerinput);
        SecondaryPlayer.AddMovement(SecondPlayerInput);
        
        MovePlayer();
        UpdateCamera();
    }
    // Update is called once per frame
    void Update()
    {
        if (MultiPlayer)
        {
            MultiPlayerUpdate();
            if (HorizontalCams != previousBool)
            {
                StartCoroutine(LerpToHorizontal(HorizontalCams));
                previousBool = HorizontalCams;
            }
        }
        else
        {
            Playerinput.x = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyBinds.SingleSwapPlace))
                Swap();

            if(Input.GetKeyDown(KeyBinds.SingleSwapTarget))
                SwapTarget();

            if (Input.GetKeyDown(KeyBinds.SingleJump))
                Jump();
            
            MovePlayer();
        }
        
        UpdateCamera();
    }

    void SetUpHorizontalCameras()
    {
        SecondaryCamera.enabled = true;
        PrimaryCamera.rect = new Rect(0,0.501f, 1f, 0.495f);
        SecondaryCamera.rect = new Rect(0,0, 1f, 0.495f);
    }

    private IEnumerator LerpToHorizontal(bool Moddify)
    {
        float timer = 0.0f;
        float t = 0.0f;
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            if (Moddify)
                t = timer;
            else
                t = 1.0f - timer;

            
            Vector4 Primary = Vector4.Lerp(new Vector4(0, 0, 0.495f, 1.0f), new Vector4(0,0, 1.0f, 0.495f), t);
            PrimaryCamera.rect = new Rect(Primary.x, Primary.y, Primary.z, Primary.w); 
            
            Vector4 Secondery = Vector4.Lerp(new Vector4(0.501f,0, 0.495f, 1.0f), new Vector4(0,.501f, 1f, 0.495f), t);
            SecondaryCamera.rect = new Rect(Secondery.x, Secondery.y, Secondery.z, Secondery.w);
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
    void SetupVerticalCameras()
    {
        SecondaryCamera.enabled = true;
        PrimaryCamera.rect = new Rect(0,0, 0.495f, 1.0f);
        SecondaryCamera.rect = new Rect(0.501f,0, 0.495f, 1.0f);
    }
    
    void Jump()
    {
        CurrentPlayer.Jump();
        if(MirrorJump && !SecondaryPlayer.Dead)
            SecondaryPlayer.Jump();
    }
    void MovePlayer()
    {
        if (CONSTANTMIRRORMOVE)
        {
            Player1.AddMovement(Playerinput);
            if(MirrorMove && !SecondaryPlayer.Dead)
                Player2.AddMovement(-Playerinput);
        }
        else
        {
            CurrentPlayer.AddMovement(Playerinput);
            if(MirrorMove && !SecondaryPlayer.Dead)
                SecondaryPlayer.AddMovement(-Playerinput);
        }
    }

    public void Swap()
    {
        if (SecondaryPlayer.Dead)
            return;
        Rigidbody2D Second = SecondaryPlayer.GetComponent<Rigidbody2D>();
        Rigidbody2D First = CurrentPlayer.GetComponent<Rigidbody2D>();
        if (KeepVelocityOnSwap)
        {
            Second = SecondaryPlayer.GetComponent<Rigidbody2D>();
            First = CurrentPlayer.GetComponent<Rigidbody2D>();
            Vector2 TempVelocity = Second.velocity;
            Second.velocity = First.velocity;
            First.velocity = TempVelocity;
        }

        if (SwapGravityOnSwap)
        {
            float tempGravity = Second.gravityScale;
            Second.gravityScale = First.gravityScale;
            First.gravityScale = tempGravity;
        }
        
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
        if (MultiPlayer)
        {
            PrimaryCamera.transform.position = Player1.transform.position + CameraOffset;
            SecondaryCamera.transform.position = Player2.transform.position + CameraOffset;
            return;
        }
        if (SecondaryPlayer.Dead)
        {
            PrimaryCamera.orthographicSize = MinSize;
            PrimaryCamera.transform.position = new Vector3(CurrentPlayer.transform.position.x, CurrentPlayer.transform.position.y, -10);
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

        PrimaryCamera.orthographicSize = Mathf.Lerp(MinSize, MaxSize, LerpValue );
        PrimaryCamera.transform.position = Center;
    }


    public void ActivateCoolSuperMechanic()
    {
        PowerUpTimer += Duration;
        if (PowerUpCorutine != null)
            PowerUpCorutine = StartCoroutine(PowerUpCountDown());

    }

    private IEnumerator PowerUpCountDown()
    {
        float loopTimer = 0.0f;
        Player1.ActivateSuperPower(JumpForceMultiplier, ExtraJumps);
        Player2.ActivateSuperPower(JumpForceMultiplier, ExtraJumps);
        while (loopTimer < PowerUpTimer)
        {
            float delta = Time.deltaTime;
            loopTimer += delta;
            yield return new WaitForSeconds(delta);
        }
        Player1.DeactivateSuperPower();
        Player2.DeactivateSuperPower();
        PowerUpTimer = 0.0f;
        PowerUpCorutine = null;
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
