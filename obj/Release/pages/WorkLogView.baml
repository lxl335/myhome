   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  L HDissolutionClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Pharmacy.INST.DissolutionClient.common&Pharmacy.INST.DissolutionClient.common D @PagingKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT8clr-namespace:Pharmacy.INST.PagingKit;assembly=PagingKitPharmacy.INST.PagingKit 7   1Pharmacy.INST.DissolutionClient.pages.WorkLogView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     Gcommon4clr-namespace:Pharmacy.INST.DissolutionClient.common     Kpaging8clr-namespace:Pharmacy.INST.PagingKit;assembly=PagingKit     -     N� 
Background
  ����� N� Title$ WorkLogView��$
��1278��$	��596��.���� $��0,0,0,0q�$
��1278��$	��596��.+��� $��检索条件��$��16(�$	��154��$
��1264�� �� Left$ 9��
 �� Top$ 1�����	#FF36C965.���� .+��� $��起始时间：��$	��120��$��16(�$ 18��$ 22��(  "System.Windows.Controls.DatePicker  -     Name$ WLV_DP_METHODDATE_SATRT��
 �� Now   SelectedDate# Z $	 143��$ 22��$	��255��$��35��$��16(� � 	Resources :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType	 �� Property
 �� Value ��Name �� RelativeSource �� AncestorType �� StringFormat��%�   '        �� "  .��� 	 � ��$
 0q��� 	 � ��
 �� .R��� $ PART_TextBox��$��16(�$��28������ *SelectedDate+ �� "  $ yyyy-MM-dd HH:mm:ss���� $��终止时间：��$��99��$	 494��$ 22��$��16(�  -   $ WLV_DP_METHODDATE_END��# Z $	 610��$ 22��$	��255�� J� RenderTransformOrigin$ 0.471,0.375&�$��35��$��16(�${�False�� ��%�   '        �� "  .��� 	 � ��$
 0q��� 	 � ��
 �� .R��� $ PART_TextBox��$��16(�$��28������ *SelectedDate+ �� "  $ yyyy-MM-dd HH:mm:ss���� -    ��Name$ WLV_CKB_DATETIMEENABLE��$��启用��$	 868��$ 29��$��16(��� $��	账号：��$	 933��$ 22��$��16(��� -   $ WLV_TB_ACCOUNT��   DefaultTextBox#��[  $	 985��$ 22�� �� TextWrapping$
 Wrap=�$	��255��$��30���� -    ��Name$ WLV_BTN_SEARCH��$��检索��  DynamicButton#��[ $	 988��$ 80��$��90��$��35���� -   $ WLV_BTN_EMPTY��$��清空��#��[ $
 1083��$ 80��$��60��$��35���� -   $ WLV_BTN_EXPORT��$��导出��#��[ $
 1177��$ 80��$��60��$��35���� $��日志类别：��$	��120��$��16(�$ 18��$ 79���� $��用户行为：��$	��115��$��16(�$	 492��$ 79���� -	    ��Name$ WLV_COMBO_BEHAVIOR��  DefaultComboBox#��[ $	 610��$ 81��$	��255��$��30���� -
   $ WLV_COMBO_LOGTYPE��#��[ $	 143��$ 81��$	��255��$��30���� -    ��Name$ 
LV_WORKLOG��
������$	��390��$ 10��$	 160��$
��1260��$��20(� ��%-   '��       �� " ��.��� 	 � ��$
 30������  �� ResourceKey$ DataListViewItem�� �� View �� .P��� $��序号�� �� Width$	 100�� �� DisplayMemberBinding �� *SID+�� $��账户��$	 150�� �� *	LoginName+�� $��日志类别��$	 150�� �� *	LogType+�� $��行为��$	 200�� �� *
Behavior+�� $��时间��$	 250�� �� *
ActionTime+�� $��说明��$	 350�� �� *Remark+*  $Pharmacy.INST.PagingKit.UserControl1  -     Name$ 
WLV_PAGING��$��40��$
��1250��$ 13��$	 550��