   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SistemaGestion.VistaModeloSistemaGestion.VistaModelo  5   	   9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5	   	       SistemaGestion.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   8	viewModel(clr-namespace:SistemaGestion.VistaModelo  5   	   -local!clr-namespace:SistemaGestion.View  5	   	     � Title$  
MainWindow��5   	   $	��650��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      .   (SistemaGestion.VistaModelo.MainViewModel  5   
    � 	Resources 5       �� DataType.   (SistemaGestion.VistaModelo.HomeViewModel"   SistemaGestion.View.HomeView0   *SistemaGestion.VistaModelo.VentasViewModel$   SistemaGestion.View.VentasView4   .SistemaGestion.VistaModelo.ProvedoresViewModel(   "SistemaGestion.View.ProvedoresView3   -SistemaGestion.VistaModelo.ProductosViewModel'	   !SistemaGestion.View.ProductosView2
   ,SistemaGestion.VistaModelo.ClientesViewModel&    SistemaGestion.View.ClientesView/   )SistemaGestion.VistaModelo.StockViewModel#   SistemaGestion.View.StockView2   ,SistemaGestion.VistaModelo.FacturasViewModel&    SistemaGestion.View.FacturasView2   ,SistemaGestion.VistaModelo.ReportesViewModel&    SistemaGestion.View.ReportesView2   ,SistemaGestion.VistaModelo.FinanzasViewModel&    SistemaGestion.View.FinanzasView��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5!      *, +)(�� �     5%      *,
 +)(�� �     5)      *, +)(�� ,    5-      *, +)(�� ^    51      *, +)(�� �    55      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5!   
   "  6   .R�5"      	  5#      �� 5%   
   " 
 6   .R�5&        5'      �� 5)   
   "  6   .R�5*        5+      �� 5-   
   "  6   .R�5.        5/      �� 51   
   "  6   .R�52        53      �� 55   
   "  6   .R�56        57      59      .��5>      ��  �� CornerRadius$ 10��6   $��1q�6   ��5?   
   �� 5A       �� 
StartPoint$	 0,0&�6"   	 �� EndPoint$		 1,1&�63   .��5B      �    winBorderColor1
 � Color#
 [  6    � Offset$ 0Q�6H   � 5C        winBorderColor2#
 [ 6   $
 0.75Q�6H   � 5D        winBorderColor3#
 [ 6   $ 1Q�6H   5E      ._�5I   
   �  � ColumnDefinitions 5J      �� 5L      $	��250 �6#   �� 5N      $��* �6#   .+�5R      �� $ 	10,0,0,10��6   $��0��6.   ��5S      �� 5U      $	 0,0&�6*   $	 1,0.7&�6;   .��5V      �   secondaryBackColor1#
 [ 6'   $ 0Q�6T   � 5W        secondaryBackColor2#
 [ 6'   $ 1Q�6T   5X      ._�5\      �� .+�5^      ��  �� Orientation$ 
Horizontal=�6!   $��35��6:   $��
15,25,0,40q�6F   .+�5_      �� $%��/Images/tarjeta-electronica.png��6    $��30��5`       $��30��6,   �� 5a      $��SISTEMA POS��6$     titleColor2#��[ 5b   $   $��20(�5c   $   $��
Montserrat)�5d   $   $��Medium"�5e   $   $��Oblique$�5f   $   $��Center=�5g   $   $��10,0,0,0q�5h   $   5i      �� 5m      .+�5n      �� $��	PRINCIPAL��6%   $��45,0,0,0q�66   #��[ 6H   $��12(�5o   %   $��Light"�63   $��
Montserrat)�6F   $��Oblique$�6_   5p      	� 5q        
menuButton#��[ 6"    
 color1
 � Tag# [ 5r   "   $��	dashboard��5t   "   ��5s   "   �� *ShowHomeViewCommand+.��5v      �� $ 
Horizontal=�6%   .+�5w      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=�6+     menuButtonIcon#��[ 67   $��20��6_   $��20��6k   $	��5,0q�6v   �� 5x      $��	Dashboard��6(    	 menuButtonText#��[	 69   $��10,0,0,0q�6a   $��Oblique$�6s   5y      5z      	� 5|      #��[ 6"    

 color2# [
 5}   "   $��Ventas��5   "   ��5~   "   �� *ShowVentasViewCommand+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ CashRegister=�6+   #��[ 6?   $��20��6g   $��20��6s   $	��5,0q�6~   �� 5�      $��Ventas��6(   #��[	 66   $��10,0,0,0q�6^   $��Oblique$�6p   5�      5�      	� 5�      #��[ 6"    
 color3# [ 5�   "    d� 	IsChecked$
 TrueY�5�   "   ��5�   "   �� *ShowProductosViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ BagShopping=�6+   #��[ 6>   $��20��6f   $��20��6r   $	��5,0q�6}   �� 5�      $��	Productos��6(   #��[	 69   $��10,0,0,0q�6a   $��Oblique$�6s   5�      5�      	� 5�      #��[ 6"    
 color4# [ 6F   $
 TrueY�6d   ��5�   "   �� *ShowClientesViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ Users=�6+   #��[ 68   $��20��6`   $��20��6l   $	��5,0q�6w   �� 5�      $��Clientes��6(   #��[	 68   $��10,0,0,0q�6`   $��Oblique$�6r   5�      5�      	� 5�      #��[ 6"    
 color5# [ 6F   $
 TrueY�6d   ��6u   �� *ShowStockViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ BoxesStacked=�6+   #��[ 6?   $��20��6g   $��20��6s   $	��5,0q�6~   �� 5�      $��Stock��6(   #��[	 65   $��10,0,0,0q�6]   $��Oblique$�6o   5�      5�      	� 5�      #��[ 6"    
 color6# [ 6F   $
 TrueY�6d   ��6u   �� *ShowFacturasViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ FileInvoiceDollar=�6+   #��[ 6D   $��20��6l   $��20��6x   $	��5,0q�6�   �� 5�      $��Facturas��6(   #��[	 68   $��10,0,0,0q�6`   $��Oblique$�6r   5�      5�      	� 5�      #��[ 6"    
 color7# [ 6F   $
 TrueY�6d   ��6u   �� *ShowReporteViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ FileContract=�6+   #��[ 6?   $��20��6g   $��20��6s   $	��5,0q�6~   �� 5�      $��Reportes��6(   #��[	 68   $��10,0,0,0q�6`   $��Oblique$�6r   5�      5�      	� 5�      #��[ 6"    
 color8# [ 5�   "   ��5�   "   �� *ShowProvedoresViewCommand+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ 	TruckFast=�6+   #��[ 6<   $��20��6d   $��20��6p   $	��5,0q�6{   �� 5�      $��
Provedores��6(   #��[	 6:   $��10,0,0,0q�6b   $��Oblique$�6t   5�      5�      	� 5�      #��[ 6"     color10# [ 6F   $
 TrueY�6e   ��6v   �� *ShowFinanzasViewModel+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ 
SackDollar=�6+   #��[ 6=   $��20��6e   $��20��6q   $	��5,0q�6|   �� 5�      $��Finanzas��6(   #��[	 68   $��10,0,0,0q�6`   $��Oblique$�6r   5�      5�      5�      5�      �� 5�      $ 	0,10,10,0��6   $��1��6.   ��5�      �� 5�      $	 1,1&�6*   $		 0,0&�6;   .��5�      �   primaryBackColor1#
 [ 6'   $ 0Q�6R   � 5�        primaryBackColor2#
 [ 6'   $ 1Q�6R   5�      ._�5�      �  � RowDefinitions 5�      �� 5�      $��25 �6(   �� 5�      $��35 �6(   �� 5�      $��* �6(   .+�5�      �� -   5         ��Name$ pnlControlBar��5�   !   $��0��5�   !   $ 
Horizontal=�5�   !   $��RightToLeft=�5�   !   
������ $��0,0,5,0q�5�   !   .+�5�      �� -   5         ��Name$ btnClose��5�   !     controlButton#��[ 5�   !   # [ 5�   !   .��5�        $ Xmark=�6+     controlButtonIcon#��[ 68   5�      �� 5�      -   5        $ btnMaximize��5�   !   #��[ 5�   !   # [ 5�   !   .��5�        $ Square=�6+   #��[ 69   5�      �� 5�      -   5        $ btnMinimize��5�   !   #��[ 5�   !   # [ 5�   !   .��5�        $ WindowMinimize=�6+   $��12��6A   #��[ 6L   5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5      .+�5     �� $��0��6%   $ 
Horizontal=�5  %   $��Center=�5  %   .+�5       $��20��5  +   $��20��5	  +      
Foreground# [ 5
  +   $��	35,0,10,0q�5  +    5  +   �� *Icon+5  +   �� 5     #��[ 5  (   $��16(�5  (   $��
Montserrat)�5  (   $��Oblique$�5  (   $��Medium"�5  (   $��Center=�5  (   ��5  (   �� *	Caption+5  (   5     �� 5     $��1��6%   $ 
Horizontal=�5  %   $��RightToLeft=�5  %   $��Center=�5  %   $��0,0,10,0q�5  %   .+�5     ��   
iconButton#��[ 6%   # [ 5  %   .��5  "     $ 	AngleDown=�6/     iconButtonIcon#��[ 6@   5     �� 5       titleColor3#��[ 5  (   $��
Montserrat)�5   (   $��12(�5!  (   $��Center=�5"  (   ��5  (   �� * CurrentUserAccount.DisplayName+5"  (   A� 5#     $��35��6&   $��35��62   #��[
 5$  &   $��2��5%  &   $��	10,0,10,0q�5&  &   ��5'  "   �� 5(  &    �� ImageSource$ /Images/usuario.png��61   5+     �� 5,     #��[ 6%   # [ 5-  %   .��5.  "     $ Clock=�6/   #��[ 6<   50     �� 51     #��[ 6%   # [ 52  %   .��53  "     $ Envelope=�6/   #��[ 6?   55     �� 56     #��[ 6%   # [ 57  %   .��58  "     $
 Bell=�6/   #��[ 6;   5:     5;     5<     �� 5?     $��2��5@  %   $��25q�5A  %   ��5?  %   �� *CurrentChildView+5A  %   5D     5F     5G     5H     5O     