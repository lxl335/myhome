   M S B A M L   `   `   `  ����   DissolutionClient6    0Pharmacy.INST.DissolutionClient.pages.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  	主窗口��$	��720��$
��1280�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency$
 True��.��(�  (� Stretch$
 Fill=�.���� -    ��Name$ MainView��$	��720��$
��1280��$	��Top=�.+��� -    ��Name$ MainTabControl��$��0,60,0,0q�   MainTabControl#��[  $
��1280��$��3��$	��640��
	 �� Top$	 -3��.@��� -   
 ��Name$
 Tab_MainView��$�� ��  MainTabItem#��[ $��18(�$��42�� �� 
IsSelected$
 True��.�� �   � Source$6 0/DissolutionClient;component/pages/MainView.xaml?�$��0,-1,0,1q��� -   $
 Tab_DataSearchView��$�� ��#��[ $��18(�$��42��.�� � $< 6/DissolutionClient;component/pages/DataSearchView.xaml?��� -   $
 Tab_WorkLogView��$�� ��#��[ $��18(�$��42��.�� � $C =/DissolutionClient;component/pages/component/WorkLogView.xaml?��� -   $
 Tab_InstrumentVerifyView��$�� ��#��[ $��18(�$��42��.�� � $B </DissolutionClient;component/pages/InstrumentVerifyView.xaml?��� -   $
 Tab_DataBackupView��$�� ��#��[ $��18(�$��42��.�� � $F @/DissolutionClient;component/pages/component/DataBackupView.xaml?��� -	   $
 Tab_DeviceInfoView��$�� ��#��[ $��18(�$��42��.�� � $< 6/DissolutionClient;component/pages/DeviceInfoView.xaml?��� -
   $
 Tab_TechSupportView��$�� ��#��[ $��18(�$��42��.�� � $= 7/DissolutionClient;component/pages/TechSupportView.xaml?��� -   $
 Tab_VideoMonitorView��$�� ��#��[ $��18(�$��42��.�� � $> 8/DissolutionClient;component/pages/VideoMonitorView.xaml?��� -   $
 Tab_FilterHeadExchView��$�� ��#��[ $��18(�$��42��.�� � $@ :/DissolutionClient;component/pages/FilterHeadExchView.xaml?��� -   $
 Tab_SystemManageView��$�� ��#��[ $��18(�$��42��.�� � $> 8/DissolutionClient;component/pages/SystemManageView.xaml?��� -   $
 Tab_AboutView��$�� ��#��[ $��18(�$��42��.�� � $7 1/DissolutionClient;component/pages/AboutView.xaml?��� -    ��Name$ LB_SYSTEM_TITLE��$�� ��$��	25,10,0,0q�$	��266��$��20(�$��Normal"�$��Microsoft YaHei)��� -   $ LB_DB_STATUS��$��Center=�$�� �� �� Left$	 215��$		 697��$��50��$��23���� -   $ LB_PRINTCOM_STATUS��$��Center=�$�� ��$	 351��$		 697��$��50��$��23���� $��Label��$
 1320��$		 410���� -   $ LB_MAIN_STATUS��$��Center=�$�� ��$ 87��$		 697��$��50��$��23���� -   $ LB_SYS_STATUS_TIP��$��主机通信：��$ 8��$		 697��$��80��$��23���� -   $ LB_DB_STATUS_TIP��$��数据库：��$	 151��$		 697��$��75��$��23���� -   $ LB_PRINTER_STATUS_TIP��$��打印机：��$	 292��$		 697��$��75��$��23��� -    �Name$ PGB_SELFCHECK��$��17��$
 1006��$		 701��$	��270���� -   $ LB_SYS_STATUS��$�� ��$	 660��$		 697��$	��340��$��23���� -   $ LB_COUNT_STATUS_TIP��$��计时器：��$	 469��$		 697��$��52��$��23���� -   $ LB_COUNT_STATUS��$��Center=�$�� ��$	 519��$		 697��$	��100��$��23���� -    ��Name$ IMG_RUNNINGPIC��$y�Hidden=�$��39��$	 482��$	 10��$��57���� -   $ IMG_RUNNINGTEXT��$y�Hidden=�$��32��$	 542��$	 13��$��66��