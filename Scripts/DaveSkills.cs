using UnityEngine;

public class DaveSkills : MonoBehaviour
{
    public float biteRange = 4f;
    public float biteCooldown = 1f;
    public Transform biteOrigin;
    public LayerMask biteableLayers;

    private DaveStats stats;
    private float biteCooldownTimer;

    void Start()
    {
        stats = GetComponent<DaveStats>();
    }

    void Update()
    {
        biteCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && biteCooldownTimer <= 0f)
        {
            Bite();
            biteCooldownTimer = biteCooldown;
        }
    }

    void Bite()
    {
        Collider[] hits = Physics.OverlapSphere(biteOrigin.position, biteRange, biteableLayers);

        foreach (Collider hit in hits)
        {
            IBiteable biteTarget = hit.GetComponent<IBiteable>();
            if (biteTarget != null)
            {
                int biteDamage = Mathf.RoundToInt(stats.strength * 1.5f); // Bite scales with strength
                biteTarget.OnBitten(biteDamage);
                Debug.Log("Dave bit " + hit.name + " for " + biteDamage + " damage.");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (biteOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(biteOrigin.position, biteRange);
        }
    }
}
