   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      &     SistemaGestion.View.ClientesView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:SistemaGestion.View     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Agregar Nuevo Cliente��6   $��24(�5      $
��Bold"�5      $��Center=�5      $��	0,20,0,20q�5      
����  �5      � 5   
   $��1��6   $
��50,0q�6     5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       $��* �6#   .+�5#      �� $��Nombres��6   $��0��6&   $��0��63   $��Center=�5$      $
��0,10q�6/   �� 5%      -   5         ��Name$ 
txtNombres��5%      $��0��5&      $��1��6#   $
��0,10q�5'      $��5q�5(      �� 5*      $��Cédula��6   $��1��6%   $��0��62   $��Center=�5+      $
��0,10q�6/   �� 5,      -   5        $ 	txtCedula��5,      $��1��5-      $��1��6#   $
��0,10q�5.      $��5q�5/      �� 51      $��Celular��6   $��2��6&   $��0��63   $��Center=�52      $
��0,10q�6/   �� 53      -   5        $ 
txtCelular��53      $��2��54      $��1��6#   $
��0,10q�55      $��5q�56      �� 58      $��
Dirección��6   $��3��6(   $��0��65   $��Center=�59      $
��0,10q�6/   �� 5:      -   5        $ txtDireccion��5:      $��3��5;      $��1��6#   $
��0,10q�5<      $��5q�5=       �� TextWrapping$
 Wrap=�5>       �� AcceptsReturn$
 True��5?      $��60��5@      �� 5B      $��4��6   $��0��6&   $��2��66    �� Orientation$ 
Horizontal=�5C      $��Center=�5D      $
��0,20q�5E      .+�5F      �� -   5         ��Name$ 
btnGuardar��5K      $��Guardar��5F      $	��100��5G      $��0,0,10,0q�5H      
����  �
�������5K      �� 5L      -   5        $ btnCancelar��5O      $��Cancelar��5L      $	��100��5M      
�������5O      5P      5Q      5R      5S      