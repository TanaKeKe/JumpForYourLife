using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("Đất va chạm vào tường nè bà");
            collision.gameObject.GetComponent<Block>().ChangeDirection();
        }
    }
}