   M S B A M L   `   `   `  ????C  ?Project2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Project2Project2  5   	       Project2.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Project2     5   	     ? Title$  
MainWindow??5   	   $
??1006??6   $??1196.5??6*   .??5	      ? .+?5
   
   '? -   5         '?Name$ 	viewport1??5
      $??0,0,0,83q?5   7    '? Camera 5      =? 5      -   5        
kamera?? ? Position$ 115, 250, 3101?5   4    ? LookDirection$ 0, -220, -2005?5   $    ? UpDirection$ 0,1,05?6B   .x?5      g?  g? Content 5      t? 5       ?? Color$ White??6'    t? 	Direction$ 10,-1000,-1005?65   5      g? 5      -   5        MyModel?? 5      ? 5       -   5        Top??	 ? Material	 5!      u? 5"      
 u? Brush
 5#   "   ?? 5$   &    ?? ImageSource$ bin/Debug/map.jpg??61   5&       ? Geometry 5(      k? 5)       k? 	Positions% ?   ???=	???=	 k? TextureCoordinates ?    k? TriangleIndices ?     5+   !   5.      51      g? 52      -   5        MyModel2?? 53      ? 55      -   5        Top2??	 56      u? 57      
 58   "   ?? 59   &   $ bin/Debug/map.jpg??61   5;       5=      k? 5>      % ?   ???=	???=	 ?    ?    5@   !   5C      5F      5G      ?? 5I   
   -   5         ??Name$ button??5I      $
??Load??6!   $??83??61   $??Bottom=?6=   $??Center=?6X   $	??200??6u   $??606,0,382,0q?6?   $??30(?6?   $??Showcard Gothic)?6?   6?   ]? 5J   
   $??38??6   $??	0,0,857,0q?6    $??Bottom=?63   .+?5L      ?? $??Switch color??6   $??Center=?6+   $	??0,7q?6F   $	??108??6S   	? 5M      -   5         	?Name$
 rbrg??5M      $??Center=?5N      $??	Red/Green??61   $
??Left=?6E    d? 	IsChecked$ FalseY?6`   $
??0,12q?6?   $	??107??6?   	? 5O      -	   5        $
 rbst??5O      $??Standard??5P      $??Center=?6)   $
??Left=?6D   $
 TrueY?5Q      $
??0,12q?6;   $??97??6I   5R      ]? 5S   
   $??
0,0,857,43q?6   $??Bottom=?6(   .+?5T      ?? $??Lines by resistance??6   $??Center=?62   	? 5U      -
   5        $ vodavoda??5U      $??different colors??5V      $
??Left=?62   $ FalseY?5W      $??Center=?6B   $	??109??6^   	? 5Y      -   5        $ 	vodnormal??5Y      $
 TrueY?5Z      $
??Left=?6=   $??Center=?6X   $??Standard??5[      $	??0,6q?6(   $??88??65   5]      ]? 5^   
   $??83??6   $
??Auto??6    $??Bottom=?6-   $??443,0,641,1q?6H   .+?5_      ?? $??
Show Lines??6   $??Center=?6)   ?? 5a      -   5         ??Name$ 	linesshow??5a      $
 TrueY?5b      $
??Left=?6O   $??Center=?6j   $
??0,34q?5c      $??16??6%   5e      ]? 5f   
   $??83??6   $
??Auto??6    $??Bottom=?6-   $??300,0,750,0q?6H   .+?5g      ?? $??Hide inactive lines??6   $??Center=?62   ?? 5i      -   5        $ inactiveLines??5i      $ FalseY?5j      $
??Left=?6M   $??Center=?6h   $
??0,34q?5k      $??18??6   5m      5n      5o      