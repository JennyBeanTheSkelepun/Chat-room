using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public enum packetType {EMPTY, NICKNAME, CHATMESSAGE, NOTICE, GAME, FUNCTION};

    [Serializable()]
    public class Packet
    {
        protected packetType type = packetType.EMPTY;

        public packetType GetType()
        {
            return type;
        }
    }

    [Serializable()]
    public class ChatMessagePacket : Packet
    {
        public string message;

        public ChatMessagePacket(string text)
        {
            type = packetType.CHATMESSAGE;
            message = text;
        }
    }

    [Serializable()]
    public class NicknamePacket : Packet
    {
        public string nickname;

        public NicknamePacket(string text)
        {
            type = packetType.NICKNAME;
            nickname = text;
        }
    }

    [Serializable()]
    public class NoticePacket : Packet
    {
        public string notice;

        public NoticePacket(string text)
        {
            type = packetType.NOTICE;
            notice = text;
        }
    }

    [Serializable()]
    public class GamePacket : Packet
    {
        public string message;

        public GamePacket(string text)
        {
            type = packetType.GAME;
            message = text;
        }
    }

    [Serializable()]
    public class FunctionPacket : Packet
    {
        public string command;

        public FunctionPacket(string text)
        {
            type = packetType.FUNCTION;
            command = text;
        }
    }
}
