using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System;

namespace GJ
{
    public class Client : MonoBehaviour
    {
        public InputField IPInput, PortInput, NickInput;
        string clientName;

        bool socketReady;
        TcpClient socket;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        public void ConnectToServer()
        {
            // �̹� ����Ǿ��ٸ� �Լ� ����
            if (socketReady) { return; }

            // �⺻ ȣ��Ʈ/��Ʈ��ȣ
            string ip = IPInput.text == "" ? "127.0.0.1" : IPInput.text;
            int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);

            // ���� ����
            try
            {
                socket = new TcpClient(ip, port);
                stream = socket.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);
                socketReady = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"���Ͽ��� : {e.Message}");
            }
        }

        void Update()
        {
            if (socketReady && stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }

        void OnIncomingData(string _data)
        {
            if (_data == "%NAME")
            {
                clientName = NickInput.text == "" ? "Guest" + UnityEngine.Random.Range(1000, 10000) : NickInput.text;
                Send($"&NAME|{clientName}");
                return;
            }
            Debug.Log(_data);
        }

        void Send(string _data)
        {
            if (!socketReady) { return; }

            writer.WriteLine(_data);
            writer.Flush();
        }

        public void OnSendButton(InputField _sendInput)
        {
#if (UNITY_EDITOR || UNITY_STANDALONE)
            if (!Input.GetButtonDown("Submit")) { return; }
            _sendInput.ActivateInputField();
#endif
            if (_sendInput.text.Trim() == "") { return; }

            string message = _sendInput.text;
            _sendInput.text = "";
            Send(message);
        }
        private void OnApplicationQuit()
        {
            CloseSocket();
        }
        void CloseSocket()
        {
            if (!socketReady) { return; }

            writer.Close();
            reader.Close();
            socket.Close();
            socketReady = false;
        }
    }
}
