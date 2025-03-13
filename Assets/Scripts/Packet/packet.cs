using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ccrc;

public class packet : MonoBehaviour
{
    public class Constants
    {
        public const int TRANSMIT_PACKET_SIZE = 25;
        public const int TRANS_FEEDBACK_SIZE = 20;
    }

    public class Command
    {
        public float power = 0.0f;
        public float dribble = 0.0f;
        public float vx = 0.0f;
        public float vy = 0.0f;
        public float vr = 0.0f;
        public int id = 0;
        public bool valid = false;
        public bool kick_mode = false;
    }

    public class RadioPacket
    {
        public int robotID = 0;
        public float velX = 0.0f;
        public float velY = 0.0f;
        public float velR = 0.0f;
        public bool ctrl = false;
        public float ctrlPowerLevel = 0.0f;
        public bool shoot = false;
        public float shootPowerLevel = 0.0f;
        public bool shootMode = false;
        public int frequency;
        public byte[] transmitPacket;
        public byte[] start_packet1;
        public byte[] start_packet2;

        public RadioPacket(int frequency)
        {
            this.frequency = frequency;
            this.transmitPacket = new byte[Constants.TRANSMIT_PACKET_SIZE];
            CreateStartPacket(frequency, out this.start_packet1, out this.start_packet2);
        }

        public bool Encode()
        {
            Command HU_CMD = new Command();
            HU_CMD.valid = true;
            HU_CMD.id = this.robotID;
            HU_CMD.vx = this.velX;
            HU_CMD.vy = this.velY;
            HU_CMD.vr = this.velR;
            HU_CMD.dribble = this.ctrl ? this.ctrlPowerLevel : 0;
            HU_CMD.power = this.shoot ? this.shootPowerLevel : 0;
            HU_CMD.kick_mode = this.shootMode;

            Array.Clear(this.transmitPacket, 0, this.transmitPacket.Length);
            this.transmitPacket[0] = 0xFF;
            this.transmitPacket[21] = (byte)(((this.frequency & 0x0F) << 4) | 0x07);

            EncodeLegacy(HU_CMD, this.transmitPacket, 0);
            return true;
        }

        private void EncodeLegacy(Command command, byte[] TXBuff, int num)
        {
            int real_num = command.id;
            int vx = (int)command.vx;
            int vy = (int)command.vy;
            int ivr = (int)command.vr;
            int vr = Math.Min(Math.Abs(ivr), 511) * (ivr > 0 ? 1 : -1);
            int power = (int)command.power;
            bool kick_mode = command.kick_mode;
            int dribble = (int)(command.dribble + 0.1f);

            int vx_value_uint = Math.Abs(vx);
            int vy_value_uint = Math.Abs(vy);
            int w_value_uint = Math.Abs(vr);

            if (real_num >= 8)
            {
                TXBuff[1] |= (byte)(0x01 << (real_num - 8));
            }

            if (real_num < 8)
            {
                TXBuff[2] |= (byte)(0x01 << real_num);
            }

            int baseIndex = 6 * num;
            TXBuff[baseIndex + 3] = (byte)(0x01 | ((((kick_mode ? 0x01 : 0x00) << 2) | (0x03 & dribble)) << 4));

            // vx
            if (vx < 0)
            {
                TXBuff[baseIndex + 4] |= 0x20;
            }
            TXBuff[baseIndex + 4] |= (byte)((vx_value_uint & 0x1F0) >> 4);
            TXBuff[baseIndex + 5] |= (byte)((vx_value_uint & 0x0F) << 4);

            // vy
            if (vy < 0)
            {
                TXBuff[baseIndex + 5] |= 0x08;
            }
            TXBuff[baseIndex + 5] |= (byte)((vy_value_uint & 0x1C0) >> 6);
            TXBuff[baseIndex + 6] |= (byte)((vy_value_uint & 0x3F) << 2);

            // vr
            if (vr < 0)
            {
                TXBuff[baseIndex + 6] |= 0x02;
            }
            TXBuff[baseIndex + 6] |= (byte)((w_value_uint & 0x100) >> 8);
            TXBuff[baseIndex + 7] |= (byte)(w_value_uint & 0xFF);

            // shoot power
            TXBuff[baseIndex + 8] = (byte)power;
        }

        private void CreateStartPacket(int frequency, out byte[] packet1, out byte[] packet2)
        {
            packet1 = new byte[Constants.TRANSMIT_PACKET_SIZE];
            packet1[0] = 0xFF;
            packet1[1] = 0xB0;
            packet1[2] = 0x01;
            packet1[3] = 0x02;
            packet1[4] = 0x03;
            packet1[Constants.TRANSMIT_PACKET_SIZE - 1] = ccrc.CCrc8.Calc(packet1, Constants.TRANSMIT_PACKET_SIZE - 1);

            packet2 = new byte[Constants.TRANSMIT_PACKET_SIZE];
            packet2[0] = 0xFF;
            packet2[1] = 0xB0;
            packet2[2] = 0x04;
            packet2[3] = 0x05;
            packet2[4] = 0x06;
            packet2[5] = (byte)(0x10 + frequency);
            packet2[Constants.TRANSMIT_PACKET_SIZE - 1] = ccrc.CCrc8.Calc(packet2, Constants.TRANSMIT_PACKET_SIZE - 1);
        }
    }



}
