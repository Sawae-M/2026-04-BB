using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string triggeringTag = "Player";
    [SerializeField] private bool pickupDestroysItem = true;

    [SerializeField] private AudioClip pickupSound;
    [SerializeField, Range(0f, 1f)] private float pickupVolume = 1f; 

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggeringTag)) return;
        RotSystem.Instance?.Restore();

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);
 
        if (pickupDestroysItem)
            Destroy(gameObject);
    }
}
