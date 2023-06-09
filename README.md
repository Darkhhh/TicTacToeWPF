# TicTacToeWPF
Оконное приложение для игры в крестики-нолики. 

Предоставляет два режима игры: против человека и против компьютера. 
Компьютер имеет два эвристических алгоритма выбора хода: минимакс и минимакс с альфа-бета отсечением.

Оптимизированный алгоритм после «прохода» по одной из ветвей дерева решений «отсекает» ветви, заведомо не имеющие оптимального решения, относительно коэффициентов альфа и бета, полученных на первом проходе. Альфа — это минимально возможная оценка, которую может получить максимизирующий игрок, инициализируется значением минус бесконечности и наоборот. Если в одном из узлов ветви значения альфа больше либо равно бета, найден ход, который гарантирует победу максимизирующему игроку. Если значения этих параметров в текущем узле не улучшаются, ветвь не следует рассматривать, поскольку все ее «потомки», как и она сама, не содержат оптимального решения. Глубина прохода задается при вызове функции ComputerPlayer.MinMaxAlphaBeta. 

```cs
public static (int, (int, int)) MinMaxAlphaBeta(GameBoard board, bool isMaximizing, int depth = 10, int alpha = -int.MaxValue, int beta = int.MaxValue)
{
  ...
}
```

Так как работа алгоритма может быть достаточно продолжительной, во избежание "заморозки" интерфейса, вызовы реализованы асинхронно. 

## Окна интерфейса
### Приветственый экран
<img src="https://user-images.githubusercontent.com/82178107/236267992-0bd089f4-096f-4f11-abfc-b2a743573109.png" width="500">

### Экран выбора режима и размера поля
<img src="https://user-images.githubusercontent.com/82178107/236270126-c4f2b4b7-a5d1-44f6-bd7d-3161d52982b5.png" width="500">

### Игровой экран
<img src="https://user-images.githubusercontent.com/82178107/236270313-8efb4f52-6ca5-4b0d-ab18-8cc61cc860bd.png" width="500">
