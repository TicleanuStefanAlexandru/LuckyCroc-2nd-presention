using UnityEngine;
using UnityEngine.UI;

public class DaveUI : MonoBehaviour
{
    public DaveStats daveStats;

    public Text healthText;
    public Text staminaText;
    public Text moneyText;
    public Text targetHealthText; // New: UI text for target health

    void Update()
    {
        if (daveStats == null) return;

        // Dave's own stats
        healthText.text = "Health: " + daveStats.Health + "/" + daveStats.maxHealth;
        staminaText.text = "Stamina: " + Mathf.FloorToInt(daveStats.stamina) + "/" + daveStats.maxStamina;
        moneyText.text = "Money: " + daveStats.money;

        // Target stats
        UpdateTargetHealth();
    }

    void UpdateTargetHealth()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Biteable")))
        {
            IBiteable biteable = hit.collider.GetComponent<IBiteable>();
            if (biteable != null)
            {
                targetHealthText.text = $"Target HP: {biteable.GetCurrentHealth()} / {biteable.GetMaxHealth()}";
                return;
            }
        }

        targetHealthText.text = ""; // Clear UI if not hovering a biteable
    }
}
