using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace GJ
{
    public class ClientFinal : MonoBehaviour
    {
        void Start()
        {
            // Socket EndPoint
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("222.237.134.184"), 10001);
            // 서버에 전송할 gameData 문자열
            string msg = ""; 
            // Socket 인스턴스 생성
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(ipEndPoint);         // 소켓 접속
                new Task(() =>                      // 접속이 되면 Task로 병렬 처리
                {
                    try
                    {
                        // 종료되면 자동 client 종료, 무한 루프
                        byte[] binary = new byte[1024]; // 통신 바이너리 버퍼
                        client.Receive(binary);         // 서버로부터 메시지 대기

                        

                        // Trim으로 지워야 하는가? 그럼 리스트로 만들 때 이상이 안생기나?
                        string data = Encoding.ASCII.GetString(binary).Trim('\0');  // 서버로 받은 메시지를 String으로 변환

                        // 메시지 내용이 공백이라면 계속 대기 상태로
                        if (System.String.IsNullOrEmpty(data))
                        {
                            Debug.LogWarning("Data is Null Or Empty");
                        }
                        else
                        {
                            GameDataManager.Instance.ReceiveServerData_And_CheckDuplicate(data);
                        }
                        

                        Debug.Log(data);
                    }
                    catch (SocketException SE)
                    {
                        Debug.LogWarning(SE);
                        // 접속 끊김이 발생하면 여기서 Exception이 발생
                    }
                }).Start();     // Task 실행 

                // 유저가 메시지 전송
                msg = GameDataManager.Instance.jsonData;
                client.Send(Encoding.ASCII.GetBytes(msg));

                if ("EXIT".Equals(msg, StringComparison.OrdinalIgnoreCase))
                {
                    // break;
                }
                Debug.Log($"Disconnected");
            }
        }
    }
}
