   M S B A M L   `   `   `  ����L  HSellerScreen-2022, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SellerScreen_2022SellerScreen_2022  5      D @FluentWPF, Version=0.8.0.6, Culture=neutral, PublicKeyToken=nullP6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPFSourceChord.FluentWPF 5	      "    SellerScreen_2022.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:SellerScreen_2022     5      D @ModernWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.3.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5      Afw6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPF   5	      -   5        )  #SourceChord.FluentWPF.AcrylicWindow   Name$  acrylicWindow��5       � Title$ SellerScreen-2022��5
      $	��500��60   $
��1050��6=   (  "SourceChord.FluentWPF.AccentColors!   ImmersiveSystemAccentBrush#��Z 6J   $��1q�6�   ���	#CCFFFFFF  ModernWpf.ThemeManager   IsThemeAware$
 True��5      $	��638��6@   $	��850��6P   !  ModernWpf.Controls.TitleBar   ExtendViewIntoTitleBar$
 True��5      0  *ModernWpf.Controls.Primitives.WindowHelper   UseModernWindowStyle$
 True��6/    � 	Resources 5       ��Name   NavViewOpen �� 
TargetName	 �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime p� Value   EasingFunction0  *System.Windows.Media.Animation.QuarticEase7	  1System.Windows.Media.Animation.EasingFunctionBase 	  
EasingMode  NavViewClose   EasingFunction��%�  &	        &	 +    �� 5   
   $ NavViewOpen��6   .��5      U� $ WindowIconImg��6,   $	 (FrameworkElement.Height)�6R   .\�5        $
 00:00:00.1000000��6'   $ 50Q�6B   5      U� 5      $ WindowTitleTxt��6,   $	 (TextBlock.FontSize)�6S   .\�5        $
 00:00:00.1000000��6'   $ 18Q�6B   5      r� 5      $ WindowIconImg��6/   $	 (FrameworkElement.Margin)�6U   .	�5        $ 00:00:00.1000000��6*   $ 10q�6E    5        5      $ EaseOut=�6&   5      5      r� 5      $ TitleBar��6/   $	 (FrameworkElement.Margin)�6P   .	�5        $ 00:00:00.1000000��6*   $ 	30,10,0,0q�6E    5        5       $ EaseOut=�6&   5"      5#      U� 5$      $ WindowIconImg��6,   $6	 0(UIElement.Effect).(DropShadowEffect.BlurRadius)�6R   .\�5%        $
 00:00:00.1000000��6'   $ 60Q�6B   5&      U� 5'      $ WindowIconImg��6,   $7	 1(UIElement.Effect).(DropShadowEffect.ShadowDepth)�6R   .\�5(        $
 00:00:00.1000000��6'   $ 4Q�6B   5)      5*      �� 5+   
   .��5,      U� $ WindowIconImg��6,   $	 (FrameworkElement.Height)�6R   .\�5-        $
 00:00:00.1000000��6'   $ 25Q�6B    5.        5/      $ EaseOut=�6&   51      52      U� 53      $ WindowTitleTxt��6,   $	 (TextBlock.FontSize)�6S   .\�54        $
 00:00:00.1000000��6'   $ 15Q�6B    55        56      $ EaseOut=�6&   58      59      r� 5:      $ WindowIconImg��6/   $	 (FrameworkElement.Margin)�6U   .	�5;        $ 00:00:00.1000000��6*   $ 3,0,15,0q�6E    5<        5=      $ EaseOut=�6&   5?      5@      r� 5A      $ TitleBar��6/   $	 (FrameworkElement.Margin)�6P   .	�5B        $ 00:00:00.1000000��6*   $ 5q�6E    5C        5D      $ EaseOut=�6&   5F      5G      U� 5H      $ WindowIconImg��6,   $6	 0(UIElement.Effect).(DropShadowEffect.BlurRadius)�6R   .\�5I        $
 00:00:00.1000000��6'   $ 10Q�6B   5J      U� 5K      $ WindowIconImg��6,   $7	 1(UIElement.Effect).(DropShadowEffect.ShadowDepth)�6R   .\�5L        $
 00:00:00.1000000��6'   $ 0Q�6B   5M      5N      5O      '
  !System.Windows.Shell.WindowChrome 
  WindowChrome 5P      
  5Q   
   %  System.Windows.SystemParameters"   WindowResizeBorderThickness 
  ResizeBorderThickness# Z 6T    
  CaptionHeight 6   �� *ActualHeight+ �� ElementName$ TitleBar��6T   .��5S      � .+�5T   
   '  !ModernWpf.Controls.NavigationView  -   5          Name$ MainNavView��5T      $��1��61      IsBackButtonVisible$ 	Collapsed=�6>      OpenPaneLength$	 260Q�6^      	MenuItems 5U      +  %ModernWpf.Controls.NavigationViewItem  5V         Icon-  'ModernWpf.Controls.IconElementConverter$
 Home 6(   $��
Startseite��64   
 � Tag$
 home��6I   /  )ModernWpf.Controls.NavigationViewItemBase   
IsSelected$
 True��6T   4  .ModernWpf.Controls.NavigationViewItemSeparator  5W        5X      $
 Shop 6(   $��Kasse��64     5Y      $ AllApps 6(   $��Lager��67     5Z      $��Statistiken��6(    5[      !  ModernWpf.Controls.FontIcon  5\         
FontFamily$ Segoe MDL2 Assets)�6&      Glyph$	 ��6E   5^        5_      $��PlugIn-Manager��6(    5`        5a      $ Segoe MDL2 Assets)�6&   $	 ��6E   5c      .��5e        ModernWpf.Controls.Frame  -   5          Name$ ContentFrame��5e        
  IsHitTestVisibleInChrome$
  True��6-   $��0,0,2,2q�6Z   5f      � 5g   
   -   5        ! �Name$! TitleBar��5g      $	��Top=�6!   $��	30,10,0,0q�69   .+�5h      � $
��Left=�6   $	��240��6.   " � ColumnDefinitions" 5i      �� 5j      $
��Auto �6'   �� 5k      .+�5m      �� -   5        # ��Name$# WindowIconImg��5m      $��0��6/   $!��/Resources/SellerScreen.png��6?   $��50��6d   $��10q�6p   $
��Left=�6|   $ J� Effect$ 5n      3  -System.Windows.Media.Effects.DropShadowEffect  5o      %   ShadowDepth$% 4Q�6+   &   
BlurRadius$& 50Q�6;   5q      �� 5r      -   5        ' ��Name$' WindowTitleTxt��5r      $��1��64   $"��SellerScreen-2022 Enterprise��6D   $
��Wrap=�6h   $��18(�6|   ( �� TextAlignment$
( Left=�6�   $��Center=�6�   5s      5t      5u      5v      