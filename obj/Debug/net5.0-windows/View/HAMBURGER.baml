   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5   	   #    SistemaGestion.View.HAMBURGER   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   -local!clr-namespace:SistemaGestion.View  5   	     � Title$  	HAMBURGER��5	   	   $	��450��5
   	   $	��800��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
������ .��5      ��  �� CornerRadius$ 10��6   
�������._�5   
   � -   5        $��	Principal��5      
������ .+�5      � -   5        $��Header��5      $��35��5      $	��Top=�5      
������ .+�5      �� $ 	10,10,0,0��6   
���P��5      � 5       � ColumnDefinitions 5      �� 5       �� 5!      $	��220 �6+   �� 5"      $	��100 �6+   .+�5&      �� $��Punto de Venta��6    
���   �$��15(�5(       $
��Left=�5)       $��Center=�5*       �� 5.      -   5         ��Name$ 	btnCuenta��5.      $��1��5/      
������ $��15(�51      
���   �$��Center=�53      $
��Left=�54       � Cursor$
 Hand��55      .��56      �  57      �� 58   "   $��15 �63   �� 59   "   .+�5;      �� $��1��6(   $��	Mi Cuenta��5<   (   $��Center=�5=   (   $��Center=�5>   (   $��5,0,0,0q�5?   (   5@      5A      5C      5D      � 5H      -   5         �Name$ GridMenu��5H      
������ $��75��5J      $
��Left=�5K      $��0,35,0,0q�5L      .+�5M      �� 
�������$ 0,0,0,10��5N      �� 5Q      
������ $	��Top=�5R      .+�5T      � 
������ $	��Top=�5U      $
��Left=�5V      $��	50,10,0,0q�5W      .+�5Y      d� -   5        	 d�Name$	 BtnShowHide��5Y   '   $	��150��5Z      $��40��5[      
������ $
 Hand��5]      $
��Left=�5^      $��Center=�5_      ��5`      �� 5a   "   
 �� Orientation$
 
Horizontal=�6-   $
��Left=�6F   .+�5c   &   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Bars=�63   $��20��5d      $��20��5e      $
��Left=�5f      $	��5,0q�5g      5h   #    � Triggers 5j      :� 5l   "    :� RoutedEvent$ ToggleButton.Unchecked��6/   .[�5m   &   �� .��5n   *   �� -   5         ��Name$ 	HidePanel��5n   5   .��5o   .   W�  �� 
TargetName$ GridMenu��6>    �� TargetProperty$ Width�5p   *    W� From 2005q   *   	 W� To 7565    g� Duration$ 0:0:0.5D�5r   *   5s   +   5t   '   5u   #   :� 5y   "   $ ToggleButton.Checked��6/   .[�5z   &   �� .��5{   *   �� -   5        $ 	ShowPanel��5{   5   .��5|   .   W� $ GridMenu��6>   $ Width�5}   *    755~   *    20064   $ 0:0:0.5D�5   *   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      