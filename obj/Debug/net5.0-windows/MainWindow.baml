   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SistemaGestion.VistaModeloSistemaGestion.VistaModelo  5   	   9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5	   	       SistemaGestion.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   8	viewModel(clr-namespace:SistemaGestion.VistaModelo  5   	   -local!clr-namespace:SistemaGestion.View  5	   	     � Title$  Sistema Punto Venta��5   	   $	��650��6%   $
��1200��62    � 
ResizeMode$ 	CanResize=�5   	   $��Stretch=�5   	   $��Stretch=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      .   (SistemaGestion.VistaModelo.MainViewModel  5   
    � 	Resources 5         MenuBackground �� Color  ContentBackground  	TextColor �� DataType.   (SistemaGestion.VistaModelo.HomeViewModel"   SistemaGestion.View.HomeView0   *SistemaGestion.VistaModelo.VentasViewModel$   SistemaGestion.View.VentasView5   /SistemaGestion.VistaModelo.ProveedoresViewModel(   "SistemaGestion.View.ProvedoresView3   -SistemaGestion.VistaModelo.ProductosViewModel'	   !SistemaGestion.View.ProductosView2
   ,SistemaGestion.VistaModelo.ClientesViewModel&    SistemaGestion.View.ClientesView/   )SistemaGestion.VistaModelo.StockViewModel#   SistemaGestion.View.StockView2   ,SistemaGestion.VistaModelo.FacturasViewModel&    SistemaGestion.View.FacturasView2   ,SistemaGestion.VistaModelo.ReportesViewModel&    SistemaGestion.View.ReportesView2   ,SistemaGestion.VistaModelo.FinanzasViewModel&    SistemaGestion.View.FinanzasView	 �� 
TargetType  CustomToggleButtonStyle
 �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value  btnShowHide!  FontAwesome.Sharp.IconImage��%�  &	        &	 !     &	 B     (�� a     5      *, +)(�� �     5"      *, +)(�� �     5%      *, +)(�� �     5(      *, +)(�� )    5+      *,
 +)(�� [    5.      *, +)(�� �    51      *, +)(�� �    54      *, +)(�� �    57      *, +)&	 #    7  7  &	 s    �� 5   
   $ #333333��61   �� 5   
   $ #2c3e50��64   �� 5   
   $ White��6,   �� 5   
   "  6   .R�5         5!      �� 5"   
   "  6   .R�5#        5$      �� 5%   
   "  6   .R�5&        5'      �� 5(   
   "  6   .R�5)      	  5*      �� 5+   
   " 
 6   .R�5,        5-      �� 5.   
   "  6   .R�5/        50      �� 51   
   "  6   .R�52        53      �� 54   
   "  6   .R�55        56      �� 57   
   "  6   .R�58        59      �� 5<   
   "	 d�6   .�5=      �� 
 � ��
 ���� 6+   �� 5>      
 � ��
 ���� 6,   �� 5?      
 � ��8   6+   �� 5@      
 �@�Cursor$
 Hand��6'   �� 5A      
 � ��$ 0q�6(   �� 5B      
 � �� 5C      �� 5D      " d�6&   .R�5E      � .+�5F      �� $��Center=�6/   $��Center=�6L   5G       5H      P� 5I       �@d�	IsChecked$
 TrueY�6;   . �5J   "   �� 8  
 � ��6?   5K      P� 5L       �@J�IsMouseOver$
 True��6=   . �5M   "   �� 
 �@J�Opacity$	 0.7Q�6<   5N      5P      5R      5S      �� 5V   
   "	  6$   5W      .��5[      ��  �� CornerRadius$ 10��6   $��1q�6    �� Padding$ 5q�63   ._�5\   
   �� $ 10��6   $��1q�6#   ��5^      �� 5_       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5`      �  � Color$ #2c3e50��6#    � Offset$ 0Q�63   � 5a      $ #333333��6#   $ 1Q�63   5b      ._�5f      �  � ColumnDefinitions 5g      �� 5i      -   5         ��Name$ 
MenuColumn��5i   3   $	��250 �6'   63   �� 5k      $��* �6'   .+�5o      �� $ 	10,0,0,10��6   $��0��62   #��[  6B   ._�5p      �� .+�5r      ��  �� Orientation$ 
Horizontal=�6%   $��35��6>   $��
15,25,0,40q�6J   .+�5s      �� $%��/Images/tarjeta-electronica.png��6$   $��30��6M   $��30��6Y   �� 5t      $��SISTEMA POS��6(   #��[ 5u   (   $��20(�5v   (   $��
Montserrat)�5w   (   $��Medium"�5x   (   $��Oblique$�5y   (   $��Center=�5z   (   $��10,0,0,0q�5{   (   5}      �� 5�      .+�5�      �� $��	PRINCIPAL��6(   $��45,0,0,0q�5�   (   #��[ 5�   (   $��12(�5�   (   $��Light"�5�   (   $��
Montserrat)�5�   (   $��Oblique$�5�   (   5�      	� 5�        
menuButton#��[ 6&    
 color1
 � Tag# [ 5�   &   $��	dashboard��5�   &   ��5�   &   �� *ShowHomeViewCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=�6/     menuButtonIcon#��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��	Dashboard��6,     menuButtonText#��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    
	 color2# [	 5�   &   $��Ventas��5�   &   ��5�   &   �� *ShowVentasViewCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ CashRegister=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��Ventas��6,   #��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    

 color3# [
 5�   &    d� 	IsChecked$
 TrueY�5�   &   ��5�   &   �� *ShowProductosViewModel+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ BagShopping=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��	Productos��6,   #��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    
 color4# [ 5�   &   $
 TrueY�5�   &   ��5�   &   �� *ShowClientesViewModel+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Users=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��Clientes��6,   #��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    
 color5# [ 5�   &   $
 TrueY�5�   &   ��5�   &   �� *ShowStockViewModel+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ BoxesStacked=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��Stock��6,   #��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    
 color6# [ 5�   &   $
 TrueY�5�   &   ��5�   &   �� *ShowFacturasViewModel+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ FileInvoiceDollar=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5�   "   $��Compras��6,   #��[ 5�   ,   $��10,0,0,0q�5�   ,   $��Oblique$�5�   ,   #��[ 5�   ,   5�      5�      	� 5�      #��[ 6&    
 color7# [ 5�   &   $
 TrueY�5�   &   ��5�   &   �� *ShowReporteViewModel+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ FileContract=�6/   #��[ 5�   /   $��20��5�   /   $��20��6;   $	��5,0q�5�   /   �� 5  "   $��Reportes��6,   #��[ 5  ,   $��10,0,0,0q�5  ,   $��Oblique$�5  ,   #��[ 5  ,   5     5     	� 5	     #��[ 6&    
 color8# [ 5
  &   ��5  &   �� *ShowProvedoresViewCommand+.��5     �� $ 
Horizontal=�6)   .+�5  "     $ 	TruckFast=�6/   #��[ 5  /   $��20��5  /   $��20��6;   $	��5,0q�5  /   �� 5  "   $��
Provedores��6,   #��[ 5  ,   $��10,0,0,0q�5  ,   $��Oblique$�5  ,   #��[ 5  ,   5     5     	� 5     #��[ 6&     color10# [ 5  &   $
 TrueY�5  &   ��5  &   �� *ShowFinanzasViewModel+.��5     �� $ 
Horizontal=�6)   .+�5  "     $ 
SackDollar=�6/   #��[ 5   /   $��20��5!  /   $��20��6;   $	��5,0q�5"  /   �� 5$  "   $��Finanzas��6,   #��[ 5%  ,   $��10,0,0,0q�5&  ,   $��Oblique$�5'  ,   #��[ 5(  ,   5)     5*     5+     5,     �� 50     $ 	0,10,10,0��6   $��1��62   #��[ 6B   ._�51     �  � RowDefinitions 52     �� 54     $��25 �6,   �� 56     $��35 �6,   �� 58     $��* �6,   .+�5<     �� -   5         ��Name$ pnlControlBar��5<  %   $��0��5=  %   $ 
Horizontal=�5>  %   $��RightToLeft=�5?  %   
������ $��0,0,5,0q�5A  %   .+�5D     �� -   5         ��Name$ btnClose��5D  %     controlButton#��[ 5E  %   # [ 5F  %   .��5H  "     $ Xmark=�6/     controlButtonIcon#��[ 5I  /      
Foreground# [ 5J  /   5K     �� 5L     -   5        $ btnMaximize��5L  %   #��[ 5M  %   # [ 5N  %   .��5P  "     $ Square=�6/   #��[ 5Q  /   # [ 5R  /   5S     �� 5T     -   5        $ btnMinimize��5T  %   #��[ 5U  %   # [ 5V  %   .��5X  "     $ WindowMinimize=�6/   $��12��5Y  /   #��[ 5Z  /   # [ 5[  /   5\     5]     � 5a     $��1��6    5b     �� 5c  "   �� 5d  "   .+�5g     �� $��0��6)   $ 
Horizontal=�5h  )   $��Center=�5i  )   .+�5j  "     $��20��5k  /   $��20��5l  /   # [ 5m  /   $��	35,0,10,0q�5n  /    5j  /   �� *Icon+5n  /   �� 5o  "   #��[ 5p  ,   $��16(�5q  ,   $��
Montserrat)�5r  ,   $��Oblique$�5s  ,   $��Medium"�5t  ,   $��Center=�5u  ,   ��5o  ,   �� *	Caption+5u  ,   5v     �� 5w     $��1��6)   $ 
Horizontal=�5x  )   $��RightToLeft=�5y  )   $��Center=�5z  )   $��0,0,10,0q�5{  )   .+�5|  "   ��   
iconButton#��[ 6)   # [ 5}  )   .��5~  &     $ 	AngleDown=�63     iconButtonIcon#��[ 5  3   # [ 5�  3   5�  #   �� 5�  "   #��[ 5�  ,   $��
Montserrat)�5�  ,   $��12(�5�  ,   $��Center=�5�  ,   ��5�  ,   �� * CurrentUserAccount.DisplayName+5�  ,   A� 5�  "   $��35��6*   $��35��66   #��[	 5�  *   $��2��5�  *   $��	10,0,10,0q�5�  *   ��5�  &   �� 5�  *     �� ImageSource$  /Images/usuario.png��65   5�  #   �� 5�  "   #��[ 6)   # [
 5�  )   .��5�  &     $ Clock=�63   #��[ 5�  3   # [ 5�  3   5�  #   �� 5�  "   #��[ 6)   # [ 5�  )   .��5�  &     $ Envelope=�63   #��[ 5�  3   # [ 5�  3   5�  #   �� 5�  "   #��[ 6)   # [ 5�  )   .��5�  &     $
 Bell=�63   #��[ 5�  3   # [ 5�  3   5�  #   5�     5�     �� 5�     $��2��5�  )   $��25q�5�  )   ��5�  )   �� *CurrentChildView+5�  )   5�     5�     5�     5�     5�     5�     