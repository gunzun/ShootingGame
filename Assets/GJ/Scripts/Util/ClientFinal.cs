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
        string msg = "";
        bool isNetWorkEnd = false;

        void Start()
        {
            // Socket EndPoint
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("222.237.134.184"), 10002);
            // ������ ������ gameData ���ڿ�
            
            // Socket �ν��Ͻ� ����
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(ipEndPoint);         // ���� ����
                // Task t = Task.Run() =>
                Task t = Task.Run(() => ServerNetwork());
                
                new Task(() =>                      // ������ �Ǹ� Task�� ���� ó��
                {
                    try
                    {
                        while (true)
                        {
                            // ����Ǹ� �ڵ� client ����
                            byte[] binary = new byte[1024]; // ��� ���̳ʸ� ����
                            client.Receive(binary);         // �����κ��� �޽��� ���

                            // Trim���� ������ �ϴ°�? �׷� ����Ʈ�� ���� �� �̻��� �Ȼ��⳪?
                            string data = Encoding.ASCII.GetString(binary).Trim('\0');  // ������ ���� �޽����� String���� ��ȯ

                            // ������ ���ӵ����Ϳ� �� �� �ߺ��Ǵ� ������ �����Ѵ�.
                            GameDataManager.Instance.CheckDuplicate_ServerData(data);

                            // �޽��� ������ �����̶�� ��� ��� ���·�
                            if (System.String.IsNullOrEmpty(data))
                            {
                                Debug.LogWarning("Data is Null Or Empty");
                            }
                            else
                            {
                                
                                isNetWorkEnd = true;
                            }
                        }
                    }
                    catch (SocketException SE)
                    {
                        Debug.LogWarning(SE);
                        // ���� ������ �߻��ϸ� ���⼭ Exception�� �߻�
                    }
                }).Start();     // Task ���� 

                t.Wait();

                // ������ �޽��� ����
                msg = JsonUtility.ToJson(GameDataManager.Instance.gameDataGroup, true);
                client.Send(Encoding.ASCII.GetBytes(msg));
                client.Send(Encoding.ASCII.GetBytes("EXIT"));

                Debug.Log($"Disconnected");
            }
        }
        static async Task ServerNetwork()
        {
            await Task.Delay(200);
        }
    }
}
