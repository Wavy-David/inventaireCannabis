   M S B A M L   `   `   `  ����B  >Canabis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Canabis.ViewsCanabis.Views  5      "    Canabis.Views.PageHistorique   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Canabis.Views     5        N� Title$  PageHistorique��5	       � 	Resources 5         RoundedTextBox �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name  RoundedButtonStyle ��Name	 �� 
StartPoint
 �� EndPoint � Color � Offset �� Triggers P� Property P� Value �� 
TargetName  RoundButtonStyle��%i	  &	        &	 �    &	 :    �� 5   
   " ��6'   .�5      ��  � �� �#ccc6,   �� 5       � ��$ 1q�60   �� 5       � �� �#fff6+   �� 5       � ��$ 5q�6(   �� 5       � ��$ 30��6'   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   	   #��z��5   	   $ 14��5   	   ._�5      �� $ PART_ContentHost��6+   5      5      5       5!      �� 5$   
   " ��6+   .�5%      ��  � ��
 �����6,   �� 5&       � ��$ 0q�60   �� 5'       � ��
 �����6+   �� 5(       � ��$
 10,5q�6(   �� 5)       � �� 5*      �� 5+      " ��6&   .R�5,      �� $ ButtonBorder��6!   #��z��67   $��0q�5-      $ 12��5.      ��5/      �� 50   "   $		 0,0&�66   $	
 1,1&�6G   .��51   &   � $ #11d171��63   $ 0Q�6C   � 52   &   $ #3ea882��63   $ 1Q�6C   53   #   ._�55      �� $��Center=�6/   $��Center=�56   "   57       5:      P� 5;       �@J�IsMouseOver$
 True��6=   . �5<   "   �� $ ButtonBorder��6)    � �� 5=   &   �� 5>   *   $		 0,0&�6>   $	
 1,1&�6O   .��5?   .   � $ 
DodgerBlue��6;   $ 0Q�6N   � 5@   .   $ 	RoyalBlue��6;   $ 1Q�6M   5A   +   5C   #   5D      5G      5I      5J      �� 5M   
   " ��6)   .�5N      ��  � ��$ 40��6'   �� 5O       � ��$	 100��6&   �� 5P       � �� �#fff6+   �� 5Q       � �� �#4CAF506,   �� 5R       � ��$ 0q�60   �� 5S       � ��$ Center=�69   �� 5T       � ��$ Center=�6;   �� 5W       � �� 5X      �� 5Y      $		 0,0&�6*   $	
 1,1&�6;   .��5Z      � $ #11d171��6'   $ 0Q�67   � 5[      $ #3ea882��6'   $ 1Q�67   5\      5^      �� 5a       � �� 5b      �� 5c      " ��6&   .R�5d      �� $ BorderElement��6!   #��z��68   #��z��6b   #��z��6�   $ 50��5e      ._�5f      �� $��Center=�6/   $��Center=�6L   #��z��6g   5g       5j      P� 5k       �@J�IsMouseOver$
 True��6=   . �5l   "   �� $ BorderElement��6)    � �� 5m   &   �� 5n   *   $		 0,0&�6>   $	
 1,1&�6O   .��5o   .   � $ 
DodgerBlue��6;   $ 0Q�6N   � 5p   .   $ 	RoyalBlue��6;   $ 1Q�6M   5q   +   5s   #   5t      5v      5x      5y      5z      .��5|      ]� ���	#FFF0F0F0$��20q�6'   .+�5}   
   �� $��Bottom=�6   ���	#FF2E8B57
���������5~      �� 5      .R�5�      �  � RowDefinitions 5�      �� 5�      $��* �6,    � ColumnDefinitions 5�      �� 5�      $��4* �6/   �� 5�      $��* �6/   5�      5�      .@�5�      �� .��5�      �� -   5        $��statusMessage��5�      
Message:6;   5�      �� 5�      $��2��6   .��5�      �� -   5        $��statusAction��5�      	Action!69   5�      5�      �� 5�   
   $
��Auto=�6   .��5�      � 
������� 5�      �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $��* �6$   .+�5�      ��  �� Orientation$ 
Horizontal=�6   $��	2,10,2,10q�66   $��0��6I   .+�5�      �� -   5         ��Name$ 	btnRetour��5�      $
��Left=�60   $	��Top=�5�      $��30��65   $��30��6@   $��10q�6L   #��[ 5�      .��5�      �� $.��(pack://application:,,,/AutreDoc/back.png��6    $��5q�6R   5�      5�      �� 5�      $��
Historique��6   $��Center=�6-   $��Latha)�5�      $
��Bold"�6   $��25(�6&   $��Italic$�64   $��1��6G   ��5�      �� 5�      " ��6*   .R�5�      �� $��0q�6%   
������ ._�5�   "   �� $��Latha)�6,   $
��Bold"�6?   $��25(�6Q   $��Italic$�6_   $��Center=�5�      ��5�   &   �� 5�   *   $		 0,0&�6>   $	
 1,5&�6O   .��5�   .   � $ #11d171��6;   $ 0Q�6K   � 5�   .   $ 	RoyalBlue��6;   $ 1Q�6M   5�   +   �� 5�      5�   #   5�      5�      5�      �� 5�      $��Center=�6   $��2��6:   .+�5�      �� $ 
Horizontal=�6!   $��
70,50,2,10q�6:   $��1��6N   .+�5�      �� $��Identification��6    $	��170��69   $��14(�6E   $
��Bold"�6S   ���	#FF2E8B576e   �� 5�      -   5         ��Name$ tbIdentification��5�   "   $	��234��6<   #��[  6H   �� 5�      -   5        $ btnSupprimerSpecialite��5�   !   $��	Recherche��6A   $��1��6U   $��1��6c   $��90��5�   !   $��25��6-   $��21,0,0,0q�5�   !   #��[ 5�   !   5�      �� 5�      ���	#FFD3D3D3$��1q�65   $ 10��6I   $��
70,10,30,2q�6[   $
��1080��6o   $��2��5�       �� Padding$ 10q�6   ���#F0F3F7$
��auto��60   $
��auto��6=   $��2��6K   $��2��6[   ._�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ grillePlante��5�   #   $��2��69   $��9��6F   
������ $   HorizontalScrollBarVisibility$ Visible=�5�      "   VerticalScrollBarVisibility$ Visible=�6/      SelectionMode$ Single=�6U      
IsReadOnly$
 True��5�         CanUserSortColumns$
 True��6      AreRowDetailsFrozen$
 True��63      CanUserResizeColumns$
 True��6N      CanUserReorderColumns$
 True��5�          GridLinesVisibility$
  None=�6$   
������ $��0q�5�   !   !   RowBackground! 5�      �� 5�   "   $		 0,0&�66   $	
 1,1&�6G   .��5�   &   � $ #60DCA1��63   $ 0Q�6C   � 5�   &   $ #B2F1E5��63   $ 1Q�6C   5�   #   "   AlternatingRowBackground" 5�      �� 5�   "   $		 0,0&�66   $	
 1,1&�6G   .��5�   &   � $ White��63   $ 0Q�6A   � 5�   &   $ #E4F5EF��63   $ 1Q�6C   5�   #    5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%�   '        '  �     �� 5�   "   "  6(   .�5�   &   ��  � ��
 ���� 6C   �� 5�   &    � �� �	#FFD3D3D36D   �� 5�   &    � ��$ 1q�6H   5�   #   �� 5�   "   "  6(   .�5�   &   ��  � ��$ 15q�6@   5�   #   5�      5�      5�      5�      5�      5�      5�      5      