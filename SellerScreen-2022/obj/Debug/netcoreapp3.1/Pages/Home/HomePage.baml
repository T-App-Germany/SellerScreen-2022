   M S B A M L   `   `   `  ����L  HSellerScreen-2022, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:SellerScreen_2022.Pages.HomeSellerScreen_2022.Pages.Home  5      D @FluentWPF, Version=0.8.0.6, Culture=neutral, PublicKeyToken=nullP6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPFSourceChord.FluentWPF 5      +    %SellerScreen_2022.Pages.Home.HomePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:SellerScreen_2022.Pages.Home     5      D @ModernWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.3.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5      Afw6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPF   5      -   5          N� Title$  HomePage��5      .��5      � .+�5   
   �� -   5         ��Name$ ContentPanel��5      .��5      �� .+�5      �� -   5         ��Name$ TitleTxt��5      $&�� Willkommen bei SellerScreen-2022��6.   $��Center=�6V   $��	0,50,0,50q�6s   $��45(�6�   $
��Thin"�6�   � 5      -   5         �Name$ DistancePanel1��5      $	��400��6/   � 5      -   5         �Name$ 	LogoPanel��5      $��Center=�6/   .+�5      �� -   5         ��Name$ AppLogo��5      $!��/Resources/SellerScreen.png��6-   $	��200��6R   $��20q�6_   $��Center=�6k   ��6�   �� *Width+ �� ElementName$ CompanyLogo�� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 3Q�6/   	   Opacity$
	 0.79Q�6?   
   	Direction$	
 315Q�6N      
BlurRadius$ 30Q�6^      RenderingBias$ Performance=�6n   5      �� 5      -   5        $ CompanyLogo��5      $ ��/Resources/tappgermany.png��61   $	��200��6U   $��20q�6b   $��Center=�6n   $	��350��6�    5        5      $ 3Q�6/   $
	 0.79Q�6?   $	
 315Q�6N   $ 30Q�6^   $ Performance=�6n   5      5      � 5      -   5        $ DistancePanel2��5      $	��400��6/   � 5      -	   5        $ DownIconPanel��5      $��60��6.   .+�5       !  ModernWpf.Controls.FontIcon     Glyph$	 ��6"      FontSize$ 30(�6,   $��5q�6:   $��Center=�6E   $��Center=�6b      
FontWeight$ 
ExtraBlack"�6}   5!      �� 5"      $��Stretch=�6       SystemAltMediumLowColorBrush#���   67   ._�5#      � $��10q�6   $��Center=�6'    � ColumnDefinitions 5$      �� 5%      �� 5&      .+�5(      �� $��0��6    $��/Resources/logo-qu.png��60   $��0,0,50,0q�6P   $	��350��6b   � 5)      -
   5        $
 grid��5)      $��1��6-    � RowDefinitions 5*      �� 5+   "   �� 5,   "   �� 5-   "   �� 5.   "   .+�50      �� $��0��6)   $��Center=�66   $��5q�6Q   .+�51   "   �� $"��SellerScreen-2022 Enterprise��6,   $��30(�6P   $
��Wrap=�6^   �� 52   "    �� Orientation$ 
Horizontal=�6-   .+�53   &   #  ModernWpf.Controls.SymbolIcon     Symbol$ Accept=�64   $��5q�6D   �� 54   &   $��Lizenz gültig��60   $��15(�6E   $
��Wrap=�6S   $��5q�6g   55   #   56      � 57      $��1��6#   $��Center=�60    58   "   �� 59   &   $
��Auto �67   �� 5:   &   $
��Auto �67   .+�5<   "   �� $��0��6-   $��5q�6=   .+�5=   &   �� $��Version��60   $��20(�6?   $
��Wrap=�6M   �� 5>   &   $��1��60   $��
Produkt-ID��6=   $��20(�6O   $
��Wrap=�6]   5?   #   �� 5@   "   $��1��6-   $��5q�6=   .+�5A   &   �� $��0.0.0��60   $��20(�6=   $
��Wrap=�6K   �� 5B   &   $��
xxxxxxxxxx��60   $��20(�6B   $
��Wrap=�6P   5C   #   5D      �� 5E      $��2��6)   $��Center=�66   $��5q�6Q   .+�5F   "   �� $)��#Dieses Produkt ist lizensiert für:��6,   $��20(�6V   $
��Wrap=�6d   �� 5G   "   $��T-App Germany��6,   $��20(�6A   $
��Wrap=�6O   5H      �� 5I      $��3��6(   $'��!Copyright © 2021 - T-App Germany��65   $��20(�6]   $
��Wrap=�6k   $��5q�6   5J      5K      5L      5M      5N      5O      5P      