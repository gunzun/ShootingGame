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
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("222.237.134.178"), 10000);
            // ������ ������ gameData ���ڿ�
            string msg = ""; 
            msg = GameDataManager.Instance.jsonData;
            // Socket �ν��Ͻ� ����
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(ipEndPoint);         // ���� ����
                new Task(() =>                      // ������ �Ǹ� Task�� ���� ó��
                {
                    try
                    {
                        // ����Ǹ� �ڵ� client ����, ���� ����
                        byte[] binary = new byte[1024]; // ��� ���̳ʸ� ����
                        client.Receive(binary);         // �����κ��� �޽��� ���
                        string data = Encoding.ASCII.GetString(binary).Trim('\0');  // ������ ���� �޽����� String���� ��ȯ

                        // �޽��� ������ �����̶�� ��� ��� ���·�
                        if (System.String.IsNullOrWhiteSpace(data))
                        {
                            Debug.LogError("Data is Null Or White Space");
                        }
                        Debug.Log(data);
                        if (data != null)
                        {
                            GameDataManager.Instance.ReceiveJsonData = data;
                        }
                    }
                    catch (SocketException SE)
                    {
                        Debug.LogWarning(SE);
                        // ���� ������ �߻��ϸ� ���⼭ Exception�� �߻�
                    }
                }).Start();     // Task ���� 


                // �����κ��� �޽����� �ޱ� ���� ����
                client.Send(Encoding.ASCII.GetBytes(msg + "\r\n"));
                if ("EXIT".Equals(msg, StringComparison.OrdinalIgnoreCase))
                {
                    // break;
                }
                Console.WriteLine($"Disconnected");
                Debug.Log($"Disconnected");
            }
            Console.WriteLine("Press Any Key...");
            Debug.Log("Press Any Key...");
            // Console.ReadLine();
        }
    }
}
