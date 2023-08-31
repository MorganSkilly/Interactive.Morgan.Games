using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CrossPlatformInputManager : MonoBehaviour
{
    public enum inputDevice
    {
        unknown,
        touch,
        keyboardMouse,
        controller
    }

    [SerializeField] private inputDevice currentDevice = inputDevice.keyboardMouse;

    [SerializeField] private GameObject uiCanvas_CrossPlatform;
    [SerializeField] private GameObject uiCanvas_Touch;
    [SerializeField] private GameObject uiCanvas_KeyboardMouse;
    [SerializeField] private GameObject uiCanvas_Controller;

    [SerializeField] public Component crossPlatformInputHandler;

    // Start is called before the first frame update
    void Start()
    {
        //rudimentary initial check to guess what type of inpout device is required
        string os = SystemInfo.operatingSystem.ToLower();

        if (os.Contains("android") || os.Contains("iphone") || os.Contains("ipad") || os.Contains("ios"))
        {
            currentDevice = inputDevice.touch;
        }
        else if (os.Contains("windows") || os.Contains("mac") || os.Contains("linux") || os.Contains("chrome"))
        {
            currentDevice = inputDevice.keyboardMouse;
        }
        else if (os.Contains("") || os.Contains("") || os.Contains(""))
        {
            currentDevice = inputDevice.controller;
        }
        else
        {
            currentDevice = inputDevice.unknown;
        }

        crossPlatformInputHandler.GetComponent<CrossPlatformInputHandlerFirstPerson>().SetInputDevice(currentDevice);

        SetInputUI(currentDevice);
    }

    private void OnGUI()
    {
        if (currentDevice == inputDevice.unknown)
        {
            GUI.Label(new Rect(10, 50, 1000, 1000), "Input device type could not be automatically configured. Touch input will not be available.");
        }
        else
        {
            GUI.Label(new Rect(10, 50, 1000, 1000), "Detected input configuration: " + currentDevice);
        }

        GUI.Label(new Rect(10, 70, 1000, 1000), "OS: " + SystemInfo.operatingSystem.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetInputUI(inputDevice currentDevice)
    {
        if (currentDevice == inputDevice.touch)
        {
            uiCanvas_CrossPlatform.SetActive(true);
            uiCanvas_Touch.SetActive(true);
            uiCanvas_KeyboardMouse.SetActive(false);
            uiCanvas_Controller.SetActive(false);
        }
        else if (currentDevice == inputDevice.keyboardMouse)
        {
            uiCanvas_CrossPlatform.SetActive(true);
            uiCanvas_Touch.SetActive(false);
            uiCanvas_KeyboardMouse.SetActive(true);
            uiCanvas_Controller.SetActive(false);
        }
        else if (currentDevice == inputDevice.controller)
        {
            uiCanvas_CrossPlatform.SetActive(true);
            uiCanvas_Touch.SetActive(false);
            uiCanvas_KeyboardMouse.SetActive(false);
            uiCanvas_Controller.SetActive(true);
        }
        else
        {
            uiCanvas_CrossPlatform.SetActive(true);
            uiCanvas_Touch.SetActive(false);
            uiCanvas_KeyboardMouse.SetActive(true);
            uiCanvas_Controller.SetActive(false);
        }
    }
}
