   M S B A M L   `   `   `  ���� L  HDissolutionClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:Pharmacy.INST.DissolutionClient.pages.modal+Pharmacy.INST.DissolutionClient.pages.modal  _4clr-namespace:Pharmacy.INST.DissolutionClient.common&Pharmacy.INST.DissolutionClient.common  C ?GeckoKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKitcom.ccg.GeckoKit.ctrl C    =Pharmacy.INST.DissolutionClient.pages.modal.SpeedSettingModal   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Klocal9clr-namespace:Pharmacy.INST.DissolutionClient.pages.modal     Gcommon4clr-namespace:Pharmacy.INST.DissolutionClient.common     Dkb5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKit     -     �Name$  SSM_MW�� � Title$ 转速设置��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=�.���� $	��800��$	��400��.+��� $��16(�$	��385��$	��775��
 �� Top$ 7�� �� Left$ 5��.���� -    ��Name$ 
MainCanvas��.+��� -    ��Name$ Combo_Speed_Mode��   DefaultComboBox#��[  $	 158��$ 43��$	��180���� -   $ Combo_Electricmotor_Mode��#��[  $	 536��$ 44��$	��180���� -    ��Name$ LB_Combo_Speed_Mode��$��转速模式：��$ 44��$ 42���� -   $! LB_Combo_Electricmotor_Mode��$��电机模式：��$	 401��$ 40�� J� RenderTransformOrigin$ 0.222,0.553&��� -   $ LB_TB_Front_Speed_1��$��前排转速1：��$ 45��$	 101���� -   	 ��Name$	 TB_Front_Speed_1��  DefaultTextBox#��[ $	 158��$	 104��
 �� TextWrapping$

 Wrap=�$	��180���� -	   $ LB_TB_Front_StartTime_1��$��前排启动时间1：��$	 398��$ 98��$ 
0.122,0.52&��� -
   $	 TB_Front_StartTime_1��#��[ $	 536��$	 101��$

 Wrap=�$	��180���� -   $ LB_TB_Front_Speed_2��$��前排转速2：��$ 45��$	 159���� -   $	 TB_Front_Speed_2��#��[ $	 158��$	 159��$

 Wrap=�$	��180���� -   $ LB_TB_Front_StartTime_2��$��前排启动时间2：��$	 398��$	 158���� -   $	 TB_Front_StartTime_2��#��[ $	 536��$	 159��$

 Wrap=�$	��179���� -   $ LB_TB_Back_Speed_1��$��后排转速1：��$ 46��$	 215���� -   $	 TB_Back_Speed_1��#��[ $	 158��$	 215��$

 Wrap=�$	��180���� -   $ LB_TB_Back_StartTime_1��$��后排启动时间1：��$	 399��$	 215���� -   $	 TB_Back_StartTime_1��#��[ $	 536��$	 215��$

 Wrap=�$	��180���� -   $ LB_TB_Back_Speed_2��$��后排转速2：��$ 45��$	 266���� -   $	 TB_Back_Speed_2��#��[ $	 158��$	 268��$

 Wrap=�$	��180���� -   $ LB_TB_Back_StartTime_2��$��后排启动时间2：��$	 398��$	 266���� -   $	 TB_Back_StartTime_2��#��[ $	 536��$	 268��$

 Wrap=�$	��180���� -    ��Name$ Btn_Confirm��  DynamicButton#��[ $��确定��$	 549��$	 336��$��40��$��70���� -   $ 	Btn_Close��#��[ $��关闭��$	 633��$	 336��$��40��$��70��(  "com.ccg.GeckoKit.ctrl.KeyboardCtrl  $ 39��$	 339��