   M S B A M L   `   `   `  ����Y  UCompany Personnel Tracking App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Company_Personnel_Tracking_App.Views$Company_Personnel_Tracking_App.Views  5      7    1Company_Personnel_Tracking_App.Views.EmployeeList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:Company_Personnel_Tracking_App.Views     5      -   5        $��18(�5   #   $
��Bold"�61   .��5	      �   � RowDefinitions  5
   
   �� 5      $��3* �6   �� 5      $��6* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ gridEmployee��5      $��1��6)   $��5��66      
IsReadOnly$
 True��6J      AutoGenerateColumns$ False��6\      SelectionMode$ Single=�6x      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 61   	   
Visibility$	 Hidden=�6R   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *Id+6R     5      $ UserNo��6%   $ 75 65   
 6@   �� *UserNo+  5      $
 Name��6%   $	 145 63   
 6?   �� *Name+  5      $ Surname��6%   $	 145 66   
 6B   �� *	Surname+  5      $ Position Name��6%   $	 180 6<   
 6H   �� *PositionName+  5      $ Department Name��6%   $	 170 6>   
 6J   �� *DepartmentName+  5      $ Salary��6%   $ * 65   
 6?   �� *Salary+  5!      $ 
PositionId��6%   $ * 69   $	 Hidden=�6b   
 6C   �� *
PositionId+6b     5"      $ DepartmentId��6%   $ * 6;   $	 Hidden=�6f   
 6E   �� *DepartmentId+6f     5#      $ isAdmin��6%   $ * 66   $	 Hidden=�6\   
 6@   �� *	isAdmin+6\     5$      $ Birthday��6%   $ * 67   $	 Hidden=�6^   
 6A   �� *
Birthday+6^     5%      $ Address��6%   $ * 66   $	 Hidden=�6\   
 6@   �� *	Address+6\     5&      $ Password��6%   $ * 67   $	 Hidden=�6^   
 6A   �� *
Password+6^     5'      $ 	ImagePath��6%   $ * 68   $	 Hidden=�6`   
 6B   �� *	ImagePath+6`   5+      � 5-   
   $��5��6     5.      �� 5/      �� 50      �� 51       53      �� 54      $��60 �6#   �� 55      $	��100 �6#   �� 56      $��60 �6#   �� 57      $	��100 �6#   �� 58      $��80 �6#   .+�5;      �� $��UserNo��6   $��34��6%   $��90��61   $
��Left=�6<   $��Center=�6W   $��2��6r   �� 5<      -   5         ��Name$ 	txtUserNo��5<      $��1��6)    �� TextWrapping$
 Wrap=�69   $��66,10,10,10q�6M   $��3��6b   �� 5>      $
��Name��6   $��1��6#   $��34��60   $��90��6<   $
��Left=�6G   $��Center=�6b   $��2��6}   �� 5?      -   5        $ txtName��5?      $��1��6'   $��1��64   $
 Wrap=�6D   $��66,10,10,10q�6X   $��3��6m   �� 5A      $��Surmame��6   $��2��6&   $��34��63   $��98��6?   $
��Left=�6J   $��Center=�6e   $��2��6�   �� 5B      -   5        $ 
txtSurname��5B      $��2��6*   $��1��67   $
 Wrap=�6G   $��66,10,10,10q�6[   $��3��6p   �� 5D      $��
Department��6   $��4��6)   $��
10,8,-47,0q�69   $��34��6M   $	��Top=�6Y   �� 5E      -   5        $��cmbDepartment��5E      $��4��6,   $��Center=�6<   $��30��6W   $��132,0,-240,0q�6c   6y   �� 5G      $��Position��6   $	��Top=�6'   $��34��6?   $��4��6K   $��1��6[   $��
10,4,-42,0q�6h   �� 5H      -   5        $��cmbPosition��5H      $��4��6*   $��1��6:   $	��Top=�6G   $��30��6_   $��132,7,-240,0q�6k   �� 5J      -   5        $��	btnSearch��5J   6   $��4��6   $��Search��6%   $��343,5,-390,5q�6G   6]   �� 5K      -	   5        $��btnClear��5K   B   $��4��6   $��1��6%   $��Clear��62   $��343,2,-390,8q�6R   5O      �� 5Q   
   -
   5         ��Name$ btnAdd��5Q      $	��Add��6!   $��18(�6/   $
��Bold"�6=   $��1��6O   $��2��6_   $��	40,5,40,5q�6l   6   �� 5R   
   -   5        $ 	btnUpdate��5R      $��Update��6$   $��18(�65   $
��Bold"�6C   $��2��6U   $��2��6e   $��	40,5,40,5q�6r   6�   �� 5S   
   -   5        $ 	btnDelete��5S      $��Delete��6$   $��18(�65   $
��Bold"�6C   $��3��6U   $��2��6e   $��	40,5,40,5q�6r   6�   5U      5V      