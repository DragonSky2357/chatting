   M S B A M L   `   `   `  ����J  FCHATTING_CLIENT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CHATTING_CLIENTCHATTING_CLIENT  5   	   "    CHATTING_CLIENT.ChattingRoom   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CHATTING_CLIENT     5   	   -   5        $	��600��5      $	��400��6'     � WindowStyle$
  None=�63    � AllowsTransparency .  � Title$ Chatting��6�   ���#A9BDCE��6�   Z� .��5      � .+�5	   
   � .+�5
      �� -   5        $
��Left=�5
      $��20��6/   $��
345,10,0,0q�6;   $	��Top=�6O   $��20��6g    J� RenderTransformOrigin$ 1.833,-2.667&�6r   $��./Img/Minimize_Window.png��6�   6�   �� 5      -   5        $
��Left=�5      $��20��6/   $��
370,10,0,0q�6;   $	��Top=�6O   $��20��6g   $ 1.833,-2.667&�6r   $��./Img/Close_Window.png��6�   6�   5      � 5   
   .+�5      �� -   5         ��Name$ ContentOfTalk��5      $��15(�61    �� 
IsReadOnly . ���#B2C7D9" �� VerticalScrollBarVisibility$
 Auto=�6g   $
��Left=�6�   $	��450��6�   $	��Top=�6�   $	��400��6�    �� SelectionBrush
 �����$��0,55,0,0q�6�   $
��Bold"�6�   6  5      � 5   
   .+�5      �� -   5         ��Name$ SendDataText��5      	 �� MinLines$	 1��6,   
 �� MaxLines$
 5��69    �� 	MaxLength$	 500��6F   $
��Left=�6V   $��95��6q   $��	0,505,0,0q�6}    �� TextWrapping$ WrapWithOverflow=�6�   $	��Top=�6�   $	��400��6�   $��14(�6�   
�������
 ����� �� SelectionOpacity$ 1Q�6   �� 
AcceptsTab . 5   (   �� 5      -   5         ��Name$ 
SendButton��5      {�.  $
��Left=�6;   $��30��6V   $��60��6b   $��330,520,0,50q�6m   $��전송��6�   
��� ���$	��Top=�6�   $ 1.833,-2.667&�6�   ���	#FFF0F0526�   5      5      5      