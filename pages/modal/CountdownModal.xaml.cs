using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// CountdownModal.xaml 的交互逻辑
    /// </summary>
    public partial class CountdownModal : Window
    {
        private int m_nCountDownTimeSecond;
        DispatcherTimer m_CountDownDispatcherTimer;

        public delegate void CallBackExperimentStart();
        public CallBackExperimentStart callBackExperimentStart;
        public CountdownModal(int countdowntimesecond,CallBackExperimentStart callBackExperimentStart)
        {
            this.m_nCountDownTimeSecond = countdowntimesecond;
            this.callBackExperimentStart = callBackExperimentStart;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            CM_WM.Title = App.m_LangPackage.CM_WM;
            LB_LB_COUNTDOWNBULLETIN.Content = App.m_LangPackage.LB_LB_COUNTDOWNBULLETIN;
        }
            #region
            //窗口加载事件
            private void ME_COUNTDOWN_Loaded(object sender, RoutedEventArgs e)
        {
            m_CountDownDispatcherTimer = new DispatcherTimer();
            m_CountDownDispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            m_CountDownDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            m_CountDownDispatcherTimer.Start();
            ME_COUNTDOWN.Play();
        }
        //关闭窗口事件
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_nCountDownTimeSecond > 0)
            {
                m_CountDownDispatcherTimer.Stop(); //暂停倒计时
                ME_COUNTDOWN.Pause();
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_COUNTDOWN_EXIT_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = true;
                        m_CountDownDispatcherTimer.Start();
                        ME_COUNTDOWN.Play();
                        return;
                }
            }
        }
        //媒体播放结束循环事件
        private void ME_COUNTDOWN_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
        //定时器绑定事件
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            CreateCountDown();
        }
        #endregion

        //根据传入的秒数创建倒计时计时器
        private void CreateCountDown()
        {
            m_nCountDownTimeSecond--;
            if (m_nCountDownTimeSecond < 0)
            {
                m_CountDownDispatcherTimer.Stop();
                callBackExperimentStart();  //回调通知主窗口倒计时结束，可以立即执行
                this.Close();
            }
            Dispatcher.Invoke(new Action(() =>
            {
                LB_COUNTDOWNBULLETIN.Content = BaseUtils.GetHHMMSSRemainTime(m_nCountDownTimeSecond, "ENG");
            }));
        }
    }
}
