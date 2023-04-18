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

            // Socket �ν��Ͻ� ����
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(ipEndPoint);         // ���� ����
                new Task(() =>                      // ������ �Ǹ� Task�� ���� ó��
                {
                    try
                    {
                        // ����Ǹ� �ڵ� client ����, ���� ����
                        while (true)
                        {
                            byte[] binary = new byte[1024]; // ��� ���̳ʸ� ����
                            client.Receive(binary);         // �����κ��� �޽��� ���
                            string data = Encoding.ASCII.GetString(binary).Trim('\0');  // ������ ���� �޽����� String���� ��ȯ

                            // �޽��� ������ �����̶�� ��� ��� ���·�
                            if (System.String.IsNullOrWhiteSpace(data))
                            {
                                continue;
                            }
                            Console.WriteLine(data);
                        }
                    }
                    catch (SocketException)
                    {
                        // ���� ������ �߻��ϸ� ���⼭ Exception�� �߻�
                    }
                }).Start();     // Task ���� 

                // �����κ��� �޽����� �ޱ� ���� ����
                while (true)
                {
                    var msg = Console.ReadLine();       // �ܼ� �Է�
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
