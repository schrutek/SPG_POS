   M S B A M L   `   `   `  ����G  CCatchTheBall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CatchTheBallCatchTheBall  5   	       CatchTheBall.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CatchTheBall     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   
�������.+�5      �� -   5         ��Name$ 
ScoreLabel��5      $��0��6(   $��Right=�64   �� 5      $��Score:��6   $��Right=�6%   �� 5      -   5        $ 
ClickLabel��5      $��0��6(   $��Right=�64   �� 5      $��Clicks:��6   $��Right=�6&   �� 5      -   5         ��Name$ 
SizeSlider��5      $��0��6)    � Minimum$ 25Q�66    � Maximum$	 100Q�6C   $
��Left=�6Q   6i   5      �� 5   
   -   5         ��Name$ EllipseContainer��5      $��1��6,   $��Stretch=�69   $��Stretch=�6W   
����� �.+�5      A� -   5         A�Name$ FirstEllipse��5      
���   �$��4��5      
���������5      ��  �� ElementName$ 
SizeSlider��	 �� Path$	 Value���5      �� $ 
SizeSlider��$	 Value�5       5!      5"      5#      