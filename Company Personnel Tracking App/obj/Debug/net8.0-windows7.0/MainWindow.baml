   M S B A M L   `   `   `  ����Y  UCompany Personnel Tracking App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Company_Personnel_Tracking_AppCompany_Personnel_Tracking_App  5   	   [2clr-namespace:Company_Personnel_Tracking_App.Views$Company_Personnel_Tracking_App.Views  5   	   e7clr-namespace:Company_Personnel_Tracking_App.ViewModels)Company_Personnel_Tracking_App.ViewModels  5   	   /    )Company_Personnel_Tracking_App.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:Company_Personnel_Tracking_App     5   	   DViews2clr-namespace:Company_Personnel_Tracking_App.Views     5   	   N
ViewModels7clr-namespace:Company_Personnel_Tracking_App.ViewModels     5   	   -   5          �Name$  PersonnelMainWindow��5   	    � Title$ Personnel Tracking App��5   	   $	��540��6(   $
��1200��65   $��24(�6B   $
��Bold"�6P   ���	#FFA9AAFF � WindowStartupLocation$ CenterScreen=�6y    � 
ResizeMode$ NoResize=�6�   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��1��6   $��1��6=   $��8��6J   $	��NaN��6[   $	��NaN��6h   ��6)   �� 6h   �� 5   
   -   5         ��Name$ lblWindowName��5      $��1��6'   $��Window's Name��67   $��Center=�6O   $��Center=�6l   $��36(�6�   �� 5   
   $��1��6   $��0,2,0,0q�6"   .+�5       �� -   5        $��btnEmployee��5    "   $��1��6   $��60��65   $	��250��6A   $
��Left=�6M   .��5!      � $	��230��6    5"      �� 5#      $
��auto �6+   .+�5%      �� $��Employee��6    $��1��60   $
��Left=�6@   $��Center=�6[   5&      5(      5+      �� 5-   
   $��2��6   $��0,2,0,0q�6"   .+�5.      �� -   5        $��btnTask��5.   "   $��2��6   $��60��61   $	��250��6=   $
��Left=�6I   .��5/      � $	��230��6    50      �� 51      $
��auto �6+   .+�53      �� $
��Task��6    $��1��6,   $
��Left=�6<   $��Center=�6W   54      56      59      �� 5<   
   $��3��6   $��0,2,0,0q�6"   .+�5=      �� -   5        $��	btnSalary��5=   "   $��3��6   $��60��63   $	��250��6?   $
��Left=�6K   .��5>      � $	��230��6    5?      �� 5@      $
��auto �6+   .+�5B      �� $��Salary��6    $��1��6.   $
��Left=�6>   $��Center=�6Y   5C      5E      5H      �� 5K   
   $��4��6   $��0,2,0,0q�6"   .+�5L      �� -   5        $��btnPermissions��5L   "   $��4��6   $��60��68   $	��250��6D   $
��Left=�6P   .��5M      � $	��230��6    5N      �� 5O      $
��auto �6+   .+�5Q      �� $��Permissions��6    $��1��63   $
��Left=�6C   $��Center=�6^   5R      5T      5W      �� 5Y   
   $��5��6   $��0,2,0,0q�6"   .+�5Z      �� -   5        $��btnDepartment��5Z   "   $��5��6   $��60��67   $	��250��6C   $
��Left=�6O   .��5[      � $	��230��6    5\      �� 5]      $
��auto �6+   .+�5_      �� $��
Department��6    $��1��62   $
��Left=�6B   $��Center=�6]   5`      5b      5e      �� 5h   
   $��6��6   $��0,2,0,0q�6"   .+�5i      �� -   5        $��btnPosition��5i   "   $��6��6   $��60��65   $	��250��6A   $
��Left=�6M   .��5j      � $	��230��6    5k      �� 5l      $
��auto �6+   .+�5n      �� $��Position��6    $��1��60   $
��Left=�6@   $��Center=�6[   5o      5q      5t      �� 5w   
   $��7��6   $��0,2,0,0q�6"   .+�5x      �� -	   5        $��	btnLogOut��5x   "   $��7��6   $��60��63   $	��250��6?   $
��Left=�6K   .��5y      � $	��230��6    5z      �� 5{      $
��auto �6+   .+�5}      �� $��Log Out��6    $��1��6/   $
��Left=�6?   $��Center=�6Z   5~      5�      5�      �� 5�   
   $��9��6   $��0,2,0,0q�6"   .+�5�      �� -
   5        $��btnExit��5�   "   $��9��6   $��60��61   $	��250��6=   $
��Left=�6I   .��5�      � $	��230��6    5�      �� 5�      $
��auto �6+   .+�5�      �� $
��Exit��6    $��1��6,   $
��Left=�6<   $��Center=�6W   5�      5�      5�      5�       � 	Resources 5�       �� DataTypeC   =Company_Personnel_Tracking_App.ViewModels.DepartmentViewModel9   3Company_Personnel_Tracking_App.Views.DepartmentListA   ;Company_Personnel_Tracking_App.ViewModels.PositionViewModel7   1Company_Personnel_Tracking_App.Views.PositionListA  ;Company_Personnel_Tracking_App.ViewModels.EmployeeViewModel7   1Company_Personnel_Tracking_App.Views.EmployeeList=  7Company_Personnel_Tracking_App.ViewModels.TaskViewModel3   -Company_Personnel_Tracking_App.Views.TaskList?	  9Company_Personnel_Tracking_App.ViewModels.SalaryViewModel5
   /Company_Personnel_Tracking_App.Views.SalaryListC   =Company_Personnel_Tracking_App.ViewModels.PermissionViewModel9   3Company_Personnel_Tracking_App.Views.PermissionList��%t  (��       5�   3   *, +)(�� R     5�   1   *, +)(�� �     5�   1   *, +)(�� �     5�   -   *, +)(�� >    5�   /   *,	 +)(�� �    5�   3   *, +)�� 5�   
   DepartmentTemplate��"  63   .R�5�        5�      5�      �� 5�   
   PositionTemplate��"  61   .R�5�        5�      5�      �� 5�   
   EmployeeTemplate��"  61   .R�5�        5�      5�      �� 5�   
   TaskTemplate��"  6-   .R�5�        5�      5�      �� 5�   
   SalaryTemplate��" 	 6/   .R�5�      
  5�      5�      �� 5�   
   PermissionTemplate��"  63   .R�5�        5�      5�      5�      5�      