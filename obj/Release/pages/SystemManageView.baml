   M S B A M L   `   `   `  ����   DissolutionClient<    6Pharmacy.INST.DissolutionClient.pages.SystemManageView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  SystemManageView�� N� 
Background
 �����.���� .+��� $	��600��$
��1280�� �� TabStripPlacement$
 Left=�.@��� -    ��Name$ SMV_ACCOUNT_MANAGE��$�� 账号管理 ��$��16(�$��	-2,-2,0,2q�$��60��.�� �   � Source$F @/DissolutionClient;component/pages/component/UserManageView.xaml?�$��0,-1,0,1q��� -   $ SMV_PRIVILEGE_MANAGE��$�� 权限管理 ��$��16(�$��	-2,1,0,-1q�$��60��.�� � $F @/DissolutionClient;component/pages/component/RoleManageView.xaml?�$��0,-1,0,1q��� -   $ SMV_SYSTEMSETTING_MANAGE��$�� 系统设置 ��$��16(�$��	-2,7,0,-7q�$��60��.���� .+��� -    ��Name$  SMV_SYSTEMSETTING_GROUPBOX��$��系统设置��$	��590��$
��1182��
 �� Top$ 4��$��16(� �� Left$ 3��   GroupBoxBorderBrush#���   .���� $��	0,-16,0,0q�.+��� -    ��Name$ SMV_BTN_SAVESETTING��$��保存设置��  DynamicButton#��[ $	 528��$	 517��$��50��$	��130��	 J� RenderTransformOrigin$	 0.5,0.5&��� -   $ SMV_MAINBOARD_GROUPBOX��$��主机��$��75��$
��1130��$ 21��$ 28��.���� .+��� -   
 ��Name$
 LB_SMV_COMBO_COMPORT��$��通信端口：��$��Right=�$	��129��$ 8���� -	    ��Name$ SMV_COMBO_COMPORT��  DefaultComboBox#��[ $	��300��$	 129��$ 10���� -
   $
 LB_SMV_COMBO_COMBAUD��$��波特率：��$��Right=�$	 434��$��95��$ 9���� -   $ SMV_COMBO_COMBAUD��#��[ $	��200��$	 529��$ 11���� -   $ SMV_PRINTER_GROUPBOX��$��	打印机��$��85��$
��1130��$ 21��$	 103��.���� .+��� -   $ SMV_COMBO_PRINTER��#��[ $	��300��$	 129��$ 16���� -   $ SMV_COMBO_PRINTERPORT��#��[ $	��200��$ 16��$	 531���� -   $
 LB_SMV_COMBO_PRINTER��$��	名称：��$��Right=�$	��127��$ 2��$ 13��$	 0.5,0.5&�z�V� .~��� �� ��  �� Angle$ 0.092Q�T� �� -   $
 LB_SMV_COMBO_PRINTERPORT��$��	端口：��$��Right=�$��97��$	 434��$ 12���� -   $
 LB_SMV_COMBO_PRINTCOMBAUD��$��波特率：��$	 757��$	��102��$ 12��$��Right=��� -   $ SMV_COMBO_PRINTCOMBAUD��#��[ $	��200��$	 859��$ 14���� -   $ SMV_SYSTEMPARAMS_GROUPBOX��$��系统参数��$	��272��$
��1130��$ 21��$	 195��.���� .+��� -    ��Name$ SMV_CKB_BEEP_ENABLE��$��启用取样前蜂鸣��$	 397��$ 25���� -   $ SMV_CKB_DOUBLEMOTOR��$��	双电机��$	 397��$ 60���� -   $ SMV_CKB_PUTPILL_ENABLE��$��启用投药装置��$ 35��$ 60���� -   $  SMV_CKB_SAMPLEPOINT_ENABLE��$��启用取样针装置��$ 35��$ 95���� -   $ SMV_CKB_CUPTEMP_ENABLE��$��启用杯内测温装置��$ 35��$	 130���� -   $ SMV_CKB_AUTOSTART��$��开机自启��$y�Hidden=�$	 374��$ 61���� -   $ SMV_CKB_WHOLEMODE_ENABLE��$��启用取样收集装置��$ 35��$ 25���� -   $
 LB_SMV_TB_TEMPOFFSET��$��温度偏差值：��$	 759��$ 19���� -    ��Name$ SMV_TB_TEMPOFFSET��$	 928��  DefaultTextBox#��[ $ 19�� �� TextWrapping$
 Wrap=�$��79���� -   $
 LB_SMV_TB_PWDVALIDDAYS��$��口令有效期：��$	 758��$ 89���� -   $ SMV_TB_PWDVALIDDAYS��$	 928��#��[ $ 91��$
 Wrap=�$��79���� $	��℃��$
 1013��$ 19���� $	��天��$
 1013��$ 89���� -   $
 LB_SMV_TB_DEFHEATTEMP��$��默认加热温度：��$	 743��$ 54���� -    $ SMV_TB_DEFHEATTEMP��$	 928��#��[ $ 54��$
 Wrap=�$��79���� $	��℃��$
 1013��$ 54���� -!   $ SMV_CKB_AUTOHEAT_ENABLE��$��自动加热��$	 397��$ 95���� -"   $ SMV_CKB_TEMP_ENABLE��$��启用温度采集��$	 397��$	 130���� -#   $
 LB_SMV_TB_ROTATERATE��$��转速倍率：��$	 774��$	 127���� -$   $ SMV_TB_ROTATERATE��$	 928��#��[ $	 129��$
 Wrap=�$��79���� -%   $"
 LB_SMV_TB_FIRSTFILTER_OFFSET��$��初滤液补偿：��$	 758��$	 167���� -&   $ SMV_TB_FIRSTFILTER_OFFSET��$	 928��#��[ $	 169��$
 Wrap=�$��79���� $��mL��$
 1013��$	 166���� $��±��$	 893��$ 19���� -'   $
 LB_SMV_TB_SPEEDOFFSET��$��转速偏差值：��$	 759��$	 206���� -(   $ SMV_TB_SPEEDOFFSET��$	 928��#��[ $	 206��$
 Wrap=�$��79���� $��r��$
 1013��$	 206���� $��±��$	 893��$	 206���� -)   $ SMV_RETRIVER_MANAGE��$�� 恢复出厂 ��$��16(�$��
-2,0,0,-13q�$��60��$��Bottom=�.�� � $K E/DissolutionClient;component/pages/component/RetriverFactoryView.xaml?�$��0,-1,0,1q�