   M S B A M L   `   `   `  ���� D  @PagingKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:com.ccg.PagingKit;assembly=PagingKitcom.ccg.PagingKit  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem C ?GeckoKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKitcom.ccg.GeckoKit.ctrl  DissolutionClient:   4Pharmacy.INST.DissolutionClient.pages.DataSearchView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Epaging2clr-namespace:com.ccg.PagingKit;assembly=PagingKit     6sys&clr-namespace:System;assembly=mscorlib     Dkb5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKit     -     N� Title$  DataSearchView�� N� 
Background
 �����$
��1278��$	��596��.���� $��0,0,4,0q�$
��1278��$	��596��.+��� -    ��Name$ DSV_GROUPBOX��$��检索条件��$��16(�$	��179��$
��1264�� �� Left$ 9��
 �� Top$ 1��   GroupBoxBorderBrush#���   .���� $	��165��$��0,0,1,0q�.+��� -    ��Name$  LB_DSV_DP_METHODDATE_SATRT��$��实验开始时间：��$	��120��$��16(�$ 18��$ 22��(  "System.Windows.Controls.DatePicker  -     Name$ DSV_DP_METHODDATE_SATRT��
 �� Now   SelectedDate# Z $	 143��$ 22��$	��255��$��35��$��16(�	 � 	Resources	 :  4System.Windows.Controls.Primitives.DatePickerTextBox
 �� 
TargetType �� Property �� Value ��Name �� Path �� StringFormat �� RelativeSource �� AncestorType��%�   '        �� "
  .���  � ��$ 0q���  � �� �� .R��� $ PART_TextBox��$��16(�$��28������ $ SelectedDate�$ yyyy-MM-dd HH:mm:ss�� �� "  ����  �� EndPoint$ 0.5,0&� �� 
StartPoint$ 0.5,1&�.���  � Color$ 	#FFA3AEB9��� $ 	#FF8399A9�� � Offset$ 0.375Q�� $ 	#FF718597��$ 0.375Q�� $ 	#FFABADB3��$ 1Q��� -   $ LB_DSV_DP_METHODDATE_END��$��实验结束时间：��$	��120��$	 467��$ 22��$��16(�  -   $ DSV_DP_METHODDATE_END��# Z $	 594��$ 22��$	��255�� J� RenderTransformOrigin$ 0.471,0.375&�$��35��$��16(�${�False��	 ��%�   '        �� "
  .���  � ��$ 0q���  � �� �� .R��� $ PART_TextBox��$��16(�$��28������ $ SelectedDate�$ yyyy-MM-dd HH:mm:ss�� �� "  ���� $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFA3AEB9��� $ 	#FF8399A9��$ 0.375Q�� $ 	#FF718597��$ 0.375Q�� $ 	#FFABADB3��$ 1Q��� -    ��Name$ DSV_CKB_DATETIMEENABLE��$��启用��$	 852��$ 29��$��16(��� -   $ LB_DSV_METHODNAME��$��	名称：��$ 18��$ 70��$��16(�$	��120���� -	   $ DSV_METHODNAME��  DefaultTextBox#��[ $	 143��$ 68�� �� TextWrapping$
 Wrap=�$	��255��$��30��$��16(��� -
   $ LB_DSV_MANUFACTURER��$��生产厂家：��$	 467��$ 70��$��16(�$	��120���� -   $ DSV_MANUFACTURER��#��[ $	 595��$ 71��$
 Wrap=�$	��255��$��30��$��16(��� -   $ LB_DSV_BATCHNO��$��	批号：��$ 18��$	 111��$��16(�$	��120���� -   $ DSV_BATCHNO��#��[ $	 143��$	 114��$
 Wrap=�$	��255��$��30��$��16(��� -   $ LB_DSV_TESTDEPARTMENT��$��检测单位：��$	 467��$	 111��$��16(�$	��120���� -   $ DSV_TESTDEPARTMENT��#��[ $	 595��$	 114��$
 Wrap=�$	��255��$��30��$��16(��� -    ��Name$ DSV_BTN_SEARCH��$��检索��  DynamicButton#��[ $
 1014��$	 110��$	��100��$��35���� -   $ DSV_BTN_EMPTY��$��重置��#��[ $
 1131��$	 110��$	��100��$��35���� -   $ LB_DSV_TB_ACCOUNT��$��	账号：��$	 938��$ 70��$��16(�$��76���� -   $ DSV_TB_ACCOUNT��#��[ $
 1005��$ 71��$
 Wrap=�$	��220��$��30���� -   $ DSV_CKB_PAUSEEXP�� d� 	IsChecked$
 TrueY�$��中途中止实验��$
 1010��$ 29���� -   $ DSV_CKB_WHOLEEXP��$
 TrueY�$��完整实验��$
 1140��$ 29���� -   $ LB_DSV_CKB_STATUS��$��	状态：��$	 938��$ 22��$��16(�$��76���� $��1q� �� CornerRadius$ 3�����	#FFABADB3$��38��$
 1004��$ 20��$	��249���� -    ��Name$ LV_TESTDATE��
������$	��365��$	 185��$
��1260��$��20(�$ 10��  TestDataViewItem#��[  �� View �� P�$   com.ccg.PagingKit.UserControl1  -     Name$ 
WLV_PAGING��$��40��$
��1250��$ 13��$	 550��