using UnityEngine;
using System.Collections;

public class PlayerHazardController : MonoBehaviour
{
    [Header("Fall Death Settings")]
    public float deathY = -10f; // Y position below which the player dies

    [Header("Acid Damage Settings")]
    public int acidDamage = 1; // Amount of damage per acid hit (in hearts)
    public float acidDamageInterval = 0.5f; // Time between acid damage ticks

    private PlayerHealth playerHealth;
    private bool isInAcid = false;
    private Coroutine acidDamageCoroutine;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player. Hazard controller will not function correctly.");
        }
    }

    private void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
            return;

        CheckFallDeath();
    }

    private void CheckFallDeath()
    {
        if (transform.position.y < deathY)
        {
            playerHealth.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Acid"))
        {
            isInAcid = true;
            if (acidDamageCoroutine == null)
            {
                acidDamageCoroutine = StartCoroutine(ApplyAcidDamage());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Acid"))
        {
            isInAcid = false;
            if (acidDamageCoroutine != null)
            {
                StopCoroutine(acidDamageCoroutine);
                acidDamageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyAcidDamage()
    {
        while (isInAcid)
        {
            if (!PauseManager.Instance.IsPaused)
            {
                playerHealth.TakeDamage(acidDamage);
            }
            yield return new WaitForSeconds(acidDamageInterval);
        }
    }
}