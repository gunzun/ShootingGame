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

        TcpListener server;                     // 서버를 받는다.
        bool isServerStarted;                   // 서버가 열렸나?

        public void ServerCreate()
        {
            clients = new List<ServerClient>();
            disconnectList = new List<ServerClient>();

            try
            {
                int port = PortInput.text == "" ? 7777 : int.Parse(PortInput.text);     // 포트가 비어있다면 임의로 7777 주고, 아니라면 포트인풋텍스트를 넣어준다.
                                                                                        // 포트란 항구에서 배에 따라 자리를 나누는 것처럼 어디로 출입할 수 있는지 알려주는 역할을 한다.
                server = new TcpListener(IPAddress.Any, port);                          // IPAddress.Any는 자신의 컴퓨터인 0.0.0.0 을 가리킨다.
                                                                                        // 각 소켓 주소는 하나의 포트만 사용할 수 있어, 사용되지 않은 포트로 사용해야 한다.
                server.Start();                                                         // 서버 바인드

                StartListening();
                isServerStarted = true;
                Console.WriteLine($"서버가 {port}에서 시작되었습니다.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Socket error : {e.Message}");
            }
        }

        private void Update()
        {
            if (!isServerStarted) { return; }

            // 현재 연결된 클라이언트를 다 꺼낸다.
            foreach (ServerClient c in clients)
            {
                // 클라이언트가 여전히 연결되어 있나? 끊어진 상태라면 Tcp를 닫고, Socket도 닫고, disconnectedList에 추가하고, continue 해서 다음 반복문을 돌린다.
                if (!IsConnected(c.tcp))
                {
                    c.tcp.Close();
                    disconnectList.Add(c);
                    continue;
                }
                // 클라이언트가 연결되어 있으면, 클라이언트로부터 체크 메시지를 받는다.
                else
                {
                    NetworkStream s = c.tcp.GetStream();                        // NetworkStream은 네트워크에서 데이터의 흐름을 담당한다. 
                    if (s.DataAvailable)
                    {
                        string data = new StreamReader(s, true).ReadLine();     // 데이터가 존재한다면 읽어온다.
                        if (data != null)
                        {
                            OnIncomingData(c, data);                           // 그 데이터가 null 이 아니라면 호출한다.
                        }
                    }
                }

                for (int i = 0; i < disconnectList.Count; i++)
                {
                    Broadcast($"{disconnectList[i].clientName} 연결이 끊어졌습니다", clients);

                    clients.Remove(disconnectList[i]);
                    disconnectList.RemoveAt(i);
                }
            }

            bool IsConnected(TcpClient c)
            {
                try
                {
                    if (c != null && c.Client != null && c.Client.Connected)            // Client가 소켓이고, Connected가 연결되었는가? 
                    {
                        if (c.Client.Poll(0, SelectMode.SelectRead))                    // 클라이언트에게 연결되었는지 테스트로 1바이트 데이터를 보내주고
                                                                                        // 제대로 받으면 true로 반환한다.
                        {
                            return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                        }
                        return true;
                    }
                    // 여기부터 밑에는 다 연결이 끊어진 것
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
            server.BeginAcceptTcpClient(AcceptTcpClient, server);                       // 비동기로 듣기를 시작, 듣기를 시작하고, 다음 내용 듣기를 준비한다.
                                                                                        // 동기로 듣기를 하면 내용이 진행될 동안 다음 내용이 실행되지 않아 멈추게 된다.
        }


        /// <summary>
        /// clients 리스트에 Add할 때 이 함수가 실행된다.  
        /// </summary>
        /// <param name="ar"></param>
        void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;                          // 콜백이 뜨면은 ar로 받은 IAsyncResult를 TcpListener로 바꾼다.
            clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));             // 새로운 클래스인 ServerClient를 리스트에 더한다.
            StartListening();                                                           // 그리고 또 자신을 호출하며 무한 반복한다.

            // 메시지를 연결된 모두에게 보냄
            Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });
        }

        void OnIncomingData(ServerClient c, string data)
        {
            if (data.Contains("&Name"))
            {
                c.clientName = data.Split('|')[1];
                Broadcast($"{c.clientName}이 연결되었습니다", clients);
            }

            Broadcast($"{c.clientName} : {data}", clients);                             // 모든 client에게 string을 그대로 보낸다.
        }
        void Broadcast(string data, List<ServerClient> c1)
        {
            foreach (var c in c1)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(c.tcp.GetStream());          // 쓰기모드를 활성화 한다.
                    writer.WriteLine(data);                                             // 모든 사람들에게 string을 쓴다. 
                    writer.Flush();                                                     // 지금까지 쓴 데이터들을 강제로 내보낸다.
                }
                catch (Exception e)
                {
                    Debug.LogError($"쓰기 에러 : {e.Message}를 클라이언트에게 {c.clientName}");
                }
            }
        }

    }
    public class ServerClient
    {
        public TcpClient tcp;               // tcp 형식
        public string clientName;

        public ServerClient(TcpClient clientSocket)
        {
            clientName = "Guest";           // 최초 생성시에는 클라이언트 이름을 Guest로 해놓는다.
            tcp = clientSocket;             // 들어온 소켓을 넣어준다.
        }
    }
}
*/