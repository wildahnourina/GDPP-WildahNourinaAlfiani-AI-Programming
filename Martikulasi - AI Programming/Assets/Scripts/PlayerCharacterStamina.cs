using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100; 
    [SerializeField] private float sprintStaminaCost = 20;
    [SerializeField] private float staminaRegenValue = 20;

    private float currentStamina;
    private PlayerCharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<PlayerCharacterMovement>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        CalculateStamina();
    }

    public void CalculateStamina()
    {
        if (characterMovement.IsSprint)
        {
            if (currentStamina > 0)
                currentStamina -= sprintStaminaCost * Time.deltaTime;
            else
                characterMovement.SetSprint(false);
        }
        else
            currentStamina += staminaRegenValue * Time.deltaTime;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }
}
