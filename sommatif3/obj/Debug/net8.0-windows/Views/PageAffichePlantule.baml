   M S B A M L   `   `   `  ����B  >Canabis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Canabis.ViewsCanabis.Views  5      '    !Canabis.Views.PageAffichePlantule   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Canabis.Views     5        N� Title$  PageAffichePlantule��5	      .��5      � ���	#FFF0F0F0$��20q�6"    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��	0,10,0,10q�6.   $
��Left=�6A   .+�5      �� $��Plantules Actives:��6   $	��200��61   $��16(�6=   $
��Bold"�6K   ���	#FF2E8B576]   �� 5      -   5         ��Name$ lbPlantActif��5      $	��...��6*   $	��200��68   $��16(�6D   ���	#FF2E8B576R   5      �� 5   
   $ 
Horizontal=�6   $��	0,10,0,10q�6.   $��1��6A   $
��Left=�6N   .+�5      �� $��Plantules Inactives:��6   $	��200��63   $��16(�6?   $
��Bold"�6M   ���	#FF8B00006_   �� 5      -   5        $ lbPlantInactif��5      $	��...��6,   $	��200��6:   $��16(�6F   ���	#FF8B00006T   5      �� 5   
   $ 
Horizontal=�6   $��	0,10,0,20q�6.   $��2��6A   $
��Left=�6N   .+�5      �� $ ��Total nombre de Plantules:��6   $	��200��69   $��16(�6E   $
��Bold"�6S   ���	#FF00008B6e   �� 5      -   5        $ lbTotalPlant��5      $	��...��6*   $	��200��68   $��16(�6D   ���	#FF00008B6R   5       &   System.Windows.Controls.DataGrid  5"   
   -   5          Name$ grillePlante��5"      $��3��6)   
�������   AutoGenerateColumns$
 True��6I      
IsReadOnly$
 True��6d      AlternatingRowBackground �	#FFEEEEEE���	#FFD3D3D3$��1q�6�      CanUserAddRows$ False��5#      	   CanUserDeleteRows$	 False��6*   
   CanUserResizeRows$
 False��6D      CanUserResizeColumns$
 True��6^      CanUserReorderColumns$
 True��6z   $��0,0,0,0q�5$      $   HorizontalScrollBarVisibility$ Visible=�6$   "   VerticalScrollBarVisibility$ Visible=�6L   5%      5&      