   M S B A M L   `   `   `  ���� D  @PagingKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:com.ccg.PagingKit;assembly=PagingKitcom.ccg.PagingKit   DissolutionClient@   :Pharmacy.INST.DissolutionClient.pages.InstrumentVerifyView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Epaging2clr-namespace:com.ccg.PagingKit;assembly=PagingKit     -     N� 
Background
  ����� N� Title$ InstrumentVerifyView��$
��1278��$	��596��.���� $��0,0,0,0q�$
��1278��$	��596��.+��� -    ��Name$ IVV_GROUPBOX��$��仪器验证��$
��1264�� �� Left$ 9��$��18(�   GroupBoxBorderBrush#���   $	��340��.���� $	��323��$
��1264��$��	0,0,-12,0q�.+��� -    ��Name$ LB_IVV_RB_VERIFY��$��验证状态：��$ 51��
 �� Top$ 18��$	��140��	� -    	�Name$ IVV_RB_VERIFY��$��	已验证��$	 223��$ 24���� -   $ LB_IVV_DP_VERIFYDATE��$��验证日期：��$ 51��$ 70��$	��140��(  "System.Windows.Controls.DatePicker  -     Name$ IVV_DP_VERIFYDATE��$	 213��$ 70��$	��220��$��30���� -   $ LB_IVV_TB_VALIDDAYS��$��有效期：��$ 51��$	 129��$	��140���� -    ��Name$ IVV_TB_VALIDDAYS��$	 213��  DefaultTextBox#��[ $	 132��	 �� TextWrapping$
	 Wrap=�$	��180��$��30��$	��180���� -	   $ IVV_UNIT_DAYS��$	��天��$	 405��$	 147���� -
   $ LB_IVV_TB_REMARK��$��	备注：��$ 51��$	 179��$	��140���� -   $ IVV_TB_REMARK��$	 213��#��[ $	 180��$
	 Wrap=�$	��220��$��30���� -   
 ��Name$
 IVV_BTN_CONFIRM��$��验证��  DynamicButton#��[ $ 57��$	 236��$��50��$	��100���� -   $
 IVV_BTN_SEARCH��$��检索��#��[ $	 336��$	 236��$��50��$	��100���� -   $
 IVV_BTN_EMPTY��$��清空��#��[ $	 193��$	 236��$��50��$	��100���� -    ��Name$ LV_VERIFYDATE��
������$	��205��$	 345��$
��1260��$��20(�$ 10�� � 	Resources  �� 
TargetType �� Property �� Value��%-   '��       �� " ��.���  � ��$ 30������  �� ResourceKey$ DataListViewItem�� �� View �� .P��� $   com.ccg.PagingKit.UserControl1  -     Name$ 
WLV_PAGING��$��40��$
��1250��$ 13��$	 550��