using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GJ
{
    class ServerFinal
    {
        // ���� ���� Task �޼ҵ�
        static async Task RunServer(int _port)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, _port);

            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(ipEndPoint);        // ���� ���Ͽ� EndPoint ����
                server.Listen(20);              // Ŭ���̾�Ʈ ���� ��� ����
                System.Console.WriteLine($"Server Start... Listen Port {ipEndPoint.Port}...");

                // server Accept�� Task�� ���� ó�� (�񵿱�� �����)
                var task = new Task(() =>
                {
                    while (true)
                    {
                        var client = server.Accept();                               // Ŭ���̾�Ʈ�κ��� ���� ���
                        new Task(() =>                                              // ������ �Ǹ� Task�� ���� ó��
                        {
                            var ip = client.RemoteEndPoint as IPEndPoint;           // Ŭ���̾�Ʈ EndPoint�� �����´�.
                            System.Console.WriteLine                                // ���� ip�� ���� �ð� �ܼ� ���
                        ($"Client : (From : {ip.Address} : {ip.Port}, Connection time : {System.DateTime.Now}");

                            client.Send(Encoding.ASCII.GetBytes("Welcome server!\r\n>"));// Ŭ��κ��� ���� �޽����� byte��ȯ�Ͽ� �۽�
                            StringBuilder msgBuffer = new StringBuilder();          // �޽��� ����

                            // ����Ǹ� �ڵ� client ����
                            using (client)
                            {
                                while (true)
                                {
                                    byte[] binary = new byte[1024];                 // ��� ���̳ʸ� ����
                                    client.Receive(binary);                         // Ŭ���̾�Ʈ�κ��� �޽��� ���
                                    string data = Encoding.ASCII.GetString(binary); // Ŭ�󿡰� ���� �޽����� ���ڿ��� ��ȯ
                                    msgBuffer.Append(data.Trim('\0'));              // �޽��� ������ ����


                                    // �޽��� �� ������ 2���� �̻��̰� ����(\r\n)�� �߻��ϸ�
                                    if (msgBuffer.Length > 2 && msgBuffer[msgBuffer.Length - 2] == '\r' && msgBuffer[msgBuffer.Length - 1] == '\n')
                                    {
                                        // �޽��� ������ ������ string���� ��ȯ
                                        data = msgBuffer.ToString().Replace("\n", "").Replace("\r", "");

                                        // �޽��� ������ �����̶�� ��� ��� ����
                                        if (System.String.IsNullOrWhiteSpace(data)) { continue; }

                                        // �޽��� ������ exit��� ���� ���� ���� (���� ����)
                                        if ("EXIT".Equals(data, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            break;
                                        }
                                        System.Console.WriteLine("Message = " + data);

                                        msgBuffer.Length = 0;                                                   // ���� �ʱ�ȭ
                                        var sendMsg = Encoding.ASCII.GetBytes("ECHO : " + data + "\r\n>");      // �޽����� ECHO�� ����
                                        client.Send(sendMsg);                                                   // Ŭ���̾�Ʈ�� �޽��� �۽�
                                    }
                                }
                            }
                            // �ܼ� ��� - ���� ���� �޽���
                            System.Console.WriteLine($"Disconnected : (From : {ip.Address.ToString()} : {ip.Port}, Connection time : {System.DateTime.Now}");
                        }).Start();  // Task ����
                    }
                });
                task.Start();       // Task ����
                await task;         // ���

            }
        }
        static void Main(string[] args)
        {
            // Task�� Socket ������ �����. (������ ����� ������ ���)
            RunServer(10000).Wait();

            // �ƹ�Ű�� ������ ����
            System.Console.WriteLine("Press Any Key...");
            System.Console.ReadLine();
        }
    }
}
