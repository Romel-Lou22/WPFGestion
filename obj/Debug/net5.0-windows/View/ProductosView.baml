   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SistemaGestion.VistaModeloSistemaGestion.VistaModelo  5      9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      '    !SistemaGestion.View.ProductosView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >	viewmodel(clr-namespace:SistemaGestion.VistaModelo     5      3local!clr-namespace:SistemaGestion.View     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � 	Resources  5         ModernButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value  ModernDataGridStyle&   System.Windows.Controls.DataGrid # ModernDataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%&  &	        &	 �    &	 K    �� 5   
   " ��6*   .�5      ��  � �� �#34495e6+   �� 5       � ��
 �����6+   �� 5       � ��$	 8,4q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 4��5   	   $
 True��5    	   ._�5!      �� $��Center=�6/   $��Center=�5"      5#       5$      P� 5%       �@J�IsMouseOver$
	 True��6=   . �5&   "   ��  � �� �#2c3e506?   5'      P� 5(       � ��$
	 True��6;   . �5)   "   ��  � �� �#2c3e506?   5*      5,      5.      5/      �� 52   
   "  6,   .�53      ��  � �� �#34495e6+   �� 54       � ��
 �����6+   �� 55       �@ RowBackground �#34495e6.   �� 56        �@ AlternatingRowBackground �#3b566e69   �� 57       � �� �#2c3e506,   �� 58        �@ HorizontalGridLinesBrush �#2c3e5069   �� 59       �@ VerticalGridLinesBrush �#2c3e5067   5:      �� 5=   
   "  68   .�5>      ��  � �� �#2c3e506+   �� 5?       � ��
 �����6+   �� 5@       � ��$
 Bold"�6+   �� 5A       � ��$ 0q�60   5B      5C      .��5F      � 
������ $��10q�6$   .+�5G   
   �� $��1q�6   ��5H      �� 5J      
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5K      �  � Color$ #2c3e50��6#    � Offset$ 0Q�63   � 5L      $ #34495e��6#   $ 1Q�63   5M      ._�5P      � $��15q�6    � RowDefinitions 5Q      �� 5R      $
��Auto �6$   �� 5S      $
��Auto �6$   �� 5T      $��* �6$   �� 5U      $
��Auto �6$   .+�5Y      �� $��Gestión de Productos��6   $��0��5Z   
   $��Center=�5[   
   $��28(�5\   
   $
��Bold"�5]   
   $��Italic$�5^   
   
�������5_   
   � 5c      $��1��6   $��	0,10,0,10q�6$    � ColumnDefinitions 5d      �� 5f      $��* �6+   �� 5h      $
��Auto �6+   .+�5l      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5m      �� $	��250��6"   $��5q�6.    � ToolTip$ Buscar producto...��5o      
����������#34495e$��1q�5q      ��5n      �� *FiltroBusqueda+ �� UpdateSourceTrigger$ PropertyChanged=�5q      �� 5r      #��[  6!   ���#2c3e50��5t      �� *BuscarProductoCommand+.��5u      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5v   "   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6/   $��16��5w      $��16��6   $��0,0,5,0q�5x         
Foreground
 �����5y      �� 5z   "   $��Buscar��6,   
�������$��Center=�5|      5}      5~      5      �� 5�      $��1��6!   $ 
Horizontal=�61   $��Center=�6J   .+�5�      �� #��[  6!   ���#2c3e50��5�      �� *AbrirAgregarProductoCommand+.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ CartPlus=�6/   $��16��5�   
   $��16��6   $��0,0,5,0q�5�   
   
 �����5�   
   �� 5�   "   $��Nuevo��6,   
�������$��Center=�5�   
   5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�      �� *EditarProductoCommand+��5�      �� *SelectedItem+ �� ElementName$ ProductosGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $
 Edit=�6/   $��16��5�      $��16��6   $��0,0,5,0q�5�      
 �����5�      �� 5�   "   $��Editar��6,   
�������$��Center=�5�      5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�      �� *EliminarProductoCommand+��5�      �� *SelectedItem+$ ProductosGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ TrashCan=�6/   $��16��5�      $��16��6   $��0,0,5,0q�5�      
 �����5�      �� 5�   "   $��Eliminar��6,   
�������$��Center=�5�      5�      5�      5�      5�        5�      -   5          Name$ ProductosGrid��5�      $��2��62      AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��6!      SelectionMode$ Single=�5�      #��[ 5�         ColumnHeaderStyle# [ 5�         HeadersVisibility$ Column=�5�      ��5�   ?   �� *	Productos+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Nombre+6X     5�      $ Precio��6-   $	 120 6h    6=   �� *Precio+  �� StringFormat$  C��6h     5�      $ Código��6-   $	 160 6^    6=   �� *CodigoBarras+6^     5�      $ Fecha de Caducidad��6-   $	 120 5�       5�      �� *FechaCaducidad+$  {0:d}��5�        5�      $ 
Categoría��6-   $	 150 5�       5�      �� *	Categoria+5�      4	  .System.Windows.Controls.DataGridCheckBoxColumn	  5�      $ Activo��61   $ 80 6\    6A   �� *Estado+6\   5�      �� 5�      $��3��6   $��Right=�5�   
   $��Italic$�6&   
���������5�   
   �� *Productos.Count+$  Total de productos: {0}��5�   9   5�      5�      5�      5�      