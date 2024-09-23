using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarEndgame : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnExitTrigger2D: Nhân vật chạm vào thanh endgame");
            GamePlayController.Instance.isFinish = true;
            PanelManager.Instance.OpenPanel("EndGamePanel");
        }
    }
}
