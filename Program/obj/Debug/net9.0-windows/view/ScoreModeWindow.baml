   M S B A M L   `   `   `  ����B  >Program, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Program.viewProgram.view  5   	   "    Program.view.ScoreModeWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Program.view     5   	     � WindowStartupLocation$  CenterOwner=�5   	    � Title$ ScoreModeWindow��5	   	   $	��445��6!   $	��795��6.   .��5
      �  � ShowGridLines$ False��6   $��0,0,0,-6q�6!    � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��60 �6   .+�5   
   �� $��1��5      $��3��5      $��3��5      ���	#FF4874A85      �� 5    
   -   5         ��Name$ MainCardScoreMode��5       $��2��6K   $��29,20q�6[    J� RenderTransformOrigin$ 0.362,0.386&�6j   $
��Bold"�6�   $��30(�6�   ���	#FF4C98CF � 	Resources 5"       �� 
TargetType	 �� Property
 �� Value��%_   '��       �� 5#      " ��6   .�5$      �� 	 �@��CornerRadius$
 30��65   5%      5&      .��5'      �� -   5         ��Name$ MainTextScoreMode��5'      $
��Wrap=�5(       �� TextAlignment$ Center=�5)      5*      �� 5+   
   -   5        $ 
wasCorrect��5+      $��	0,0,29,10q�6&   ���	#FF4874A8$�� ��6�   $��2��6�   $��Right=�6�   $��57��6�   $��57��6�   $��Bottom=�6�   ���	#FF4874A8 5,      ��%_   '��       �� 5-      " ��6   .�5.      �� 	 �@��CornerRadius$
 30��65   5/      50      51      � 52   
   $��Center=�6   $��2��6,   $	��986��6=   $��77��6I   $��Bottom=�6U   .+�53      �� -   5        $��	523,0,0,0q�53   5   ���	#FF4DA478$��Correct��6�   $
��Left=�6�   $	��113��6�   $��57��6�   $��Center=�6�   �� 54      -   5        $��350,10,0,10q�54   3   ���	#FFAE5555$��Wrong��6�   $
��Left=�6�   $	��113��6�   56      58      59      