using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Printing;
using System.Windows.Media;
using Pharmacy.INST.DissolutionClient.entities;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using com.ccg.GeckoKit.api;

namespace Pharmacy.INST.DissolutionClient.common
{
    public class Printer
    {
        private TestData m_TestData;
       // private string m_ReportName;
        private string m_ReportTitle;
        private int m_nFontSize;                              //报表正文字体大小
        private int m_nTitleFontSize;                         //报表标题字体大小 
        private FontFamily m_FontFamily;                    //报表字体
        private Font m_FontHeader;
        private Font m_FontFooter;
        private Font m_FontTitle;
        private Font m_FontBody;
        private Font m_FontReturnLine;                      //换行字体
        private Font m_FontParagraphLine;                   //段间距字体
        private int m_nColumn;
        private int m_PageWidth;
        private int m_PageHeight;
        public int m_PageCount;
        
        public Printer()
        {

        }
        
        public Printer(TestData testData)
        {
            m_TestData = Tools.XmlDeepCopy<TestData>(testData);
            m_nTitleFontSize = 32;
            m_nFontSize = 25;
            m_nColumn = 3;
            m_PageWidth = 600;
            m_PageHeight = 1000;
            m_FontFamily = new FontFamily("宋体");
        }
        public bool Print()
        {
            var printers = new LocalPrintServer().GetPrintQueues();
            var selectedPrinter = printers.FirstOrDefault(p => p.Name == App.m_strPrinter);//选择打印机
            if (selectedPrinter == null)
            {
                return false;
            }
            //设置打印机
            PrintDialog printDialog = new PrintDialog();
            printDialog.PageRangeSelection = PageRangeSelection.AllPages;
            printDialog.UserPageRangeEnabled = true;
            printDialog.PrintQueue = selectedPrinter;
            System.Windows.Documents.FlowDocument doc = new System.Windows.Documents.FlowDocument();
            doc.PagePadding = new Thickness(50, 50, 0, 0);//设置页面与页面之间的边距宽度                                         //doc.ColumnWidth = 240;

            doc.FontFamily = m_FontFamily;
            doc.PageWidth = m_PageWidth;
            doc.PageHeight = m_PageHeight;

            MakeReport(ref doc); //正文


            System.Windows.Documents.DocumentPaginator dp = ((System.Windows.Documents.IDocumentPaginatorSource)doc).DocumentPaginator;
            printDialog.PrintDocument(dp, "report");

            return true;
        }

        private void MakeReport(ref System.Windows.Documents.FlowDocument doc)
        {
            System.Windows.Documents.Table t = new System.Windows.Documents.Table();
            for (int i = 0; i < m_nColumn; i++)
            {
                System.Windows.Documents.TableColumn tc = new System.Windows.Documents.TableColumn();
                tc.Width = new GridLength(m_PageWidth / m_nColumn);
                t.Columns.Add(tc);
            }

            var rg = new System.Windows.Documents.TableRowGroup();
            System.Windows.Documents.TableRow row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nTitleFontSize;
            System.Windows.Documents.TableCell cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_TITLE))); //溶出度试验报告
            cell.TextAlignment = TextAlignment.Center;
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);

            //创建日期
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run("   ")));
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);

            //创建日期
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_CREATETIME)));  //创建日期：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(BaseUtils.GetCurrentDate())));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //软件信息
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_SOFTWARE))); //软件信息
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run("     ")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //软件信息
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_AV_APPNAME)));//软件名称：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_strAppTitle)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //软件信息
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.AppVersionIcon))); //版本：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_strAppVersion)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //实验状态
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_EXP_STATUS)));//实验状态：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.Status)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //起始时间
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_STARTTIME))); //起始时间：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.StartTime)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //结束时间
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.RPT_ENDTIME))); //结束时间：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.EndTime)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //ID
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run("ID：")));
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.LoginName)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //样品名称
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_DSV_METHODNAME))); //名称：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.MethodName)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //批号
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_DSV_BATCHNO))); //批号：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.BatchNo)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //生产厂家
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA1_MANUFACTURER))); //生产厂家：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.Manufacturer)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //检测单位
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA1_TESTDEPARTMENT)));  //检测单位：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.TestDepartment)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //溶出方法
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA2_COMBO_DISSOLUTIONMETHODNAME))); //溶出方法：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.MethodName)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);

            //水浴温度
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_EDM_WATERBOXTEMP)));  //水浴温度：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.TempWaterBox.ToString())));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //溶媒体积
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_RT_SOLUTIONVOLUME))); //溶媒体积：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.SolventVolume.ToString() + "  ml")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //实验总时长
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA2_ALLTIMESPAN)));  //实验总时长：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.AllTimeSpan.ToString() + "  min")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //转速模式
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_Combo_Speed_Mode)));  //转速模式：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.SpeedMode)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //电机模式
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_Combo_Electricmotor_Mode))); //电机模式：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.ElectricmotorMode)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //转速
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run("转速："))); //转速：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.FrontRowSpeed_1.ToString())));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            //取样体积
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA3_SAMPLEVOLUME))); //取样体积：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.SampleVolume.ToString() + "  ml")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //取样次数
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA3_SAMPLETIMES))); //取样次数：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.SampleTimes.ToString() + "  次")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //自动补液
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA3_COMBO_AUTOSUPPLY))); //自动补液：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.Auto_Fluid_Infusion)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //初滤体积
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.PARA3_FIRSTFILTERVOLUME))); //初滤体积：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(m_TestData.First_Filter_Volume.ToString() + " ml")));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //是否稀释
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            string strDilutionEnabled = m_TestData.DilutionEnabled ? "是" : "否";
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA3_CHK_DILUTIONENABLED))); //是否稀释：
            row.Cells.Add(cell);
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(strDilutionEnabled)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //稀释倍数
            rg = new System.Windows.Documents.TableRowGroup();
            row = new System.Windows.Documents.TableRow();
            row.FontSize = m_nFontSize;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(App.m_LangPackage.LB_PARA3_COMBO_DILUTIONRATIO)));  //稀释倍数：
            row.Cells.Add(cell);
            string strDilutionRatio = m_TestData.DilutionEnabled ? m_TestData.DilutionRatio.ToString() : string.Empty;
            cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(strDilutionRatio)));
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            rg.Rows.Add(row);
            t.RowGroups.Add(rg);
            //取样时间点
            for (int i = 0; i < m_TestData.SampleTimes; i++)
            {
                rg = new System.Windows.Documents.TableRowGroup();
                row = new System.Windows.Documents.TableRow();
                row.FontSize = m_nFontSize;
                cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(string.Format(App.m_LangPackage.RPT_SAMPLEPOINT, i + 1))));  //取样时间点{0}：
                row.Cells.Add(cell);
                cell = new System.Windows.Documents.TableCell(new System.Windows.Documents.Paragraph(new System.Windows.Documents.Run(string.Format(App.m_LangPackage.RPT_SAMPLETIME, m_TestData.Sample_Time_Arr[i].TimeValue)))); //第{0}分钟

                cell.ColumnSpan = 2;
                row.Cells.Add(cell);
                rg.Rows.Add(row);
                t.RowGroups.Add(rg);
            }
            doc.Blocks.Add(t);
        }
        //预生成PDF文档
        public void PrePrintPDF(string strfilename,string reporttitle, string reportname,
            string reviewer, string reviewerid, string content, string reportdatetime, double[,] arr)
        {
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            BaseFont baseFont = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);
            m_FontHeader = new Font(baseFont, 8);
            m_FontTitle = new Font(baseFont, 32);
            m_FontBody = new Font(baseFont, 10);
            m_FontFooter = new Font(baseFont, 8);
            m_FontReturnLine = new Font(baseFont, 4);
            m_FontParagraphLine = new Font(baseFont, 6);

            //构造页眉
            DrawHeader(document);
            //构造页脚
            DrawFooter(document, m_PageCount);
            //打开文档
            document.Open();
            //构造报告基本信息
            DrawReportInfo(document, strfilename, reporttitle, reportname,
              reviewer, reviewerid, content, reportdatetime, arr);

            m_PageCount = writer.PageNumber;


            writer.Close();
            //关闭文档
            document.Close();
        }
        //生成PDF文档
        public void PrintPDF(string reporttitle, string reportname,
            string reviewer, string reviewerid, string content, string reportdatetime, double[,] arr)
        {
            string strFileName = string.Format("{0}.pdf", reportname);
            string strFile = App.g_AppDirectory + "report\\" + strFileName;
            m_ReportTitle = reporttitle;

            PrePrintPDF(strFileName, reporttitle, reportname, reviewer, reviewerid, content, reportdatetime, arr);
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            //构造页眉
            DrawHeader(document);
            //构造页脚
            DrawFooter(document, m_PageCount);
            document.Open();

            //构造报告基本信息
            DrawReportInfo(document, strFile, reporttitle, reportname,
              reviewer, reviewerid, content, reportdatetime, arr);
            //关闭文档
            document.Close();
            //保存pdf文件
            byte[] fileByte = stream.GetBuffer();
            FileStream fs = new FileStream(strFile, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(fileByte);
            bw.Close();
            fs.Close();
        }

        //页眉
        public void DrawHeader(Document doc)
        {
            //构造页眉
            Chunk chunk_printDateTime = new Chunk("Print Date: " + BaseUtils.GetCurrentDateTime() + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n", m_FontHeader);
            Chunk chunk_Title = new Chunk(m_ReportTitle, m_FontTitle);
            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(BaseUtils.CreateBarCode("RC6A2021083003", 180, 45), System.Drawing.Imaging.ImageFormat.Jpeg);
            //Chunk chunkBarCode = new Chunk(img, 45, -20);
            Chunk chunkReturn = new Chunk("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n", m_FontHeader);
            Phrase ph = new Phrase(3);
            ph.Add(chunk_printDateTime);
            ph.Add(chunk_Title);
            //ph.Add(chunkBarCode);
            ph.Add(chunkReturn);
            HeaderFooter header = new HeaderFooter(ph, false);
            header.Border = Rectangle.NO_BORDER;
            doc.Header = header;
        }
        //页脚
        public void DrawFooter(Document doc, int PageCount)
        {
            //构造页脚
            BaseFont baseFont1 = BaseFont.CreateFont("Helvetica", BaseFont.WINANSI, BaseFont.EMBEDDED);
            Font m_FontTemp = new Font(baseFont1, 12);
            Phrase ph1 = new Phrase(1);
            Chunk chunk_pagenumAndtotalnum = new Chunk("\r\n\r\n\r\n\r\n\r\n", m_FontTemp);
            ph1.Add(chunk_pagenumAndtotalnum);
            Phrase ph2 = new Phrase(3);
            //计算总页数
            ph2.Add(new Chunk(string.Format("/{0}", PageCount), m_FontTemp));
            HeaderFooter footer = new HeaderFooter(ph1, ph2);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.Border = Rectangle.NO_BORDER;
            doc.Footer = footer;
        }
        //报告基本信息
        public void DrawReportInfo(Document doc, string strfilename,string reporttitle, string reportname,
            string reviewer, string reviewerid, string content, string reportdatetime, double[,] arr)
        {
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_CREATETIME + BaseUtils.GetCurrentDate(), m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_FILENAME + strfilename, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_SOFTWARE, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_SOFTWARENAME + App.m_strAppTitle, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_SOFTWAREVERSION + App.m_strAppVersion, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_EXP_STATUS + m_TestData.Status, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_STARTTIME + m_TestData.StartTime, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_ENDTIME + m_TestData.EndTime, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_USERID + m_TestData.LoginName, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_METHODNAME + m_TestData.MethodName, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_LOTNO + m_TestData.BatchNo, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_MANUFACTURER + m_TestData.Manufacturer, m_FontBody));
            doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_TESTDEPARTMENT + m_TestData.TestDepartment, m_FontBody));

            string str;
            string strHead, strTail;
            strHead = string.Format(App.m_LangPackage.RPT_TESTMETHOD, m_TestData.TestMethod, m_TestData.TempWaterBox.ToString());
            strTail = string.Format(App.m_LangPackage.RPT_TANKTEMP, m_TestData.TempWaterBox.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            strHead = string.Format(App.m_LangPackage.RPT_SOLVENTVOLUME, m_TestData.SolventVolume.ToString());
            strTail = string.Format(App.m_LangPackage.RPT_ALLTIMESPAN, m_TestData.AllTimeSpan.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            strHead = string.Format(App.m_LangPackage.RPT_FRONT_SPEED_1, m_TestData.FrontRowSpeed_1.ToString());
            strTail = string.Format(App.m_LangPackage.RPT_FRONT_SPEED_2, m_TestData.FrontRowSpeed_2.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            strHead = string.Format(App.m_LangPackage.RPT_BACK_SPEED_1, m_TestData.BackRowSpeed_1.ToString());
            strTail = string.Format(App.m_LangPackage.RPT_BACK_SPEED_2, m_TestData.BackRowSpeed_2.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            strHead = string.Format(App.m_LangPackage.RPT_SAMPLE_VOLUME, m_TestData.SampleVolume.ToString());
            strTail = string.Format(App.m_LangPackage.RPT_SAMPLE_TIME, m_TestData.SampleTimes.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            strHead = string.Format(App.m_LangPackage.RPT_AUTO_FLUID_INFUSION, m_TestData.Auto_Fluid_Infusion);
            strTail = string.Format(App.m_LangPackage.RPT_FIRST_FILTER_VOLUME, m_TestData.First_Filter_Volume.ToString());
            str = strHead + GetBlanks(strHead) + strTail;
            doc.Add(new Paragraph(str, m_FontBody));

            if (App.m_bCupTempEnable)
            {
                //取样时间点
                for (int i = 0; i < m_TestData.SampleTimes; i++)
                {
                    doc.Add(new iTextSharp.text.Paragraph(string.Format(App.m_LangPackage.RPT_SAMPLEPOINT, i + 1) + string.Format(App.m_LangPackage.RPT_SAMPLETIME, m_TestData.Sample_Time_Arr[i].TimeValue), m_FontBody));
                    string cup_info = String.Empty;
                    doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_CUPTEMP, m_FontBody));
                    for (int j = 0; j < App.m_nCupNum; j++)
                    {
                        if (j / 6 != 0 && j % 6 == 0)
                        {
                            cup_info += "\r\n";
                        }
                        string strTemp = strTemp = arr[i, j].ToString("F");

                        //判断是实验生成报告还是生成签名报告
                        if (!string.IsNullOrEmpty(reviewerid) && !string.IsNullOrEmpty(reviewer))
                        {
                            //生成签名报告
                            if (arr[i, j] == -1)
                                strTemp = "-";
                        }
                        else
                        {
                            //生成实验未签名报告
                            if (!App.g_cuptempenabled[j])
                                strTemp = "-";
                        }

                        cup_info += string.Format("{0}：{1}℃   ", j + 1, strTemp);
                    }

                    //string cup_info_1 = "1：{0}℃   2：{1}℃   3：{2}℃   4：{3}℃   5：{4}℃   6：{5}℃";
                    //string cup_info_2 = "7：{0}℃   8：{1}℃   9：{2}℃ 10：{3}℃ 11：{4}℃ 12：{5}℃";
                    //document.Add(new iTextSharp.text.Paragraph(string.Format(cup_info_1, ((double)arr[i, 0] / 100).ToString("F"), ((double)arr[i, 1] / 100).ToString("F"), ((double)arr[i, 2] / 100).ToString("F"), ((double)arr[i, 3] / 100).ToString("F"), ((double)arr[i, 4] / 100).ToString("F"), ((double)arr[i, 5] / 100).ToString("F")), fontBody));
                    //document.Add(new iTextSharp.text.Paragraph(string.Format(cup_info_2, ((double)arr[i, 6] / 100).ToString("F"), ((double)arr[i, 7] / 100).ToString("F"), ((double)arr[i, 8] / 100).ToString("F"), ((double)arr[i, 9] / 100).ToString("F"), ((double)arr[i, 10] / 100).ToString("F"), ((double)arr[i, 11] / 100).ToString("F")), fontBody));
                    doc.Add(new iTextSharp.text.Paragraph(cup_info, m_FontBody));
                    doc.Add(new iTextSharp.text.Paragraph("                "));
                }
            }

            if (!String.IsNullOrEmpty(reviewer))   //有签名人时，打印审核人
            {
                doc.Add(new iTextSharp.text.Paragraph("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n", m_FontBody));
                doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_REVIEWERID + reviewerid + "\r\n", m_FontBody));
                doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_REVIEWER + reviewer + "\r\n", m_FontBody));
                doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_CONTENT + content + "\r\n", m_FontBody));
                doc.Add(new iTextSharp.text.Paragraph(App.m_LangPackage.RPT_REPORTTIME + reportdatetime + "\r\n", m_FontBody));
            }
        }
        public string GetBlanks(string strHead)
        {
            string strBlanks = String.Empty;
            if (strHead.Length > 60) return strBlanks;
            for (int i = 0; i < 60 - trueLength(strHead); i++)
                strBlanks += " ";
            return strBlanks;
        }
        static public int trueLength(string str)
        {
            // str 字符串
            // return 字符串的字节长度
            int lenTotal = 0;
            int n = str.Length;
            string strWord = "";
            int asc;
            for (int i = 0; i < n; i++)
            {
                strWord = str.Substring(i, 1);
                asc = Convert.ToChar(strWord);
                if (asc < 0 || asc > 127)
                    lenTotal = lenTotal + 2;
                else
                    lenTotal = lenTotal + 1;
            }

            return lenTotal;
        }
    }
}
