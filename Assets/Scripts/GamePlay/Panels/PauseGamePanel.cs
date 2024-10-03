using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGamePanel : Panel
{
    public void Resume()
    {
        GamePlayController.Instance.isPause = false;
        GamePlayController.Instance.isResume = true;
        PanelManager.Instance.ClosePanel("PauseGamePanel");
    }

    public void Replay()
    {
        Messenger.Broadcast(EventKey.Replay);
    }

    public void ReturnHome()
    {
        Time.timeScale = 1f;
        Messenger.Broadcast(EventKey.GoHome);
    }

    public void ChangeSound()
    {
        //
    }

    public void ChangeMusic()
    {
        //
    }
}
