using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string triggeringTag = "Player";
    [SerializeField] private bool pickupDestroysItem = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggeringTag)) return;
        RotSystem.Instance?.Restore();

        if (pickupDestroysItem)
            Destroy(gameObject);
    }
}
