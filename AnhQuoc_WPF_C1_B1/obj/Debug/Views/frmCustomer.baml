   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5   	   #    AnhQuoc_WPF_C1_B1.frmCustomer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:AnhQuoc_WPF_C1_B1     5   	   -   5          � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ frmCustomer��5
   	   $	��300��6   $	��350��6*   .��5      �� .+�5   
   �� $��30 30 30 25q�6   $��Center=�5      $��20(�5      $��Customer Infomation��5      � 5   
   $
��30 5q�6    � ColumnDefinitions 5      �� 5      $��90 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��15(�5      $��5q�5      $��Name:��5      $
��Left=�5      �� 5      -   5        $��txtCustomerName��5      $��1��5      $��5q�5      $��0��5      $��15(�5      $��35��5       5!      5"      � 5#   
   $
��30 5q�6    5$      �� 5%      $��90 �6#   �� 5&      $��* �6#   .+�5(      �� $��0 10 0 0q�6   $��1��5)      $��15(�5*      $��5q�5+      $��Phone:��5,      $
��Left=�5-      �� 5.      -   5        $��txtPhone��51      $��0 10 0 0q�5.      $��1��5/      $��1��50      $��15(�52      $��5q�53      $��0��54      $��35��55      56      57      �� 58   
   $��40q�6   $��Bottom=�59       �� Orientation$ 
Horizontal=�5:      $��Center=�5;      .+�5<      �� 
���   �$��1q�5=      ._�5>      �� -   5        $��	btnCancel��5>      $��5q�5?      $��14(�5@      $��70��5A      ���#ffffff$��Center=�5C      $��Cancel��5D      5E      5F      �� 5G      -   5        $��
btnConfirm��5G      $��10 0 0 0q�5H      $��5q�5I      $��14(�5J      $��80��5K      ���#007bff
�������$��Center=�5N      $��Confirm��5O      5P      5Q      5R      5S      