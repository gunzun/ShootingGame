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
            // ������ ������ gameData ���ڿ�
            string msg = ""; 
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

                        

                        // Trim���� ������ �ϴ°�? �׷� ����Ʈ�� ���� �� �̻��� �Ȼ��⳪?
                        string data = Encoding.ASCII.GetString(binary).Trim('\0');  // ������ ���� �޽����� String���� ��ȯ

                        // �޽��� ������ �����̶�� ��� ��� ���·�
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
                        // ���� ������ �߻��ϸ� ���⼭ Exception�� �߻�
                    }
                }).Start();     // Task ���� 

                // ������ �޽��� ����
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
