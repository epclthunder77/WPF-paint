   M S B A M L   `   `   `  ����@  <paint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:paintpaint  5   	       paint.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   localclr-namespace:paint  5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� $��
20,20,0,20q�6   $��0,0,2,0q�6%   
������$��0��6X   ._�5      � $��20,20,40,20q�6    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6'      
BlurRadius$ 15Q�65      Opacity$
 0.15Q�6E      ShadowDepth$ 2Q�6T   .+�5      �� -   5         ��Name$ Canvas��5       �� 
Background
 �����6,   5      5      �� 5   
   $��1��6   	 �� Orientation$	 Vertical=�6%   .+�5      �� -   5        $��
Load Image��5      $��0,10,0,0q�6B   �� 5      .��6   Editing Mode6#   �� 5      $	 
Horizontal=�6   $��5,10,0,0q�62   .+�5      d� -   5        $��PenBtn��5      $��5q�6-   $��0,0,10,0q�69   
 � ToolTip$	
 Pen��6K   
������� d� 	IsChecked$
 TrueY�6l   .��5      �� $��24��6   $��24��6(   $1��+https://img.icons8.com/fluency/48/pen-1.png��63   5      d� 5       -   5        $��	EraserBtn��5       $��5q�60   $��0,0,10,0q�6<   $
 Eraser��6N   
�������.��5!      �� $��24��6   $��24��6(   $1��+https://img.icons8.com/fluency/48/erase.png��63   5"      5#      �� 5$      $��Pen thickness��6   $��SemiBold"�6,   $��16(�6B   $��0,25,0,0q�6P   �� 5%      -   5         ��Name$ ThicknessSlider��5%      $��5,10,0,0q�6Z    �� IsSnapToTickEnabled$
 True��6l    � Minimum$ 1Q�6�    � Maximum$ 5Q�6�    � Value$ 2Q�6�   �� 5&      .��6   
Pen Colour6    #  Xceed.Wpf.Toolkit.ColorPicker  5'      -   5          Name$ PenColorPicker��5'         ShowStandardColors$ False��6r      UsingAlphaChannel$ False��6�      SelectedColor	 Black6�   $��5,10,0,0q�6�   $��30��6�      	ColorMode$ ColorCanvas=�6�      DisplayColorAndName$
 True��6�   $	��200��6  $
��Left=�6  $��14(�65  �� 5(      -   5        $��Save Changes��5(      $��0,10,0,0q�6F   �� 5)      -   5        $��
Save Image��5)      $��0,10,0,0q�6B   �� 5*      -	   5        $��Load Saved Changes��5*      $��0,10,0,0q�6W   5,      5-      5.      