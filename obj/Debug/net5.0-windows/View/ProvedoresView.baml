   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      (    "SistemaGestion.View.ProvedoresView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:SistemaGestion.View     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � 	Resources  5         ModernButtonStyle �� 
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
 Bold"�6+   �� 5;       � ��$ 0q�60   5<      5=      .��5A      � .+�5B   
   �� $��1q�6   ��5C      �� 5E      
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5F      �  � Color$ #2c3e50��6#    � Offset$ 0Q�63   � 5G      $ #34495e��6#   $ 1Q�63   5H      ._�5K      � $��15q�6    � RowDefinitions 5L      �� 5M      $
��Auto �6$   �� 5O      $
��Auto �6$   �� 5Q      $��* �6$   �� 5S      $
��Auto �6$   .+�5X      �� $��Gestión de Proveedores��6   $��0��5Y      $��Center=�5Z      $��28(�5[      $
��Bold"�5\      $��Italic$�5]      
�������5^      � 5b      $��1��6   $��	0,10,0,10q�6$    � ColumnDefinitions 5c      �� 5e      $��* �6+   �� 5g      $
��Auto �6+   .+�5k      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5l      �� $	��250��6"   $��5q�6.    � ToolTip$ Buscar proveedor...��5n   "   
����������#34495e$��1q�5p   "   ��5m   "   �� *FiltroBusqueda+ �� UpdateSourceTrigger$ PropertyChanged=�5p   "   �� 5q      #��[  6!   ���#2c3e50��5s   !   �� *BuscarProveedorCommand+.��5t      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5u   "   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6/   $��16��5v   /   $��16��6;   $��0,0,5,0q�5w   /      
Foreground
 �����5x   /   �� 5y   "   $��Buscar��6,   
�������$��Center=�5{   ,   5|      5}      5~      �� 5�      $��1��6!   $ 
Horizontal=�61   $��Center=�6J   .+�5�      �� #��[  6!   ���#2c3e50��5�   !   �� *AbrirAgregarProveedorCommand+.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ CartPlus=�6/   $��16��5�   /   $��16��6;   $��0,0,5,0q�5�   /   
 �����5�   /   �� 5�   "   $��Nuevo��6,   
�������$��Center=�5�   ,   5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�   !   �� *EditarProveedorCommand+��5�   !   �� *SelectedItem+ �� ElementName$ ProveedoresGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $
 Edit=�6/   $��16��5�   /   $��16��6;   $��0,0,5,0q�5�   /   
 �����5�   /   �� 5�   "   $��Editar��6,   
�������$��Center=�5�   ,   5�      5�      �� 5�      #��[  6!   ���#2c3e50��5�   !   �� *EliminarProveedorCommand+��5�   !   �� *SelectedItem+$ ProveedoresGrid��.��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $ TrashCan=�6/   $��16��5�   /   $��16��6;   $��0,0,5,0q�5�   /   
 �����5�   /   �� 5�   "   $��Eliminar��6,   
�������$��Center=�5�   ,   5�      5�      5�      5�        5�      -   5          Name$ ProveedoresGrid��5�      $��2��5�         AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��5�         SelectionMode$ Single=�5�      #��[ 5�         ColumnHeaderStyle# [ 5�         HeadersVisibility$ Column=�5�      ��5�      �� *Proveedores+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5�   -      MinWidth$ 80Q�5�   -   1  +System.Windows.Controls.DataGridBoundColumn    Binding  5�   -   �� *ProveedorId+5�   -     5�      $ Nombre��6-   $
 Auto 5�   -   $	 150Q�5�   -     5�   -   �� *Nombre+5�   -     5�      $ 	Teléfono��6-   $
 Auto 5�   -   $	 120Q�5�   -     5�   -   �� *
Telefono+5�   -     5�      $ Email��6-   $
 Auto 5�   -   $	 150Q�5�   -     5�   -   �� *Email+5�   -     5�      $ 
Dirección��6-   $
 Auto 5�   -   $	 200Q�5�   -     5�   -   �� *	Direccion+5�   -   5�      �� 5�      $��3��6   $��Right=�5�      $��Italic$�5�      
���������5�      �� *Proveedores.Count+! �� StringFormat$! Total de proveedores: {0}��5�      5�      5�      5�      5�      