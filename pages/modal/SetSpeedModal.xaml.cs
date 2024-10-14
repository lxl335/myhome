using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// SetSpeedModal.xaml 的交互逻辑
    /// </summary>
    public partial class SetSpeedModal : Window
    {
        public delegate void CallBackSelectSwivel(int nSwivel);
        public CallBackSelectSwivel callBackSelectSwivel;
        public SetSpeedModal(CallBackSelectSwivel callBackSelectSwivel)
        {
            this.callBackSelectSwivel = callBackSelectSwivel;
            InitializeComponent();
        }

        private void BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            //校验输入
            try
            {
                if (string.IsNullOrEmpty(TB_SWIVEL.Text))
                {
                    MessageBox.Show("转速值不能为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int nSwivel = int.Parse(TB_SWIVEL.Text.Trim());
                if (nSwivel < 1 || nSwivel > StaticParam.ROTATION_MAX_SPEED)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_TSV_ROTATE_SPEED_LIMIT, StaticParam.ROTATION_MAX_SPEED), App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Close();
                //回调
                callBackSelectSwivel(nSwivel);
                
            }
            catch (Exception e1)
            {
                MessageBox.Show("输入格式有误！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                App.WriteSystemLog(e1.ToString());
            } 
        }
    }
}
