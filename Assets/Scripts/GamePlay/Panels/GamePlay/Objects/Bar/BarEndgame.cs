using Common;
using UnityEngine;

public class BarEndgame : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTags.PLAYER_TAG))
        {
            AudioGamePlayManager.Instance.StopMusic();
            AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.GameOverSound);
            //Debug.Log("OnExitTrigger2D: Nhân vật chạm vào thanh endgame");
            GamePlayController.Instance.isFinish = true;
            collision.gameObject.SetActive(false);
            PanelManager.Instance.OpenPanel(GameConfig.ENDGAME_PANEL);
        }
    }
}
