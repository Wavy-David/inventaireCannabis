   M S B A M L   `   `   `  ����B  >Canabis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Canabis.ViewsCanabis.Views  5      "    Canabis.Views.PageHistorique   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Canabis.Views     5        N� Title$  PageHistorique��5	      .��5      ]� ���	#FFF0F0F0$��20q�6'   .+�5   
   �� $��Bottom=�6   ���	#FF2E8B57
���������5      �� 5      .R�5      �  � RowDefinitions 5      �� 5      $��* �6,    � ColumnDefinitions 5      �� 5      $��4* �6/   �� 5      $��* �6/   5      5      .@�5      �� .��5      �� -   5        $��statusMessage��5      
Message:6;   5      �� 5      $��2��6   .��5      �� -   5        $��statusAction��5      	Action!69   5      5       �� 5"   
   $
��Auto=�6   .��5#      �  5%      �� 5&      $
��auto �6$   �� 5'      $
��auto �6$   �� 5(      $
��auto �6$   �� 5)      $
��auto �6$   �� 5*      $
��auto �6$   �� 5+      $
��auto �6$   �� 5,      $
��auto �6$   �� 5-      $
��auto �6$   �� 5.      $
��auto �6$   �� 5/      $
��auto �6$   �� 50      $
��auto �6$   �� 51      $
��auto �6$   �� 52      $
��auto �6$   �� 53      $
��auto �6$   �� 54      $
��auto �6$   �� 55      $
��auto �6$   �� 56      $��* �6$   .+�59      ��  �� Orientation$ 
Horizontal=�6   $��	2,10,2,10q�66   $��1��6I   .+�5:      �� $��Identification��6   $	��170��65   �� 5;      -   5         ��Name$ tbIdentification��5;      $	��234��68   �� 5<      -   5         ��Name$ btnSupprimerSpecialite��5<      $��	Recherche��6=   $��1��6Q   $��1��6_   $��60��6o   $��21,0,0,0q�6{   6�   5>      &   System.Windows.Controls.DataGrid  5@      -   5          Name$ grillePlante��5@      $��5��61   $��9��6>   
�������   RowBackground
 �����   AlternatingRowBackground
 ��d�$	   HorizontalScrollBarVisibility$	 Visible=�6�   "
   VerticalScrollBarVisibility$
 Visible=�6�      SelectionMode$ Single=�6�   
�������$��5q�5A         
IsReadOnly$
 True��6)      CanUserSortColumns$
 True��6;      AreRowDetailsFrozen$
 True��6Y      CanUserResizeColumns$ False��6t      CanUserReorderColumns$
 True��6�   $��2,2,2,2q�6�   5D      5E      5F      5G      