   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      &     SistemaGestion.View.ClientesView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression.blend/2008     5      3local!clr-namespace:SistemaGestion.View     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � 	Resources  5         ModernButtonStyle �� 
TargetType �� Property �� Value  ModernDataGridStyle&   System.Windows.Controls.DataGrid # ModernDataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  &	        &	     &	 �    �� 5   
   " ��6*   .�5      ��  � �� �#34495e6+   �� 5       � ��
 �����6+   �� 5       � ��$	 8,4q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   5      �� 5   
   "  6,   .�5      ��  � �� �#34495e6+   �� 5       � ��
 �����6+   �� 5       �@ RowBackground �#34495e6.   �� 5        �@ AlternatingRowBackground �#3b566e69   5      �� 5   
   "  68   .�5      ��  � �� �#2c3e506+   �� 5       � ��
 �����6+   �� 5        � ��$
 Bold"�6+   5!      5"      .��5%      � 
������ $��10q�6$   .+�5&   
   �� $��1q�6   ��5'      �� 5(       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5)      �  � Color$ #2c3e50��6#    � Offset$ 0Q�63   � 5*      $ #34495e��6#   $ 1Q�63   5+      ._�5.      � $��15q�6    � RowDefinitions 5/      �� 50      $
��Auto �6$   �� 51      $
��Auto �6$   �� 52      $��* �6$   �� 53      $
��Auto �6$   .+�57      �� $��Gestión de Clientes��6   $��0��58      $��Center=�59      $��28(�5:      $
��Bold"�5;      $��Italic$�5<      
�������5=      � 5A      $��1��6   $��	0,10,0,10q�6$   	 � ColumnDefinitions	 5B      �� 5C      $��* �6+   �� 5D      $
��Auto �6+   .+�5G      �� 
 �� Orientation$
 
Horizontal=�6!   $��Center=�6:   .+�5H      �� $	��250��6"   $��5q�6.    � ToolTip$ Buscar cliente...��5J   "   
����������#34495e$��1q�5L   "   ��5I   "   �� *FiltroBusqueda+ �� UpdateSourceTrigger$ PropertyChanged=�5L   "   �� 5M      #��[  6!   ��6L   �� *BuscarClienteCommand+.��5N      �� $
 
Horizontal=�6)   $��Center=�6B   .+�5O   "   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6/   $��16��6=   $��16��6I   $��0,0,5,0q�6T      
Foreground
 �����6e   �� 5P   "   $��Buscar��6,   
�������$��Center=�6M   5Q      5R      5S      �� 5U      $��1��6!   $
 
Horizontal=�61   $��Center=�6J   .+�5V      �� #��[  6!   ��6L   �� *AbrirAgregarClienteCommand+.��5W      �� $
 
Horizontal=�6)   $��Center=�6B   .+�5X   "     $ UserPlus=�6/   $��16��6?   $��16��6K   $��0,0,5,0q�6V   
 �����6g   �� 5Y   "   $��Nuevo��6,   
�������$��Center=�6L   5Z      5[      �� 5]      #��[  6!   ��6L   �� *EditarClienteCommand+��6u   �� *SelectedItem+ �� ElementName$ ClientesGrid��.��5^      �� $
 
Horizontal=�6)   $��Center=�6B   .+�5_   "     $
 Edit=�6/   $��16��6;   $��16��6G   $��0,0,5,0q�6R   
 �����6c   �� 5`   "   $��Editar��6,   
�������$��Center=�6M   5a      5b      �� 5d      #��[  6!   ��6L   �� *EliminarClienteCommand+��6w   �� *SelectedItem+$ ClientesGrid��.��5e      �� $
 
Horizontal=�6)   $��Center=�6B   .+�5f   "     $ TrashCan=�6/   $��16��6?   $��16��6K   $��0,0,5,0q�6V   
 �����6g   �� 5g   "   $��Eliminar��6,   
�������$��Center=�6O   5h      5i      5j      5k        5o      -   5          Name$ ClientesGrid��5o      $��2��61      AutoGenerateColumns$ False��5p         
IsReadOnly$
 True��67      SelectionMode$ Single=�6I   #��[ 5q         ColumnHeaderStyle# [ 5r         HeadersVisibility$ Column=�5s      ��5o   >   �� *
Clientes+   Columns 5t      0  *System.Windows.Controls.DataGridTextColumn  5u      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Nombre+6X     5v      $ 	Teléfono��6-   $	 120 6\    6?   �� *
Telefono+6\     5w      $ Email��6-   $	 160 6V    6<   �� *Email+6V     5x      $ 
Dirección��6-   $	 200 6^    6@   �� *	Direccion+6^     5y      $ Cedula��6-   $ 80 6X    6=   �� *Cedula+6X   5{      �� 5      $��3��6   $��Right=�5�      $��Italic$�68   
���������5   )   �� *Clientes.Count+ �� StringFormat$ Total de clientes: {0}��5�   K   5�      5�      5�      5�      