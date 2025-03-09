using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float forceAmount;    // 캐릭터에 가하는 힘

    private Rect jumpArea;  // 점프가 가능한 표면의 점프 가능 구역 사각형
    private float minY; // 점프 가능한 최소 높이

    private BoxCollider col;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        
        forceAmount = 10f;
        InitJumpArea();
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.collider.CompareTag("Player")) return;
        if (DoAddFore(other.transform.position))
        {
            GameManager.Instance.Player.Controller.Rigidbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 점프 표면에 점프 가능 사각형 구역 초기화
    /// </summary>
    private void InitJumpArea()
    {
        var minX = col.bounds.min.x + 0.05f;
        var maxX = col.bounds.max.x - 0.05f;
        var minZ = col.bounds.min.z + 0.05f;
        var maxZ = col.bounds.max.z - 0.05f;
        
        jumpArea = new Rect(minX, minZ, maxX - minX, maxZ - minZ);
        minY = col.bounds.max.y - 0.05f;
    }

    /// <summary>
    /// 점프 구역에 들어왔는지 검사
    /// </summary>
    /// <param name="collisionPos">충돌한 위치</param>
    /// <returns>True: 점프 가능 구역에 들어옴
    /// <para>False: 점프 구역에 들어오지 않음</para></returns>
    private bool DoAddFore(Vector3 collisionPos)
    {
        var pos2D = new Vector2(collisionPos.x, collisionPos.z);
        return collisionPos.y >= minY && jumpArea.Contains(pos2D);
    }

}
