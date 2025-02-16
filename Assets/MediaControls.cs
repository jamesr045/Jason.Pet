using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MediaControls : MonoBehaviour
{
    internal enum VirtualKeyCodes
        : uint
    {
        VOLUME_MUTE = 0xAD,
        VOLUME_DOWN = 0xAE,
        VOLUME_UP = 0xAF,
        NEXT_TRACK = 0xB0,
        PREVIOUS_TRACK = 0xB1,
        STOP = 0xB2,
        PLAY_PAUSE = 0xB3,
        LAUNCH_MEDIA = 0xB5,
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    internal static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

    internal static void SendKey(VirtualKeyCodes virtualKeyCode)
    {
        keybd_event((uint)virtualKeyCode, 0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPauseButton()
    {
        SendKey(VirtualKeyCodes.PLAY_PAUSE);
    }

    public void ForwardButton()
    {
        SendKey(VirtualKeyCodes.NEXT_TRACK);
    }

    public void BackButton()
    {
        SendKey(VirtualKeyCodes.PREVIOUS_TRACK);
    }
}
