   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      )    #AnhQuoc_WPF_C1_B1.ucScheduleBooking   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      -   5        .��5
      �� .+�5   
   � $
��10 5q�6     � ColumnDefinitions  5      �� 5      $	��130 �6#   �� 5      $��1* �6#   .+�5      �� $��0��6   $��-1��5      $��12(�5      $
��10 0q�5      $��Center=�5      $
��Left=�5      $��Cinema Type:��5      �� 5      -   5        $��cbCinemaTypes��5      $��1��5       �� 
IsEditable .  �� 
IsReadOnly . $��Center=�5       �� Text$ Select Cinema Type��5      $��5q�5      $��Center=�5      $��12(�5       �� DisplayMemberPath$ 
CinemaType��5!      ��5       �� *#!movieSchedule.CinemaTypeSchedules+5!      5"      � 5#   
   $
��10 5q�6     5$      �� 5%      $	��130 �6#   �� 5&      $��1* �6#   .+�5(      �� $��0��6   $��-1��5)      $��12(�5*      $
��10 0q�5+      $��Center=�5,      $
��Left=�5-      $��Cinema:��5.      �� 5/      -   5        $��	cbCinemas��54      $��1��5/       .  . $��Center=�52      $ Select Cinema��53      $��5q�55      $��Center=�56      $��12(�57      $ Cinema.Name��59      ��58      ��  �� ElementName$ cbCinemaTypes�� �� Path$" SelectedItem.CinemaSchedules�59      5:      �� 5;   
   
���   �$��1q�5<      $��0 15 0 0q�5=      ._�5>      �� .+�5?      �� $��Center=�6   .��65   Date and Time6D   � 5@      $
��10 5q�6     5A      �� 5B      $��1* �6+   �� 5C      $��2* �6+   .+�5F      �� -   5        $��lbDate��5H      $��0��5F      $��Center=�5G      $��5q�5I      $��Center=�5J      $��12(�5K      ��5L      �� $ 	cbCinemas��$  SelectedItem.DatesSchedule���5M      �� 5N      .R�5O   "   �� ��6,   �� $
 Date� �� StringFormat$ {0:dd/MM/yyyy}��5P      5R      �� 5S      -   5        $��lbTime��5U      $��1��5S      $��Center=�5T      $��5q�5V      $��Center=�5W      $��12(�5X      ��5Y      �� $ lbDate��$ SelectedItem.TimeShedules���5Z      �� 5[      .R�5\   "   �� ��6,   �� *Time+$ 
{0:hh\:mm}��5]      5_      5`      5a      5b      �� 5c   
   $��10q�6   $��Bottom=�5d       �� Orientation$ 
Horizontal=�5e      $��Center=�5f      .+�5g      �� 
���   �$��1q�5h      ._�5i      �� -   5        $��	btnCancel��5i      $��5q�5j      $��14(�5k      $��70��5l      ���#ffffff$��Center=�5n      $��Cancel��5o      5p      5q      �� 5r      -   5        $��
btnConfirm��5r      $��10 0 0 0q�5s      $��5q�5t      $��14(�5u      $��80��5v      ���#007bff
�������$��Center=�5y      $��Confirm��5z      5{      5|      5}      5~      