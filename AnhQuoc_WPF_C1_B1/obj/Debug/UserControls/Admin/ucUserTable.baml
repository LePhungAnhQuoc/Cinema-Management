   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      #    AnhQuoc_WPF_C1_B1.ucUserTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      -   5          � 	Resources  5
      -   'AnhQuoc_WPF_C1_B1.StatusToLockConverter   statusToLockConverter-   'AnhQuoc_WPF_C1_B1.StatusToTextConverter  statusToTextConverter��%:   &	        &	        5   
   6&     5   
   6&   5      .��5      �� $��1��6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgTable��5      $��Stretch=�5         
IsReadOnly .    AutoGenerateColumns .     RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      ��  �� Property � �� �� Value$ Center=�6A   5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6%   	   Header$	 Username��6M   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 60   �� *
Username+6M     5      $ 1* 6%   $	 Password��6N   
 61   �� *
Password+6N     5      $	 100 6%   $	 Status��6~   
 61   �� *Status+ �� 	Converter# [ 6~   4	  .System.Windows.Controls.DataGridTemplateColumn	  5      $
 auto 6)    	  CellTemplate 5      �� 5      .R�5      �� $
��auto��6)   $��Center=�5    )    �� Orientation$ 
Horizontal=�5!   )   .+�5"   "   �� $��btnLockState��5#   )   -   5        
��� ���
�������$��12(�5$   )   $��10 5 0 5q�5%   )   $
��15 5q�6;   $��70��5&   )   ��5(   )   �� *Status+# [  5)   #   �� 5*   "   $��	btnUpdate��5+   )   -   5        ���#28a745
�������$��12(�5,   )   $��	10 5 10 5q�5-   )   $
��15 5q�6<   .��5.   A   Update6I   5/      50      52      54      �� 55   
   $
��0 10q�6   $ 
Horizontal=�56      $��Center=�57      .+�58      �� -   5        $��Center=�58      ���#007bff
�������$��12(�5:      $
��15 5q�5;      $��Add New User��5<      5=      5>      5?      5@      