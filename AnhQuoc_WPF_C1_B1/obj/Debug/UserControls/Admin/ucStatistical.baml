   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      %    AnhQuoc_WPF_C1_B1.ucStatistical   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $	��180 �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��Center=�6   $��0��5      .+�5      �� $��Center=�6    �� Orientation$ 
Horizontal=�5      .+�5      �� $��15(�6   $��5q�5      $��Dated:��5      $��Center=�5      (  "System.Windows.Controls.DatePicker  5      -   5        $
��date��5      
�������$��Center=�5      $��Center=�5      5      5       5!      �� 5"   
   $
��10 0q�6   ���#f8f9fd$��2��5$      $��1��5%      .+�5&      �� $��15(�6   $��5q�5'      $��
Order list��5(      $��Center=�5)      �� 5*      ��. $
��Auto=�5+      .��5,      &   System.Windows.Controls.DataGrid  -   5        $��dgOrders��5-         
IsReadOnly . $	��120��5.         AutoGenerateColumns .     Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6-      Header$ Id��6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *Id+6O     53      $	 150 6-   $ Customer��6[    69   �� *Customer.Name+6[     54      $	 150 6-   $ PayMent��6U    69   �� *	PayMent+6U     55      $ * 6-   $
 Date��6q    67   �� *Date+	 �� StringFormat$	 {0:dd/MM/yyyy}��6q   57      58      59      �� 5:   
   $
��10 0q�6   $��2��5;      ���#f8f9fd$��2��5=      .+�5>      �� $��15(�6   $��5q�5?      $��Detail of Order��5@      $��Center=�5A      �� 5B      ��. $
��Auto=�5C      .��5D        -   5        $��dgOrderDetails��5F       . $��Stretch=�5E      $	��150��5G      
������� .   5J        5K      $ 80 6-   $ Id��6T    68   �� 
 �� Path$
 Id�6T     5L      $ * 6-   $
 Seat��6^    67   �� $
 BookedSeat.Id�6^     5M      $ * 6-   $ Ticket type��6[    67   �� $
 
TicketType�6[     5N      $ * 6-   $ 	SeatPrice��6a    67   �� $
 BookedSeat.Price�6a     5O      $ * 6-   $ Discount��6a    67   �� $
 PayMent.Discount�6a     5P      $ * 6-   $ TotalPayment��6^    67   �� $
 PayMent.Price�6^   5R      5S      5T      �� 5U   
   $��Center=�6   $��Center=�5V      $��1��5W      .+�5X      �� $��Center=�6   $ 
Horizontal=�5Y      .+�5Z      �� 
��� � �$��15(�5[      $��Revenue:��5\      $��70��5]      �� 5^      -   5        $��
txtRevenue��5c      $��15(�5^      $��Center=�5_      $��0q�5`      
������  �� 
IsReadOnly . $	��100��5d      5e      5f      5g      5h      