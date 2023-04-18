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
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);

            // Socket 인스턴스 생성
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(ipEndPoint);         // 소켓 접속
                new Task(() =>                      // 접속이 되면 Task로 병렬 처리
                {
                    try
                    {
                        // 종료되면 자동 client 종료, 무한 루프
                        while (true)
                        {
                            byte[] binary = new byte[1024]; // 통신 바이너리 버퍼
                            client.Receive(binary);         // 서버로부터 메시지 대기
                            string data = Encoding.ASCII.GetString(binary).Trim('\0');  // 서버로 받은 메시지를 String으로 변환

                            // 메시지 내용이 공백이라면 계속 대기 상태로
                            if (System.String.IsNullOrWhiteSpace(data))
                            {
                                continue;
                            }
                            Console.WriteLine(data);
                        }
                    }
                    catch (SocketException)
                    {
                        // 접속 끊김이 발생하면 여기서 Exception이 발생
                    }
                }).Start();     // Task 실행 

                // 유저로부터 메시지를 받기 위한 루프
                while (true)
                {
                    var msg = Console.ReadLine();       // 콘솔 입력
                    client.Send(Encoding.ASCII.GetBytes(msg + "\r\n"));
                    if ("EXIT".Equals(msg, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
                Console.WriteLine($"Disconnected");
            }
            Console.WriteLine("Press Any Key...");
            Console.ReadLine();
        }
    }
}
