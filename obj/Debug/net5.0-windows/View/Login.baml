   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SistemaGestion.VistaModeloSistemaGestion.VistaModelo  5   	   M+clr-namespace:SistemaGestion.CustomControlsSistemaGestion.CustomControls  5   	       SistemaGestion.View.Login   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >	viewmodel(clr-namespace:SistemaGestion.VistaModelo     5   	   Fcustomcontrols+clr-namespace:SistemaGestion.CustomControls     5   	   -   5          � Title$  Login��5	   	   $	��550��6   $	��800��6$    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      /   )SistemaGestion.VistaModelo.LoginViewModel  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'   	 �� 	Converter#	 [  65   .��5!      �� 
 �� CornerRadius$
 12��6   ��5"   
   �� 5#       �� ImageSource$ /Images/permiso.png��6    j� Stretch$
 None=�6;   ._�5'   
   �� $
 10��6   $��1q�6#    J� Opacity$
 0.95Q�67   ��5(      �� 5)       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5*      �  � Color$ #462ADB��6#    � Offset$ 0Q�63   � 5+      $ #DA43AE��6#   $
 0.75Q�63   � 5,      $ #8A16C1��6#   $ 1Q�63   5-      ��51      �� 52      $	 0,1&�6&   $	 1,0&�67   .��53      � $ #060531��6#   $ 0Q�63   � 54      $ #1B144B��6#   $
 0.75Q�63   55      ._�59      �  � RowDefinitions 5:      �� 5;      $��30 �6$   �� 5<      .+�5@      �  � ColumnDefinitions 5A      �� 5B      �� 5C      $��25 �6+   �� 5D      $��25 �6+   �� 5E      $��5 �6+   .+�5H      �� $��LOG IN��6    
�������$��10(�5J       $��
Montserrat)�6.   $��0��5K       $��Center=�60   $��10,0,0,0q�5L       �� 5O      -   5         ��Name$ btnMinimize��5O      $��0q�62   $��-��5P      
�������$��16(�5Q      $��
Montserrat)�6+    � Cursor$
 Hand��5R      $��1��6+   ��5S      �� 5T       �� 
TargetType" ��6$   .�5U   "   ��  �� Property � �� �� Value �#28AEED6?    �� Triggers 5V   "   P� 5W   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   5Y      ��5[      �� 5\       �� 
TargetType" ��6.   .R�5]   "   �� $��18��6)   $��18��64   $
 9��6@   #��z��6Q   ._�5^   &   �� $��Center=�67   $��Center=�6R   5_   #   5`      5b      �� 5e      -   5        $ 	btnCerrar��5e      $��0q�60   $��X��6D   
�������$��12(�5f      $��
Montserrat)�6+   $
 Hand��6C   $��2��6Q   ��5g      �� 5h      " ��6$   .�5i   "   ��  � �� �#DA34AE6?    5j   "   P� 5k   &    �@J�IsMouseOver$
 True��6E   5m      ��5o      �� 5p      " ��6.   .R�5q   "   �� $��18��6)   $��18��64   $
 9��6@   #��z��6Q   ._�5r   &   �� $��Center=�67   $��Center=�6R   5s   #   5t      5v      5w      �� 5z      $	��250��6   $��1��6)    �� Orientation$ Vertical=�66   $��0,35,0,0q�6M   .+�5{      �� $��/Images/permiso.png��6   $	��100��69   $	��100��6E   �� 5|      $��Punto Venta��6    
�������$��25(�5}       $��Medium"�6.   $��
Montserrat)�5~       $��Center=�68   �� 5�      $��Usuario��6    
�������$��12(�5�       $��Medium"�6.   $��
Montserrat)�6B   $��0,35,0,0q�5�       �� 5�      -   5         ��Name$ txtUser��5�      $��13(�6n   $��Medium"�6|   $��
Montserrat)�5�      
������� �� 
CaretBrush
 �����
�������$��0,0,0,1q�5�      $��28��68   $��Center=�6D   $��0,5,0,0q�6f   $��30,0,0,0q�6w   ��5�   /   �� *
Username+  �� UpdateSourceTrigger$  PropertyChanged=�.��5�   �   ��5�      �� 5�      $ /Images/usuario.png��6)   $
 None=�6K   ! j� 
AlignmentX$
! Left=�6Z   .��5�   .   5�      �� 5�      $��Contraseña��6    
�������$��12(�6H   $��Medium"�6V   $��
Montserrat)�6j   $��0,35,0,0q�6�   7   1SistemaGestion.CustomControls.BindablePasswordBox  5�      $��28��5�   9   $��0,5,0,0q�6E   "   Password" 5�   9   �� *
Password+$ TwoWay=�$  PropertyChanged=�5�   E   �� 5�      ���#D7596D$��12(�5�   5   $��Medium"�5�       $��
Montserrat)�64   $��0,10,0,0q�5�       $
��Wrap=�62   ��5�       �� *ErrorMessage+5�   2   �� 5�      -   5        $ btnLogin��5�      $��0q�6P   $��Ingresar��5�      
�������$��0,50,0,0q�5�      $��16(�6/   $��
Montserrat)�5�      $
 Hand��65   $��1��6C   ��5�   /   �� *LoginCommand+��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#28AEED6?    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $
 9��6A   #��z��6R   ._�5�   &   �� $��Center=�67   $��Center=�6R   5�   #   5�      5�      �� 5�      $��Center=�6!   $ 
Horizontal=�6>   $��0,15,0,0q�6W   .+�5�      �� $��¿Olvidó su contraseña?��6$   
�������$��12(�5�   $   $��Medium"�5�   $   $��
Montserrat)�5�   $   �� 5�      $��Restablecer��6$   
�������$��12(�5�   $   $��Medium"�5�   $   $��
Montserrat)�5�   $   $��8,0,0,0q�5�   $   5�      5�      5�      5�      5�      5�      