using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float forceAmount;

    private Rect jumpArea;
    private float minY;

    private BoxCollider col;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        
        forceAmount = 10f;
        SetJumpArea();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (DoAddFore(other.transform.position))
            {
                GameManager.Instance.Player.Controller.Rigidbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }
    }

    private void SetJumpArea()
    {
        var minX = col.bounds.min.x + 0.05f;
        var maxX = col.bounds.max.x - 0.05f;
        var minZ = col.bounds.min.z + 0.05f;
        var maxZ = col.bounds.max.z - 0.05f;
        
        jumpArea = new Rect(minX, minZ, maxX - minX, maxZ - minZ);
        minY = col.bounds.max.y - 0.05f;
    }

    private bool DoAddFore(Vector3 collisionPos)
    {
        var pos2D = new Vector2(collisionPos.x, collisionPos.z);
        return collisionPos.y >= minY && jumpArea.Contains(pos2D);
    }

}
