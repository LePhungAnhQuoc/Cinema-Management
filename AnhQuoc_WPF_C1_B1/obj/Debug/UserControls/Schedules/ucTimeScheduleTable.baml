   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      +    %AnhQuoc_WPF_C1_B1.ucTimeScheduleTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      -   5        .��5
      �� -   5        $��stkTable��5
      .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgTable��5          
IsReadOnly  . $��Stretch=�5         AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$
 Time��6f   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6/   �� *Time+ �� StringFormat$ 
{0:hh\:mm}��6f   4  .System.Windows.Controls.DataGridTemplateColumn  5      $	 180 6)      CellTemplate 5      �� 5      .R�5      �� $
��auto��6)   $��Center=�5   )    �� Orientation$ 
Horizontal=�5   )   .+�5   "   �� $��	btnDelete��5   )   -   5        ���#dc3545
�������$��12(�5   )   $��10 5 0 5q�5   )   $
��15 5q�6;   .��5   A   Delete6I   �� 5   "   $��	btnUpdate��5   )   -   5        ���#28a745
�������$��12(�5   )   $��	10 5 10 5q�5   )   $
��15 5q�6<   .��5    A   Update6I   5!      5"      5$      5&      �� 5'   
   -   5        $��	stkAddBtn��5'      $
��0 10q�5(      $ 
Horizontal=�5)      $��Center=�5*      .+�5+      �� -   5        $��btnAdd��5-      $��Center=�5+      ���#007bff
�������$��12(�5.      $
��15 5q�5/      $��Add New Time Schedule��50      51      52      53      54      