   M S B A M L   `   `   `  ����I  ESistemaGestion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaGestion.ViewSistemaGestion.View  5      $    SistemaGestion.View.VentasView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:SistemaGestion.View     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��10q�6"   .+�5      �� $��Gestión de Ventas��6   $��24(�5      $
��Bold"�5      $��0,0,0,10q�5      
�������5      �� 5       �� Orientation$ 
Horizontal=�6   $
��0,10q�62   .+�5      �� $��Nueva Venta��6   $	��100��5      $��0,0,10,0q�5      �� 5      $��Buscar��6   $	��100��5      5      5       &   System.Windows.Controls.DataGrid  5#   
   $��1��6   $��10q�5$         AutoGenerateColumns$ False��5%         
IsReadOnly$
 True��5&         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 61     5)      $ Fecha��6%   $	 100 64     5*      $ Cliente��6%   $	 200 66     5+      $ Total��6%   $	 100 64     5,      $ Estado��6%   $	 100 65   5.      �� 51   
   $��2��6   $ 
Horizontal=�52      $��Right=�53      $��10q�54      .+�55      �� $��Total Ventas: ��6   $��Center=�56      �� 57      $��$0.00��6   $
��Bold"�58      $��Center=�59      5:      5;      5<      