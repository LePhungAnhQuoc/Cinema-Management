   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      +    %AnhQuoc_WPF_C1_B1.ucDateScheduleTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      -   5        .��5	      �� -   5        $��stkTable��5	      .+�5
   
   �� -   5        $��btnAdd��5      $��Right=�5
      ���#007bff
�������$��12(�5      $��5q�5      $��0,0,10,0q�6   $��Add New Date��5      $	��151��5   &   � 5   
     � ColumnDefinitions  5      �� 5      $	��150 �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      �� $��0��6   $
��Left=�5      .��6/   Choose Dates:6>   �� 5      -   5        $��cbDates��5      $��1��5      $��Stretch=�5       �� DisplayMemberPath$
 Date��5       �� ItemStringFormat$ {0:dd/MM/yyyy}��5        �� SelectedItem 5      �� *CurrentItem+��5      �� *DateSchedules+5!      �� 5#      $��2��6    �� Orientation$ 
Horizontal=�5$      .+�5%      �� -   5        $��	btnDelete��5&      ���#dc3545
�������$��12(�5'      $��70��5(      $��10 0 0 0q�5)      $��5q�6+   .��5*   1   Delete69   �� 5+      -   5        $��	btnUpdate��5,      ���#28a745
�������$��12(�5-      $��	10 0 10 0q�5.      $
��15 5q�6,   .��5/   1   Update69   50      51      52      53      