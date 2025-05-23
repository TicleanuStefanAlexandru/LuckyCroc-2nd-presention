using UnityEngine;

public class DaveStats : MonoBehaviour
{
    [Header("Core Stats")]
    [SerializeField] private int currentHealth = 1000;
    public int maxHealth = 1000;
    public float strength = 100f;
    public float speed = 20f;

    [Header("Money")]
    public int money = 1000;
    public int bankBalance = 10000;

    [Header("Stamina")]
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;
    public float staminaDrainPerSecond = 25f;
    public bool isSprinting = false;

    void Update()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (!isSprinting && stamina < maxStamina)
        {
            stamina = Mathf.Clamp(stamina + staminaRegenRate * Time.deltaTime, 0f, maxStamina);
        }
    }

    public void UseStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina - amount, 0f, maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0f, maxStamina);
    }

    public int Health => currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Dave is down!");
            OnDeath();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    private void OnDeath()
    {
        // Placeholder for death logic
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    // 🔓 Bank functions with no limits
    public void Deposit(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            bankBalance += amount;
        }
    }

    public bool Withdraw(int amount)
    {
        if (bankBalance >= amount)
        {
            bankBalance -= amount;
            money += amount;
            return true;
        }
        return false;
    }
}
