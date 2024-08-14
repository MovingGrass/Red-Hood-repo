using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int health;
    public int numOfHearts;

    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Death Settings")]
    public Animator animator;
    public MonoBehaviour[] scriptsToDisable;
    public float deathAnimationDuration = 2f;

    private bool isDead = false;
    private PlayerMovement playerMovement;

    private void Start()
    {
        health = numOfHearts;
        UpdateHealthDisplay();
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on the player!");
        }
    }

    private void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
            return;

        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;
        health = Mathf.Clamp(health, 0, numOfHearts);

        if (playerMovement != null)
        {
            playerMovement.PlayDamageSound();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead) return;

        health += healAmount;
        health = Mathf.Clamp(health, 0, numOfHearts);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable scripts
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        // Schedule transition to game over scene
        Invoke("LoadGameOverScene", deathAnimationDuration);
    }

    private void LoadGameOverScene()
    {
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadGameOver();
        }
        else
        {
            Debug.LogError("GameSceneManager instance not found!");
        }
    }
}