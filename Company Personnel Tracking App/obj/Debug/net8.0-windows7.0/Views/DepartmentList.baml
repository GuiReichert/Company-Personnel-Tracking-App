   M S B A M L   `   `   `  ����Y  UCompany Personnel Tracking App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Company_Personnel_Tracking_App.Views$Company_Personnel_Tracking_App.Views  5      9    3Company_Personnel_Tracking_App.Views.DepartmentList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:Company_Personnel_Tracking_App.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��6* �6   �� 5      $��* �6   .+�5   
   � $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5        $��btnAdd��5   (   $��	40,5,40,5q�6   $	��Add��66   $��18(�6D   $
��Bold"�6R   6d   �� 5      -   5        $��	btnUpdate��5   (   $��	40,5,40,5q�6   $��Update��69   $��1��6J   $��18(�6Z   $
��Bold"�6h   6z   �� 5      -   5        $��	btnDelete��5   (   $��	40,5,40,5q�6   $��Delete��69   $��2��6J   $��18(�6Z   $
��Bold"�6h   6z   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ gridDepartment��5         
IsReadOnly$
 True��6+      AutoGenerateColumns$ False��6=      SelectionUnit$ FullRow=�6Y      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 61   	   
Visibility$	 Hidden=�6R   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *Id+6R     5      $ Department Name��6%   $
 auto 6>   
 6K   �� *DepartmentName+5      5!      5%      