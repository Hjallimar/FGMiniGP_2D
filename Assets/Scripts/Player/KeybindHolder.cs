using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "KeybindHolder")]
public class KeybindHolder : ScriptableObject
{
    [Header("Generic Keybinds")]
    [SerializeField] public KeyCode Quit = KeyCode.Escape;
    
    [Space, Header("Single Player")]
    [SerializeField] public KeyCode SingleJump = KeyCode.Space;
    [SerializeField] public KeyCode SingleSwapPlace = KeyCode.Q;
    [SerializeField] public KeyCode SingleSwapTarget = KeyCode.E;
    [SerializeField] public KeyCode SingleInteract = KeyCode.R;
    [Header("The player movement in singleplayer will always be Horizontal.")]
    [Header("Horizontal is A, D and Arrow keys")]
    
    [Space, Header("Multi Player")]
        [Header("Player One")]
    [SerializeField] public KeyCode PlayerOneJump = KeyCode.W;
    [SerializeField] public KeyCode PlayerOneSwapPlace = KeyCode.Tab;
    [SerializeField] public KeyCode PlayerOneLeft = KeyCode.A;
    [SerializeField] public KeyCode PlayerOneRight = KeyCode.D;
    [SerializeField] public KeyCode PlayerOneInteract = KeyCode.E;
        [Header("Player Two")]
    [SerializeField] public KeyCode PlayerTwoJump = KeyCode.UpArrow;
    [SerializeField] public KeyCode PlayerTwoSwapPlace = KeyCode.RightShift;
    [SerializeField] public KeyCode PlayerTwoLeft = KeyCode.LeftArrow;
    [SerializeField] public KeyCode PlayerTwoRight = KeyCode.RightArrow;
    [SerializeField] public KeyCode PlayerTwoInteract = KeyCode.RightControl;
}
