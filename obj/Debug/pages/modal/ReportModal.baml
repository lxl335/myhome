   M S B A M L   `   `   `  ����L  HDissolutionClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:Pharmacy.INST.DissolutionClient.pages.modal+Pharmacy.INST.DissolutionClient.pages.modal  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   E AMoonPdfLib, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:MoonPdfLib;assembly=MoonPdfLib
MoonPdfLib 5	   	   =    7Pharmacy.INST.DissolutionClient.pages.modal.ReportModal   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Klocal9clr-namespace:Pharmacy.INST.DissolutionClient.pages.modal     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   <mpp,clr-namespace:MoonPdfLib;assembly=MoonPdfLib     5	   	   -   5          �Name$  RM_MW��5
   	    � Title$ 实验报告��6   $	��700��6%   $
��1080��62    � WindowStartupLocation$ CenterScreen=�6?    � 
ResizeMode$ NoResize=�6{   .��5      �� $
��1080��6   $��0,0,0,4q�6   .+�5   
   �� -   5         ��Name$ LV_REPORTVIEW��5      
������$	��519��6D   
 �� Top$	 149��6Q   $	��337��6b   $��16(�6n    �� Left$ 10��6|      ReportDataViewItem#��[  6�    �� View 5      �� 5      P�5      5      5      �� 5   
   -   5         ��Name$ LB_RM_DatePicker��5      $��实验日期：��6*   $ 8��6:   $ 25��6J   $��16(�6Z   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��RM_DatePicker��5      
	 �� Now
   SelectedDate#
 Z	 6*   $ 95��6U   $ 28��6f   $	��135��6v   $��30��6�   $��16(�6�   ${�False��6�   �� 5   
   -   5         ��Name$ RM_BTN_QUERY��5        DynamicButton#��[ 6'   $��检索��6N   $	 241��6[   $ 66��6m   $��16(�6}   $��35��6�   $��88��6�   6�   �� 5   
   -   5        $ RM_BTN_PREVIEW��5      #��[ 6)   $��打开��6P   $	 174��6]   $	 109��6o   $��16(�6�   $��35��6�   $	��155��6�   6�   �� 5   
   -   5        $ RM_BTN_EXPORT��5      #��[ 6(   $��另存为...��6O   $ 10��6`   $	 109��6q   $��16(�6�   $��35��6�   $	��158��6�   6�   �� 5   
   -   5         ��Name$ pdfview��5      $	��747��6"   $	��707��6/   $	 358��6;   $ 28��6M   .+�5      �� ���#f0f0f0$ 0��6*   $	��702��69   $��37��6E   ._�5      �� $��Center=�6    �� Orientation$ 
Horizontal=�6:   $	��625��6S   .+�5      �� -	   5        $ LB_ZoomInButton��5      $��放大��66   #��[ 6C   $��90��6j   $	��0,5q�6�   $��16(�6�   �� 5       -
   5        $ LB_ZoomOutButton��5       $��缩小��67   #��[ 6D   $	��100��6k   $	��5,5q�6�   $��16(�6�   �� 5"      -   5        $ LB_NormalButton��5"      $
��100%��66   #��[ 6E   $��70��6l   $	��0,5q�6�   $��16(�6�   �� 5$      -   5        $ LB_FitToHeightButton��5$      $��整页��6;   #��[ 6H   $	��110��6o   $	��5,5q�6�   $��16(�6�   �� 5&      -   5        $ LB_CloseWindow��5&      $��关闭窗口��65   #��[ 6D   $	��130��6k   $	��5,5q�6�   $��16(�6�   5'      5(      �� 5*      ���#d3d3d3$ 40��6*   ._�5+        MoonPdfLib.MoonPdfPanel  -   5          Name$ moonPdfPanel��5+   #   $	��600��69   $	��700��6F   
�������   ViewType$ 
SinglePage=�6i      PageRowDisplay$ ContinuousPageRows=�6      
PageMargin$ 0,2,4,2q�6�    J� 	AllowDrop$
 True��6�   5,      5-      �� 5.   
   -   5        $ LB_RM_COMBO_REPORTTYPE��5.      $��	状态：��60   $ 8��6>   $ 68��6N   $��16(�6^   $��81��6l   �� 5/   
   -   5         ��Name$ RM_COMBO_REPORTTYPE��5/      $ 95��60   $��16(�6A   $ 69��6O   $	��135��6_   $��30��6k   �� 50   
   -   5         ��Name$ RM_CHK_ENABLEDATE��50      $��启用��6.   $	 240��6;   $ 34��6M   $��16(�6]   6k   52      53      