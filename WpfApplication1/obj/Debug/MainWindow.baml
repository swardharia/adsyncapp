   M S B A M L   `   `   `  ����  	ADsyncApp     WpfApplication1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4UwpfxJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5        � Title$  
MainWindow��5   	   $	��500��6   $	��650��6)   
�������.��5      �  � RowDefinitions 5   
   �� 5	      $	��130 �6   �� 5
      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��1��6   
���   ����	#FF009CFF$��1q�6I   $	��700��6]   $	��Top=�6j   $
��auto��6�   �� 5   
   -   5         ��Name$
 Logo��5   =   $��0��6   $��Images\simplerlogo.png��6   $��Center=�6K   $��55��6h   $��Center=�6u   $	��263��6�   �� 5   
   $��1��6   $��	WELCOME��6   
�������$��Center=�6G   $��168,71.4,233.2,0q�6d   $	��Top=�6~   $��49��6�   $	��242��6�   $��
Trajan Pro)�6�   $��22(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$	 lbl��5      $��1��6   $�� ��6*   
�������$��83,117.4,82.2,0q�6H   $��Center=�6a   $	��Top=�6�   $��24(�6�   $
��Bold"�6�   �� 5   
   -   5        $��1��5      $��Ready to Connect ?��6   $��Center=�6;   $��190,226.4,179.2,0q�6X   $	��Top=�6s   $	��274��6�   $��49��6�   .  (Microsoft.Windows.Controls.BusyIndicator  5   
   -   5        $��BusyBar��5      $��0,173.4,0.2,397.4q�6-   $��1��6H      BusyContent$S MTrying to connect Active Directory.                            Please wait...��6U   5       � 	Resources 5         WindowBrush �� Color��%)   &	        �� 5   
   $ White��6.   5      ��5      �� 5   
    �� 
TargetType" �6   .�5      �� 	 �� Property	 � ��
 �� Value#
 [  6+   5      5      