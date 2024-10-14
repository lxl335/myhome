using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// SampleTimeModal.xaml 的交互逻辑
    /// </summary>
    public partial class SampleTimeModal : Window
    {
        public delegate void CallBackSampleTime(SampleTime[] arr);
        public CallBackSampleTime callBackSampleTime;
        public SampleTime[] m_SampleTime;
        public SampleTimeModal(CallBackSampleTime callBackSampleTime,SampleTime[] sampleTime)
        {
            this.callBackSampleTime = callBackSampleTime;
            m_SampleTime = sampleTime;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeComponentEx()
        {
            DataTable qyTable = new DataTable("QYTABLE");
            // Add three column objects to the table.
            DataColumn keyColumn = new DataColumn();
            keyColumn.Caption = App.m_LangPackage.STM_SAMPLE_TIMES; //"取样次第";
            keyColumn.DataType = Type.GetType("System.Int32");
            keyColumn.ColumnName = "Key";
            qyTable.Columns.Add(keyColumn);

            DataColumn timeValueColumn = new DataColumn();
            keyColumn.Caption = App.m_LangPackage.STM_SAMPLE_POS_TIME; //"取样时间点（分钟）";
            timeValueColumn.DataType = Type.GetType("System.Int32");
            timeValueColumn.ColumnName = "TimeValue";
            qyTable.Columns.Add(timeValueColumn);

            for (int i = 0;i < m_SampleTime.Length;i++)
            {
                DataRow dr = qyTable.NewRow();
                dr["Key"] = m_SampleTime[i].Key;
                dr["TimeValue"] = m_SampleTime[i].TimeValue;
                qyTable.Rows.Add(dr);
                
            }

            SampleTime_DataGrid.ItemsSource = qyTable.DefaultView;
            SampleTime_DataGrid.Columns[0].Header = App.m_LangPackage.STM_SAMPLE_TIMES;;
            SampleTime_DataGrid.Columns[0].Width = 170;
            SampleTime_DataGrid.Columns[0].IsReadOnly = true;
            SampleTime_DataGrid.Columns[1].Header = App.m_LangPackage.STM_SAMPLE_POS_TIME;
            SampleTime_DataGrid.Columns[1].Width = 215;

        }
        public void InitializeInterface()
        {
            STM_MW.Title = App.m_LangPackage.STM_MW;
            STM_SAMPLE_TIMES.Content = App.m_LangPackage.STM_SAMPLE_TIMES;
            STM_SAMPLE_POS_TIME.Content = App.m_LangPackage.STM_SAMPLE_POS_TIME;
            Btn_SaveSampleTime.Content = App.m_LangPackage.Btn_SaveSampleTime;
            Btn_SaveSampleTime_Return.Content = App.m_LangPackage.Btn_SaveSampleTime_Return;
        }
        //窗口加载时执行
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponentEx();
        }
        //检查输入是否有效
        private bool CheckGridViewData()
        {
            //检查数据
            try
            {
                for (int i = 0; i < SampleTime_DataGrid.Items.Count - 2; i++)
                {
                    DataRowView drv = (DataRowView)SampleTime_DataGrid.Items[i];
                    DataRowView nextdrv = (DataRowView)SampleTime_DataGrid.Items[i + 1];
                    try
                    {
                        int a = int.Parse(drv.Row[1].ToString());
                        int b = int.Parse(nextdrv.Row[1].ToString());
                        if (b == 0 && a >= 0)
                        {
                            for (int j = i+1; j < SampleTime_DataGrid.Items.Count - 1;j++)
                            {
                                DataRowView drv_r = (DataRowView)SampleTime_DataGrid.Items[j];
                                int t = int.Parse(drv_r.Row[1].ToString());
                                if (t != 0)
                                {
                                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_STM_SAMPLE_POS_EXSIT_ZERO,
                                                                  StaticParam.SAMPLE_MIN_INTERVAL), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                            }
                            break;
                        }

                        if (b > 0 && a == 0)
                        {
                            MessageBox.Show(string.Format(App.m_LangPackage.TIP_STM_SAMPLE_POS_EXSIT_ZERO,
                                StaticParam.SAMPLE_MIN_INTERVAL), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                        
                        if (b - a < StaticParam.SAMPLE_MIN_INTERVAL)
                        {
                            MessageBox.Show(string.Format(App.m_LangPackage.TIP_STM_SAMPLE_INTERVAL_LESS_THEN,
                                StaticParam.SAMPLE_MIN_INTERVAL), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.ToString());
                        MessageBox.Show(string.Format(App.m_LangPackage.TIP_STM_INPUT_FORMAT_ERROR,
                                StaticParam.SAMPLE_MIN_INTERVAL), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                App.WriteSystemLog(ex.ToString());
                return false;
            }
            return true;
        }
        //保存按钮事件
        private void Btn_SaveSampleTime_Click(object sender, RoutedEventArgs e)
        {
            SampleTime_DataGrid.CancelEdit();
            if (!CheckGridViewData()) return;
            for (int i = 0; i < SampleTime_DataGrid.Items.Count - 1; i++)
            {
                DataRowView drv = (DataRowView)SampleTime_DataGrid.Items[i];
                SampleTime sampleTime = new SampleTime();
                sampleTime.Key = int.Parse(drv.Row[0].ToString());
                sampleTime.TimeValue = int.Parse(drv.Row[1].ToString());
                m_SampleTime[i] = sampleTime;
            }
            callBackSampleTime(m_SampleTime);

            this.Close();
        }

        private void SampleTime_DataGrid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = BaseUtils.ControlInput0To9(e);
        }
        //返回退出
        private void Btn_SaveSampleTime_Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //单元格退出编辑时保存当前值
        private void SampleTime_DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string newValue = (e.EditingElement as TextBox).Text;
            try
            {
                int rowindex = e.Row.GetIndex();
                DataRowView drv = (DataRowView)SampleTime_DataGrid.Items[rowindex];
                drv.Row[1] = newValue;
            }
            catch (Exception e1) { Console.WriteLine(e1.ToString()); }
        }
    }
}
