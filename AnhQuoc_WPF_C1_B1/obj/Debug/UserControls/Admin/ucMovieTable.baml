   M S B A M L   `   `   `  ����L  HAnhQuoc_WPF_C1_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AnhQuoc_WPF_C1_B1AnhQuoc_WPF_C1_B1  5      $    AnhQuoc_WPF_C1_B1.ucMovieTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AnhQuoc_WPF_C1_B1     5      -   5        .��5
      �� $
��Auto=�6   ��. .��5   
   �� .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dgTable��5          
IsReadOnly  . $��Center=�5         AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6@   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Id+6@     5      $
 Name��6B    6)   �� *Name+6B     5      $ Rated��6F    6)   �� *
Rated.Id+6F     5      $ Running Time��6I    6)   �� *RunningTime+6I     5      $ ReleaseDate��6I    6)   �� *ReleaseDate+6I   4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5   "   �� $��	btnDelete��5   )   -   5        ���#dc3545
�������$��12(�5   )   $��10q�5   )   $
��15 5q�65   .��5   A   Delete6I   5      5!        5"       5#      �� 5$      .R�5%   "   �� $��	btnUpdate��5&   )   -   5        ���#28a745
�������$��12(�5'   )   $��10q�5(   )   $
��15 5q�65   .��5)   A   Update6I   5*      5,      5.      �� 5/      $
��0 10q�6    �� Orientation$ 
Horizontal=�50      $��Center=�51      .+�52      �� -   5        $��btnAdd��54      $��Center=�52      ���#007bff
�������$��12(�55      $
��15 5q�56      $	��Add��57      $��70��58      59      5:      5;      5<      5=      