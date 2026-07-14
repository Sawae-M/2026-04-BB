using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string triggeringTag = "Player";

    private void OnTrrigerEnter(Collider other)
    {
        if (!other.CompareTag(triggeringTag)) return;
        RotSystem.Instance?.Restore();
    }
}
