using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;   //��� ���̳ʸ� ��������!

namespace GJ
{
    [Serializable]  //�ϳ��� ����ȭ ���ڴ�. ��? ����Ʈȭ �ϰڴ�?
    public class SimplePacket       //�������̺��� �̱������� ����Ŷ� ���⼭�� ����
    {
        public float mouseX = 0.0f;
        public float mouseY = 0.0f;

        public List<GameData> gameDatas_Net = new List<GameData>();


        //��°�
        public static byte[] ToByteArray(SimplePacket packet)
        {
            //��Ʈ������ �Ѵ�.  �����������
            MemoryStream stream = new MemoryStream();

            //��Ʈ������ �ǳʿ� ��Ŷ�� �������� ���̳ʸ� �����ش�.
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, packet.mouseX);       //��Ʈ���� ��´�. �ø��������� ��´ٴ� ����.
            formatter.Serialize(stream, packet.mouseY);

            return stream.ToArray();
        }

        //�޴°�
        public static SimplePacket FromByteArray(byte[] input)
        {
            //��Ʈ�� ����
            MemoryStream stream = new MemoryStream(input);
            //��Ʈ������ ������ ���� �� ���̳ʸ� ������ ���� �ٸ��ŵ� �ִ��� ã�ƺ���
            //���̳ʸ� �����ͷ� ��Ʈ���� �������� �����͸� ��������.
            BinaryFormatter formatter = new BinaryFormatter();
            //��Ŷ�� �����ؼ�      //��Ŷ �����⿡ ���� �˾ƺ���!
            SimplePacket packet = new SimplePacket();
            //������ ��Ŷ�� �����͸� ��ø��� �������ؼ� ��´�.
            packet.mouseX = (float)formatter.Deserialize(stream);
            packet.mouseY = (float)formatter.Deserialize(stream);

            return packet;
        }
        public static byte[] GameDataToByteArray(byte[] input)
        {
            return input;
        }

        public static SimplePacket SendGameDataToServer(byte _gameData)
        {
            MemoryStream stream = new MemoryStream(_gameData);
            BinaryFormatter formatter = new BinaryFormatter();
            SimplePacket packet = new SimplePacket();
            packet.gameDatas_Net = (List<GameData>)formatter.Deserialize(stream);
            return packet;
        }
    }
}