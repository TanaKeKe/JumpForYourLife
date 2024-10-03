using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SettingPanel : Panel
{
    public void ClosePanel()
    {
        PanelManager.Instance.ClosePanel("SettingPanel");
    }

    public void OpenLink()
    {
        UnityEngine.Debug.Log("Ấn vào nút open");

        // Liên kết Facebook cần mở
        string url = "https://www.facebook.com/tan.phanthanh.731";

        // Kiểm tra hệ điều hành (để xử lý theo cách tương thích)
        try
        {
            // Đối với Windows
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            // Xử lý ngoại lệ nếu không thể mở URL
            Console.WriteLine("Không thể mở URL: " + e.Message);
        }
    }
}
