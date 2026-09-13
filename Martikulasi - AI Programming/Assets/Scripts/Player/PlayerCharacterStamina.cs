using System.Collections;
using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100; 
    [SerializeField] private float sprintStaminaCost = 20;
    [SerializeField] private float staminaRegenValue = 20;
    [SerializeField] private AudioSource breathingAudio;

    private float currentStamina;
    private PlayerCharacterMovement characterMovement;
    private Coroutine stopRegenStaminaCo;
    private bool isWaitingRegenStamina;

    private void Awake()
    {
        characterMovement = GetComponent<PlayerCharacterMovement>();
    }

    private void Start()
    {
        currentStamina = maxStamina;
        HUDManager.Instance.StaminaUI.SetStaminaFill(currentStamina, maxStamina);
    }

    private void Update()
    {
        CalculateStamina();
    }

    public void CalculateStamina()
    {
        if (characterMovement.IsSprint)
        {
            if (stopRegenStaminaCo != null)
            {
                StopCoroutine(stopRegenStaminaCo);
                stopRegenStaminaCo = null;
            }
            isWaitingRegenStamina = false;

            if (currentStamina > 0)
                currentStamina -= sprintStaminaCost * Time.deltaTime;
            else
                characterMovement.SetSprint(false);
        }
        else
        {
            if (currentStamina < maxStamina)
                currentStamina += staminaRegenValue * Time.deltaTime;
            else if (!isWaitingRegenStamina)
            {
                stopRegenStaminaCo = StartCoroutine(StopRegenStaminaWait());
                isWaitingRegenStamina = true;
            }
        }
        HUDManager.Instance.StaminaUI.SetStaminaFill(currentStamina, maxStamina);
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        breathingAudio.volume = 1 - (currentStamina / maxStamina);
    }

    private IEnumerator StopRegenStaminaWait()
    {
        yield return new WaitForSeconds(1f);
        HUDManager.Instance.StaminaUI.SetVisible(false);
    }
}
