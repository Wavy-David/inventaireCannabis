   M S B A M L   `   `   `  ����B  >Canabis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Canabis.ViewsCanabis.Views  5      (    "Canabis.Views.PageModifierPlantule   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Canabis.Views     5        N� Title$  PageModifierPlantule��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      $
��auto �6   $	��230��6,   6=    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5       $
��auto �6   �� 5!      $
��auto �6   �� 5"      $
��auto �6   �� 5#      $
��auto �6   �� 5$      $��* �6   .+�5&   
   �� $��Specialité��6   $
��Left=�6%   $��147,10,0,46q�6@   $��0��6U   $��60��6b   $��1��6m   �� 5(   
    �� Orientation$ 
Horizontal=�6   $��	2,10,2,10q�6.   $��1��6A   $��2��6N   .+�5)      �� $��Identification��6   $	��170��6-   �� 5*      -   5         ��Name$ tbIdentification��5*      $	��234��60   �� 5+      -   5         ��Name$ btnSupprimerSpecialite��5+      $��	Recherche��65   $��1��6I   $��1��6W   $��60��6g   $��21,0,0,0q�6s   6�   5,      �� 5.   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��2��6A   $��2��6N   .+�5/      �� $��Etat de santé��6   $	��170��6,   �� 51      -   5         ��Name$ cbEtatDeSante��51      $	��234��6.   52      �� 54   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��3��6A   $��2��6N   .+�55      �� $
��Date��6   $	��170��6#   (  "System.Windows.Controls.DatePicker  57      -   5          Name$ 
calendrier��57      $	��Top=�6-   $
��Left=�6E   $	��234��6`   58      �� 5:   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��4��6A   $��2��6N   .+�5;      �� $��
Provenance��6   $	��170��6)   �� 5<      -   5        $ tbProvenance��5<      $	��234��6,   5=      �� 5?   
   $ 
Horizontal=�6   $��4��6.   $��1��6;   $��	0,10,0,10q�6L   $��Center=�6_   $	��230��6|   .+�5@      �� -   5        $ btnModifierSpecialite��5@      $��Modifier��64   $��60��6G   $��4q�6R   6]   5A      &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ grillePlante��5C      $��5��6)   $��9��66   $��1��6G   
�������	   RowBackground
	 �����
   AlternatingRowBackground

 ��d�$   HorizontalScrollBarVisibility$ Visible=�6�   "   VerticalScrollBarVisibility$ Visible=�6�      SelectionMode$ Single=�6  
�������$��5q�5D         
IsReadOnly$
 True��6)      CanUserSortColumns$
 True��6;      AreRowDetailsFrozen$
 True��6Y      CanUserResizeColumns$ False��6t      CanUserReorderColumns$
 True��6�   $��21,2,2,2q�6�   �� 5F   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��5��6A   $��2��6N   .+�5G      �� $��Description��6   $	��170��6*   �� 5H      -   5        $ tbDescription��5H      $	��234��6-   5I      �� 5K   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��6��6A   $��2��6N   .+�5L      �� $��Stade��6   $	��170��6$   �� 5N      -	   5        $ cbStade��5N      $	��234��6(   5O      �� 5Q   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��7��6A   $��2��6N   .+�5R      �� $��Entreposage��6   $	��170��6*   �� 5T      -
   5        $ cbEntreposage��5T      $	��234��6.   5U      �� 5W   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��8��6A   $��2��6N   .+�5X      �� $��Quantité ajoutée��6   $	��170��6/   	� 5[      -   5         	�Name$ rbActif��5[       d� 	IsChecked$
 TrueY�6+   $��2,0,0,0q�6<   $��Actif��6N   6`   	� 5\      -   5        $ 	rbInactif��5\      $��11,0,0,0q�6.   $��Inactif��6A   6U   5]      �� 5_   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��9��6A   $��2��6N   .+�5`      �� $$��Item retiré de l’inventaire��6   $	��170��6:   �� 5b      -   5        $ cbItemRetireDeLInventaire��5b      $	��234��6:   5c      �� 5g   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��10��6A   $��2��6O   .+�5h      �� $
��Note��6   $	��170��6#   �� 5i      -   5        $ tbNote��5i      $	��234��6&   5j      �� 5l   
   $ 
Horizontal=�6   $��	2,10,2,10q�6.   $��11��6A   $��2��6O   .+�5m      �� $"��Responsable décontamination��6   $	��170��6:   �� 5n      -   5        $" tbResponsableDecontamination��5n      $	��234��6<   5o      5q      5r      