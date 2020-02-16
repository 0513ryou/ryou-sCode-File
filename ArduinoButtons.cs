using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Events;

public class ArduinoButtons : MonoBehaviour
{
    public string portName = "COM3";
    public int baudRate = 9600;

    public bool opened {
        get;
        private set;
    }

    private SerialPort m_serialPort;
    private Thread m_thread;
    private volatile bool m_threadRunning = false;
    private bool m_messageReceived = false;
    public string m_message;
    public int m_button_code = 0;

    private bool[] m_button_down = new bool[10];
    private bool[] m_button_up = new bool[10];

    public bool GetButtonDown(int number) {
        if (number >= 0 && number < m_button_down.Length) {
            return m_button_down[number];
        }
        return false;
    }

    public bool GetButtonUp(int number) {
        if (number >= 0 && number < m_button_up.Length) {
            return m_button_up[number];
        }
        return false;
    }

    public void Open() {
        if (!opened) {
            try {
                m_serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                m_serialPort.Open();
                opened = true;

                m_messageReceived = false;

                m_thread = new Thread(ReadData);
                m_thread.Start();
            }
            catch(IOException e) {
                Debug.Log("コントローラが接続されていない。");
            }
            
        }
    }

    public void Close() {
        if (opened) {
            opened = false;
            if (m_thread != null && m_threadRunning) {
                m_threadRunning = false;
                m_thread.Join(1000);
            }

            if (m_serialPort != null && m_serialPort.IsOpen) {
                m_serialPort.Close();
                m_serialPort.Dispose();
            }
        }
    }

    void OnEnable() {
        Open();
    }

    void OnDisable() {
        Close();
    }

    void Awake()
    {
        opened = false;
    }

    private void ReadData() {
        m_threadRunning = true;
        while(m_threadRunning && m_serialPort != null && m_serialPort.IsOpen) {
            try {
                int c = m_serialPort.BytesToRead;
                if (c > 0) {
                    while(c > 0) {
                        m_button_code = m_serialPort.ReadByte();
                        m_messageReceived = true;
                        c--;
                    }
                    
                }
                
            }
            catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
        m_threadRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_button_down.Length; i++) {
            m_button_down[i] = false;
        }

        if (m_messageReceived) {
            // string[] parts = m_message.Split(':');
            // if (parts.Length == 2) {
            //     int btn = int.Parse(parts[0]);
            //     int state = int.Parse(parts[1]);
            //     if (state > 0 && btn >= 0 && btn < m_button_down.Length) {
            //         m_button_down[btn] = true;
            //     }
                
            //     if (state > 0) {
            //         OnButtonDown(btn);
            //     }
            //     else {
            //         OnButtonUp(btn);
            //     }
            // }

            bool buttonDown = m_button_code >= 16;
            int btn = buttonDown ? m_button_code - 16 : m_button_code;
            if (btn < m_button_down.Length) {
                m_button_down[btn] = buttonDown;
            }
            if (buttonDown) {
                OnButtonDown(btn);
            }
            else {
                OnButtonUp(btn);
            }

            // これを消すな！
            m_messageReceived = false;
        }
    }

    private void OnButtonDown(int button) {
        Debug.Log("Down: " + button);
       // SceneManager.LoadScene("DemoScene20190516");
    }

    private void OnButtonUp(int button) {
        Debug.Log("Up: " + button);
        // TODO
    }
}
