using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Data;
using System.IO;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;
using WPFFolderBrowser;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// ReportModal.xaml 的交互逻辑
    /// </summary>
    public partial class ReportModal : Window
    {
        public List<PDFFile> PDFFileList { get; set; }
        private bool _isLoaded = false;
        private string m_strFilePath = null;

        public ReportModal()
        {
            InitializeComponent();
            PDFFileList = new List<PDFFile>();
            InitializeComponentEx();
            InitializeInterface();
        }

        public ReportModal(string filepath)
        {
            InitializeComponent();
            PDFFileList = new List<PDFFile>();
            m_strFilePath = filepath;
            InitializeComponentEx();
            InitializeInterface();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (m_strFilePath != null)
            {
                OpenExperimentFile();
                m_strFilePath = null;
            }
        }
        public void InitializeComponentEx()
        {
            //加载溶出方法下拉框
            UIOperator.ComboBoxBinder(RM_COMBO_REPORTTYPE, StaticParam.ReportSignType);
            RM_COMBO_REPORTTYPE.SelectedIndex = 0;
            InitialzeDataListView();
        }
        public void InitializeInterface()
        {
            RM_MW.Title = App.m_LangPackage.RM_MW;
            LB_RM_DatePicker.Content = App.m_LangPackage.LB_RM_DatePicker;
            RM_CHK_ENABLEDATE.Content = App.m_LangPackage.RM_CHK_ENABLEDATE;
            LB_RM_COMBO_REPORTTYPE.Content = App.m_LangPackage.LB_RM_COMBO_REPORTTYPE;
            RM_BTN_QUERY.Content = App.m_LangPackage.RM_BTN_QUERY;
            RM_BTN_PREVIEW.Content = App.m_LangPackage.RM_BTN_PREVIEW;
            RM_BTN_EXPORT.Content = App.m_LangPackage.RM_BTN_EXPORT;
            LB_ZoomInButton.Content = App.m_LangPackage.LB_ZoomInButton;
            LB_ZoomOutButton.Content = App.m_LangPackage.LB_ZoomOutButton;
            LB_NormalButton.Content = App.m_LangPackage.LB_NormalButton;
            LB_FitToHeightButton.Content = App.m_LangPackage.LB_FitToHeightButton;
            LB_CloseWindow.Content = App.m_LangPackage.LB_CloseWindow;
            //LB_SinglePageButton.Content = App.m_LangPackage.LB_SinglePageButton;
            //LB_FacingButton.Content = App.m_LangPackage.LB_FacingButton;

        }
        public void InitialzeDataListView()
        {
            GridView gridView = new GridView();
            GridViewColumn[] gvColumn = new GridViewColumn[StaticParam.ReportViewHeadColumn.Length];

            for (int i = 0; i < gvColumn.GetLength(0); i++)
            {
                gvColumn[i] = new GridViewColumn();
                if (i == 0)  //选择列
                {
                    DataTemplate template = new DataTemplate();
                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
                    template.VisualTree = factory;
                    FrameworkElementFactory chkSelected = new FrameworkElementFactory(typeof(CheckBox));
                    chkSelected.SetValue(CheckBox.WidthProperty, 30.0);
                    factory.AppendChild(chkSelected);
                    gvColumn[i].CellTemplate = template;
                    gvColumn[i].Header = StaticParam.ReportViewHeadColumn[i];
                    gvColumn[i].Width = StaticParam.ReportViewHeadWidth[i];
                    chkSelected.AddHandler(CheckBox.ClickEvent, new RoutedEventHandler(OnCheckSelected));
                }
                else
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath(StaticParam.ReportViewHeadField[i]);
                    gvColumn[i].DisplayMemberBinding = binding;
                    gvColumn[i].Header = StaticParam.ReportViewHeadColumn[i];
                    gvColumn[i].Width = StaticParam.ReportViewHeadWidth[i];
                }
                gridView.Columns.Add(gvColumn[i]);
            }

            LV_REPORTVIEW.View = gridView;

        }
        //选中数据检索列表中记录
        private void OnCheckSelected(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            PDFFile pdfFile = (PDFFile)ckb.DataContext;
            if (ckb.IsChecked == true)
                pdfFile.Checked = true;
            else
                pdfFile.Checked = false;
        }

        private bool GetReportView(ListView lv)
        {
            lv.ItemsSource = null;
            UIOperator.EmptyListView(lv);
            PDFFileList.Clear();
            string folderName = "report";
            string mFilePath = App.g_AppDirectory + folderName;
            string[] fileNames = Directory.GetFiles(mFilePath, "*.pdf");
            Array.Reverse(fileNames);
            int nCurrentDateReport = 0;

            string queryDate;
            string signPrefix = ".sign";

            try
            {
                if (RM_DatePicker.IsEnabled == true)
                {
                    //按日期搜索
                    queryDate = RM_DatePicker.SelectedDate.Value.ToString(String.Format("{0}", "yyyyMMdd"));
                    //搜索已签名
                    foreach (string fileName in fileNames)
                    {
                        PDFFile pdfFile = new PDFFile();
                        pdfFile.Filename = Path.GetFileName(fileName);
                        if (pdfFile.Filename.StartsWith("report_"))
                        {
                            if (RM_COMBO_REPORTTYPE.Text.Equals(StaticParam.ReportSignType[1]))
                            {
                                if (pdfFile.Filename.Contains(signPrefix))
                                {
                                    if (pdfFile.Filename.Substring(7, 8).Equals(queryDate) ||
                                           pdfFile.Filename.Substring(12, 8).Equals(queryDate))
                                    {
                                        pdfFile.Filepath = fileName;
                                        pdfFile.Filetype = Path.GetExtension(fileName);
                                        nCurrentDateReport++;
                                        lv.Items.Add(pdfFile);
                                    }
                                }
                            }
                            else
                            {
                                if (!pdfFile.Filename.Contains(signPrefix))
                                {
                                    if (pdfFile.Filename.Substring(7, 8).Equals(queryDate) ||
                                           pdfFile.Filename.Substring(12, 8).Equals(queryDate))
                                    {
                                        pdfFile.Filepath = fileName;
                                        pdfFile.Filetype = Path.GetExtension(fileName);
                                        nCurrentDateReport++;
                                        lv.Items.Add(pdfFile);
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    //搜索全部
                    foreach (string fileName in fileNames)
                    {
                        PDFFile pdfFile = new PDFFile();
                        pdfFile.Filename = Path.GetFileName(fileName);
                        if (pdfFile.Filename.StartsWith("report_"))
                        {

                            if (RM_COMBO_REPORTTYPE.Text.Equals(StaticParam.ReportSignType[1]))
                            {
                                if (pdfFile.Filename.Contains(signPrefix))
                                {
                                    pdfFile.Filepath = fileName;
                                    pdfFile.Filetype = Path.GetExtension(fileName);
                                    nCurrentDateReport++;
                                    lv.Items.Add(pdfFile);
                                }
                            }
                            else
                            {
                                if (!pdfFile.Filename.Contains(signPrefix))
                                {
                                    pdfFile.Filepath = fileName;
                                    pdfFile.Filetype = Path.GetExtension(fileName);
                                    nCurrentDateReport++;
                                    lv.Items.Add(pdfFile);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                App.WriteSystemLog(e1.ToString());
                MessageBox.Show("搜索报告时发生错误！请联系管理员", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            lv.Items.Refresh();
            if (nCurrentDateReport == 0) return false;
            return true;
        }
        //搜索报表
        private void RM_BTN_QUERY_Click(object sender, RoutedEventArgs e)
        {
            if (!GetReportView(LV_REPORTVIEW))
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_REPORT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        //预览报表
        private void RM_BTN_PREVIEW_Click(object sender, RoutedEventArgs e)
        {
            if (LV_REPORTVIEW.SelectedIndex == -1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_REPORT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            PDFFile liPDFFile = (PDFFile)LV_REPORTVIEW.SelectedItem;

            moonPdfPanel.OpenFile(liPDFFile.Filepath);

            _isLoaded = true;
        }
        //导出报表
        private void RM_BTN_EXPORT_Click(object sender, RoutedEventArgs e)
        {
            int nSelectedCount = 0;
            for (int i = 0; i < LV_REPORTVIEW.Items.Count; i++)
            {
                PDFFile pdfFile = LV_REPORTVIEW.Items[i] as PDFFile;
                if (pdfFile.Checked == true)
                {
                    PDFFileList.Add(pdfFile);
                    nSelectedCount++;
                }
            }
            if (nSelectedCount > 0)
            {
                WPFFolderBrowserDialog wPFFolderBrowserDialog = new WPFFolderBrowserDialog
                {
                    InitialDirectory = "C:\\",

                };

                if (wPFFolderBrowserDialog.ShowDialog(this) == true)//注意，此处一定要手动引入System.Window.Forms空间，否则你如果使用默认的DialogResult会发现没有OK属性
                {
                    string folderName = wPFFolderBrowserDialog.FileName;
                    for (int j = 0; j < PDFFileList.Count; j++)
                    {
                        string sourceFile = PDFFileList[j].Filepath;
                        string destFile = String.Format("{0}\\{1}", folderName, PDFFileList[j].Filename);
                        try
                        {
                            File.Copy(sourceFile, destFile);
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine(e1.ToString());
                            MessageBox.Show(App.m_LangPackage.TIP_EXPORT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                    }
                    MessageBox.Show(App.m_LangPackage.TIP_EXPORT_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(100), App.GetBehaviorRemark(100)));
                }
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_EXPORT_REPORT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                string filePath = dialog.FileName;
                try
                {
                    moonPdfPanel.OpenFile(filePath);
                    _isLoaded = true;
                }
                catch (Exception)
                {
                    _isLoaded = false;
                }
            }
        }
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomIn();
            }
        }
        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomOut();
            }
        }
        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.Zoom(1.0);
            }
        }
        private void FitToHeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomToHeight();
            }
        }

        private void FacingButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ViewType = MoonPdfLib.ViewType.Facing;
            }
        }
        private void SinglePageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
            }
        }
        public void OpenExperimentFile()
        {
            moonPdfPanel.OpenFile(m_strFilePath);
            _isLoaded = true;
        }
        private void RM_CHK_ENABLEDATE_Click(object sender, RoutedEventArgs e)
        {
            if (RM_CHK_ENABLEDATE.IsChecked == true)
                RM_DatePicker.IsEnabled = true;
            else
                RM_DatePicker.IsEnabled = false;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
