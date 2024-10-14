using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WPFFolderBrowser;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;
using com.ccg.GeckoKit.api;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;


namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// DataBackupView.xaml 的交互逻辑
    /// </summary>
    public partial class DataBackupView : Page
    {
        public DataBackupView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        public void InitializeComponentEx()
        {
            //初始化按钮样式
        }
        //初始化界面
        public void InitializeInterface()
        {
            DBV_GROUPBOX.Header = App.m_LangPackage.DBV_GROUPBOX;
            DBV_BACKUP_GROUPBOX.Header = App.m_LangPackage.DBV_BACKUP_GROUPBOX;
            DBV_RESTORE_GROUPBOX.Header = App.m_LangPackage.DBV_RESTORE_GROUPBOX;
            LB_DBV_TB_BACKUPDIR.Content = App.m_LangPackage.LB_DBV_TB_BACKUPDIR;
            LB_DBV_TB_RESTOREFILE.Content = App.m_LangPackage.LB_DBV_TB_RESTOREFILE;
            DBV_BTN_BACKUP_EXPLORER.Content = App.m_LangPackage.DBV_BTN_BACKUP_EXPLORER;
            DBV_BTN_BACKUP.Content = App.m_LangPackage.DBV_BTN_BACKUP;
            DBV_BTN_RESTORE_EXPLORER.Content = App.m_LangPackage.DBV_BTN_RESTORE_EXPLORER;
            DBV_BTN_RESTORE.Content = App.m_LangPackage.DBV_BTN_RESTORE;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(6), App.GetBehaviorRemark(6)));
        }
        //选择备份目录 按钮 事件
        private void DBV_BTN_BACKUP_EXPLORER_Click(object sender, RoutedEventArgs e)
        {
            WPFFolderBrowserDialog wPFFolderBrowserDialog = new WPFFolderBrowserDialog
            {
                InitialDirectory = App.g_AppDirectory,
            };

            if (wPFFolderBrowserDialog.ShowDialog() == true)//注意，此处一定要手动引入System.Window.Forms空间，否则你如果使用默认的DialogResult会发现没有OK属性
            {
                DBV_TB_BACKUPDIR.Text = wPFFolderBrowserDialog.FileName;
            }
        }
        //备份数据库 按钮 事件
        private void DBV_BTN_BACKUP_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(((Button)sender).Content.ToString(),true))
                BackupDataBase();
        }
        //选择数据库文件 按钮 事件
        private void DBV_BTN_RESTORE_EXPLORER_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = App.g_EngVer ? "bakup file（bak)|*.bak" : "备份文件（bak)|*.bak",
                FilterIndex = 1,
                RestoreDirectory = true,
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string strSourceFile = openFileDialog.FileName;
                DBV_TB_RESTOREFILE.Text = strSourceFile;
            }
        }
        //还原 按钮 事件
        private void DBV_BTN_RESTORE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(((Button)sender).Content.ToString(), true))
                RetriverDataBase();
        }

        public void BackupDataBase()
        {
            string folderName = DBV_TB_BACKUPDIR.Text.Trim();
            string sourceFile = App.g_AppDirectory + StaticParam.DB_NAME;
            string destFileName = String.Format("{0}_{1}.bak", StaticParam.DB_NAME, BaseUtils.GetFileCurrentDateTime());
            string destFile = folderName + "\\" + destFileName;

            try
            {
                File.Copy(sourceFile, destFile);
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_DBV_BACKUP_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(App.m_LangPackage.TIP_DBV_BACKUP_FAILURE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1),App.GetBehavior(33),App.GetBehaviorRemark(33)));
            DBV_TB_BACKUPDIR.Clear();
        }

        public void RetriverDataBase()
        {
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_DBV_RESTORE_WARNING, App.m_LangPackage.WARNING, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (mBoxResult)
            {
                case MessageBoxResult.Yes:
                    if (App.m_WorkLogTimer != null)
                        App.m_WorkLogTimer.Stop();
                    App.WriteWorkLog();   //将缓存日志写入库
                    App.m_SQLiteDBUtils.Close();
                    App.m_SQLiteDBUtils = null;
                    string sourceFile = DBV_TB_RESTOREFILE.Text.Trim();
                    string destFile = App.g_AppDirectory + StaticParam.DB_NAME;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    if (File.Exists(destFile))
                    {
                        try { File.Delete(destFile); }
                        catch (Exception e1)
                        {
                            Console.WriteLine(e1.ToString());
                            MessageBox.Show("还原准备工作失败！", App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    System.Threading.Thread.Sleep(1000);

                    try
                    {
                        File.Copy(sourceFile, destFile);
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.ToString());
                        MessageBox.Show(App.m_LangPackage.TIP_DBV_RESTORE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show(App.m_LangPackage.TIP_DBV_RESTORE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(34),App.GetBehaviorRemark(34)));
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }


    }
}
