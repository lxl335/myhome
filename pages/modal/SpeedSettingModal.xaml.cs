using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// SpeedSettingModal.xaml 的交互逻辑
    /// </summary>
    public partial class SpeedSettingModal : Window
    {
        public delegate void CallBackSpeedSetting(SpeedModule speedModule);
        public CallBackSpeedSetting callBackSpeedSetting;
        public SpeedModule m_SpeedModule;
        public SpeedSettingModal(CallBackSpeedSetting callBackSpeedSetting, SpeedModule speedModule)
        {
            this.callBackSpeedSetting = callBackSpeedSetting;
            m_SpeedModule = speedModule;
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        //初始化窗口组件参数
        private void InitializeComponentEx()
        {
            //加载转速模式下拉框
            UIOperator.ComboBoxBinder(Combo_Speed_Mode, StaticParam.Speed_Mode_Arr);
            //加载电机模式下拉框
            UIOperator.ComboBoxBinder(Combo_Electricmotor_Mode, StaticParam.Electricmotor_Mode_Arr);
            if (!App.m_bDoubleMotor) Combo_Electricmotor_Mode.Items.RemoveAt(1); //单电机模式
        }
        public void InitializeInterface()
        {
            SSM_MW.Title = App.m_LangPackage.SSM_MW;
            LB_Combo_Speed_Mode.Content = App.m_LangPackage.LB_Combo_Speed_Mode;
            LB_Combo_Electricmotor_Mode.Content = App.m_LangPackage.LB_Combo_Electricmotor_Mode;
            LB_TB_Front_Speed_1.Content = App.m_LangPackage.LB_TB_Front_Speed_1;
            LB_TB_Front_StartTime_1.Content = App.m_LangPackage.LB_TB_Front_StartTime_1;
            LB_TB_Front_Speed_2.Content = App.m_LangPackage.LB_TB_Front_Speed_2;
            LB_TB_Front_StartTime_2.Content = App.m_LangPackage.LB_TB_Front_StartTime_2;
            LB_TB_Back_Speed_1.Content = App.m_LangPackage.LB_TB_Back_Speed_1;
            LB_TB_Back_StartTime_1.Content = App.m_LangPackage.LB_TB_Back_StartTime_1;
            LB_TB_Back_Speed_2.Content = App.m_LangPackage.LB_TB_Back_Speed_2;
            LB_TB_Back_StartTime_2.Content = App.m_LangPackage.LB_TB_Back_StartTime_2;
            Btn_Confirm.Content = App.m_LangPackage.Btn_Confirm;
            Btn_Close.Content = App.m_LangPackage.Btn_Close;
        }
        #region 
        //窗口事件群
        //窗口加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSpeedAndStartTime();
        }
        //按钮事件，确认
        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            //一级判断
            if (string.IsNullOrEmpty(Combo_Speed_Mode.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_SELECT_SPEED_MODE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(Combo_Electricmotor_Mode.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_SELECT_ELECTRICMOTOR_MODE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string strSpeedMode = Combo_Speed_Mode.Text.Trim();
            string strElectricmotorMode = Combo_Electricmotor_Mode.Text.Trim();

            if (string.IsNullOrEmpty(TB_Front_Speed_1.Text)
                || string.IsNullOrEmpty(TB_Front_StartTime_1.Text)
                || string.IsNullOrEmpty(TB_Front_Speed_2.Text)
                || string.IsNullOrEmpty(TB_Front_StartTime_2.Text)
                || string.IsNullOrEmpty(TB_Back_Speed_1.Text)
                || string.IsNullOrEmpty(TB_Back_StartTime_1.Text)
                || string.IsNullOrEmpty(TB_Back_Speed_2.Text)
                || string.IsNullOrEmpty(TB_Back_StartTime_2.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SPEED_STARTTIME_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //二级判断
            int FrontRowSpeed_1;
            int FrontRowSpeed_2;
            int FrontRow_StartTime_1;
            int FrontRow_StartTime_2;
            int BackRowSpeed_1;
            int BackRowSpeed_2;
            int BackRow_StartTime_1;
            int BackRow_StartTime_2;

            try { FrontRowSpeed_1 = int.Parse(TB_Front_Speed_1.Text.Trim()); } catch (Exception e1) { FrontRowSpeed_1 = 0; Console.WriteLine(e1.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { FrontRowSpeed_2 = int.Parse(TB_Front_Speed_2.Text.Trim()); } catch (Exception e2) { FrontRowSpeed_2 = 0; Console.WriteLine(e2.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { FrontRow_StartTime_1 = int.Parse(TB_Front_StartTime_1.Text.Trim()); } catch (Exception e3) { FrontRow_StartTime_1 = 0; Console.WriteLine(e3.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { FrontRow_StartTime_2 = int.Parse(TB_Front_StartTime_2.Text.Trim()); } catch (Exception e4) { FrontRow_StartTime_2 = 0; Console.WriteLine(e4.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { BackRowSpeed_1 = int.Parse(TB_Back_Speed_1.Text.Trim()); } catch (Exception e5) { BackRowSpeed_1 = 0; Console.WriteLine(e5.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { BackRowSpeed_2 = int.Parse(TB_Back_Speed_2.Text.Trim()); } catch (Exception e6) { BackRowSpeed_2 = 0; Console.WriteLine(e6.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { BackRow_StartTime_1 = int.Parse(TB_Back_StartTime_1.Text.Trim()); } catch (Exception e7) { BackRow_StartTime_1 = 0; Console.WriteLine(e7.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }
            try { BackRow_StartTime_2 = int.Parse(TB_Back_StartTime_2.Text.Trim()); } catch (Exception e8) { BackRow_StartTime_2 = 0; Console.WriteLine(e8.ToString()); MessageBox.Show(App.m_LangPackage.TIP_SSM_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); }

            if (strSpeedMode.Equals(StaticParam.Speed_Mode_Arr[0])) //统一转速
            {
                if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[0])) //单电机
                {
                    if (FrontRowSpeed_1 <= 0)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_FR_SPEED1_GREATER_THEN_ONE, App.m_LangPackage.ERROR, MessageBoxButton.OK,MessageBoxImage.Error);
                        return;
                    }
                }
                else //双电机
                {
                    if (FrontRowSpeed_1 <= 0 || BackRowSpeed_1 <=0)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_FR_SPEED1_AND_BK_SPEED1_GREATER_THEN_ONE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            else //梯度转速
            {
                if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[0])
                   ) //单电机
                {
                    if (FrontRowSpeed_1 <= 0 || FrontRowSpeed_2 <= 0)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_FR_SPEED1_AND_FR_SPEED2_GREATER_THEN_ONE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (FrontRow_StartTime_1 >= FrontRow_StartTime_2) //前排启动时间比较
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_FR_STARTTIME2_GREATER_THEN_FR_STARTTIME1, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else//双电机
                {
                    if (FrontRowSpeed_1 <= 0 || FrontRowSpeed_2 <= 0 || BackRowSpeed_1 <= 0 || BackRowSpeed_2 <= 0)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_ALL_SPEED_GREATER_THEN_ZERO, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (FrontRow_StartTime_1 >= FrontRow_StartTime_2 || BackRow_StartTime_1 >= BackRow_StartTime_2)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_FR_BK_SPEED2_GRATER_THEN_SPEED1, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (FrontRowSpeed_1 > StaticParam.ROTATION_MAX_SPEED
                || FrontRowSpeed_2 > StaticParam.ROTATION_MAX_SPEED
                || BackRowSpeed_1 > StaticParam.ROTATION_MAX_SPEED
                || BackRowSpeed_2 > StaticParam.ROTATION_MAX_SPEED)
            {
                MessageBox.Show(string.Format(App.m_LangPackage.TIP_SPEED_LIMITIED, StaticParam.ROTATION_MAX_SPEED), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SpeedModule speedModule = new SpeedModule();
            speedModule.SpeedMode = strSpeedMode;
            speedModule.ElectricmotorMmode = strElectricmotorMode;
            speedModule.FrontRowSpeed_1 = FrontRowSpeed_1;
            speedModule.FrontRowSpeed_2 = FrontRowSpeed_2;
            speedModule.FrontRowStartTime_1 = FrontRow_StartTime_1;
            speedModule.FrontRowStartTime_2 = FrontRow_StartTime_2;
            speedModule.BackRowSpeed_1 = BackRowSpeed_1;
            speedModule.BackRowSpeed_2 = BackRowSpeed_2;
            speedModule.BackRowStartTime_1 = BackRow_StartTime_1;
            speedModule.BackRowStartTime_2 = BackRow_StartTime_2;

            SaveSpeedAndStartTime(speedModule);

            this.Close();
        }
        //按钮事件，返回
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //下拉框选择事件，转速模式改变
        private void Combo_Speed_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (item == null) return;
            string strSpeedMode = item.Content.ToString();
            if (!String.IsNullOrEmpty(Combo_Electricmotor_Mode.Text))
            {
                string strElectricmotorMode = Combo_Electricmotor_Mode.Text.ToString();
                Set_Component_Status(strSpeedMode, strElectricmotorMode);
            }
        }
        //下拉框选择事件，电机模式改变
        private void Combo_Electricmotor_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (item == null) return;
            string strElectricmotorMode = item.Content.ToString();
            if (Combo_Speed_Mode.Text == null) return;
            if (!String.IsNullOrEmpty(Combo_Speed_Mode.Text))
            {
                string strSpeedMode = Combo_Speed_Mode.Text.ToString();
                Set_Component_Status(strSpeedMode, strElectricmotorMode);
            }
        }
        #endregion

        #region
        //输入验证事件群
        //限制0-9之外的字符输入
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = BaseUtils.ControlInput0To9(e);
        }
        //限制转速在0-400之间
        private void Speed_ControlEvent(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (!BaseUtils.ControlSpeedInput(int.Parse(((TextBox)sender).Text.Trim().ToString())))
                {
                    MessageBox.Show(App.m_LangPackage.TIP_SPEED_INPUT_LIMITIED, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        //限制启动时间
        private void StartTime_ControlEvent(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (!BaseUtils.ControlStartTimeInput(int.Parse(((TextBox)sender).Text.Trim().ToString())))
                {
                    MessageBox.Show(App.m_LangPackage.TIP_STARTTIME_LIMITIED, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        #endregion

        #region

        //调用方法后，打开时加载方法参数
        private void LoadSpeedAndStartTime()
        {
            Combo_Speed_Mode.Text = m_SpeedModule.SpeedMode;
            Combo_Electricmotor_Mode.Text = m_SpeedModule.ElectricmotorMmode;
            TB_Front_Speed_1.Text = m_SpeedModule.FrontRowSpeed_1.ToString();
            TB_Front_Speed_2.Text = m_SpeedModule.FrontRowSpeed_2.ToString();
            TB_Front_StartTime_1.Text = m_SpeedModule.FrontRowStartTime_1.ToString();
            TB_Front_StartTime_2.Text = m_SpeedModule.FrontRowStartTime_2.ToString();
            TB_Back_Speed_1.Text = m_SpeedModule.BackRowSpeed_1.ToString();
            TB_Back_Speed_2.Text = m_SpeedModule.BackRowSpeed_2.ToString();
            TB_Back_StartTime_1.Text = m_SpeedModule.BackRowStartTime_1.ToString();
            TB_Back_StartTime_2.Text = m_SpeedModule.BackRowStartTime_2.ToString();
        }
        //确认转速和开始时间
        private bool SaveSpeedAndStartTime(SpeedModule speedModule)
        {
            callBackSpeedSetting(speedModule);//回调设置参数
            return true;
        }
        //界面组件全部禁用
        private void All_Component_Invalid(bool bInvalid)
        {
            TB_Front_Speed_1.IsEnabled = bInvalid;
            TB_Front_Speed_2.IsEnabled = bInvalid;
            TB_Front_StartTime_1.IsEnabled = bInvalid;
            TB_Front_StartTime_2.IsEnabled = bInvalid;

            TB_Back_Speed_1.IsEnabled = bInvalid;
            TB_Back_Speed_2.IsEnabled = bInvalid;
            TB_Back_StartTime_1.IsEnabled = bInvalid;
            TB_Back_StartTime_2.IsEnabled = bInvalid;
        }
        //界面组件启停设置
        private void Set_Component_Status(string strSpeedMode, string strElectricmotorMode)
        {
            if (strSpeedMode.Equals("") || strElectricmotorMode.Equals("")) return;

            if (strSpeedMode.Equals(StaticParam.Speed_Mode_Arr[0])) //统一转速
            {
                if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[0])
                   ) //单电机
                {
                    All_Component_Invalid(false);
                    TB_Front_Speed_1.IsEnabled = true;
                    TB_Front_StartTime_1.IsEnabled = true;
                    TB_Back_Speed_1.Text = "0";
                    TB_Back_StartTime_1.Text = "0";
                    TB_Back_Speed_2.Text = "0";
                    TB_Back_StartTime_2.Text = "0";
                }
                else if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[1])) //双电机
                {
                    All_Component_Invalid(false);
                    TB_Front_Speed_1.IsEnabled = true;
                    TB_Front_StartTime_1.IsEnabled = true;
                    TB_Back_Speed_1.IsEnabled = true;
                    TB_Back_StartTime_1.IsEnabled = true;
                }
                TB_Front_Speed_2.Text = "0";
                TB_Front_StartTime_2.Text = "0";
                TB_Back_Speed_2.Text = "0";
                TB_Back_StartTime_2.Text = "0";
                return;
            }
            else if (strSpeedMode.Equals(StaticParam.Speed_Mode_Arr[1]))  //梯度转速
            {
                if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[0])) //单电机
                {
                    All_Component_Invalid(false);
                    TB_Front_Speed_1.IsEnabled = true;
                    TB_Front_StartTime_1.IsEnabled = true;
                    TB_Front_Speed_2.IsEnabled = true;
                    TB_Front_StartTime_2.IsEnabled = true;
                    TB_Back_Speed_1.Text = "0";
                    TB_Back_StartTime_1.Text = "0";
                    TB_Back_Speed_2.Text = "0";
                    TB_Back_StartTime_2.Text = "0";
                }
                else if (strElectricmotorMode.Equals(StaticParam.Electricmotor_Mode_Arr[1])) //双电机
                {
                    All_Component_Invalid(false);
                    TB_Front_Speed_1.IsEnabled = true;
                    TB_Front_StartTime_1.IsEnabled = true;
                    TB_Front_Speed_2.IsEnabled = true;
                    TB_Front_StartTime_2.IsEnabled = true;
                    TB_Back_Speed_1.IsEnabled = true;
                    TB_Back_Speed_2.IsEnabled = true;
                    TB_Back_StartTime_1.IsEnabled = true;
                    TB_Back_StartTime_2.IsEnabled = true;
                }
            }
        }
        #endregion
    }
}
