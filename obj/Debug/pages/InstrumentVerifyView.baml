   M S B A M L   `   `   `  ����D  @PagingKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:com.ccg.PagingKit;assembly=PagingKitcom.ccg.PagingKit  5       DissolutionClient@   :Pharmacy.INST.DissolutionClient.pages.InstrumentVerifyView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Epaging2clr-namespace:com.ccg.PagingKit;assembly=PagingKit     5      -   5          N� 
Background
  ����� N� Title$ InstrumentVerifyView��5
      $
��1278��6%   $	��596��62   .��5      �� $��0,0,0,0q�6   $
��1278��6   $	��596��6+   .+�5   
   �� -   5         ��Name$ IVV_GROUPBOX��5      $��仪器验证��6)   $
��1264��67    �� Left$ 9��6D   $��18(�6T      GroupBoxBorderBrush#���   6b   $	��340��6�   .��5      �� $	��323��6   $
��1264��6"   $��	0,0,-12,0q�6/   .+�5      �� -   5         ��Name$ LB_IVV_RB_VERIFY��5      $��验证状态：��62   $ 51��6B   
 �� Top$ 18��6S   $	��140��6c   	� 5      -   5         	�Name$ IVV_RB_VERIFY��5      $��	已验证��65   $	 223��6C   $ 24��6U   �� 5      -   5        $ LB_IVV_DP_VERIFYDATE��5      $��验证日期：��66   $ 51��6F   $ 70��6W   $	��140��6g   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ IVV_DP_VERIFYDATE��5      $	 213��68   $ 70��6J   $	��220��6Z   $��30��6f   �� 5      -   5        $ LB_IVV_TB_VALIDDAYS��5      $��有效期：��65   $ 51��6D   $	 129��6U   $	��140��6f   �� 5      -   5         ��Name$ IVV_TB_VALIDDAYS��5      $	 213��64     DefaultTextBox#��[ 6G   $	 132��6p   	 �� TextWrapping$
	 Wrap=�6�   $	��180��6�   $��30��6�   $	��180��6�   �� 5      -	   5        $ IVV_UNIT_DAYS��5      $	��天��6/   $	 405��6;   $	 147��6M   �� 5      -
   5        $ LB_IVV_TB_REMARK��5      $��	备注：��62   $ 51��6@   $	 179��6Q   $	��140��6b   �� 5      -   5        $ IVV_TB_REMARK��5      $	 213��61   #��[ 6D   $	 180��6m   $
	 Wrap=�6~   $	��220��6�   $��30��6�   �� 5      -   5        
 ��Name$
 IVV_BTN_CONFIRM��5      $��验证��62     DynamicButton#��[ 6@   $ 57��6h   $	 236��6y   $��50��6�   $	��100��6�   6�   �� 5      -   5        $
 IVV_BTN_SEARCH��5      $��检索��61   #��[ 6?   $	 336��6g   $	 236��6y   $��50��6�   $	��100��6�   6�   �� 5      -   5        $
 IVV_BTN_EMPTY��5      $��清空��60   #��[ 6>   $	 193��6�   $	 236��6�   $��50��6�   $	��100��6�   5      5      �� 5   
   -   5         ��Name$ LV_VERIFYDATE��5      
������$	��205��6D   $	 345��6Q   $
��1260��6b   $��20(�6o   $ 10��6}    � 	Resources 5       �� 
TargetType �� Property �� Value��%R   '��       �� 5      " ��6   .�5      ��  � ��$ 30��6/   5       5!      ��5"      �� 5#       �� ResourceKey$ DataListViewItem��6!    �� View 5%      �� 5&      .P�5'      �� 5(      5*      $   com.ccg.PagingKit.UserControl1  5+   
   -   5          Name$ 
WLV_PAGING��5+      $��40��62   $
��1250��6?   $ 13��6L   $	 550��6]   6�   5,      5-      