using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;
using System.Security.Cryptography;
using com.ccg.GeckoKit.api;
using System.Collections;
using Pharmacy.INST.CommonLib;

namespace Pharmacy.INST.DissolutionClient.common
{
    public static class BaseUtils
    {
        #region
        //限制0-9数字输入
        public static bool ControlInput0To9(KeyEventArgs e)
        {
            if (e.Key != Key.Delete && e.Key != Key.Back)
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
                {
                    return true; //e.Handled = true;
                }
            }
            return false;
        }
        //限制转速输入值
        public static bool ControlSpeedInput(int rpm)
        {
            if (rpm < 0 || rpm > 350) return false;
            return true;
        }
        //限制温度输入值
        public static bool ControlTempInput(double temp)
        {
            if (temp < 37 || temp > 50) return false;
            return true;
        }
        //限制开始时间输入值
        public static bool ControlStartTimeInput(int starttime)
        {
            if (starttime < 0 || starttime > 100000) return false;
            return true;
        }
        #endregion
        //检查模块权限
        public static bool CheckModulePrivelege(string moduleName)
        {
            //if (App.g_TSession.TTUser.RoleName.Equals(StaticParam.ROOT_ROLE)) return true;
            if (App.g_TSession.IsRootManager()) return true;
            bool bExsit = false;
            foreach (string str in App.g_TSession.TModuleList)
            {
                if (str.Equals(moduleName))
                {
                    bExsit = true;
                    break;
                }
            }
            return bExsit;
        }
        //检查功能权限 
        public static bool CheckFunctionPrivelege(string captionName, bool bAlarm)
        {
            if (App.g_TSession.IsRootManager()) return true;
            //if (App.g_TSession.TTUser.RoleName.Equals(StaticParam.ROOT_ROLE)) return true;
            bool bExsit = false;
            foreach (string function in App.g_TSession.TFunctionList)
            {
                if (function.Equals(captionName.Trim()))
                {
                    bExsit = true;
                    break;
                }
            }
            if (!bExsit && bAlarm)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_FUNC_AUTHORITY, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return bExsit;
        }
        //设置红灯
        public static void TurnOnRedLight(Image img)
        {
            if (img != null)
            {
                img.Dispatcher.Invoke(new Action(delegate
                {
                    UIOperator.LoadImage(App.g_ResourceDir + StaticParam.LIGHT_RED_FILE, img);
                }));
            }
        }
        //设置绿灯
        public static void TurnOnGreenLight(Image img)
        {
            if (img != null)
            {
                img.Dispatcher.Invoke(new Action(delegate
                {
                    UIOperator.LoadImage(App.g_ResourceDir + StaticParam.LIGHT_GREEN_FILE, img);
                }));
            }
        }
        #region GDI+ 
        public static ImageBrush GDIPlusBrush(string imgUrl)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(imgUrl, UriKind.RelativeOrAbsolute));
            return imageBrush;
        }

        //全局获取当前日期时间字符串
        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString(String.Format("{0} {1}", App.m_strDateFormat, App.m_strTimeFormat));
        }
        //全局获取dt日期字符串
        public static string GetCurrentDate(DateTime dt)
        {
            return dt.ToString(String.Format("{0}", App.m_strDateFormat));
        }
        //全局获取dt日期时间字符串
        public static string GetCurrentDateTime(DateTime dt)
        {
            return dt.ToString(String.Format("{0} {1}", App.m_strDateFormat, App.m_strTimeFormat));
        }
        //全局获取当前日期时间字符串
        public static string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString(String.Format("{0}{1}", "yyyyMMdd", "HHmmss"));
        }
        //全局获取dt日期时间字符串
        public static string GetDateTimeString(DateTime dt)
        {
            return dt.ToString(String.Format("{0}{1}", "yyyyMMdd", "HHmmss"));
        }
        //全局获取当前日期字符串
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(String.Format("{0}", App.m_strDateFormat));
        }
        //全局获取指定DT的日期部分字符串
        public static string GetDate(DateTime dt)
        {
            return dt.ToString(String.Format("{0}", App.m_strDateFormat));
        }
        //全局获取当前时间字符串
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString(String.Format("{0}", App.m_strTimeFormat));
        }
        //全局获取文件日期时间字符串
        public static string GetFileCurrentDateTime()
        {
            return DateTime.Now.ToString(String.Format("{0}", App.m_strFileDateTimeFormat));
        }
        //根据给定的时间全局获取文件日期时间字符串
        public static string GetFileCurrentDateTime(DateTime dt)
        {
            return dt.ToString(String.Format("{0}", App.m_strFileDateTimeFormat));
        }

        //发送控制指令并确认，单线程
        public static bool SendYKDatagram(string strDatagrame)
        {
            SendHighPriorityDatagram();
            byte[] sendbuf;
            byte[] recvbuf = new byte[StaticParam.BUFFERSIZE];
            sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagrame);
            
            int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagrame);
            iSerialCom.__SendData(sendbuf, len);
            App.WriteSystemLog("下发：" + strDatagrame);

            Thread.Sleep(App.m_nRecvSendInterval);

            int size = iSerialCom.__RecvData(recvbuf);
            if (size == 0 || len != size) return false;
            bool bSuccess = true;

            for (int i = 0; i < len; i++)
            {
                if (sendbuf[i] != recvbuf[i])
                {
                    bSuccess = false;
                    return false;
                }
            }
            string strRecv = CommonLib.CommonLib.ByteToHexStr(recvbuf, size);
            App.WriteSystemLog("收到：" + strRecv);
            return bSuccess;
        }
        //发送控制指令无需确认，单线程
        public static bool SendYKDatagramNoConfirm(string strDatagrame)
        {
            SendHighPriorityDatagram();
            byte[] sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagrame);
            int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagrame);
            return iSerialCom.__SendData(sendbuf, len);
        }
        //发送数据指令并确认，单线程
        public static byte[] RecvStatusDatagram(string strDatagrame)
        {
            SendHighPriorityDatagram();
            byte[] sendbuf = new byte[StaticParam.BUFFERSIZE];
            byte[] recvbuf = new byte[StaticParam.BUFFERSIZE];
            sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagrame);
            int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagrame);
            iSerialCom.__SendData(sendbuf, len);
            App.WriteSystemLog("下发：" + strDatagrame);
            Thread.Sleep(App.m_nRecvSendInterval);
            int size = iSerialCom.__RecvData(recvbuf);
            if (size == 0) return null;
            if (recvbuf[0] == 0xA5 && recvbuf[size - 1] == 0x5A)
            {
                string strRecv = CommonLib.CommonLib.ByteToHexStr(recvbuf, size);
                App.WriteSystemLog("收到：" + strRecv);
                return recvbuf;
            }
            return null;
        }
        //收发温度数据指令，单线程
        public static byte[] SendRecvTempDatagram(string strDatagrame, ref int size)
        {
            SendHighPriorityDatagram();
            byte[] sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagrame);
            byte[] recvbuf = new byte[StaticParam.BUFFERSIZE];
            int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagrame);
            iSerialCom.__SendData(sendbuf, len);
            Thread.Sleep(App.m_nTempSendRecvInterval);
            size = iSerialCom.__RecvData(recvbuf);
            if (size == 0) return null;
            return recvbuf;
        }
        //高优先级发送
        public static void SendHighPriorityDatagram()
        {
            while (App.m_SpeedSwitchQueue.Count > 0)
            {
                byte[] sendbuf;
                string strDatagram = (string)App.m_SpeedSwitchQueue.Dequeue();
                sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagram);
                int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagram);
                iSerialCom.__SendData(sendbuf, len);
                App.WriteSystemLog("优先下发：" + strDatagram);
                Thread.Sleep(1000);
            }
        }
        //根据传入的秒数得到时分秒倒计时
        public static string GetHHMMSSRemainTime(int nCountDownTimeSecond)
        {
            int nHour = nCountDownTimeSecond / 3600;
            int nMinute = (nCountDownTimeSecond - nHour * 3600) / 60;
            int nSecond = (nCountDownTimeSecond - nHour * 3600 - nMinute * 60) % 60;
            string strTime = "{0} 时 {1} 分 {2} 秒";
            return String.Format(strTime, nHour.ToString(), nMinute.ToString(), nSecond.ToString());
        }

        //根据传入的秒数得到时分秒倒计时
        public static string GetHHMMSSRemainTime(int nCountDownTimeSecond, string Lang)
        {
            int nHour = nCountDownTimeSecond / 3600;
            int nMinute = (nCountDownTimeSecond - nHour * 3600) / 60;
            int nSecond = (nCountDownTimeSecond - nHour * 3600 - nMinute * 60) % 60;
            string strTime = "{0} 时 {1} 分 {2} 秒";
            if (Lang.ToUpper().Equals("ENG"))
                strTime = "{0} : {1} : {2} ";
            return String.Format(strTime, nHour.ToString(), nMinute.ToString(), nSecond.ToString());
        }
        //判断目录是否存在，不存在则创建
        public static void CheckDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        //生成条形码
        public static System.Drawing.Bitmap CreateBarCode(String strBarCodeInfo, int width, int height)
        {
            EncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions
            {
                Width = width,
                Height = height
            };
            BarcodeWriter write = new BarcodeWriter();
            write.Format = BarcodeFormat.CODE_39;
            write.Options = options;
            return write.Write(strBarCodeInfo);
        }
        //获取数据库文件信息
        public static string GetFileSizeInfo(string filepath)
        {
            System.IO.FileInfo fileInfo = null;
            try
            {
                fileInfo = new System.IO.FileInfo(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // 其他处理异常的代码
            }
            // 如果文件存在
            if (fileInfo != null && fileInfo.Exists)
            {
                if (fileInfo.Length > 1024 * 1024)
                    return string.Format("{0} MB", (((double)fileInfo.Length) / 1024 / 1024).ToString("F"));
                if (fileInfo.Length > 1024 && fileInfo.Length < 1024 * 1024)
                    return string.Format("{0} KB", fileInfo.Length / 1024);
            }
            return string.Format("{0} B", fileInfo.Length);
        }
        //获取report目录下实验报告的数量
        public static string GetReportInfo()
        {
            string mFilePath = string.Format(App.g_AppDirectory + "{0}", "report");
            string[] fileNames = Directory.GetFiles(mFilePath);
            return fileNames.Length.ToString();
        }

        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// MD5解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();

            return Encoding.Default.GetString(ms.ToArray());
        }
        public static string DateLimit(string strDecrypt)
        {
            string[] arr = WeiYiJieMiGuid(strDecrypt).Split('_');
            DateTime dt = DateTime.Parse(arr[0]);
            dt = dt.AddDays(double.Parse(arr[1].ToString()));
            return BaseUtils.GetDate(dt);
        }

        /// <summary>
        /// 是否超期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsOverTime(string strSerialNum)
        {
            Int32 DayCount;
            Int32 OutDay;
            DateTime StartDate;
            try
            {
                var _str = WeiYiJieMiGuid(strSerialNum);
                if (DateTime.TryParse(_str.Split(new char[] { '_' })[0], out StartDate))
                {
                    DayCount = Convert.ToInt32(_str.Split(new char[] { '_' })[1]);
                    OutDay = Convert.ToInt32(_str.Split(new char[] { '_' })[2]);
                    if (DayCount < OutDay)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                App.WriteSystemLog(e.ToString());
                return true;
            }
            if (Convert.ToInt64(StartDate.ToString("yyyyMMdd")) > Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")))
            {
                return true;
            }
            var _days = Math.Ceiling(DateTime.Now.Subtract(StartDate).TotalDays);
            if (_days < 0 || _days > DayCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 唯一加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WeiYiJiaMiGuid(string str)
        {
            string keys = "BJ-BY-KY";
            return MD5Encrypt(str, keys) + "=" + keys;
        }
        /// <summary>
        /// 唯一解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WeiYiJieMiGuid(string str)
        {
            string[] pwa = str.Split(new char[] { '=' });
            return MD5Decrypt(pwa[0], pwa[1]);
        }
        #endregion
    }
}
