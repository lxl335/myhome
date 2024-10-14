using System;
using System.Runtime.InteropServices;
using Pharmacy.INST.DissolutionClient.pages;

namespace Pharmacy.INST.DissolutionClient.common
{
    public class DatagramParse
    {
        public static MainWindow m_MainWindow;
        public static DeviceInfoView m_DeviceInfoView;
        public DatagramParse() { }
        public DatagramParse(MainWindow mainWindow)
        {
            m_MainWindow = mainWindow;
        }
        public DatagramParse(DeviceInfoView deviceInfoView)
        {
            m_DeviceInfoView = deviceInfoView;
        }
        //底层串口回调解析
        public void Parse(IntPtr pData, int nLen)
        {
            if (nLen < 4) return;
            byte[] data = new byte[nLen];
            Marshal.Copy(pData, data, 0, nLen);
            byte cmd = data[0x02];
            switch (cmd)
            {
                case 0x1B:
                    {
                        short t1 = (short)((data[0x03] << 8) + data[0x04]);
                        short t2 = (short)((data[0x05] << 8) + data[0x06]);
                        m_MainWindow.RefreshBoxTemp(t1, t2);
                    }
                    break;
                case 0x1E:
                    {
                        short[] t = new short[0x0D];
                        for (int i = 0; i < 0x0D; i++)
                        {
                            t[i] = (short)((data[0x03 + i * 2] << 8) + data[0x03 + i * 2 + 1]);
                        }
                        m_MainWindow.RefreshCupTemp(t);
                    }
                    break;
            }
        }
        //解析温度数据,回调刷新显示
        public void Parse(Byte[] data, int nLen, dynamic obj)
        {
            if (nLen < 4) return;
            byte cmd = data[0x02];
            switch (cmd)
            {
                case 0x19:
                    {
                        short t1 = (short)((data[0x03] << 8) + data[0x04]);
                        short t2 = (short)((data[0x05] << 8) + data[0x06]);
                        obj.RefreshBoxTempCali(t1, t2);
                    }
                    break;
                case 0x1B:
                    {
                        short t1 = (short)((data[0x03] << 8) + data[0x04]);
                        short t2 = (short)((data[0x05] << 8) + data[0x06]);
                        obj.RefreshBoxTemp(t1, t2);
                    }
                    break;
                case 0x1E:
                    {
                        short[] t = new short[0x0D];
                        for (int i = 0; i < 0x0D; i++)
                        {
                            t[i] = (short)((data[0x03 + i * 2] << 8) + data[0x03 + i * 2 + 1]);
                        }
                        obj.RefreshCupTemp(t);
                    }
                    break;
                case 0x22:
                    {
                        short[] t = new short[0x0D];
                        for (int i = 0; i < 0x0D; i++)
                        {
                            t[i] = (short)((data[0x03 + i * 2] << 8) + data[0x03 + i * 2 + 1]);
                        }
                        obj.RefreshCupTempCali(t);
                    }
                    break;
            }
        }
        //解析温度数据
        public void Parse(Byte[] data, int nLen,ref short t1,ref short t2,ref short[] t)
        {
            if (nLen < 4) return;
            byte cmd = data[0x02];
            switch (cmd)
            {
                case 0x1B:
                    {
                        t1 = (short)((data[0x03] << 8) + data[0x04]);
                        t2 = (short)((data[0x05] << 8) + data[0x06]);
                    }
                    break;
                case 0x1E:
                    {
                        for (int i = 0; i < t.Length; i++)
                        {
                            t[i] = (short)((data[0x03 + i * 2] << 8) + data[0x03 + i * 2 + 1]);
                        }
                    }
                    break;
               
            }
        }
    }
}
