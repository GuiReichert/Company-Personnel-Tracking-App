   M S B A M L   `   `   `  ����Y  UCompany Personnel Tracking App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Company_Personnel_Tracking_App.Views$Company_Personnel_Tracking_App.Views  5   	   >    8Company_Personnel_Tracking_App.Views.AddPermissionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlocal2clr-namespace:Company_Personnel_Tracking_App.Views     5   	   -   5          � Title$  Permission Page��5   	   ���	#FF8EAFF7 � 
ResizeMode$ NoResize=�68   $
��Bold"�6N    � WindowStartupLocation$ CenterScreen=�6`   $��18(�6�   $	��450��6�   $	��450��6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��User No��6   $��34��6"   $	��100��6.   $
��Left=�6:   $��Center=�6U   �� 5   
   -   5         ��Name$ 	txtUserNo��5      ${�False��6%   $��1��67   $��26��6G   $��0,0,30,0q�6S   �� 5   
   $��
Start Date��6   $��34��6%   $��1��61   $
��Left=�6>   $��Center=�6Y   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dpStart��5      $��1��6$   $��1��64   6A   �� 5   
   $��End Date��6   $��34��6#   $��2��6/   $	��100��6<   $
��Left=�6H   $��Center=�6c     5   
   -   5        $��dpEnd��5      $��1��6"   $��2��62   6?   �� 5   
   $��
Day Amount��6   $��34��6%   $��3��61   $	��120��6>   $
��Left=�6J   $��Center=�6e   �� 5    
   -   5        $ txtDayAmount��5       ${�False��6(   $��3��6:   $��1��6G   $��26��6W   $��0,0,30,0q�6c   �� 5!   
   $��
Explnation��6   $��34��6%   $	��120��61   $
��Left=�6=   $��Center=�6X   $��4��6s   �� 5"   
   -   5        $ txtExplanation��5"      $��4��6*   $��2��67   $��1��6H   $��0,0,30,0q�6X   $��Stretch=�6j   $��Stretch=�6�   � 5#   
   $��7��6   $��2��6    5$      �� 5%      �� 5&      .+�5(      �� -   5         ��Name$ btnSave��5(      $
��Save��6&   $��7��65   $��Right=�6B   $��0,0,0,0q�6^   $	��140��6o   6{   �� 5)      -   5        $ btnClose��5)      $��Close��6'   $��7��67   $��Center=�6D   $��0,0,0,0q�6a   $	��140��6r   $��1��6~   6�   5*      5+      5,      