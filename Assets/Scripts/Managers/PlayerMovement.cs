using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.InputSystem; // <-- REQUIRED FOR THE NEW SYSTEM

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintMultiplier = 1.6f; 
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isHoldingShift;
    public Animator animator;
    private string currentAnimation;

    [Header("Stamina Settings")]
    public int maxStamina = 6;
    private int currentStamina;
    public float drainRate = 0.5f; 
    public float recoverRate = 1f; 
    private float sprintTimer = 0f;
    private float recoverTimer = 0f;

    [Header("UI Settings")]
    public Image[] lightningBolts; 

    // Coffee Boost Variables
    private bool isCaffeinated = false;
    private float originalWalkSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    void Update()
    {
        
        // --- NEW INPUT SYSTEM LOGIC --- //
        movement = Vector2.zero;
        isHoldingShift = false;

        // Check if a keyboard is actually plugged in
        if (Keyboard.current != null)
        {
            // Vertical Movement (W/S or Up/Down Arrows)
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) movement.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) movement.y -= 1;

            // Horizontal Movement (A/D or Left/Right Arrows)
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movement.x += 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movement.x -= 1;

            // Shift Keys
            isHoldingShift = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
        }

        // Normalize so diagonal movement isn't faster than walking in a straight line
        movement = movement.normalized;

        UpdateAnimation();

        HandleStaminaMath();
    }

    private void UpdateAnimation()
{
    if (animator == null) 
    {
        Debug.Log("ANIMATOR IS NULL - returning early!");
        return;
    }

    bool isTryingToSprint = isHoldingShift && movement != Vector2.zero && currentStamina > 0 && !isCaffeinated;
    string prefix = isTryingToSprint ? "dash" : "walk";

    string clipToPlay = "";
    
    if (movement == Vector2.zero)
        clipToPlay = "walk_down";
    else if (movement.x > 0 && movement.y > 0)
        clipToPlay = prefix + "_rightup";
    else if (movement.x > 0 && movement.y < 0)
        clipToPlay = prefix + "_rightdown";
    else if (movement.x < 0 && movement.y > 0)
        clipToPlay = prefix + "_leftup";
    else if (movement.x < 0 && movement.y < 0)
        clipToPlay = prefix + "_leftdown";
    else if (movement.x > 0)
        clipToPlay = prefix + "_rightup";
    else if (movement.x < 0)
        clipToPlay = prefix + "_leftup";
    else if (movement.y > 0)
        clipToPlay = prefix + "_up";
    else if (movement.y < 0)
        clipToPlay = prefix + "_down";

    if (clipToPlay != currentAnimation)
    {
        currentAnimation = clipToPlay;
        animator.Play(clipToPlay);
    }
}

    void FixedUpdate()
    {
        float currentSpeed = walkSpeed;

        bool isTryingToSprint = isHoldingShift && movement != Vector2.zero;

        if (isTryingToSprint && currentStamina > 0 && !isCaffeinated)
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    private void HandleStaminaMath()
    {
        bool isTryingToSprint = isHoldingShift && movement != Vector2.zero;

        if (isTryingToSprint && currentStamina > 0 && !isCaffeinated)
        {
            recoverTimer = 0f; 
            
            sprintTimer += Time.deltaTime;
            if (sprintTimer >= drainRate)
            {
                currentStamina--;
                sprintTimer = 0f;
                UpdateStaminaUI();
            }
        }
        else if (currentStamina < maxStamina)
        {
            sprintTimer = 0f; 

            recoverTimer += Time.deltaTime;
            if (recoverTimer >= recoverRate)
            {
                currentStamina++;
                recoverTimer = 0f;
                UpdateStaminaUI();
            }
        }
    }

    private void UpdateStaminaUI()
    {
        for (int i = 0; i < lightningBolts.Length; i++)
        {
            if (i < currentStamina)
                lightningBolts[i].enabled = true;
            else
                lightningBolts[i].enabled = false;
        }
    }

    // --- COFFEE BOOST LOGIC ---
    public void StartSpeedBoost(float boostDuration, float speedMultiplier)
    {
        if (!isCaffeinated) 
        {
            StartCoroutine(CoffeeRushRoutine(boostDuration, speedMultiplier));
        }
    }

    private System.Collections.IEnumerator CoffeeRushRoutine(float duration, float multiplier)
    {
        isCaffeinated = true;
        originalWalkSpeed = walkSpeed; 
        
        walkSpeed *= multiplier; 
        yield return new WaitForSeconds(duration);

        walkSpeed = originalWalkSpeed;
        isCaffeinated = false;
    }
}