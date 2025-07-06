using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int howMuchButtonClick = 0;
    public int limitButtonClick = 5;
    public string ntpServer = "time.google.com";

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        DateTime currentTime = DateTime.Now;
        Debug.Log("Waktu saat ini: " + currentTime);
        StartCoroutine(GetNetworkTime());
    }

    // Update is called once per frame  
    void Update()
    {

    }

    public void ChoiceStep(int idStep)
    {
        PlayerPrefs.SetInt("SelectedStep", idStep);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    #region  
    public DateTime expiredApp = new DateTime(2025, 6, 25);

    private IEnumerator GetNetworkTime()
    {
        UdpClient client = new UdpClient(ntpServer, 123);
        byte[] data = new byte[48];
        data[0] = 0x1B;

        client.Send(data, data.Length);
        IPEndPoint ep = null;

        yield return new WaitForSeconds(1);

        if (client.Available > 0)
        {
            data = client.Receive(ref ep);
            DateTime epochStart = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            ulong seconds = (ulong)((ulong)data[40] << 24 | (ulong)data[41] << 16 | (ulong)data[42] << 8 | (ulong)data[43]);
            ulong fraction = (ulong)((ulong)data[44] << 24 | (ulong)data[45] << 16 | (ulong)data[46] << 8 | (ulong)data[47]);

            ulong milliseconds = seconds * 1000 + (fraction * 1000) / 0x100000000L;
            DateTime networkTime = epochStart.AddMilliseconds(milliseconds);

            Debug.Log("Waktu dari server NTP: " + networkTime);
        }

        client.Close();
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
       Application.Quit();  
#endif
    }
    #endregion
}
