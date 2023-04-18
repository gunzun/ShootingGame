using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GJ
{
    class ServerFinal
    {
        // 서버 실행 Task 메소드
        static async Task RunServer(int _port)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, _port);

            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(ipEndPoint);        // 서버 소켓에 EndPoint 설정
                server.Listen(20);              // 클라이언트 소켓 대기 버퍼
                System.Console.WriteLine($"Server Start... Listen Port {ipEndPoint.Port}...");

                // server Accept를 Task로 병렬 처리 (비동기로 만든다)
                var task = new Task(() =>
                {
                    while (true)
                    {
                        var client = server.Accept();                               // 클라이언트로부터 접속 대기
                        new Task(() =>                                              // 접속이 되면 Task로 병렬 처리
                        {
                            var ip = client.RemoteEndPoint as IPEndPoint;           // 클라이언트 EndPoint를 가져온다.
                            System.Console.WriteLine                                // 접속 ip와 현재 시간 콘솔 출력
                        ($"Client : (From : {ip.Address} : {ip.Port}, Connection time : {System.DateTime.Now}");

                            client.Send(Encoding.ASCII.GetBytes("Welcome server!\r\n>"));// 클라로부터 접속 메시지를 byte변환하여 송신
                            StringBuilder msgBuffer = new StringBuilder();          // 메시지 버퍼

                            // 종료되면 자동 client 종료
                            using (client)
                            {
                                while (true)
                                {
                                    byte[] binary = new byte[1024];                 // 통신 바이너리 버퍼
                                    client.Receive(binary);                         // 클라이언트로부터 메시지 대기
                                    string data = Encoding.ASCII.GetString(binary); // 클라에게 받은 메시지를 문자열로 변환
                                    msgBuffer.Append(data.Trim('\0'));              // 메시지 공백을 제거


                                    // 메시지 총 내용이 2글자 이상이고 개행(\r\n)이 발생하면
                                    if (msgBuffer.Length > 2 && msgBuffer[msgBuffer.Length - 2] == '\r' && msgBuffer[msgBuffer.Length - 1] == '\n')
                                    {
                                        // 메시지 버퍼의 내용을 string으로 변환
                                        data = msgBuffer.ToString().Replace("\n", "").Replace("\r", "");

                                        // 메시지 내용이 공백이라면 계속 대기 상태
                                        if (System.String.IsNullOrWhiteSpace(data)) { continue; }

                                        // 메시지 내용이 exit라면 무한 루프 종료 (서버 종료)
                                        if ("EXIT".Equals(data, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            break;
                                        }
                                        System.Console.WriteLine("Message = " + data);

                                        msgBuffer.Length = 0;                                                   // 버퍼 초기화
                                        var sendMsg = Encoding.ASCII.GetBytes("ECHO : " + data + "\r\n>");      // 메시지에 ECHO를 붙임
                                        client.Send(sendMsg);                                                   // 클라이언트로 메시지 송신
                                    }
                                }
                            }
                            // 콘솔 출력 - 접속 종료 메시지
                            System.Console.WriteLine($"Disconnected : (From : {ip.Address.ToString()} : {ip.Port}, Connection time : {System.DateTime.Now}");
                        }).Start();  // Task 실행
                    }
                });
                task.Start();       // Task 실행
                await task;         // 대기

            }
        }
        static void Main(string[] args)
        {
            // Task로 Socket 서버를 만든다. (서버가 종료될 때까지 대기)
            RunServer(10000).Wait();

            // 아무키나 누르면 종료
            System.Console.WriteLine("Press Any Key...");
            System.Console.ReadLine();
        }
    }
}
