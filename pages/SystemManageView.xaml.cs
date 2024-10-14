using System;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.INST.DissolutionClient.common;
using System.Printing;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;


namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// SystemManageView.xaml 的交互逻辑
    /// </summary>
    public partial class SystemManageView : Page
    {
        public SystemManageView()
        {
            InitializeComponent();
            InitializeInterface();
            InitializeSystemParam();
        }
        public void InitializeInterface()
        {
            SMV_ACCOUNT_MANAGE.Header = App.m_LangPackage.SMV_ACCOUNT_MANAGE;
            SMV_PRIVILEGE_MANAGE.Header = App.m_LangPackage.SMV_PRIVILEGE_MANAGE;
            SMV_SYSTEMSETTING_MANAGE.Header = App.m_LangPackage.SMV_SYSTEMSETTING_MANAGE;
            SMV_RETRIVER_MANAGE.Header = App.m_LangPackage.SMV_RETRIVER_MANAGE;
            SMV_SYSTEMSETTING_GROUPBOX.Header = App.m_LangPackage.SMV_SYSTEMSETTING_GROUPBOX;
            SMV_MAINBOARD_GROUPBOX.Header = App.m_LangPackage.SMV_MAINBOARD_GROUPBOX;
            LB_SMV_COMBO_COMPORT.Content = App.m_LangPackage.LB_SMV_COMBO_COMPORT;
            LB_SMV_COMBO_COMBAUD.Content = App.m_LangPackage.LB_SMV_COMBO_COMBAUD;
            SMV_PRINTER_GROUPBOX.Header = App.m_LangPackage.SMV_PRINTER_GROUPBOX;
            LB_SMV_COMBO_PRINTER.Content = App.m_LangPackage.LB_SMV_COMBO_PRINTER;
            LB_SMV_COMBO_PRINTERPORT.Content = App.m_LangPackage.LB_SMV_COMBO_PRINTERPORT;
            LB_SMV_COMBO_PRINTCOMBAUD.Content = App.m_LangPackage.LB_SMV_COMBO_PRINTCOMBAUD;
            SMV_CKB_WHOLEMODE_ENABLE.Content = App.m_LangPackage.SMV_CKB_WHOLEMODE_ENABLE;
            SMV_CKB_PUTPILL_ENABLE.Content = App.m_LangPackage.SMV_CKB_PUTPILL_ENABLE;
            SMV_CKB_SAMPLEPOINT_ENABLE.Content = App.m_LangPackage.SMV_CKB_SAMPLEPOINT_ENABLE;
            SMV_CKB_CUPTEMP_ENABLE.Content = App.m_LangPackage.SMV_CKB_CUPTEMP_ENABLE;
            SMV_CKB_BEEP_ENABLE.Content = App.m_LangPackage.SMV_CKB_BEEP_ENABLE;
            SMV_CKB_DOUBLEMOTOR.Content = App.m_LangPackage.SMV_CKB_DOUBLEMOTOR;
            SMV_CKB_AUTOHEAT_ENABLE.Content = App.m_LangPackage.SMV_CKB_AUTOHEAT_ENABLE;
            SMV_CKB_TEMP_ENABLE.Content = App.m_LangPackage.SMV_CKB_TEMP_ENABLE;
            LB_SMV_TB_TEMPOFFSET.Content = App.m_LangPackage.LB_SMV_TB_TEMPOFFSET;
            LB_SMV_TB_DEFHEATTEMP.Content = App.m_LangPackage.LB_SMV_TB_DEFHEATTEMP;
            LB_SMV_TB_PWDVALIDDAYS.Content = App.m_LangPackage.LB_SMV_TB_PWDVALIDDAYS;
            LB_SMV_TB_ROTATERATE.Content = App.m_LangPackage.LB_SMV_TB_ROTATERATE;
            LB_SMV_TB_FIRSTFILTER_OFFSET.Content = App.m_LangPackage.LB_SMV_TB_FIRSTFILTER_OFFSET;
            SMV_BTN_SAVESETTING.Content = App.m_LangPackage.SMV_BTN_SAVESETTING;
            LB_SMV_TB_SPEEDOFFSET.Content = App.m_LangPackage.LB_SMV_TB_SPEEDOFFSET;
        }
        //初始化系统参数窗口
        private void InitializeSystemParam()
        {
            //加载串口下拉框
            UIOperator.GetSerialPortList(SMV_COMBO_COMPORT);
            SMV_COMBO_COMPORT.Text = App.m_strCom;
            //加载波特率下拉框
            UIOperator.ComboBoxBinder(SMV_COMBO_COMBAUD, StaticParam.BaudArr);
            SMV_COMBO_COMBAUD.Text = App.m_nBps.ToString();
            //加载打印机波特率下拉框
            UIOperator.ComboBoxBinder(SMV_COMBO_PRINTCOMBAUD, StaticParam.BaudArr);
            SMV_COMBO_PRINTCOMBAUD.Text = App.m_nPrintBps.ToString();
            //初始化打印机
            string strPrintFullName;
            var printers = new LocalPrintServer().GetPrintQueues();//从本地计算机中获取所有打印机对象(PrintQueue)
            foreach (PrintQueue printer in printers)
            {
                strPrintFullName = printer.FullName.ToString();
                SMV_COMBO_PRINTER.Items.Add(strPrintFullName);
            }
            if (!UIOperator.GetParallelPortList(SMV_COMBO_PRINTERPORT))
            {
                string strLpt;
                for (int i = 1; i < 3; i++)
                {
                    strLpt = "LPT" + i.ToString();
                    SMV_COMBO_PRINTERPORT.Items.Add(strLpt);
                }
            }
            SMV_COMBO_COMPORT.Text = App.m_strCom;
            SMV_COMBO_COMBAUD.Text = App.m_nBps.ToString();
            SMV_COMBO_PRINTER.Text = App.m_strPrinter;
            SMV_COMBO_PRINTERPORT.Text = App.m_strLpt;
            if (App.m_bAutoStart)
                SMV_CKB_AUTOSTART.IsChecked = true;
            if (App.m_bSampleBeep)
                SMV_CKB_BEEP_ENABLE.IsChecked = true;
            if (App.m_bWholeModeEnable)
                SMV_CKB_WHOLEMODE_ENABLE.IsChecked = true;
            if (App.m_bPutPillEnable)
                SMV_CKB_PUTPILL_ENABLE.IsChecked = true;
            if (App.m_bSamplePointEnable)
                SMV_CKB_SAMPLEPOINT_ENABLE.IsChecked = true;
            if (App.m_bCupTempEnable)
                SMV_CKB_CUPTEMP_ENABLE.IsChecked = true;
            if (App.m_bDoubleMotor)
                SMV_CKB_DOUBLEMOTOR.IsChecked = true;
            if (App.m_bAutoHeatting)
                SMV_CKB_AUTOHEAT_ENABLE.IsChecked = true;
            if (App.m_bTempSurvey)
                SMV_CKB_TEMP_ENABLE.IsChecked = true;

            SMV_TB_TEMPOFFSET.Text = App.m_dlTempOffset.ToString();
            SMV_TB_DEFHEATTEMP.Text = App.m_dlDefaultHeatTemp.ToString();
            SMV_TB_PWDVALIDDAYS.Text = App.m_nPwdValitionDays.ToString();
            SMV_TB_ROTATERATE.Text = App.m_nRotateRate.ToString();
            SMV_TB_FIRSTFILTER_OFFSET.Text = App.m_dlFirstFilterOffset.ToString();
            SMV_TB_SPEEDOFFSET.Text = App.m_nSpeedOffset.ToString();

        }
        #region 窗口事件
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(11), App.GetBehaviorRemark(11)));
        }
        //保存 系统参数 按钮 事件
        private void SMV_BTN_SAVESETTING_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(21), true))
                SystemSetting();
        }
        #endregion
        //系统设置保存
        private void SystemSetting()
        {
            string strCom;
            string strBps;
            string strPrinter;
            string strLPT;
            string strAutoStart;
            string strSampleBeepEnable;
            string strWholeModeEanbel;
            string strPutPillEnable;
            string strSamplePointEnable;
            string strCupTempEnable;
            string strDoubleMotor;
            string strAutoHeatting;
            string strTempEnable;
            string strTempOffset;
            string strPwdValitionDays;
            string strRotateRate;
            string strFirstFilterOffset;
            string strSpeedOffset;
            try
            {
                strCom = SMV_COMBO_COMPORT.SelectedItem == null ? string.Empty : SMV_COMBO_COMPORT.SelectedItem.ToString();
                strBps = SMV_COMBO_COMBAUD.Text.ToString();

                strPrinter = SMV_COMBO_PRINTERPORT.SelectedItem == null ? string.Empty : SMV_COMBO_PRINTERPORT.SelectedItem.ToString();
                strLPT = SMV_COMBO_PRINTERPORT.SelectedItem.ToString();

                strAutoStart = (SMV_CKB_AUTOSTART.IsChecked == true) ? "True" : "False";
                strSampleBeepEnable = (SMV_CKB_BEEP_ENABLE.IsChecked == true) ? "True" : "False";
                strWholeModeEanbel = (SMV_CKB_WHOLEMODE_ENABLE.IsChecked == true) ? "True" : "False";
                strPutPillEnable = (SMV_CKB_PUTPILL_ENABLE.IsChecked == true) ? "True" : "False";
                strSamplePointEnable = (SMV_CKB_SAMPLEPOINT_ENABLE.IsChecked == true) ? "True" : "False";
                strCupTempEnable = (SMV_CKB_CUPTEMP_ENABLE.IsChecked == true) ? "True" : "False";
                strDoubleMotor = (SMV_CKB_DOUBLEMOTOR.IsChecked == true) ? "True" : "False";
                strAutoHeatting = (SMV_CKB_AUTOHEAT_ENABLE.IsChecked == true) ? "True" : "False";
                strTempEnable = (SMV_CKB_TEMP_ENABLE.IsChecked == true) ? "True" : "False";
                strTempOffset = SMV_TB_TEMPOFFSET.Text.Trim();
                strPwdValitionDays = SMV_TB_PWDVALIDDAYS.Text.Trim();
                strRotateRate = SMV_TB_ROTATERATE.Text.Trim();
                strFirstFilterOffset = SMV_TB_FIRSTFILTER_OFFSET.Text.Trim();
                strSpeedOffset = SMV_TB_SPEEDOFFSET.Text.Trim();

                if (string.IsNullOrEmpty(strTempOffset) 
                    || string.IsNullOrEmpty(strRotateRate)
                    || string.IsNullOrEmpty(strSpeedOffset))
                {
                    MessageBox.Show(App.m_LangPackage.TIP_PROFILE_EMPTY, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_PROFILE_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            App.m_ProfileUtils.WriteIniData("Device", "SerialsPort", strCom, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("Device", "Baud", strBps, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("Printer", "Printer", strPrinter, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("Printer", "Lpt", strLPT, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "AutoStart", strAutoStart, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "SampleBeep", strSampleBeepEnable, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "WholeModeEnable", strWholeModeEanbel, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "PutPillEnable", strPutPillEnable, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "SamplePointEnable", strSamplePointEnable, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "CupTempEnable", strCupTempEnable, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "DoubleMotor", strDoubleMotor, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "AutoHeatting", strAutoHeatting, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "TempSurvey", strTempEnable, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "TempOffset", strTempOffset, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "PwdValitionDays", strPwdValitionDays, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "RotateRate", strRotateRate, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "FirstFilterOffset", strFirstFilterOffset, App.g_IniSysFileName);
            App.m_ProfileUtils.WriteIniData("SysPara", "SpeedOffset", strSpeedOffset, App.g_IniSysFileName);

            MessageBox.Show(App.m_LangPackage.TIP_SAVE_RESTART, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}