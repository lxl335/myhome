using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.entities;
using com.ccg.GeckoKit.api;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// WashingTimesModal.xaml 的交互逻辑
    /// </summary>
    public partial class WashingTimesModal : Window
    {
        public Thread m_WashingThread;                           //清洗线程句柄

        public WashingTimesModal()
        {
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            WTM_MW.Title = App.m_LangPackage.WTM_MW;
            LB_WTM_TB_WASHINGTIMES.Content = App.m_LangPackage.LB_WTM_TB_WASHINGTIMES;
            LB_WASHINGTIME_UNIT.Content = App.m_LangPackage.LB_WASHINGTIME_UNIT;
            WTM_NORMALTIMES_GROUPBOX.Header = App.m_LangPackage.WTM_NORMALTIMES_GROUPBOX;
            WTM_BTN_TIMES_1.Content = App.m_LangPackage.WTM_BTN_TIMES_1;
            WTM_BTN_TIMES_2.Content = App.m_LangPackage.WTM_BTN_TIMES_2;
            WTM_BTN_TIMES_3.Content = App.m_LangPackage.WTM_BTN_TIMES_3;
            WTM_BTN_TIMES_5.Content = App.m_LangPackage.WTM_BTN_TIMES_5;
            WTM_WASHING_STATUS.Content = App.m_LangPackage.WTM_WASHING_STATUS;
            BTN_CONFIRM.Content = App.m_LangPackage.BTN_CONFIRM;
            BTN_CANCEL.Content = App.m_LangPackage.BTN_CANCEL;

        }
        private void WTM_BTN_TIMES_1_Click(object sender, RoutedEventArgs e)
        {
            WTM_TB_WASHINGTIMES.Text = "1";
        }

        private void WTM_BTN_TIMES_2_Click(object sender, RoutedEventArgs e)
        {
            WTM_TB_WASHINGTIMES.Text = "2";
        }

        private void WTM_BTN_TIMES_3_Click(object sender, RoutedEventArgs e)
        {
            WTM_TB_WASHINGTIMES.Text = "3";
        }

        private void WTM_BTN_TIMES_5_Click(object sender, RoutedEventArgs e)
        {
            WTM_TB_WASHINGTIMES.Text = "5";
        }

        private void WTM_TB_WASHINGTIMES_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = BaseUtils.ControlInput0To9(e);
        }

        private void BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_WASHING_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (mBoxResult)
            {
                case MessageBoxResult.Yes:
                    {
                        try
                        {
                            int nWashingTimes = int.Parse(WTM_TB_WASHINGTIMES.Text.ToString());
                            m_WashingThread = new Thread(new ParameterizedThreadStart(WashingThread));
                            m_WashingThread.Start(nWashingTimes);
                            WTM_WASHINGICON.Visibility = Visibility.Visible;
                            WTM_WASHING_STATUS.Content = App.m_LangPackage.TIP_WASHING_DO_NOT_EXIT;
                            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(104),App.GetBehaviorRemark(104)));
                            //Close();
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(App.m_LangPackage.TIP_INPUT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            App.WriteSystemLog(e1.Message.ToString());
                        }
                        return;
                    }
            }
        }

        private void BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UIOperator.LoadImage(App.g_ResourceDir + "washing_waitting.gif", WTM_WASHINGICON);
        }
        public void WashingThread(object oPara)
        {
            int nTimes = (int)oPara;
            App.g_bWashing = true;
            ExpStepAction.SuspendTempCollect();
            for (int i = 0; i < nTimes; i++)
            {
                //开始清洗
                if (ExpStepAction.SampleFrameValveInitialize())
                {
                    ExpStepAction.ValveRotation(null, 3, "补液位");
                    ExpStepAction.InjectorPump(null, 25, false);
                    ExpStepAction.ValveRotation(null, 3, "取样位");
                    ExpStepAction.InjectDrain(null, 25, false);

                    ExpStepAction.ValveRotation(null, 1, "补液位");
                    ExpStepAction.InjectorPump(null, 25, false);
                    ExpStepAction.ValveRotation(null, 2, "循环位");
                    ExpStepAction.InjectDrain(null, 25, false);

                    ExpStepAction.ValveRotation(null, 2, "补液位");
                    ExpStepAction.InjectorPump(null, 25, false);
                    ExpStepAction.ValveRotation(null, 1, "出液位");
                    if (ExpStepAction.CollectFrameSampleOrganInitialize())
                    {
                        ExpStepAction.CollectFrameSampleOrganDown(null);
                        ExpStepAction.InjectDrain(null, 25, false);
                        ExpStepAction.CollectFrameSampleOrganReset(null);
                    }
                    App.Pause();
                }
            }
            UIOperator.SetImageVisibility(WTM_WASHINGICON, Visibility.Hidden);
            UIOperator.SetLabelContent(WTM_WASHING_STATUS, App.m_LangPackage.TIP_WASHING_OVER);
            App.g_bWashing = false;
            ExpStepAction.ResumeTempCollect();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.g_bWashing)
            {
                MessageBox.Show(App.m_LangPackage.TIP_WASHING_NOT_EXIT, App.m_LangPackage.ERROR,MessageBoxButton.OK,MessageBoxImage.Error);
                e.Cancel = true;
            }
        }
    }
}
