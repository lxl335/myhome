   M S B A M L   `   `   `  ���� D  @PagingKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:com.ccg.PagingKit;assembly=PagingKitcom.ccg.PagingKit   DissolutionClientD   >Pharmacy.INST.DissolutionClient.pages.component.UserManageView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Epaging2clr-namespace:com.ccg.PagingKit;assembly=PagingKit       N� Title$  UserManageView�� N� 
Background
 �����.���� .+��� -    ��Name$ UMV_NEWUSER_GROUPBOX��$��新建账号��$	��228��$
��1180��
 �� Top$ 4��$��16(� �� Left$ 3��   GroupBoxBorderBrush#���   .���� -   $��MainView��$��	0,-16,0,0q�.+��� -    ��Name$ LB_SMV_TB_LOGINNAME��$��	账号：��$ 34��$ 27��$	��100���� -    ��Name$ SMV_TB_LOGINNAME��$	 134��$ 30�� �� TextWrapping$
 Wrap=�$	��200��  DefaultTextBox#��[ $��Microsoft YaHei UI)��� -   $ LB_SMV_PB_LOGINPWD��$��	口令：��$	 408��$ 27��$	��100���� -   $ SMV_PB_LOGINPWD��$	 505��$ 30��$	��200��$��30��  DefaultPasswordBox#��[ �� -   $ LB_SMV_PB_LOGINPWD_TWICE��$��重复输入口令：��$	 771��$ 27��$	��151���� -   $ SMV_PB_LOGINPWD_TWICE��$	 927��$ 30��$	��200��$��30��#��[ �� -	   $ LB_SMV_COMBO_ROLE��$��	角色：��$ 34��$	 117��$	��100���� -
    ��Name$ SMV_COMBO_ROLE��  DefaultComboBox#��[ $	 134��$	 118��$	��200��$��30���� -   $ LB_SMV_COMBO_STATUS��$��	状态：��$	 407��$	 119��$	��100���� -   $ SMV_COMBO_STATUS��#��[ $	 505��$	 119��$	��200��$��30���� -   $ LB_SMV_DP_VALIDDATE��$��使用期限：��$	 805��$	 118��$	��100��(  "System.Windows.Controls.DatePicker  -   	  Name$	 SMV_DP_VALIDDATE��$	 927��$	 120��$	��200��$��30���� -   $ LB_SMV_TB_USERNAME��$��	姓名：��$ 34��$ 71��$	��100���� -   $ SMV_TB_USERNAME��$	 134��$ 74��$
 Wrap=�$	��200��#��[ �� -   $ LB_SMV_TB_MOBILE��$��	电话：��$	 408��$ 74��$	��100���� -   $ SMV_TB_MOBILE��$	 505��$ 75��$
 Wrap=�$	��200��#��[ �� -   $ LB_SMV_TB_EMAIL��$��	邮箱：��$	 820��$ 74��$��92���� -   $ SMV_TB_EMAIL��$	 927��$ 77��$
 Wrap=�$	��200��#��[ �� -   
 ��Name$
 SMV_BTN_ADD��  DynamicButton#��[ $��添加��$	 462��$	 162��$��50��$��90�� J� RenderTransformOrigin$ 1.5,0.39&��� -   $
 SMV_BTN_EMPTY��#��[ $��清空��$	 571��$	 162��$��50��$��90��$ 1.5,0.39&��� $��*��$
 1130��$	 119��$
��Bold"�
���  ���� $��*��$	 343��$ 30��$
��Bold"�
���  ���� $��*��$	 342��$	 120��$
��Bold"�
���  ���� $��*��$	 715��$	 120��$
��Bold"�
���  ���� $��*��$	 715��$ 29��$
��Bold"�
���  ��$��16(��� $��*��$
 1131��$ 31��$
��Bold"�
���  ��$��16(��� -   $ LB_SMV_REQUIRED_FIELD��$��* 为必填项��$��Center=�$	 916��$	 181��$��Normal"�
���  ��$	��201���� -   $ UMV_USERLIST_GROUPBOX��$��账号列表��$��16(�$	��314��$
��1180��$ 3��$	 233��#���   .���� -    ��Name$ LV_USERLIST��$	��278��$
��1160��$ 2��$ 2��  UseListViewItem#��[ $��4,0,4,0q� � 	Resources  �� 
TargetType �� Property �� Value��%-   '��       �� " ��.���  � ��$ 40�� �� View �� .P��� $   com.ccg.PagingKit.UserControl1  -     Name$ 
SMV_PAGING��$��12(�$��40��$
��1175��$ 5��$	 546��