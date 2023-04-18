/*using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


namespace GJ
{
    public class Server : MonoBehaviour
    {
        public InputField PortInput;

        List<ServerClient> clients;
        List<ServerClient> disconnectList;

        TcpListener server;                     // ������ �޴´�.
        bool isServerStarted;                   // ������ ���ȳ�?

        public void ServerCreate()
        {
            clients = new List<ServerClient>();
            disconnectList = new List<ServerClient>();

            try
            {
                int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);     // ��Ʈ�� ����ִٸ� ���Ƿ� 7777 �ְ�, �ƴ϶�� ��Ʈ��ǲ�ؽ�Ʈ�� �־��ش�.
                                                                                        // ��Ʈ�� �ױ����� �迡 ���� �ڸ��� ������ ��ó�� ���� ������ �� �ִ��� �˷��ִ� ������ �Ѵ�.
                server = new TcpListener(IPAddress.Any, port);                          // IPAddress.Any�� �ڽ��� ��ǻ���� 0.0.0.0 �� ����Ų��.
                                                                                        // �� ���� �ּҴ� �ϳ��� ��Ʈ�� ����� �� �־�, ������ ���� ��Ʈ�� ����ؾ� �Ѵ�.
                server.Start();                                                         // ���� ���ε�

                StartListening();
                isServerStarted = true;
                Console.WriteLine($"������ {port}���� ���۵Ǿ����ϴ�.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Socket error : {e.Message}");
            }
        }

        private void Update()
        {
            if (!isServerStarted) { return; }

            // ���� ����� Ŭ���̾�Ʈ�� �� ������.
            foreach (ServerClient c in clients)
            {
                // Ŭ���̾�Ʈ�� ������ ����Ǿ� �ֳ�? ������ ���¶�� Tcp�� �ݰ�, Socket�� �ݰ�, disconnectedList�� �߰��ϰ�, continue �ؼ� ���� �ݺ����� ������.
                if (!IsConnected(c.tcp))
                {
                    c.tcp.Close();
                    disconnectList.Add(c);
                    continue;
                }
                // Ŭ���̾�Ʈ�� ����Ǿ� ������, Ŭ���̾�Ʈ�κ��� üũ �޽����� �޴´�.
                else
                {
                    NetworkStream s = c.tcp.GetStream();                        // NetworkStream�� ��Ʈ��ũ���� �������� �帧�� ����Ѵ�. 
                    if (s.DataAvailable)
                    {
                        string data = new StreamReader(s, true).ReadLine();     // �����Ͱ� �����Ѵٸ� �о�´�.
                        if (data != null)
                        {
                            OnIncomingData(c, data);                           // �� �����Ͱ� null �� �ƴ϶�� ȣ���Ѵ�.
                        }
                    }
                }

                for (int i = 0; i < disconnectList.Count; i++)
                {
                    Broadcast($"{disconnectList[i].clientName} ������ ���������ϴ�", clients);

                    clients.Remove(disconnectList[i]);
                    disconnectList.RemoveAt(i);
                }
            }

            bool IsConnected(TcpClient c)
            {
                try
                {
                    if (c != null && c.Client != null && c.Client.Connected)            // Client�� �����̰�, Connected�� ����Ǿ��°�? 
                    {
                        if (c.Client.Poll(0, SelectMode.SelectRead))                    // Ŭ���̾�Ʈ���� ����Ǿ����� �׽�Ʈ�� 1����Ʈ �����͸� �����ְ�
                                                                                        // ����� ������ true�� ��ȯ�Ѵ�.
                        {
                            return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                        }
                        return true;
                    }
                    // ������� �ؿ��� �� ������ ������ ��
                    else { return false; }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        void StartListening()
        {
            server.BeginAcceptTcpClient(AcceptTcpClient, server);                       // �񵿱�� ��⸦ ����, ��⸦ �����ϰ�, ���� ���� ��⸦ �غ��Ѵ�.
                                                                                        // ����� ��⸦ �ϸ� ������ ����� ���� ���� ������ ������� �ʾ� ���߰� �ȴ�.
        }


        /// <summary>
        /// clients ����Ʈ�� Add�� �� �� �Լ��� ����ȴ�.  
        /// </summary>
        /// <param name="ar"></param>
        void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;                          // �ݹ��� �߸��� ar�� ���� IAsyncResult�� TcpListener�� �ٲ۴�.
            clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));             // ���ο� Ŭ������ ServerClient�� ����Ʈ�� ���Ѵ�.
            StartListening();                                                           // �׸��� �� �ڽ��� ȣ���ϸ� ���� �ݺ��Ѵ�.

            // �޽����� ����� ��ο��� ����
            Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });
        }

        void OnIncomingData(ServerClient c, string data)
        {
            if (data.Contains("&Name"))
            {
                c.clientName = data.Split('|')[1];
                Broadcast($"{c.clientName}�� ����Ǿ����ϴ�", clients);
            }

            Broadcast($"{c.clientName} : {data}", clients);                             // ��� client���� string�� �״�� ������.
        }
        void Broadcast(string data, List<ServerClient> c1)
        {
            foreach (var c in c1)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(c.tcp.GetStream());          // �����带 Ȱ��ȭ �Ѵ�.
                    writer.WriteLine(data);                                             // ��� ����鿡�� string�� ����. 
                    writer.Flush();                                                     // ���ݱ��� �� �����͵��� ������ ��������.
                }
                catch (Exception e)
                {
                    Debug.LogError($"���� ���� : {e.Message}�� Ŭ���̾�Ʈ���� {c.clientName}");
                }
            }
        }

    }
    public class ServerClient
    {
        public TcpClient tcp;               // tcp ����
        public string clientName;

        public ServerClient(TcpClient clientSocket)
        {
            clientName = "Guest";           // ���� �����ÿ��� Ŭ���̾�Ʈ �̸��� Guest�� �س��´�.
            tcp = clientSocket;             // ���� ������ �־��ش�.
        }
    }
}
*/