   M S B A M L   `   `   `  ����L  HDissolutionClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Pharmacy.INST.DissolutionClient.common&Pharmacy.INST.DissolutionClient.common  5   	   C ?GeckoKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKitcom.ccg.GeckoKit.ctrl 5   	   @    :Pharmacy.INST.DissolutionClient.pages.modal.SignLoginModal   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Gcommon4clr-namespace:Pharmacy.INST.DissolutionClient.common     5   	   Dkb5clr-namespace:com.ccg.GeckoKit.ctrl;assembly=GeckoKit     5   	   -   5          �Name$  SLM_MW��5	   	    � Title$ 签名登录��6   $	��320��6&   $	��500��63   $��16(�6?    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ NoResize=�6r   .��5
      �� $	��500��6   .+�5   
   �� -   5         ��Name$ LB_SLM_TB_LOGINNAME��5      $��	账号：��6-   
 �� Top$ 16��6;    �� Left$ 22��6K   �� 5   
   -   5         ��Name$ SLM_TB_LOGINNAME��5         DefaultTextBox#��[  6,   $	��150��6U   $��30��6a   $ 19��6m   $ 85��6}   �� 5   
   -   5        $ LB_SLM_PB_LOGINPWD��5      $��	口令：��6,   $	 244��6:   $ 18��6L   �� 5   
   -   5        $ SLM_PB_LOGINPWD��5      $	 307��6,   $ 19��6>   $	��150��6N   $��30��6Z     DefaultPasswordBox#��[ 6f   �� 5   
   -   5        $ LB_SLM_TB_SIGNCONTENT��5      $��审核内容：��6/   $ 54��6?   $ 22��6O   �� 5   
   -   5        $ SLM_TB_SIGNCONTENT��5   u    �� AcceptsReturn$
 True��6   #��[  6'   "	 �� VerticalScrollBarVisibility$	 Visible=�6O   $�� ��6�   ���#EAEAEA
 �� TextWrapping$

 Wrap=�6�   $	��430��6�   $��70��6�   $��Normal"�6�   $ 28��6�   $ 84��6   $	��Top=�6  �� 5   
   -   5        $ LB_SLM_ONEKEYINPUT��5      $��一键输入：��6,   $ 25��6<   $	 157��6M   $	��225��6^   �� 5   
   -	   5         ��Name$ SLM_BTN_PASS��5      $��通过��6'     StaticButton#��[ 65   $	 264��6[   $	 159��6m   $��80��6~   $��30��6�   �� 5   
   -
   5        $ SLM_BTN_FAIL��5      $��	不通过��6'   #��[ 66   $	 357��6\   $	 159��6n   $��80��6   $��30��6�   (  "com.ccg.GeckoKit.ctrl.KeyboardCtrl  5   
   $ 29��6   $	 233��6+   �� 5   
   -   5        $ SLM_BTN_CONFIRM��5      $��确认��6*     DynamicButton#��[ 67   $��80��6^   $��40��6i   $	 163��6u   $	 224��6�   6�   �� 5   
   -   5        $ SLM_BTN_CANCEL��5      $��取消��6)   #��[ 66   $��80��6]   $��40��6h   $	 268��6t   $	 224��6�   6�   5      5      