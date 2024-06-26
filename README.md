  ## Описание

  Мне нужно было создать систему отображения и контроля игрока в моём 3d проекте. В качестве модели игрока выступает автомобиль, который может передвигаться по нескольким полосам дороги, маневрируя между трафиком. Также модель автомобиля имеет возможность тюнинга - замены некоторых частей (бамперы, фары и т.д.)

  ## Структура

  Главный класс игрока - PlayerController. Он реагирует на действия, которые может инициировать как сам игрок, нажимая на клавиши (движение, перестроения), так и внешние объекты в игре (столкновения с транспортом).

  За внешний вид игрока отвечает класс PlayerView. Он может инициировать анимации (столкновения, перестроения), звуки и другие изменения внешнего вида. Также он содержит в себе класс PlayerTuningView, который отвечает исключительно за кастомизацию модели игрока (изменение цвета, установка новых кузовных деталей), и используется преимущественно в окне кастомизации.

  Класс PlayerMovement отвечает за способ перемещения игрока. Сейчас реализовано движение по большой окружности за счёт вращения игрока вокруг центральной оси. Перемещение осуществляется без использования физики. В будущем планируется реализовать другие механики перемещения, пригодные для более свободного перемещения.

  Данные игрока хранятся в классе PlayerModel. Там хранятся заработанные очки, параметры скорости и тд. В нём же реализованы методы сохранения и загрузки данных в постоянное хранилище. 

  ## Реализация

  ![Состояние аварии](https://github.com/slavker99/Arcade-car-control-system/blob/main/gameState.gif?raw=true)

  **UML-диаграмма классов**
  ![UML-диаграмма классов](https://github.com/slavker99/Arcade-car-control-system/blob/main/readme%20images/PlayerSystem2.png?raw=true)


  PlayerController
  В начале сцены в нем вызывается метод ToStartState(), в котором игрок перемещается на стартовую позицию и у него обнуляются параметры (количество жизней и очков).

  ```c#
  public void ToStartState()
  {
    isMoving = true;
    PlayerModel.SetStartValues();
    PlayerMovement.ToStartState(startPoint);
  }
  ```

  При задевании препятствий срабатывает метод CrashState(), в котором запускается анимация аварии из PlayrView, а также запускается корутина на 2 секунды для того, чтобы 
  отключить управление машиной.

  ```c#
  public void CrashState()
  {
    PlayerModel.TakeAwayLive();
    PlayerMovement.CrashState();
    changingLaneQueue.ClearQueue();
    if (PlayerModel.currentLives == 0)
    {
      GameOverState();
      return;
    }
    StartCoroutine(CrashStateCoruntine());
    PlayerView.CrashState(isChangingLane);
  }
  ```

  ![Состояние аварии](https://github.com/slavker99/Arcade-car-control-system/blob/main/crashState.gif?raw=true)


  При утрате всех жизней срабатывает метод GameOverState(), который запускает состояние потери управления.

  ```c#
  public void GameOverState()
  {
    PlayerView.GameOverState();
    PlayerMovement.GameOverState();
    isControlled = false;
  }

  ```

  ![Состояние аварии](https://github.com/slavker99/Arcade-car-control-system/blob/main/gameOverState.gif?raw=true)


