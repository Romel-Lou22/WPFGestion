   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      '    !SistemaGestion.View.ProductosView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:SistemaGestion.View     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � 	Resources  5         ModernButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value  ModernDataGridStyle&   System.Windows.Controls.DataGrid # ModernDataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%&  &	        &	 �    &	 K    �� 5   
   " ��6*   .�5      ��  � �� �#34495e6+   �� 5       � ��
 �����6+   �� 5       � ��$	 8,4q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 4��5   !   $
 True��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5       5      P� 5       �@J�IsMouseOver$
	 True��6=   . �5    "   ��  � �� �#2c3e506?   5!      P� 5"       � ��$
	 True��6;   . �5#   "   ��  � �� �#2c3e506?   5$      5&      5(      5)      �� 5,   
   "  6,   .�5-      ��  � �� �#34495e6+   �� 5.       � ��
 �����6+   �� 5/       �@ RowBackground �#34495e6.   �� 50        �@ AlternatingRowBackground �#3b566e69   �� 51       � �� �#2c3e506,   �� 52        �@ HorizontalGridLinesBrush �#2c3e5069   �� 53       �@ VerticalGridLinesBrush �#2c3e5067   54      �� 57   
   "  68   .�58      ��  � �� �#2c3e506+   �� 59       � ��
 �����6+   �� 5:       � ��$
 Bold"�6+   �� 5;       � ��$ 0q�60   5<      5=      .��5@      � 
������ $��10q�6$   .+�5A   
   �� $��1q�6   ��5B      �� 5D      
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5E      �  � Color$ #2c3e50��6#    � Offset$ 0Q�63   � 5F      $ #34495e��6#   $ 1Q�63   5G      ._�5J      � $��15q�6    � RowDefinitions 5K      �� 5L      $
��Auto �6$   �� 5M      $
��Auto �6$   �� 5N      $��* �6$   �� 5O      $
��Auto �6$   .+�5S      �� $��Gestión de Productos��6   $��0��5T      $��Center=�5U      $��28(�5V      $
��Bold"�5W      $��Italic$�5X      
�������5Y      � 5]      $��1��6   $��	0,10,0,10q�6$    � ColumnDefinitions 5^      �� 5`      $��* �6+   �� 5b      $
��Auto �6+   .+�5f      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5g      �� $	��250��6"   $��5q�6.    � ToolTip$ Buscar producto...��5i      
����������#34495e$��1q�5k      ��5h      �� *FiltroBusqueda+ �� UpdateSourceTrigger$ PropertyChanged=�5k      �� 5l      #��[  6!   ���#2c3e50��5n      �� *BuscarProductoCommand+.��5o      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5p   "   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6/   $��16��5q      $��16��6#   $��0,0,5,0q�5r         
Foreground
 �����5s      �� 5t   "   $��Buscar��6,   
�������$��Center=�5v      5w      5x      5y      �� 5|      $��1��6!   $ 
Horizontal=�61   $��Center=�6J   .+�5~      �� #��[  6!   ���#2c3e50��5�      �� *AgregarProductoCommand+.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ CartPlus=�6/   $��16��5�      $��16��6#   $��0,0,5,0q�5�      
 �����5�      �� 5�   "   $��Nuevo��6,   
�������$��Center=�5�      5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�      �� *EditarProductoCommand+��5�      �� *SelectedItem+ �� ElementName$ ProductosGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $
 Edit=�6/   $��16��5�      $��16��6#   $��0,0,5,0q�5�      
 �����5�      �� 5�   "   $��Editar��6,   
�������$��Center=�5�      5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�      �� *EliminarProductoCommand+��5�      �� *SelectedItem+$ ProductosGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ TrashCan=�6/   $��16��5�      $��16��6#   $��0,0,5,0q�5�      
 �����5�      �� 5�   "   $��Eliminar��6,   
�������$��Center=�5�      5�      5�      5�      5�        5�      -   5          Name$ ProductosGrid��5�      $��2��62      AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��6'      SelectionMode$ Single=�5�      #��[ 5�         ColumnHeaderStyle# [ 5�         HeadersVisibility$ Column=�5�      ��5�   ?   �� *	Productos+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Nombre+6X     5�      $ Precio��6-   $	 120 6h    6=   �� *Precio+  �� StringFormat$  C��6h     5�      $ Código��6-   $	 160 6^    6=   �� *CodigoBarras+6^     5�      $ Fecha de Caducidad��6-   $	 120 5�       5�      �� *FechaCaducidad+$  {0:d}��5�        5�      $ 
Categoría��6-   $	 150 5�       5�      �� *	Categoria+5�      4	  .System.Windows.Controls.DataGridCheckBoxColumn	  5�      $ Activo��61   $ 80 6\    6A   �� *Estado+6\   5�      �� 5�      $��3��6   $��Right=�5�      $��Italic$�68   
���������5�      �� *Productos.Count+$  Total de productos: {0}��5�   K   5�      5�      5�      5�      